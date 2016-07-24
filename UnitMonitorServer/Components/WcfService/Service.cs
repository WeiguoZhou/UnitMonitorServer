using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace UnitMonitorServer
{
    public class Service : IServerService
    {
        //服务器端程序的版本

        public Version ApiVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }
        //向服务器注册一个客户端

        public bool RegService(string ip,string port)
        {
          return   Clients.Instance().RegClient(ip, Convert.ToInt32(port));
        }
        //服务器端服务的名称

        public string ServiceName()
        {
            return Properties.Settings.Default.ServerName;
        }
        //通知服务器端客户端已关闭

        public bool ServiceShutOff(string ip)
        {
           ClientInfo inf= Clients.Instance().FindClient(ip);
            if (inf != null)
            {
                inf.TurnOffLine();
                Clients.Instance().Remove(inf);

            }
            return true;
        }
       public TaskInfo[] Tasks()
        {
            string fullPath = Environment.CurrentDirectory + "\\" + "Tasks";
            fullPath = fullPath.ToLower();
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            DirectoryInfo path = new DirectoryInfo(fullPath);
                List<TaskInfo> tasks = new List<TaskInfo>();
            GetPathTasks(tasks, path, fullPath);
            return tasks.ToArray();
        }
        private void GetPathTasks(List<TaskInfo> tasks, DirectoryInfo dir,string rootPath)
        {
            foreach (var item in dir.GetFiles())
            {
                if (item.Extension.ToLower().Contains("config"))
                {
                   
                        TaskInfo inf = new TaskInfo();
                        inf.Name=item.Name.Replace(".config", "");
                        inf.ModuleName = "";

                        inf.Path = dir.FullName.Replace(rootPath, "");
                        TaskBase task = null;
                        if (TasksContainer.Instance().IsRunning)
                            task= TasksContainer.Instance().FindTask(inf.Name);
                        if (task == null)
                            task = TasksContainer.LoadTask(item);

                        if (task != null)
                        {
                            inf.IsRunning = true;
                            inf.ModuleName = task.GetType().FullName;
                            inf.BeginTime = task.BeginTime;
                            inf.FailCount = task.FailCount;
                            inf.LastRunTime = task.LastRunTime;
                            inf.LastSpendTime = task.LastSpendTime;
                            inf.LastSuccess = task.LastSuccess;
                            inf.Period = task.Period;
                            inf.PersistantFail = task.PersistantFail;
                            inf.PersistantSuccess = task.PersistantSuccess;
                            inf.RunCount = task.RunCount;
                            inf.SuccessCount = task.SuccessCount;
                        }
                        tasks.Add(inf);
             
                    }               
                
            }
            foreach (var item in dir.GetDirectories())
            {
                GetPathTasks(tasks, item,rootPath);
            }
        }
    }


}

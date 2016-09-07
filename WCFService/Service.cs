using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace UnitMonitorServer.Components.WCFService
{
    public class Service : IServerService
    {
        //服务器端程序的版本

        public Version ApiVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }
        //向服务器注册一个客户端

        public bool RegService(string ip, string port)
        {
            return WCFServiceManager.Instance.Clients.RegClient(ip, Convert.ToInt16(port));
        }
        //服务器端服务的名称

        public string ServiceName()
        {
            return WCFServiceManager.Instance.ServiceName;
        }
        //通知服务器端客户端已关闭

        public bool ServiceShutOff(string ip)
        {
            ClientInfo inf = WCFServiceManager.Instance.Clients.FindClient(ip);
            if (inf != null)
            {
                inf.TurnOffLine();
                WCFServiceManager.Instance.Clients.Remove(inf);

            }
            return true;
        }
        public bool TestOnline()
        {
            return true;
        }
        /// <summary>
        /// 读取所有任务
        /// </summary>
        /// <returns></returns>
        public List<TaskInfo> AllTasks()
        {
            string fullPath = Directory.GetCurrentDirectory() + "\\" + "Tasks";
            fullPath = fullPath.ToLower();
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            DirectoryInfo path = new DirectoryInfo(fullPath);
            List<TaskInfo> tasks = new List<TaskInfo>();
            GetPathTasks(tasks, path, fullPath);
            return tasks;
        }
        public List<TaskInfo> RunningTasks()
        {
            List<TaskInfo> tasks = new List<TaskInfo>();
            foreach (var item in TasksContainer.Instance)
            {
                tasks.Add(item.ToTaskInfo());
            }
            return tasks;
        }
        private void GetPathTasks(List<TaskInfo> tasks, DirectoryInfo dir, string rootPath)
        {
            foreach (var item in dir.GetFiles())
            {
                if (item.Extension.ToLower().Contains("task"))
                {
                   
                    TaskBase task = TasksContainer.Instance.FindTask(item.FullName);
                    if (task == null)
                        task = TaskBase.LoadTask(item);
                    if (task != null)
                        tasks.Add(task.ToTaskInfo());
                }

            }
            foreach (var item in dir.GetDirectories())
            {
                GetPathTasks(tasks, item, rootPath);
            }
        }

        public string[] HistoryMessageFiles()
        {
            List<string> files = new List<string>();
            string fullPath = Environment.CurrentDirectory + "\\" + "Log";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            DirectoryInfo dir = new DirectoryInfo(fullPath);
            foreach (var item in dir.GetFiles())
            {
                if (item.Extension.Contains("log"))
                    files.Add(item.Name.Replace(".log", ""));
            }
            return files.ToArray();
        }

        public Stream HistoryMessage(string messageFile)
        {
            string fullname = Environment.CurrentDirectory + "\\Log\\" + messageFile + ".log";
            if (File.Exists(fullname))
            {
                return File.OpenRead(fullname);
            }
            return null;
        }
    }


}

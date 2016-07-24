using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Threading;
using System.ComponentModel;
using UnitMonitorCommunication;

namespace UnitMonitorCommon
{
 public   class TasksContainer:BindingList<TaskBase>,IDisposable
    {
        Dictionary<string, XmlDocument> SharedConfigs;
        DateTime currentTime = DateTime.Now;
        static  TasksContainer instance;
        bool isbusy = false;
        double lastSpendTime;
        Timer timer;
        int period=5;
        DataMode mode = DataMode.RealTime;
        public event EventHandler RunComplete;
        public static TasksContainer Instance
        {
            get
            {
                return instance;

            }

        }
        public static void Init()
        {
            if (instance == null)
            {
                instance = new TasksContainer();

            }
        }
        public int RunCount { private set; get; }
        public  DataMode Mode {

            set {
                    mode = value;
                }

             get
            {
                return mode;
            }
        }
        public DateTime CurrentTime {
            set
            {
                currentTime = value;
            }
                
            get
            {
                if (Mode == DataMode.RealTime)
                    return DateTime.Now;
                else
                    return currentTime;
            }
        }
        public double LastSpendTime
        {
            get
            {
                return lastSpendTime;
            }
        }
        /// <summary>
        /// 运行周期，必须在运行前设置，必须为30的公约数
        /// </summary>
        public int Period
        {
            set
            {
                if (!IsRunning && (30 % value==0))
                    period = value;
            }
            get
            {
                return period;
            }
        }
        public TaskBase FindTask(string name)
        {
            foreach (TaskBase item in this)
            {
                if (item.TaskName == name)
                    return item;
            }
            return null;
        }
        public bool IsRunning { private set; get; }
        public void Start()
        {
            IsRunning = true;
            RunCount = 0;
            if(Mode!=DataMode.Debug)
                timer = new Timer(RunTasks, null, 1000,Period * 1000);
            MessageCenter.Instance.SendMessage(MessageType.System, "任务中心启动", "System");
        }
        public void StepRun()
        {
            if (Mode == DataMode.Debug)
                RunTasks(null);
        }
        public void Stop()
        {
            if (IsRunning)
            {
                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }
                IsRunning = false;
                this.Clear();
                MessageCenter.Instance.SendMessage(MessageType.System, "任务中心已停止运行", "System");
            }
        }
        private TasksContainer()
        {
            SharedConfigs = new Dictionary<string, XmlDocument>();
            
        }
        public void Dispose()
        {
            if (timer != null)
                timer.Dispose();
            timer = null;
        }
        /// <summary>
        /// 从配置文件中读取任务信息，用反射生成这个类的实例
        /// </summary>
        /// <param name="configFileName">配置文件名，放在Tasks目录下</param>
        /// <returns></returns>
        public static TaskBase LoadTask(FileInfo configFile)
        {
            //这里到底是用XmlDocument还是用Configuration很纠结
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFile.FullName);

                XmlNode taskInfo = doc.DocumentElement.SelectSingleNode("taskinfo");
                if (taskInfo != null)
                {
                    Assembly assem = Assembly.Load(taskInfo.Attributes["assembly"].Value);
                    TaskBase task = (TaskBase)assem.CreateInstance(taskInfo.Attributes["class"].Value);

                    task.TaskName = configFile.Name.Replace(".config", "");
                    task.Config = doc;
                    task.ConfigFileInfo = configFile;
                    string sharedConfigName = task.GetType().Name;
                    task.SharedConfig = TasksContainer.Instance.GetSharedConfig(sharedConfigName);
                    return task;
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("任务容器加载任务时出错。任务名称：{0}，错误消息{1}", configFile.Name, ex.Message);
                Logger.Instance.LogDebug(message);
            }
            return null;

        }
        /// <summary>
        /// 加载模块级的共享配置文件
        /// </summary>
        /// <param name="taskTypeName">任务模块名称</param>
        /// <returns></returns>
        private XmlDocument GetSharedConfig(string taskTypeName)
        {
            if (SharedConfigs.ContainsKey(taskTypeName))
                return SharedConfigs[taskTypeName];
            else
            {
                string fileFullname = Directory.GetCurrentDirectory() + "\\ModuleSettings\\" + taskTypeName +".config";
                if (!File.Exists(fileFullname))
                    throw new Exception(string.Format("名为{0}的模块配置文件不存在", taskTypeName));
                XmlDocument doc = new XmlDocument();
                doc.Load(fileFullname);
                SharedConfigs.Add(taskTypeName, doc);
                return doc;
            }
        }

        public void AddTask(TaskBase task)
        {
            if (task == null)
                return;
           if(this.FindTask(task.TaskName) == null)
            {
                task.BeginTime = this.CurrentTime;
                this.Add(task);
                task.RaiseTaskAdded();
            }

        }

        public void RunTasks(object state)
        {
            //防止来不及处理
            if (isbusy)
                return;
            if (Mode != DataMode.RealTime)
                currentTime = currentTime.AddSeconds(1);
            isbusy = true;
            DateTime beginTime = DateTime.Now;
            foreach(TaskBase item in this){
                item.LastRunTime = currentTime;
                item.Run();

            }

            lastSpendTime = CommUtil.TimeMillisecondSpan(beginTime,DateTime.Now);
            isbusy = false;
            if (RunComplete != null)
                RunComplete(this, null);
            RunCount += 1;
        }
        public void OnTaskChanged(TaskBase task)
        {
            ListChangedEventArgs e = new ListChangedEventArgs(ListChangedType.Reset, this.IndexOf(task));
            this.OnListChanged(e);
        }
    }
}

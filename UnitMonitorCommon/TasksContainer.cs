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
    public class TasksContainer : BindingList<TaskBase>, IDisposable
    {
        //保存任务模块的共享设置，因为一个任务模块可能有多个任务，指向相同的共享设置可减少内存和加快加载速度
        Dictionary<string, XmlDocument> SharedConfigs;
        static TasksContainer instance;
        //任务容器是否正在处理任务，防止运算时间太长导致一个周期还未完成下个周期又开始了。
        bool isbusy = false;
        //上次运行花费的时间（毫秒）
        double lastSpendTime;
        Timer timer;
        //任务容器运行周期,规定为30的公约数
        //改变任务容器的运行周期不会影响任务的运行周期，因为任务取的是30的公倍数
        //默认值是5秒
        int period = 5;
        //运行模式，分为实时模式、调试模式、历史模式
        DataMode mode = DataMode.RealTime;
        //任务容器运行成功完成后引发的事件
        public event EventHandler RunComplete;
        //任务容器开始启动引发的事件
        public event EventHandler BeginRun;
        //任务容器停止运行引发的事件
        public event EventHandler StopRun;
        //任务容器在实时模式中取数失败引发的事件。
        public event ExceptionEventHandler GetRtValueFailed;
        public event ExceptionEventHandler TaskExecuteErr;
        /// <summary>
        /// 任务容器的实例引用
        /// </summary>
        public static TasksContainer Instance
        {
            get
            {
                return instance;

            }

        }
        /// <summary>
        /// 任务容器的初始化
        /// </summary>
        public static void Init()
        {
            if (instance == null)
            {
                instance = new TasksContainer();

            }
        }
        /// <summary>
        /// 保存当前需要向edna取值的点
        /// </summary>
       public List<string> Points { private set; get; }
        /// <summary>
        /// 保存向edna取到的值
        /// </summary>
        public double[] Values { private set; get; }
        /// <summary>
        /// 运行次数统计
        /// </summary>
        public int RunCount { private set; get; }
        /// <summary>
        /// 运行模式，分为实时模式、调试模式、历史模式
        /// </summary>
        public DataMode Mode
        {

            set
            {
                mode = value;
            }

            get
            {
                return mode;
            }
        }
        /// <summary>
        /// 表示运行的当前时间
        /// </summary>
        public DateTime CurrentTime
        {
            set; get;
        }
        /// <summary>
        /// 任务容器启动的开始时间，在历史模式中为历史开始时间
        /// </summary>
        public DateTime BeginTime { set; get; }
        /// <summary>
        /// 在历史模式中表示运行结束的时间
        /// </summary>
        public DateTime HistoryEndTime { set; get; }
        /// <summary>
        /// 上次运行花费的时间
        /// </summary>
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
                if (!IsRunning && (30 % value == 0))
                    period = value;
            }
            get
            {
                return period;
            }
        }
        /// <summary>
        /// 查找任务容器中是否含有指定名称的任务
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TaskBase FindTask(string configFileFullName)
        {
            foreach (TaskBase item in this)
            {
                if (item.ConfigFileInfo.FullName== configFileFullName)
                    return item;
            }
            return null;
        }
        /// <summary>
        /// 任务容器是否在运行
        /// </summary>
        public bool IsRunning { private set; get; }
        /// <summary>
        /// 启动任务容器
        /// </summary>
        public void Start()
        {
            if (this.IsRunning)
                return;
            IsRunning = true;
            RunCount = 0;
            if (BeginRun != null)
                BeginRun(this, null);
            //调试模式采用点击按钮步进的方式执行
            if (Mode != DataMode.Debug)
                timer = new Timer(RunTasks, null, 1000, Period * 1000);

        }
        /// <summary>
        /// 调试模式时必须执行此方法执行
        /// </summary>
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
                if (StopRun != null)
                    StopRun(this, null);

            }
        }
        private TasksContainer()
        {
            SharedConfigs = new Dictionary<string, XmlDocument>();
            Points = new List<string>();
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
                string fileFullname = Directory.GetCurrentDirectory() + "\\ModuleSettings\\" + taskTypeName + ".config";
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
            if (this.FindTask(task.ConfigFileInfo.FullName) == null)
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
            isbusy = true;
            if (RunCount == 0)
            {
                if (Mode != DataMode.History)
                {
                    BeginTime = DateTime.Now;
                }
                CurrentTime = BeginTime;
            }
            else
            {
                if (Mode != DataMode.RealTime)
                    CurrentTime = CurrentTime.AddSeconds(this.period);
                else
                    CurrentTime = DateTime.Now;
            }
            if (Mode == DataMode.History)
            {
                if (CurrentTime >= HistoryEndTime)
                {
                    Stop();
                    return;
                }
            }
            //用于统计运行时间
            DateTime runBeginTime = DateTime.Now;
            //任务在取数前做的一些工作，比如，设置某些点不用再取值
            foreach (TaskBase item in this)
            {
                item.InternalPreRun();
            }
            //实时模式时先将需要取值的点添加到points，然后再到edna中取值
            //7月28日更改，希望能减少访问edna的次数来提高性能。

            if (Mode == DataMode.RealTime)
            {
                Points.Clear();
                foreach (TaskBase item in this)
                {
                    if (item.RunRequired)
                    {
                        
                        foreach (var point in item.Points)
                        {
                            if (point.Used)
                            {
                                int index;
                                //采用BinarySearch的方法是否能提高性能？
                                if ((index = Points.BinarySearch(point.Id)) < 0)
                                {
                                    Points.Insert(~index, point.Id);
                                }
                            }
                        }
                    }


                }
                try
                {
                   Values = Dna.GetRtValue(Points);
                }
                catch (Exception ex)
                {
                    isbusy = false;
                    RaiseGetRtValueFailed(ex);
                    return;
                }

            }

            foreach (TaskBase item in this)
            {
                item.Run();
            }

           lastSpendTime = CommUtil.TimeMillisecondSpan(runBeginTime, DateTime.Now);

            if (RunComplete != null)
                RunComplete(this, null);
            RunCount += 1;
            isbusy = false;
        }
        public void OnTaskChanged(object sender,EventArgs e)
        {
            TaskBase task = (TaskBase)sender;
            ListChangedEventArgs args = new ListChangedEventArgs(ListChangedType.Reset, this.IndexOf(task));
            this.OnListChanged(args);
        }
        public void RaiseTaskExecuteErr(TaskBase task,Exception ex)
        {
            if (TaskExecuteErr != null)
                TaskExecuteErr(task, ex);
        }
        public void RaiseGetRtValueFailed(Exception ex)
        {
            if (GetRtValueFailed != null)
            {
                GetRtValueFailed(this, ex);
            }
        }
    }
}

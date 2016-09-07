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
        private static string localIP;
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
        //TODO:在开发阶段先放在调试模式，正式发布时放实时模式
        DataMode mode = DataMode.Debug;

        /// <summary>
        /// 当任务被添加到任务容器时发生
        /// </summary>
        public event EventHandler TaskAdded;
        /// <summary>
        /// 当任务从任务容器中移除时发生
        /// </summary>
        public event EventHandler TaskRemoved;
        //任务容器运行成功完成后引发的事件
        public event EventHandler RunComplete;
        //任务容器开始启动引发的事件
        public event EventHandler BeginRun;
        //任务容器停止运行引发的事件
        public event EventHandler StopRun;
        //任务容器在实时模式中取数失败引发的事件。
        public event ExceptionEventHandler GetRtValueFailed;
        
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
        /// 本机IP 地址
        /// </summary>
        public static string LocalIP
        {
            get
            {
                if (string.IsNullOrEmpty(localIP))
                    localIP = CommUtil.GetLocalIp();
                return localIP;
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
        /// 表示运行的当前时间，历史模式中为历史时刻，其它模式都表示现在时刻
        /// </summary>
        public DateTime CurrentTime
        {
          private  set; get;
        }
        /// <summary>
        /// 从开始运行后以来的秒数
        /// 在任务中多采用此值进行时间计算，这样我临时数据可统一采用double保存，避免装拆箱操作
        /// </summary>
        public double CurrentTotalSeconds { private set; get; }
        /// <summary>
        /// 任务容器启动的开始时间，在历史模式中为历史开始时间
        /// </summary>
        public DateTime BeginTime {private  set; get; }
        /// <summary>
        /// 在历史模式中表示运行结束的时间
        /// </summary>
        public DateTime HistoryEndTime {private  set; get; }
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
        /// 指向同一配置文件就认为是同一任务
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
        /// 用于启动历史模式
        /// </summary>
        /// <param name="historyBeginTime">历史开始时间</param>
        /// <param name="historyEndTime">历史结束时间</param>
        public void StartHistory(DateTime historyBeginTime,DateTime historyEndTime)
        {
            this.BeginTime = historyBeginTime;
            this.HistoryEndTime = historyEndTime;
            this.mode = DataMode.History;
            Start();
        }
        /// <summary>
        /// 调试模式时启动后必须执行此方法单次执行
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
            Points = new List<string>();
            //接下来我对Points的访问都是binarySearch，以下这步是否必须？
            Points.Sort();
        }
        public void Dispose()
        {
            if (timer != null)
                timer.Dispose();
            timer = null;
        }
      

        public  void AddTask(TaskBase task)
        {
            if (task == null)
                return;
            if (this.FindTask(task.ConfigFileInfo.FullName) == null)
            {
                this.Add(task);
                //这里是不好的设计
                //如果调用者直接调用this.Add(task)而不是AddTask（task)，此事件不会被引发
                //加有BindingList的AddingNew事件，但那是在添加入之前，用它引发这个事件不好
                if (TaskAdded != null)
                    TaskAdded(task,null);
            }

        }
        public void RemoveTask(TaskBase task)
        {
            this.Remove(task);
            if (TaskRemoved != null)
                TaskRemoved(task, null);
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
            CurrentTotalSeconds = (new TimeSpan(CurrentTime.Ticks - BeginTime.Ticks)).TotalSeconds;
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
                item.InternalPreInit();
            }
            //实时模式时先将需要取值的点添加到points，然后再到edna中取值
            //7月28日更改，希望能减少访问edna的次数来提高性能。

            if (Mode == DataMode.RealTime)
            {
                Points.Clear();
                foreach (TaskBase task in this)
                {
                    if (task.RunRequired)
                    {
                        
                        foreach (var fun in task.FunGroups)
                        {
                            if (fun.Used)
                            {
                                foreach (Point point in fun.Points)
                                {
                                    int index= Points.BinarySearch(point.Id);
                                    if (index < 0)
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

        public void RaiseGetRtValueFailed(Exception ex)
        {
            if (GetRtValueFailed != null)
            {
                GetRtValueFailed(this, ex);
            }
        }
        /// <summary>
        /// 自动加载所有任务并启动任务容器
        /// </summary>
        public void LoadTasks()
        {
            string fullPath = Directory.GetCurrentDirectory() + "\\" + "Tasks";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            DirectoryInfo dir = new DirectoryInfo(fullPath);
            List<FileInfo> list = new List<FileInfo>();
            GetSubTaskConfigFiles(list, dir);

            foreach (var item in list)
            {
                TaskBase task = TaskBase.LoadTask(item);
                if (task != null)
                {
                    this.AddTask(task);
                }
            }
            this.Start();
        }
        private void GetSubTaskConfigFiles(List<FileInfo> list,DirectoryInfo dir)
        {
            
            foreach (var item in dir.GetFiles())
            {
                if (item.Extension.ToLower().Contains("task"))
                {
                    list.Add(item);
                }

            }
            foreach (var item in dir.GetDirectories())
            {
                GetSubTaskConfigFiles(list, item);
            }
            
        }
    }
    public delegate void ExceptionEventHandler(object sender,Exception ex);
}

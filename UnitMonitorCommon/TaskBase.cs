using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using UnitMonitorCommunication;
using System.Reflection;

namespace UnitMonitorCommon
{
    /// <summary>
    /// 每一任务相当于电厂的一个系统或一个画面，每一任务设多个功能组，每个功能组又有多个数据点(Point)
    /// 任务加入任务容器后才能被自动执行
    /// </summary>
    public class TaskBase
    {
        //在历史模式中保存和读取的历史数据
        private Lazy<Dictionary<string, Queue<double>>> historyValues;
        /// <summary>
        /// 任务的配置文件，所有的信息都要从中加载
        /// </summary>
        public FileInfo ConfigFileInfo { private set; get; }
        /// <summary>
        /// 任务的功能组
        /// 每个功能组又有多个数据点(Point)
        /// 每个功能组可以关闭执行，在需要时再打开执行，以节省资源
        /// </summary>
        public FunGroup[] FunGroups { set; get; }

        //任务的事件
        //设这么多的事件是为了给其它组件调用，比如：logger(记录debug，信息等），性能分析，各种计数器，
     
        /// <summary>
        /// 当任务开始执行时发生
        /// </summary>
        public event EventHandler BeginRun;
        /// <summary>
        /// 当预处理成功结束后发生
        /// </summary>
        public event EventHandler AfterPreInitSuccess;
        /// <summary>
        /// 当预处理失败时发生
        /// </summary>
        public event TaskFailedEventHandler AfterPreInitFailed;
        /// <summary>
        /// 开始初始化数据时发生
        /// </summary>
        public event EventHandler BeginInitData;
        /// <summary>
        /// 当初始化数据成功完成后发生
        /// </summary>
        public event EventHandler AfterInitDataSuccess;
        /// <summary>
        /// 当初始化数据失败时发生
        /// </summary>
        public event TaskFailedEventHandler AfterInitDataFailed;
        /// <summary>
        /// 开始执行Process前发生
        /// </summary>
        public event EventHandler BeginProcess;
        /// <summary>
        /// 当主逻辑处理成功完成后发生
        /// </summary>
        public event EventHandler AfterProcessSuccess;
        /// <summary>
        /// 当主逻辑处理失败后发生
        /// </summary>
        public event TaskFailedEventHandler AfterProcessFailed;
        /// <summary>
        /// 当任务一次执行完后发生
        /// </summary>
        public event EventHandler RunComplete;
        public event SendMessageEventHandler SendMessage;
        /// <summary>
        /// 当此任务中的FunGroup的Used属性变更后引发
        /// 此事件不一定有用，以后如无用就删除它
        /// </summary>
        public event FunGroupUsedChangedEventHandler FunGroupUsedChanged;
    


        /// <summary>
        /// 任务第一次开始执行的时间
        /// </summary>
        public DateTime BeginTime { private set; get; } = DateTime.MinValue;
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName
        {
            get
            {
                if (this.ConfigFileInfo != null)
                    return this.ConfigFileInfo.Name.Replace(".task", "");
                return "";
            }
        }
        /// <summary>
        /// 任务路径
        /// </summary>
        public string TaskPath
        {
            get
            {

                return ConfigFileInfo.FullName.Replace(Directory.GetCurrentDirectory(), "").Replace(TaskName + ".task", "");
            }
        }
        /// <summary>
        /// 运行次数
        /// </summary>
        public int RunCount { private set; get; }
        public bool IsRunning
        {
            get
            {
                return TasksContainer.Instance.IsRunning && TasksContainer.Instance.Contains(this);
            }
        }
        /// <summary>
        /// 运行周期（秒）
        /// 请设为30的倍数
        /// </summary>
        public int Period { private set; get; } = 60;

 

        public bool RunRequired
        {
            get
            {
                return TasksContainer.Instance.RunCount % (Period / TasksContainer.Instance.Period) == 0;
            }
        }
        public TaskBase()
        {

        }
        /// <summary>
        /// 加载任务
        /// </summary>
        /// <param name="configFile">配置文件</param>
        /// <returns></returns>
        public static TaskBase LoadTask(FileInfo configFile)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(configFile.FullName);

            XmlNode taskInfo = doc.DocumentElement.SelectSingleNode("taskinfo");
            Assembly assem = Assembly.Load(taskInfo.Attributes["assembly"].Value);
            TaskBase task = (TaskBase)assem.CreateInstance(taskInfo.Attributes["class"].Value);
            task.ConfigFileInfo = configFile;
            task.Period = Convert.ToInt32(taskInfo.Attributes["period"].Value);
            XmlNodeList funList = doc.GetElementsByTagName("funGroup");
            int count = funList.Count;
            task.FunGroups = new FunGroup[count];
            for (int i = 0; i < count; i++)
            {
                FunGroup fun = FunGroup.Load(task, funList[i]);
            }
            //执行一些模块自定义加载配置操作
            task.LoadConfig(doc);
            return task;



        }
        /// <summary>
        /// 执行一些模块自己的加载配置操作
        /// </summary>
        /// <param name="doc">配置xml文档</param>
        protected virtual void LoadConfig(XmlDocument doc)
        {

        }
        /// <summary>
        /// 引发FunGroupUsedChanged事件
        /// </summary>
        /// <param name="funGroup"></param>
        public void RaiseFunGroupUsedChanged(FunGroup funGroup)
        {
            if (FunGroupUsedChanged != null)
                FunGroupUsedChanged(funGroup);
        }

        /// <summary>
        /// 任务执行的主逻辑，各派生类根据实际内容编写
        /// </summary>
        protected virtual void Process() { }
        /// <summary>
        /// 任务执行的主体过程，由TaskContainer调用
        /// 执行此过程时，TaskContainer的点表数据已更新好了
        /// </summary>
        internal void Run()
        {
            if (!RunRequired)
                return;

            DateTime RunBeginTime = DateTime.Now;
            try
            {
                if (BeginInitData != null)
                    BeginInitData(this,null);
                //从TaskContainer中更新本任务需要的数据
                InitData();
                //引发数据初始化完成事件
                if (AfterInitDataSuccess != null)
                    AfterInitDataSuccess(this, null);
            }
            catch (Exception ex)
            {
                if (AfterInitDataFailed != null)
                    AfterInitDataFailed(this, ex);
            }

            try
            {
                if (BeginProcess != null)
                    BeginProcess(this, null);
                Process();
                if (AfterProcessSuccess != null)
                    AfterProcessSuccess(this, null);

            }
            catch (Exception ex)
            {
                if (AfterProcessFailed != null)
                    AfterProcessFailed(this, ex);
            }
            if (RunComplete != null)
                RunComplete(this, null);
            RunCount += 1;

        }

  

        /// <summary>
        /// 预处理，此方法只应由TaskContainer调用
        /// </summary>
        internal void InternalPreInit()
        {
            if (!this.RunRequired)
                return;
            //首次运行，设置开始时间
            if (RunCount == 0)
                this.BeginTime = TasksContainer.Instance.CurrentTime;
 
            try
            {
                //引发开始处理事件
            if (BeginRun != null)
                BeginRun(this, null);
                //执行模块自定义的预处理过程 
                PreInit();
                if (AfterPreInitSuccess != null)
                    AfterPreInitSuccess(this, null);
            }
            catch (Exception ex)
            {
                if (AfterPreInitFailed != null)
                    AfterPreInitFailed(this, ex);
            }
        }
        /// <summary>
        /// 各派生类根据需要可重写一些在数据初始化前需要做的工作
        /// </summary>
        protected virtual void PreInit()
        {

        }
 

        /// <summary>
        /// 任务初始化数据，允许在派生类中重写
        /// </summary>
        protected virtual void InitData()
        {


            if (TasksContainer.Instance.Mode == DataMode.RealTime)
                InitRealData();
            if (TasksContainer.Instance.Mode == DataMode.History)
                InitHistoryData();

        }

        /// <summary>
        /// 实时模式时初始化数据
        /// </summary>
        private void InitRealData()
        {


            foreach (FunGroup item in this.FunGroups)
            {
                //仅需初始化使用中的功能组
                if (item.Used)
                {
                    foreach (Point point in item.Points)
                    {
                        point.OldValue = point.Value;
                        int index = TasksContainer.Instance.Points.BinarySearch(point.Id);
                        if (index > 0)
                            point.Value = TasksContainer.Instance.Values[index];
                        else
                            throw new Exception(string.Format("{0}任务从TasksContainer的点表中读取{1}的值时发现点表中无此键值", TaskName, point.Id));
                    }
                }

            }



        }
        /// <summary>
        /// 在历史模式时初始化数据
        /// </summary>
        public void InitHistoryData()
        {
            foreach (var item in FunGroups)
            {
                //仅需初始化使用中的功能组
                if (item.Used)
                {
                    foreach (Point point in item.Points)
                    {
                        LoadHistoryData(point);
                    }

                }
                else
                {
                    //当这些点不再使用时，从历史库中删除历史数据
                    //因为到下次转为使用时，历史库的数据的时间和当前的时间已不对应了
                    
                    foreach (Point point in item.Points)
                    {
                        historyValues.Value.Remove(point.Id);
                    }
                }
            }

        }
        /// <summary>
        /// 点加载历史数据
        /// 采用按需加载的方式，要用再读取，用完再读取，一次最多只从edna中加载一小时的数据
        /// </summary>
        /// <param name="point"></param>
        private void LoadHistoryData(Point point)
        {
            if (!historyValues.Value.ContainsKey(point.Id))
                historyValues.Value.Add(point.Id, null);
            Queue<double> values = historyValues.Value[point.Id];
            if ((values == null) || (values.Count == 0))
            {
                values = Dna.DNAGetHistValue(point.Id, TasksContainer.Instance.CurrentTime, Period);
                historyValues.Value[point.Id] = values;

            }
            if ((values != null) && (values.Count > 0))
                point.Value = values.Dequeue();
            else
                throw new Exception(string.Format("{0}在读取历史数据时数据不存在", this.TaskName));
        }



        /// <summary>
        /// 发送消息到消息中心
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="messge">消息</param>

        public void RaiseSendMessage(MessageType messageType, string messge)
        {
            MessageInfo info = new MessageInfo();
            info.OccurTime = DateTime.Now;
            info.MessageType = messageType;
            info.Message = messge;
            info.TaskPath = this.TaskPath;
            info.SenderUrl = TasksContainer.LocalIP;
            info.TaskName = this.TaskName;
            if (SendMessage != null)
                SendMessage( info);

        }
        /// <summary>
        /// 考虑删除或修改
        /// </summary>
        /// <returns></returns>
        public TaskInfo ToTaskInfo()
        {
            TaskInfo inf = new TaskInfo();
            inf.BeginTime = this.BeginTime;  
            inf.IsRunning = this.IsRunning;       
            inf.ModuleName = this.GetType().Name;
            inf.Name = this.TaskName;
            inf.Path = this.TaskPath;
            inf.Period = this.Period;   
            inf.RunCount = this.RunCount;
 
            return inf;
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        public XmlDocument ConfigDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigFileInfo.FullName);
            return doc;
        }
        /// <summary>
        /// 用于显示任务参数信息时自定义显示参数信息
        /// 派生类中改写
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string,object> CustomParams()
        {
            return null;
        }

    }
    public delegate void FunGroupUsedChangedEventHandler(FunGroup funGroup);
    public delegate void TaskFailedEventHandler(TaskBase task, Exception ex);
    public delegate void SendMessageEventHandler( MessageInfo message);

}


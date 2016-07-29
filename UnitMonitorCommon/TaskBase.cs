using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using UnitMonitorCommunication;

namespace UnitMonitorCommon
{
    public class TaskBase
    {
        XmlDocument config;
        private Dictionary<string, Queue<double>> historyValues;
        public string TaskName { set; get; }
        public FileInfo ConfigFileInfo { set; get; }
        public List<Point> Points { set; get; }

        public Dictionary<string, object> TempValue { set; get; }
        public event EventHandler TaskAdded;
        public event EventHandler AfterPreInit;
        public event EventHandler AfterInitData;
        public event EventHandler AfterProcess;
        public event ExceptionEventHandler AfterTaskErr;
        public event EventHandler RunComplete;
        public DateTime LastSuccessTime { set; get; }

        public XmlDocument Config
        {
            set
            {
                config = value;
                foreach (XmlNode item in config.GetElementsByTagName("point"))
                {
                    Point point = new Point();
                    point.Alias = item.Attributes["name"].Value;
                    point.Id = item.Attributes["id"].Value;
                    if (item.Attributes["used"] != null)
                        point.Used = Convert.ToBoolean(item.Attributes["used"]);
                    else
                        point.Used = true;
                    Points.Add(point);
                }
            }

            get

            {
                return config;
            }
        }
        //任务模块共享的配置
        public XmlDocument SharedConfig { set; get; }

        public DateTime BeginTime { set; get; }
        public string TaskPath
        {
            get
            {

                return ConfigFileInfo.FullName.Replace(Directory.GetCurrentDirectory(), "").Replace(TaskName + ".config", "");
            }
        }

        public int RunCount { set; get; }
        public bool IsRunning
        {
            get
            {
                return TasksContainer.Instance.IsRunning && TasksContainer.Instance.Contains(this);
            }
        }

        public int Period
        {

            get
            {
                return IntSetting("period");
            }
        }
        /// <summary>
        /// 上次执行花费的时间(毫秒)
        /// </summary>
        public double LastSpendTime { private set; get; }
        /// <summary>
        /// 上次执行是否成功
        /// </summary>
        public bool LastSuccess { private set; get; }
        /// <summary>
        /// 总的执行成功的次数
        /// </summary>
        public int SuccessCount { private set; get; }
        /// <summary>
        /// 连续执行成功的次数
        /// </summary>
        public int PersistantSuccess { private set; get; }
        /// <summary>
        /// 连续执行失败的次数
        /// </summary>
        public int PersistantFail { private set; get; }
        /// <summary>
        /// 总的失败次数
        /// </summary>

        public int FailCount { private set; get; }
        public bool RunRequired
        {
            get
            {
                return TasksContainer.Instance.RunCount % (Period / TasksContainer.Instance.Period) == 0;
            }
        }
        public TaskBase()
        {
            Points = new List<Point>();
            historyValues = new Dictionary<string, Queue<double>>();
            TempValue = new Dictionary<string, object>();
        }
        /// <summary>
        /// 任务执行的主逻辑
        /// </summary>
        protected virtual void Process() { }
        public void Run()
        {
            if (TasksContainer.Instance.RunCount % (Period / TasksContainer.Instance.Period) != 0)
                return;
            DateTime RunBeginTime = DateTime.Now;
            try
            {
                InitData();
                if (AfterInitData != null)
                    AfterInitData(this,null);
                Process();
                if (AfterProcess != null)
                    AfterProcess(this,null);
                RunCount += 1;
                LastSuccess = true;
                LastSuccessTime = TasksContainer.Instance.CurrentTime;
            }
            catch (Exception ex)
            {
                LastSuccess = false;
                TasksContainer.Instance.RaiseTaskExecuteErr(this, ex);
            }

            if (LastSuccess)
            {
                SuccessCount += 1;
                PersistantSuccess += 1;
                PersistantFail = 0;
            }
            else
            {
                PersistantSuccess = 0;
                PersistantFail += 1;
                FailCount += 1;
            }
            LastSpendTime = CommUtil.TimeMillisecondSpan(RunBeginTime, DateTime.Now);
            if (RunComplete != null)
                RunComplete(this,null);
        }

        internal void InternalPreRun()
        {
            PreRun();
            if (AfterPreInit != null)
                AfterPreInit(this, null);
        }

        protected virtual void PreRun()
        {
            
        }

        public void RaiseTaskAdded()
        {
            if (TaskAdded != null)
                TaskAdded(this,null);
            this.RunComplete += TasksContainer.Instance.OnTaskChanged;
        }
        private List<string> GetUsedPoints()
        {
            List<string> usedPoints = new List<string>();
            foreach (Point item in Points)
            {
                if (item.Used)
                    usedPoints.Add(item.Id);
            }
            return usedPoints;
        }
        /// <summary>
        /// 任务初始化逻辑
        /// </summary>
        public virtual void InitData()
        {


            if (TasksContainer.Instance.Mode == DataMode.RealTime)
                InitRealData();
            if (TasksContainer.Instance.Mode == DataMode.History)
                InitHistoryData();

        }
        public Point FindPointById(string id)
        {
            foreach (Point item in Points)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }
        public Point FindPointByAlais(string alias)
        {
            foreach (Point item in Points)
            {
                if (item.Alias == alias)
                    return item;
            }
            return null;
        }
        private void InitRealData()
        {


            foreach (var item in this.Points)
            {
                if (item.Used)
                {
                    int index = TasksContainer.Instance.Points.BinarySearch(item.Id);
                    if (index > 0)
                        item.Value = TasksContainer.Instance.Values[index];
                }

            }



        }
        public void SetPointData(string id, double value)
        {
            Point point = FindPointById(id);
            if (point != null)               
                point.Value = value;

        }
        public void InitHistoryData()
        {
            foreach (var item in Points)
            {
                if (item.Used)
                {
                    LoadHistoryData(item);
                }
            }

        }
        private void LoadHistoryData(Point point)
        {
            if (!historyValues.ContainsKey(point.Id))
                historyValues.Add(point.Id, null);
            Queue<double> values = historyValues[point.Id];
            if ((values == null) || (values.Count == 0))
            {
                values = Dna.DNAGetHistValue(point.Id, TasksContainer.Instance.CurrentTime, Period);
                historyValues[point.Id] = values;

            }
            if ((values != null) && (values.Count > 0))
                point.Value = values.Dequeue();
            else
                throw new Exception(string.Format("{0}在读取历史数据时数据不存在", this.TaskName));


        }


        /// <summary>
        /// 根据点的别名读取点的值（模拟量点）
        /// </summary>
        /// <param name="alias">点的别名</param>
        /// <returns></returns>
        public double AnalogValue(string alias)
        {
            foreach (Point item in Points)
            {
                if (item.Alias == alias)
                    return item.Value;
            }
            throw new Exception(string.Format("在点表中未找到别名为{0}的点", alias));
        }
        /// <summary>
        /// 根据点的别名读取点的值（打包点）
        /// </summary>
        /// <param name="alias">点的别名</param>
        /// <returns></returns>
        public int LpValue(string alias)
        {
            return Convert.ToInt32(AnalogValue(alias));
        }
        /// <summary>
        /// 根据点的别名读取点的值（开关量点）
        /// </summary>
        /// <param name="alias">点的别名</param>
        /// <returns></returns>
        public bool BoolValue(string alias)
        {

            return Convert.ToBoolean(AnalogValue(alias));
        }

        /// <summary>
        /// 读取配置值（字符串）
        /// </summary>
        /// <param name="settingname">配置名称</param>
        /// <returns>配置值（字符串）</returns>
        private string SettingValue(string settingname, XmlDocument configDoc, string path = "/configuration/params/param")
        {
            string xmlPath = path + string.Format("[@name='{0}']", settingname);
            XmlNode node = configDoc.SelectSingleNode(xmlPath);
            if ((node != null) && (node.Attributes["value"] != null))
                return node.Attributes["value"].Value;
            return "";
        }
        public string SettingValue(string settingname)
        {
            string strValue = SettingValue(settingname, this.Config);
            if (strValue == "")
                strValue = SettingValue(settingname, this.SharedConfig);
            if (strValue == "")
                throw new Exception(string.Format("你要的名为{0}的设置不存在或未设置值", settingname));
            return strValue;
        }

        public string PointNameDescription(string name)
        {
            XmlNode node = this.Config.SelectSingleNode(string.Format("/configuration/points/point[@name='{0}']", name));
            if (node == null)
                return "";
            else
                return node.Attributes["description"].Value;
        }
        /// <summary>
        /// 读取配置值（bool）
        /// </summary>
        /// <param name="settingname">配置名称</param>
        /// <returns>配置值（bool）</returns>
        public bool BooleanSetting(string settingname)
        {
            return Convert.ToBoolean(SettingValue(settingname));
        }
        /// <summary>
        /// 读取配置值（double）
        /// </summary>
        /// <param name="settingname">配置名称</param>
        /// <returns>配置值（double）</returns>
        public double DoubleSetting(string settingname)
        {
            return Convert.ToDouble(SettingValue(settingname));
        }
        /// <summary>
        /// 读取配置值（int）
        /// </summary>
        /// <param name="settingname">配置名称</param>
        /// <returns>配置值（int）</returns>
        public int IntSetting(string settingname)
        {
            return Convert.ToInt32(SettingValue(settingname));
        }
        public bool FunSetting(string settingname)
        {

            XmlNode node = Config.SelectSingleNode(string.Format("/configuration/funs/fun[@name='{0}']", settingname));
            if ((node != null) && (node.Attributes["value"] != null))
                return Convert.ToBoolean(node.Attributes["value"].Value);
            return false;
        }
        public void SetPointUse(string alias)
        {
            Point point = FindPointByAlais(alias);
            if (point != null)
                point.Used = true;
        }

        public void SetPointUnUse(string alias)
        {
            Point point = FindPointByAlais(alias);

            if (point != null)
            {
                point.Used = false;
                RemoveRelatedKey(alias);
            }

        }

        /// <summary>
        /// 发送消息到消息中心
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="messge">消息</param>

        public void SendMessge(MessageType messageType, string messge)
        {
            MessageCenter.Instance.SendMessage(messageType, messge, this.TaskPath);

        }
        public void RemoveRelatedKey(string preKey)
        {
            if (this.TempValue != null)
            {
                List<string> removeKeys = new List<string>();
                foreach (string key in this.TempValue.Keys)
                {
                    if (key.Contains(preKey))
                        removeKeys.Add(key);
                }
                foreach (string key in removeKeys)
                {
                    this.TempValue.Remove(key);
                }
            }

        }
        public TaskInfo ToTaskInfo()
        {
            TaskInfo inf = new TaskInfo();
            inf.BeginTime = this.BeginTime;
            inf.FailCount = this.FailCount;
            inf.IsRunning = this.IsRunning;
            inf.LastSuccessTime = this.LastSuccessTime;
            inf.LastSpendTime = this.LastSpendTime;
            inf.LastSuccess = this.LastSuccess;
            inf.ModuleName = this.GetType().Name;
            inf.Name = this.TaskName;
            inf.Path = this.TaskPath;
            inf.Period = this.Period;
            inf.PersistantFail = this.PersistantFail;
            inf.PersistantSuccess = this.PersistantSuccess;
            inf.RunCount = this.RunCount;
            inf.SuccessCount = this.SuccessCount;
            return inf;
        }

    }
    public class Point
    {
        public string Alias { set; get; }
        public string Id { set; get; }
        public double Value { set; get; }
        public bool Used { set; get; }

    }



}


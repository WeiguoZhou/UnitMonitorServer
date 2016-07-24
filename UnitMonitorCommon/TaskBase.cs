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
    public  class TaskBase
    {
        XmlDocument config;
        XmlDocument sharedConfig;
        private Dictionary<string, Queue<double>> historyValues;
        public string TaskName { set; get; }
        public FileInfo ConfigFileInfo { set; get; }
        public List<Point>  Points {set;get;}
        public Dictionary<string, bool> Depend { set; get; }

        public Dictionary<string, object> TempValue { set; get; }
        public event TaskEventHandler TaskAdded;
        public event TaskEventHandler AfterPreInit;
        public event TaskEventHandler AfterInitData;
         public event TaskEventHandler AfterProcess;
        public event TaskErrEventHandler AfterTaskErr;
        public event TaskEventHandler RunComplete;
        public DateTime LastRunTime { set; get; }

        public XmlDocument Config
        { set
            {
                config = value;
                foreach (XmlNode item in config.GetElementsByTagName("point"))
                {
                    Point point = new Point();
                    point.Alias = item.Attributes["name"].Value;
                    point.Id = item.Attributes["id"].Value;
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
        public XmlDocument SharedConfig {
            set
            {
                sharedConfig = value;
                 foreach (XmlNode item in config.GetElementsByTagName("depend"))
                 {
                        Depend.Add(item.Attributes["name"].Value, Convert.ToBoolean(item.Attributes["value"].Value));
                 }
             }

        get

            {
                return sharedConfig;
            }

            }
        public DateTime BeginTime { set; get; }
        public string TaskPath
        {
            get
            {
                return ConfigFileInfo.FullName.Replace(Environment.CurrentDirectory, "").Replace(".config", "");
            }
        }

        public int RunCount { set; get; }
        public bool IsRunning
        {
            get
            {
                return TasksContainer.Instance.Contains(this);
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

        public TaskBase()
        {
            Points = new List<Point>();
            historyValues = new Dictionary<string, Queue<double>>();
            Depend = new Dictionary<string, bool>();
            TempValue = new Dictionary<string, object>();
        }
        /// <summary>
        /// 任务执行的主逻辑
        /// </summary>
        protected virtual void Process() { }
        public void Run()
        {
            if (TasksContainer.Instance.RunCount % (Period/ TasksContainer.Instance.Period) != 0)
                return;
            DateTime beginTime = DateTime.Now;
            try
            {
                PreInit();
                if (AfterPreInit != null)
                    AfterPreInit(this);
                InitData();
                if (AfterInitData != null)
                    AfterInitData(this);
                Process();
                if (AfterProcess != null)
                    AfterProcess(this);
                RunCount += 1;
                LastSuccess = true;
            }
            catch (Exception ex)
            {
                LastSuccess = false;
                //记录出错信息
                string message = string.Format("任务执行出错：任务模块:{0}，出错信息:{1}", this.TaskName, ex.Message);
                Logger.Instance.LogDebug(message);
                if (AfterTaskErr != null)
                    AfterTaskErr(this, ex);


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
            LastSpendTime = CommUtil.TimeMillisecondSpan(beginTime,DateTime.Now);
            if (RunComplete != null)
                RunComplete(this);
        }

        protected virtual  void PreInit()
        {
            //在这里可以主要做一些变更
        }
        public void RaiseTaskAdded()
        {
            if (TaskAdded != null)
                TaskAdded(this);
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
            List<string> usedPoints = GetUsedPoints();
           
            if (TasksContainer.Instance.Mode == DataMode.RealTime)
                InitRealData(usedPoints);
            if (TasksContainer.Instance.Mode == DataMode.History)
                InitHistoryData(usedPoints);

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
        public Point FindPointByAlais(string alias )
        {
            foreach (Point item in Points)
            {
                if (item.Alias == alias)
                    return item;
            }
            return null;
        }
        private void InitRealData(List<string> usedPoints)
        {
            int count = usedPoints.Count;
           if (count > 0)
            {
                double[] values = Dna.GetRtValue(usedPoints);
                for (int i = 0; i < count; i++)
                {
                    Point point = FindPointById(usedPoints[i]);
                    if(point!=null)
                    {
                        point.Value = values[i];
                    }
                }

            }

        }
        public void SetPointData(string id,double value)
        {
            Point point = FindPointById(id);
            if (point == null)
                throw new Exception(string.Format("在点表中未找到点id为{0}的点", id));
            point.Value = value;

        }
        public  void InitHistoryData(List<string> usedPoints)
        {
            foreach (string item in usedPoints)
            {
                LoadHistoryData(item);
            }
        }
        private void LoadHistoryData(string id)
        {
            Point point = FindPointById(id);
            if (point == null)
                return;
            if (historyValues.ContainsKey(id))
            {
                Queue<double> values = historyValues[id];
                if ((values != null) && (values.Count > 0))
                    point.Value = values.Dequeue();
                else
                {
                    values = Dna.DNAGetHistValue(id, TasksContainer.Instance.CurrentTime, Period);
                    historyValues[id] = values;
                    point.Value = values.Dequeue();
                }
            }
            else
            {
                historyValues.Add(id ,null);
                LoadHistoryData(id);
            }
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
        private string SettingValue(string settingname, XmlDocument configDoc,string path= "/configuration/params/param")
        {
            string xmlPath= path+string.Format("[@name='{0}']", settingname);
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
    }
    public class Point
    {
        public string Alias { set; get; }
        public  string Id { set; get; }
        public double Value { set; get; }
        public bool Used { set; get; }

    }

    public delegate void TaskEventHandler(TaskBase task);
    public delegate void TaskErrEventHandler(TaskBase task, Exception ex);

    }


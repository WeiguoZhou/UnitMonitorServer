using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UnitMonitorCommon
{
    /// <summary>
    /// 提供对一些参数设置的描述
    /// </summary>
    public static class ParamSetting
    {
        //开关量设定参数
        public const string OnTimeDelay = "onTimeDelay";
        public const string OffTimeDelay = "offTimeDelay";
        public const string OnDelayAlarm = "onDelayAlarm";
        public const string OffDelayAlarm = "offDelayAlarm";
        public const string ChangedOnAlarm = "changedOnAlarm";
        public const string ChangedOffAlarm = "changedOffAlarm";
        //模拟量设定参数
        public const string LowValue = "lowValue";
        public const string LowResetValue = "lowResetValue";
        public const string LowValueTimeDelay = "lowValueTimeDelay";
        public const string LowAlarm = "lowAlarm";
        public const string HighValue = "highValue";
        public const string HighResetValue = "highResetValue";
        public const string HighValueTimeDelay = "highValueTimeDelay";
        public const string HighAlarm = "highAlarm";
        public const string ChangeLowValue = "changeLowValue";
        public const string ChangeLowTimeDelay = "changeLowTimeDelay";
        public const string ChangeLowAlarm = "changeLowAlarm";
        public const string ChangeHighValue = "changeHighValue";
        public const string ChangeHighStatisticsPeriod = "changeHighStatisticsPeriod";
        public const string ChangeHighAlarm = "changeHighAlarm";
        public const string ChangeHighCount = "changeHighCount";
        /// <summary>
        /// 读取指定的参数说明配置文档
        /// </summary>
        /// <param name="configFile"></param>
        /// <returns></returns>
        private static XmlDocument GetDescriptionDocument(FileInfo configFile)
        {
            if (!configFile.Exists)
                throw new Exception(string.Format("配置文件{0}未找到", configFile.FullName));
            XmlDocument doc = new XmlDocument();
            doc.Load(configFile.FullName);
            return doc;
        }
        /// <summary>
        /// 读取默认的参数说明配置文档
        /// 参数说明包括task参数说明、funGroup参数说明、Point参数说明
        /// </summary>
        /// <returns></returns>
        private static XmlDocument GetdefaultDescriptionDocument()
        {
            string filePath = Directory.GetCurrentDirectory() + "\\ModuleSettins\\defaultParamDescription.xml";
            FileInfo configFile = new FileInfo(filePath);
            return GetDescriptionDocument(configFile);
        }
        /// <summary>
        /// 从doc中读取指定配置节中的参数说明
        /// 参数说明的格式必须是key="" description=""
        /// </summary>
        /// <param name="doc">配置文档</param>
        /// <param name="parentNodeTag">保存配置信息的父节点名</param>
        /// <param name="descriptions">保存配置信息的Dictionary</param>
        private static void GetDescriptions(XmlDocument doc,string parentNodeTag, Dictionary<string, string> descriptions)
        {
            XmlNodeList list = doc.GetElementsByTagName(parentNodeTag);
            if ((list == null) || (list.Count == 0))
                return;
            foreach (XmlNode item in list[0].ChildNodes)
            {
                descriptions[item.Attributes["key"].Value] = item.Attributes["description"].Value;
            }
        }

        public static Dictionary<string, string> DefaultPointDescriptions()
        {
            XmlDocument doc = GetdefaultDescriptionDocument();
            Dictionary<string, string> descriptions = new Dictionary<string, string>();
            GetDescriptions(doc, "pointParamDescriptions", descriptions);
            return descriptions;
        }
        public static Dictionary<string, string> DefaultFunDescriptions()
        {
            XmlDocument doc = GetdefaultDescriptionDocument();
            Dictionary<string, string> descriptions = new Dictionary<string, string>();
            GetDescriptions(doc, "funParamDescriptions", descriptions);
            return descriptions;
           
        }
        public static Dictionary<string, string> DefaultTaskDescriptions()
        {
            XmlDocument doc = GetdefaultDescriptionDocument();
            Dictionary<string, string> descriptions = new Dictionary<string, string>();
            GetDescriptions(doc, "taskParamDescriptions", descriptions);
            return descriptions;
        }
        /// <summary>
        /// 加载标准参数描述和模块自定义的参数描述
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static Dictionary<string, string> CustomPointDescriptions(this TaskBase task)
        {
            Dictionary<string, string> descriptions = DefaultPointDescriptions();
            XmlDocument doc = GetDescriptionDocument(task.ConfigFileInfo);
            GetDescriptions(doc, "pointParamDescriptions", descriptions);
            return descriptions;
        }
        public static Dictionary<string, string> CustomFunDescriptions(this TaskBase task)
        {
            Dictionary<string, string> descriptions = DefaultFunDescriptions();
            XmlDocument doc = GetDescriptionDocument(task.ConfigFileInfo);
            GetDescriptions(doc, "funParamDescriptions", descriptions);
            return descriptions;

        }
        public static Dictionary<string, string> CustomTaskDescriptions(this TaskBase task)
        {
            Dictionary<string, string> descriptions = DefaultTaskDescriptions();
            XmlDocument doc = GetDescriptionDocument(task.ConfigFileInfo);
            GetDescriptions(doc, "taskParamDescriptions", descriptions);
            return descriptions;
        }
    }
}

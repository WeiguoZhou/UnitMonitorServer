using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace UnitMonitorCommunication
{
    [DataContract]
    public class MessageInfo
    {/// <summary>
     /// 消息的类型
     /// </summary>
     [DataMember]
        public MessageType MessageType { set; get; }
        /// <summary>
        /// 消息的内容
        /// </summary>
        [DataMember]
        public string Message { set; get; }
        /// <summary>
        /// 发送消息的服务器地址
        /// </summary>
        [DataMember]
        public string SenderUrl { set; get; }
        /// <summary>
        /// 发送消息的任务路径
        /// </summary>
        [DataMember]
        public string  TaskPath { set; get; }
        [DataMember]
        public DateTime OccurTime { set; get; }
        public MessageInfo()
        {
            OccurTime = DateTime.Now;
        }
    }
    [DataContract]
    public enum MessageType
    {
        [EnumMember]
        Info = 0,
        [EnumMember]
        Alarm =1,
        [EnumMember]
        System =2,
        [EnumMember]
        Warn=4,
        [EnumMember]
        Danger=8,
        [EnumMember]
        Examination

    }
}

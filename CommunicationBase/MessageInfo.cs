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
        /// <summary>
        /// 发送消息的任务名称
        /// </summary>
        [DataMember]
        public string TaskName { set; get; }
        [DataMember]
        public DateTime OccurTime { set; get; }
        public MessageInfo()
        {
            OccurTime = DateTime.Now;
        }
    }
    [DataContract][Flags]
    public enum MessageType
    {
        [EnumMember]
        System = 0x01,
        [EnumMember]
        Info = 0x02,
        [EnumMember]
        Alarm = 0x04,
        [EnumMember]
        Warn= 0x08,
        [EnumMember]
        Danger= 0x16,
        [EnumMember]
        Examination= 0x32,
        [EnumMember]
        Debug=0X64
    }
}

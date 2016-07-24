using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommunication;

namespace UnitMonitorClient
{
    public class ClientMessageCenter
    {
        /// <summary>
        /// 订阅的消息
        /// </summary>
        public List<SubscribedInfo> Subscription { set; get; }
        /// <summary>
        /// 收到任何消息都引发的事件
        /// </summary>
        public event MessageEventHandler RecievedMessage;
        /// <summary>
        /// 收到的消息是订阅的消息才引发的事件
        /// </summary>
        public event MessageEventHandler RecievedSubscribedMessage;
        private static ClientMessageCenter instance;
        public static ClientMessageCenter Instance
        {
            get
            {
                if (instance == null)
                    instance = new ClientMessageCenter();
                return instance;
            }
        }

        public void RaiseRecievedMessage( MessageInfo info)
        {

            if (RecievedMessage!=null)
                RecievedMessage(info);
            if (IsSubscribed(info.SenderUrl, info.TaskPath, info.MessageType))
            {
                if (RecievedSubscribedMessage!=null)

                    RecievedSubscribedMessage(info);
            }
               
            
                 
        }
        public static string MessageTypeString(MessageType type)
        {
            switch (type)
            {
                case MessageType.Info:
                    return "消息";
                case MessageType.Alarm:
                    return "报警";
                case MessageType.Warn:
                    return "警告";
                case MessageType.Danger:
                    return "危险";
                case MessageType.Examination:
                    return "考核";
                case MessageType.System:
                    return "系统";
                default:
                    return "未知";
            }
        }
        public void AddSubscription(string serverUrl, string path, List<MessageType> messageTypes)
        {
            SubscribedInfo info = new SubscribedInfo();
            info.ServerUrl = serverUrl;
            info.TaskPath = path;
            info.MessageTypes = messageTypes;
            Subscription.Add(info);
        }
        public bool IsSubscribed(string serverUrl, string path, MessageType messageType)
        {
            return true;
            //if (messageType == MessageType.System)
            //    return true;
            //foreach (SubscribedInfo item in Subscription)
            //{
            //    //这里在任务路径的实现上有所改变，用到了Contains，即path可以用目录路径表示该目录下的所有任务
            //    if ((item.ServerUrl == serverUrl) && path.Contains(item.TaskPath) && item.MessageTypes.Contains(messageType))
            //        return true;
                
            //}
            //return false;
        }
        public ClientMessageCenter()
        {
            Subscription = new List<SubscribedInfo>();
        }
    }
    /// <summary>
    /// 表示订阅的规则中的一条记录
    /// </summary>
    public class SubscribedInfo
    {
        /// <summary>
        /// 发送方服务器地址
        /// </summary>
       public string ServerUrl { set; get; }
        /// <summary>
        /// 发送方任务路径
        /// </summary>
     public   string TaskPath { set; get; }
        /// <summary>
        /// 可接受的发送的消息类型
        /// </summary>
      public  List<MessageType> MessageTypes { set; get; }

    }

}


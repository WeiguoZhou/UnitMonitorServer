using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommunication;

namespace UnitMonitorCommon
{
   public class MessageCenter
    {
        private static MessageCenter messageCenter;
        string serverIp = "";
        public event MessageEventHandler SendMessageEvent;
        public void SendMessage(MessageType messageType ,string message,string source)
        {
            MessageInfo inf = new MessageInfo();
            inf.MessageType = messageType;
            inf.Message = message;
            inf.TaskPath = source;
            inf.SenderUrl = ServerIp;
            inf.OccurTime = DateTime.Now;
            OnSendMessage(inf);
        }
        public void OnSendMessage(MessageInfo inf)
        {
            if (SendMessageEvent != null)
                SendMessageEvent(inf);
        }
        private MessageCenter() { }
        public static MessageCenter Instance
        {
            get
            {
               return messageCenter;
            }

        }
        public static void Init()
        {
            if (messageCenter == null)
                messageCenter = new MessageCenter();
        }
        /// <summary>
        /// 将MessageType 转换为文字表达形式
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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
        public string ServerIp
        {
            get
            {
                if (string.IsNullOrEmpty(serverIp))
                {
                    string hostName = Dns.GetHostName();//本机名   
                    System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   
                    foreach (IPAddress item in addressList)
                    {
                        if ((item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) && (!item.ToString().Contains("127.0.0.1")))
                            serverIp= item.ToString();
                    }
                }
                return serverIp;
            }
        }
    }
    public delegate void MessageEventHandler(MessageInfo inf);
 
 
}

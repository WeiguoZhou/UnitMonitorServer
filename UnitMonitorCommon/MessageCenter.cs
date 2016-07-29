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
        public const string InfoMessageType = "消息";
        public const string SysMessageType = "系统";
        public const string AlarmMessageType = "报警";
        public const string WarnMessageType = "警告";
        public const string DangerMessageType = "危险";
        public const string ExaminationMessageType = "考核";
        public const string UnknownMessageType = "未知";
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
            {
                messageCenter = new MessageCenter();
                TasksContainer.Init();
                TasksContainer.Instance.BeginRun += messageCenter.OnTaskContainerRun;
                TasksContainer.Instance.StopRun += messageCenter.OnTaskContainerStopRun;
            }


        }
        private void OnTaskContainerStopRun(object sender,EventArgs e)
        {
            SendMessage(MessageType.System, "任务中心已停止运行", "System");
        }
        private void OnTaskContainerRun(object sender, EventArgs e)
        {
            SendMessage(MessageType.System, "任务中心已启动", "System");
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
                    return InfoMessageType;
                case MessageType.Alarm:
                    return AlarmMessageType;
                case MessageType.Warn:
                    return WarnMessageType;
                case MessageType.Danger:
                    return DangerMessageType;
                case MessageType.Examination:
                    return ExaminationMessageType;
                case MessageType.System:
                    return SysMessageType;
                default:
                    return UnknownMessageType;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommunication;

namespace UnitMonitorClient
{
    public class ClientCommunication
    {
        WebServiceHost svcHost;

        string ip = "";
        public string Ip
        {
            get
            {
                if (string.IsNullOrEmpty(ip))
                    foreach (IPAddress item in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                    {
                        if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            ip = item.ToString();
                    }
                return ip;
            }
        }
        public int Port
        {
            get
            {
                return Properties.Settings.Default.Port;
            }
        }
        public string ClientName
        {
            get
            {
                return Dns.GetHostName();
            }
        }

        public string Url
        {
            get
            {
                return string.Format("http://{0}:{1}", Ip, Port);
            }
        }
        public WebServiceHost SvcHost
        {
            get
            {
                return svcHost;
            }
        }
      public event EventHandler ServiceHostStateChanged;
        static ClientCommunication instance;
        public static ClientCommunication Instance()
        {
            if (instance == null)
            {
                instance = new ClientCommunication();

            }
            return instance;
        }
        public void StartService()
        {

            Uri baseAddress = new Uri(Url);
           
            svcHost = new WebServiceHost(typeof(ClientService), baseAddress);
     
            svcHost.Closing += RaiseServiceHostStateChanged;
            svcHost.Closed += RaiseServiceHostStateChanged;
            svcHost.Opened += RaiseServiceHostStateChanged;
            svcHost.Opening += RaiseServiceHostStateChanged;
            svcHost.Faulted+= RaiseServiceHostStateChanged;
            svcHost.Open();

            


        }
        public void RaiseServiceHostStateChanged(object sender,EventArgs e)
        {
            if (ServiceHostStateChanged != null)
            {
                ServiceHostStateChanged(svcHost, e);
            }
            string message = "";
            switch (svcHost.State)
            {
                case CommunicationState.Opening:
                    message = "本地服务正在打开";
                    break;
                case CommunicationState.Opened:
                    message = "本地服务已打开";
                    RegClients();
                    break;
                case CommunicationState.Closing:
                    message = "本地服务正在关闭";
                    break;
                case CommunicationState.Closed:
                    message = "本地服务已关闭";
                    break;
                case CommunicationState.Faulted:
                    message = "本地服务失败";
                    break;

            }
            MessageInfo info = new MessageInfo();
            info.SenderUrl = Url;
            info.MessageType = MessageType.System;
            info.Message = message;
            info.TaskPath = "本地服务系统消息";
            ClientMessageCenter.Instance.RaiseRecievedMessage(info);
        }

        private void RegClients()
        {
            Task.Run(() =>
            {
                string ip = Ip;
                int port = Port;
                foreach (ServerInfo item in Servers.Instance())
                {
                    item.RegClient(ip,port);
                }
            });
        }

        public void RaiseRecievedMessage(MessageInfo info)
        {

            ServerInfo server = Servers.Instance().FindServer(info.SenderUrl);
            if (server != null)
                server.SendMessageCount += 1;
  
             ClientMessageCenter.Instance.RaiseRecievedMessage(info );

        }
        public void StopService()
        {
            
            if ((svcHost != null) && (SvcHost.State != CommunicationState.Closed))
                svcHost.Close();
        }


    


   
    }
    public delegate void MessageEventHandler(MessageInfo info);

}

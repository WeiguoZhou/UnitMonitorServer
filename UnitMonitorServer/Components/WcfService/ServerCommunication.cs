using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Net;
using System.ServiceModel.Web;
using UnitMonitorCommunication;
using System.ServiceModel.Description;
using UnitMonitorCommon;
using System.Threading;

namespace UnitMonitorServer
{
  public  class ServerCommunication
    {
        WebServiceHost svcHost;
        string ip="";
        public string Ip { get
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
        public int Port {
            get
            {
                return Properties.Settings.Default.Port;
            }
        }
        public string ServerName
        {
            get
            {
                return Properties.Settings.Default.ServerName;
            }
        }
       
        public  string Url
        {
            get
            {
                return string.Format("http://{0}:{1}", Ip, Port);
            }
        }
        static ServerCommunication instance;
        public static ServerCommunication Instance()
        {
            if (instance == null)
            {
                instance = new ServerCommunication();
                
            }
            return instance;   
        }
        public void StartService()
        {
            if (svcHost == null)
            {
                Uri baseAddress = new Uri(Url);
                svcHost = new WebServiceHost(typeof(Service), baseAddress);

                    svcHost.Closed += OnServiceStateChanged;
                svcHost.Closing+= OnServiceStateChanged;
                svcHost.Faulted+= OnServiceStateChanged;
                svcHost.Opening+= OnServiceStateChanged;
                svcHost.Opened+= OnServiceStateChanged;
            }

            if(svcHost.State!= CommunicationState.Opened)
                svcHost.Open();           

        }
        private void OnServiceStateChanged(object sender,EventArgs e)
        {
            switch (svcHost.State)
            {
                case CommunicationState.Created:
                    break;
                case CommunicationState.Opening:
                    break;
                case CommunicationState.Opened:

                   MessageCenter.Instance().SendMessageEvent += SendMessageHandler;
                    MessageCenter.Instance().SendMessage(MessageType.System, string.Format("位于{0}的{1}服务已准备就绪", Url, ServerName), "");
                    RegServer();
                    break;
                case CommunicationState.Closing:
                    break;
                case CommunicationState.Closed:
                    MessageCenter.Instance().SendMessage(MessageType.System, string.Format("位于{0}的{1}服务已终止", Url, ServerName), "");
                    break;
                case CommunicationState.Faulted:
                    MessageCenter.Instance().SendMessage(MessageType.System, string.Format("位于{0}的{1}服务失败", Url, ServerName), "");
                    break;
                default:
                    break;
            }
        }

        private void SendMessageHandler(MessageInfo inf)
        {

            Thread thread = new Thread(SendMessage);
            thread.Start(inf);
        }

        public void Stopervice()
        {
            Clients.Instance().ServerShutOff();

            if ((svcHost != null) && (svcHost.State != CommunicationState.Closed))
                svcHost.Close();
            MessageCenter.Instance().SendMessageEvent -= SendMessageHandler;
        }
        public void SendMessage(object state)
        {
           MessageInfo inf = (MessageInfo)state;

            foreach (ClientInfo item in Clients.Instance())
            {
                item.SendMessage(inf);
            }
            
        }
        public void RegServer()
        {

            Task.Run(() =>
            {
                foreach (ClientInfo item in Clients.Instance())
                {
                    item.RegServer(Ip,Port);
                }

            });
        }




    }

    }

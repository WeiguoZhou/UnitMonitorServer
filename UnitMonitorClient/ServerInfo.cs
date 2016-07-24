using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommunication;
using System.ServiceModel;
using System.ServiceModel.Web;
using UnitMonitorCommon;
using System.Net;

namespace UnitMonitorClient

{
    public class ServerInfo: IOnlineStatus
    {
        WebChannelFactory<IServerService> cf;

        public event EventHandler Offline;
        public event EventHandler Online;

        public string Ip { set; get; }
        public int Port { set; get; }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(Ip))
                    return "";
                try
                {
                    IPAddress address = IPAddress.Parse(Ip);
                    IPHostEntry hostEntry = Dns.GetHostEntry(address);
                    return hostEntry.HostName;
                }
                catch
                {

                }
                return "";
            }
        }

        public string Url
        {
            get
            {
                return string.Format("http://{0}:{1}", Ip, Port);
            }
        }

        public DateTime OnlineTime { set; get; }


        public bool IsOnline { protected set; get; }
        public CommunicationState State
        {
            get
            {
                if (cf == null)
                    return CommunicationState.Closed;
                else
                    return cf.State;
            }
        }
        public ServerInfo(string ip, int port)
        {
            OnlineTime = DateTime.Now;
            this.Ip = ip;
            this.Port = port;
            TryLink();
        }

        public int SendMessageCount { set; get; }

        public  void TryLink()
        {
            string ip = ClientCommunication.Ip;
            int port = ClientCommunication.Port;
            if (this.IsOnline)
            {
                try
                {
                    this.IsOnline=RegClient(ip, port);
                }
                catch
                {
                    
                }
            }
            else
            {
                //如果找不到主机名，表示主机没开
                if (!string.IsNullOrEmpty(Name))
                {
                    Uri baseAddress = new Uri(Url);
                    try
                    {
                        cf = new WebChannelFactory<IServerService>(baseAddress);
                        cf.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(5);
                        cf.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(5);
                        cf.Faulted += OnClientFaulted;
                        this.IsOnline = RegClient(ip, port);
                        if (this.IsOnline)
                        {
                            this.OnlineTime = DateTime.Now;      
                            if (this.Online != null)
                                this.Online(this, null);
                        }

                    }
                    catch
                    {

                    }
                }

            }
            if (!this.IsOnline)
            {
                cf = null;
                this.OnlineTime = DateTime.MinValue;
                SendMessageCount = 0;
            }

        }

        private void OnClientFaulted(object sender, EventArgs e)
        {
            if (cf.State == CommunicationState.Faulted)
            {
                cf.Abort();
                cf = null;
                if (this.IsOnline)
                {
                    this.IsOnline = false;
                    if (this.Offline != null)
                        this.Offline(this, null);
                }
            }
        }


 
        public void TurnOffLine()
        {

            if (IsOnline)
            {

                if (cf != null)
                {

                    if (State == CommunicationState.Faulted)
                        cf.Abort();
                    if (State != CommunicationState.Closed)
                        cf.Close();
                }
                this.IsOnline = false;
                if (this.Offline != null)
                    this.Offline(this, null);
                OnlineTime = DateTime.MinValue;
                cf = null;
            }

        }

        public IServerService Channel
        {
            get
            {
                if (cf == null)
                    return null;
                else
                    return cf.CreateChannel();

            }
        }
    
        public bool RegClient(string ip,int port)
        {
            bool returnValue = false;
            if (cf!=null)
            {
                try
                {

                    returnValue=cf.CreateChannel().RegService(ip, port.ToString());
                }
                catch
                {

                }

            }
            return returnValue;
        }
        public void ClientShutOff(string ip)
        {
            if (IsOnline)
            {
                try
                {

                    cf.CreateChannel().ServiceShutOff(ip);
                }
                catch
                {

                }
            }

        }
    }

    }


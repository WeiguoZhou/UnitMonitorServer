using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommon;
using UnitMonitorCommunication;
using System.ServiceModel;
using System.Net;

namespace UnitMonitorServer
{
  public  class ClientInfo:IOnlineStatus

    {
        WebChannelFactory<IClientService> cf;
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
                if (string.IsNullOrEmpty(Ip))
                    return string.Empty;
                else
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

       

        public int SendMessageCount { set; get; }


        public void TryLink()
        {
            string ip = ServerCommunication.Ip;
            int port = ServerCommunication.Port;
            if (this.IsOnline)
            {
                try
                {
                    this.IsOnline = RegServer(ip, port);
                }
                catch
                {
                    
                }
                if (!this.IsOnline)
                {
                    if (this.Offline != null)
                        this.Offline(this, null);
                    
                }
            }
            else
            {
                //如果找不到主机名，表示主机没开
                if (!string.IsNullOrEmpty(Name) )
                {
                    Uri baseAddress = new Uri(Url);
                    try
                    {
                        cf = new WebChannelFactory<IClientService>(baseAddress);
                        cf.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(5);
                        cf.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(5);
                        cf.Faulted += OnClientFaulted;
                        this.IsOnline = RegServer(ip, port);
                        if (this.IsOnline)
                        {
                                                 
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
            else
                this.OnlineTime = DateTime.Now;

        }

        private void OnClientFaulted(object sender, EventArgs e)
        {
            if(cf.State== CommunicationState.Faulted)
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
                    if (State == CommunicationState.Opened)
                        cf.Close();
                }
                this.IsOnline = false;
                if (this.Offline != null)
                    this.Offline(this, null);
                OnlineTime = DateTime.MinValue;
                cf = null;
            }


        }

        public IClientService Channel
        {
            get
            {
                if (cf == null)
                    return null;
                else
                    return cf.CreateChannel();             
               
            }
        }

        public ClientInfo(string ip, int port)
        {
            OnlineTime = DateTime.MinValue;
            this.Ip = ip;
            this.Port = port;
            TryLink();
        }
        public void SendMessage(MessageInfo message)
        {

            if (IsOnline)
            {
                try
                {
   
                    cf.CreateChannel().SendMessage(message);
                    SendMessageCount += 1;
                }
                catch (Exception ex)
                {
                    TurnOffLine();
                    Logger.Instance.LogDebug(ex.Message);
                }
            }

            
        }
        public bool RegServer(string ip, int port)
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
        public void ServiceShutOff(string ip)
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

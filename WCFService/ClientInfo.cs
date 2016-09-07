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

namespace UnitMonitorServer.Components.WCFService
{
    public class ClientInfo : IOnlineStatus

    {
        WebChannelFactory<IClientService> cf;
        /// <summary>
        /// 离线事件通知
        /// </summary>
        public event EventHandler Offline;
        /// <summary>
        /// 上线事件通知
        /// </summary>
        public event EventHandler Online;


        /// <summary>
        /// 客户端的IP地址
        /// </summary>
        public string Ip { private set; get; }
        /// <summary>
        /// 客户端端口
        /// </summary>
        public short Port { private set; get; }

        /// <summary>
        /// 客户端服务地址
        /// </summary>
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
        /// <summary>
        /// 上线时间
        /// </summary>
        public DateTime OnlineTime { set; get; } = DateTime.MinValue;

        /// <summary>
        /// 客户端是否在线
        /// </summary>
        public bool IsOnline { protected set; get; }
        /// <summary>
        /// 客户端服务的状态
        /// </summary>
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


        /// <summary>
        /// 尝试连接到客户端
        /// </summary>
        public void TryLink()
        {

            //即使客户端在线，也尝试连接一下，因为客户端如有意外退出不通知给服务器端
            if (this.IsOnline)
            {
                this.IsOnline = TestOnline();

                //如果尝试连接后还是没连接成功，发此客户端已下线事件
                if (!this.IsOnline)
                {
                    if (this.Offline != null)
                        this.Offline(this, null);

                }
            }
            //如果本来就是离线状态
            else
            {

                Uri baseAddress = new Uri(Url);
                try
                {
                    //创建客户端通道
                    cf = new WebChannelFactory<IClientService>(baseAddress);
                    cf.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(5);
                    cf.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(5);
                    cf.Faulted += OnClientFaulted;
                    //向客户端注册服务器信息
                    this.IsOnline = TestOnline();
                    if (this.IsOnline)
                    {

                        if (this.Online != null)
                            this.Online(this, null);
                    }

                }
                catch (Exception ex)
                {

                    string message = ex.Message;
                }


            }
            if (!this.IsOnline)
            {
                cf = null;
                this.OnlineTime = DateTime.MinValue;

            }
            else
                this.OnlineTime = DateTime.Now;

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

        public ClientInfo(string ip, short port)
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

                }
                catch
                {
                    TryLink();

                }
            }


        }
        /// <summary>
        /// 赂客户端注册服务器信息
        /// </summary>
        /// <param name="ip">服务器IP地址</param>
        /// <param name="port">服务器端口</param>
        /// <returns></returns>
        public bool RegServer(string ip, short port)
        {
            bool returnValue = false;
            if (cf != null)
            {
                try
                {

                    returnValue = cf.CreateChannel().RegService(ip, port.ToString());
                }
                catch
                {
                    TryLink();
                }

            }
            return returnValue;
        }
        /// <summary>
        /// 通知客户端 服务器端已关闭
        /// </summary>
        /// <param name="ip"></param>
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
        public bool TestOnline()
        {
            bool returnValue = false;
            try
            {

                returnValue = cf.CreateChannel().TestOnline();
            }
            catch
            {

            }
            return returnValue;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnitMonitorCommon;
using System.Windows.Forms;
using UnitMonitorCommunication;

namespace UnitMonitorServer.Components.WCFService
{
  public   class WCFServiceManager:ComponentBase
    {
        ToolStripMenuItem clientsMenu;

        public  static WCFServiceManager Instance { private set; get; }
        public  Clients Clients {private  set; get; }
        public  string ServiceName {private  set; get; }
        public  ServerCommunication ServerCommunication {private  set; get; }
        public bool ServiceReady { private set; get; } = false;

        public WCFServiceManager()
        {
            Instance = this;
        }
        public override void Init()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.ConfigFile);
            //加载客户端
            XmlNodeList clientList = doc.GetElementsByTagName("clients")[0].ChildNodes;
            Clients = new Clients();
            foreach (XmlNode item in clientList)
            {
                ClientInfo client = new ClientInfo(item.Attributes["ip"].Value, Convert.ToInt16(item.Attributes["port"].Value));
                Clients.Add(client);
                client.Online += Clients.OnClientChanged;
                client.Offline += Clients.OnClientChanged;
            }
            //加载服务
            XmlNode node = doc.GetElementsByTagName("serverCommunication")[0];
            ServiceName = node.Attributes["serviceName"].Value;
            short port = Convert.ToInt16(node.Attributes["port"].Value);
            if (ServerCommunication==null)
                ServerCommunication = new ServerCommunication(port);
            //当应用程序直接退出时，停止服务并通知客户端
            Application.ApplicationExit += OnApplicationExit;
            ServerCommunication.ServiceStateChanged += OnServiceStateChanged;
            //启动服务
            ServerCommunication.StartService();

            //挂钩事件处理
            foreach (TaskBase item in TasksContainer.Instance)
            {
                item.SendMessage += Clients.SendMessage;
            }
            TasksContainer.Instance.TaskAdded += Clients.OnTaskAdded;
            TasksContainer.Instance.TaskRemoved += Clients.OnTaskRemoved;
        }
        private void OnServiceStateChanged(object sender, EventArgs e)
        {
            if (ServerCommunication.SvcHost == null)
                return;
            switch (ServerCommunication.SvcHost.State)
            {
                case System.ServiceModel.CommunicationState.Opened:
                    ServiceReady = true;
                    //通知客户端通讯服务已启动
                    Clients.RegServer();
                    //向客户端发送消息通讯服务已启动
                    SendServiceMessage("通讯服务已启动");
                    break;
                case System.ServiceModel.CommunicationState.Faulted:
                    ServiceReady = false;
                    //向客户端发送消息通讯服务出错正在关闭
                    SendServiceMessage("通讯服务遇到了一个严重问题正在关闭");
                    ServerCommunication.SvcHost.Abort();
                    break;
                case System.ServiceModel.CommunicationState.Closed:
                    ServiceReady = false;
                    //向客户端发送消息通讯服务已关闭
                    SendServiceMessage("通讯服务已关闭");
                    //通知客户端通讯服务已关闭
                    Clients.ServerShutOff();
                    break;
                case System.ServiceModel.CommunicationState.Closing:
                    ServiceReady = false;
                    break;
            }
        }
        public override void UnLoad()
        {
            if (clientsMenu != null)
            {
                this.Container.CompMenu.DropDownItems.Remove(clientsMenu);
            }
            CloseService();
        }
        private void CloseService()
        {
            SendServiceMessage("通迅服务即将关闭");
            Clients.ServerShutOff();
            foreach (TaskBase item in TasksContainer.Instance)
            {
                item.SendMessage -= Clients.SendMessage;
            }
            TasksContainer.Instance.TaskAdded -= Clients.OnTaskAdded;
            TasksContainer.Instance.TaskRemoved -= Clients.OnTaskRemoved;
            ServerCommunication.StopService();
        }

        internal void OnApplicationExit(object sender, EventArgs e)
        {
            CloseService();
        }
        public override void SetMenu(ToolStripMenuItem compMenu)
        {
            if (clientsMenu == null)
            {
                clientsMenu = new ToolStripMenuItem();
                clientsMenu.Text = "通讯客户端";
                clientsMenu.Click += new System.EventHandler(this.ShowClients);
                compMenu.DropDownItems.Add(clientsMenu);
            }

        }

        private void ShowClients(object sender, EventArgs e)
        {
            ClientsForm form = new ClientsForm();
            if (this.Container.MDIForm != null)
                form.MdiParent = this.Container.MDIForm;
            form.Show();
        }

        /// <summary>
        /// 向客户端发送有关服务的消息
        /// </summary>
        /// <param name="message"></param>
        private void SendServiceMessage(string message)
        {
            MessageInfo info = new MessageInfo();
            info.OccurTime = DateTime.Now;
            info.MessageType = MessageType.System;
            info.Message = message;
            info.TaskPath = "";
            info.SenderUrl = TasksContainer.LocalIP;
            info.TaskName = "";
            Clients.SendMessage(info);
        }
    }
}


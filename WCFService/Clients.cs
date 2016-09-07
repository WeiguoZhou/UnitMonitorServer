using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace UnitMonitorServer.Components.WCFService
{
    /// <summary>
    /// 保存所有客户端信息
    /// </summary>
    public class Clients : BindingList<ClientInfo>
    {
        /// <summary>
        /// 客户端在线状态更改后引发的事件
        /// </summary>
        public event EventHandler ClientChanged;


        public Clients()
        {

        }
        /// <summary>
        /// 注册客户端信息
        /// 当客户端向服务器端发送请求注册时调用
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool RegClient(string ip, short port)
        {
            ClientInfo client = FindClient(ip);
            if (client == null)
            {
                client = new ClientInfo(ip, port);
                this.Add(client);
                client.Online += OnClientChanged;
                client.Offline += OnClientChanged;

                //如果没找到表示配置中也没有，添加此地址后保存配置
                this.SaveClient(client);
            }
            else
            {
                if (client.Port != port)
                {
                    this.Remove(client);
                    this.DelClient(ip);
                    this.SaveClient(client);
                    RegClient(ip, port);
                }

            }

            return true;
        }
        /// <summary>
        /// 当ClientInfo在线状态变更时调用，以引发事件和更新视图中的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnClientChanged(object sender, EventArgs e)
        {
            if (ClientChanged != null)
            {
                ClientChanged(sender, e);
            }
            int index = this.IndexOf((ClientInfo)sender);
            ListChangedEventArgs args = new ListChangedEventArgs(ListChangedType.Reset, index);
            this.OnListChanged(args);
        }


        /// <summary>
        /// 删除客户端配置信息
        /// </summary>
        /// <param name="url">客户端地址</param>
        public void DelClient(string ip)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(WCFServiceManager.Instance.ConfigFile);
            XmlNode node = doc.SelectSingleNode(string.Format("\\configuration\\componentSettings\\wcfService\\clients\\client[@ip={0}", ip));
            if (node == null)
            {
                doc.RemoveChild(node);
                doc.Save(WCFServiceManager.Instance.ConfigFile);
            }
        }
        /// <summary>
        /// 当任务启动时挂钩任务的SendMessage事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnTaskAdded(object sender, EventArgs e)
        {
            TaskBase task = (TaskBase)sender;
            task.SendMessage += SendMessage;
        }
        /// <summary>
        /// 当任务停止时解除挂钩任务的SendMessage事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnTaskRemoved(object sender, EventArgs e)
        {
            TaskBase task = (TaskBase)sender;
            task.SendMessage -= SendMessage;
        }
        /// <summary>
        /// 根据IP查找客户端
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public ClientInfo FindClient(string ip)
        {
            foreach (ClientInfo item in this)
            {
                if (item.Ip == ip)
                    return item;
            }
            return null;
        }

        /// <summary>
        /// 保存客户端信息到配置文件
        /// </summary>
        /// <param name="inf"></param>
        private void SaveClient(ClientInfo inf)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(WCFServiceManager.Instance.ConfigFile);
            XmlNode node = doc.SelectSingleNode(string.Format("\\configuration\\clients\\client[@ip={0}", inf.Ip));
            if (node == null)
            {

                XmlNode parentNode = doc.SelectSingleNode("\\configuration\\clients");
                XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "client", "");
                XmlAttribute newAttrIp = doc.CreateAttribute("ip");
                newAttrIp.Value = inf.Ip;
                newNode.Attributes.Append(newAttrIp);
                XmlAttribute newAttrPort = doc.CreateAttribute("port");
                newAttrPort.Value = inf.Port.ToString();
                newNode.Attributes.Append(newAttrPort);

                parentNode.AppendChild(newNode);
                doc.Save(WCFServiceManager.Instance.ConfigFile);
            }
            else
            {
                if (inf.Port.ToString() != node.Attributes["port"].Value)
                {
                    node.Attributes["port"].Value = inf.Port.ToString();
                    doc.Save(WCFServiceManager.Instance.ConfigFile);
                }

            }
        }

        public void SendMessage( MessageInfo message)
        {

            Task.Run(() =>
            {
                foreach (ClientInfo item in this)
                {
                    item.SendMessage(message);
                }
            });

        }
        /// <summary>
        /// 服务启动后向各客户端通知服务已启动
        /// </summary>
        public void RegServer()
        {
            string Ip = WCFServiceManager.Instance.ServerCommunication.Ip;
            short port = WCFServiceManager.Instance.ServerCommunication.Port;
            if (!string.IsNullOrEmpty(Ip))
            {
                Task.Run(() =>
                {
                    foreach (ClientInfo item in WCFServiceManager.Instance.Clients)
                    {
                        item.RegServer(Ip, port);
                    }

                });
            }

        }
        /// <summary>
        /// 服务停止后向各客户端通知服务已停止
        /// </summary>
        public void ServerShutOff()
        {
            string Ip = WCFServiceManager.Instance.ServerCommunication.Ip;
            Task.Run(() =>
            {

                foreach (ClientInfo item in WCFServiceManager.Instance.Clients)
                {
                    item.ServiceShutOff(Ip);
                }
            });
        }

    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace UnitMonitorServer
{
    /// <summary>
    /// 保存所有客户端信息
    /// </summary>
    public class Clients : BindingList<ClientInfo>
    {

        public event EventHandler ClientChanged;
        private static Clients instance;
        public static Clients Instance
        {
            get
            {
                return instance;
            }

        }
        public static void Init()
        {
            if (instance == null)
                instance = Clients.LoadClients();
            MessageCenter.Init();
            MessageCenter.Instance.SendMessageEvent += instance.SendMessage;
        }
        private Clients()
        {

        }

        public bool RegClient(string ip, int port)
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
                    client.Port = port;
                    this.DelClient(ip);
                    this.SaveClient(client);
                }

            }
            return true;
        }
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

        private static string ConfigFilePath
        {
            get
            {
                return Environment.CurrentDirectory + "\\clients.config";
            }
        }
        /// <summary>
        /// 删除客户端配置信息
        /// </summary>
        /// <param name="url">客户端地址</param>
        public void DelClient(string ip)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigFilePath);
            XmlNode node = doc.SelectSingleNode(string.Format("\\configuration\\clients\\client[@ip={0}", ip));
            if (node == null)
            {
                doc.RemoveChild(node);
                doc.Save(ConfigFilePath);
            }
        }
        public ClientInfo FindClient(string ip)
        {
            foreach (ClientInfo item in this)
            {
                if (item.Ip == ip)
                    return item;
            }
            return null;
        }

        private static Clients LoadClients()
        {
            Clients clients = new Clients();
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigFilePath);
            foreach (XmlNode item in doc.GetElementsByTagName("client"))
            {
                ClientInfo inf = new ClientInfo(item.Attributes["ip"].Value, Convert.ToInt32(item.Attributes["port"].Value));
                clients.Add(inf);
                inf.Online += clients.OnClientChanged;
                inf.Offline += clients.OnClientChanged;
            }
            return clients;

        }
        private void SaveClient(ClientInfo inf)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigFilePath);
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
                doc.Save(ConfigFilePath);
            }
            else
            {
                if (inf.Port.ToString() != node.Attributes["port"].Value)
                {
                    node.Attributes["port"].Value = inf.Port.ToString();
                    doc.Save(ConfigFilePath);
                }

            }
        }
        public void ServerShutOff()
        {
            Task.Run(() =>
            {
                string ip = ServerCommunication.Ip;
                foreach (ClientInfo item in this)
                {
                    item.ServiceShutOff(ip);
                }
            });
        }
        public void SendMessage(MessageInfo message)
        {

            Task.Run(() =>
            {
                foreach (ClientInfo item in this)
                {
                    item.SendMessage(message);
                }
            });

        }
    }
}


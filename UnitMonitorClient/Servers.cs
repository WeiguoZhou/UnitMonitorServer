using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UnitMonitorClient
{
    class Servers : BindingList<ServerInfo>
    {
        public event EventHandler ServerChanged;
        private static Servers instance;
        public static Servers Instance()
        {
            if (instance == null)
                instance = Servers.LoadServers();
            return instance;
        }
        private Servers()
        {

        }

        public bool RegServer(string ip, int port)
        {
            ServerInfo server = FindServer(ip);
            if (server == null)
            {
                server = new ServerInfo(ip, port);
                this.Add(server);
                server.Offline += this.OnServerChanged;
                server.Online += this.OnServerChanged;
                //如果没找到表示配置中也没有，添加此地址后保存配置
                this.SaveServer(server);
            }
            else
            {
                if (server.Port != port)
                {
                    server.Port = port;
                    this.DelServer(ip);
                    this.SaveServer(server);
                }
                server.TryLink();
            }
            if (server.IsOnline)
                return true;
            return false;

        }
        public void OnServerChanged(object sender, EventArgs e)
        {
            if (ServerChanged != null)
            {
                ServerChanged(sender, e);
            }
            int index = this.IndexOf((ServerInfo)sender);
            ListChangedEventArgs args = new ListChangedEventArgs(ListChangedType.Reset, index);
            this.OnListChanged(args);
        }


        /// <summary>
        /// 删除客户端配置信息
        /// </summary>
        /// <param name="url">客户端地址</param>
        public void DelServer(string ip)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigFilePath);
            XmlNode node = doc.SelectSingleNode(string.Format("\\configuration\\servers\\server[@ip={0}", ip));
            if (node == null)
            {
                doc.RemoveChild(node);
                doc.Save(ConfigFilePath);
            }
        }
        public ServerInfo FindServer(string ip)
        {
            foreach (ServerInfo item in this)
            {
                if (item.Ip == ip)
                    return item;
            }
            return null;
        }
        private static string ConfigFilePath
        {
            get
            {
                return Environment.CurrentDirectory + "\\servers.config";
            }
        }
        private static Servers LoadServers()
        {
            Servers servers = new Servers();
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigFilePath);
            foreach (XmlNode item in doc.GetElementsByTagName("server"))
            {
                ServerInfo inf = new ServerInfo(item.Attributes["ip"].Value, Convert.ToInt32(item.Attributes["port"].Value));
                servers.Add(inf);
                inf.Offline += servers.OnServerChanged;
                inf.Online += servers.OnServerChanged;
            }
            return servers;

        }
        private void SaveServer(ServerInfo inf)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigFilePath);
            XmlNode node = doc.SelectSingleNode(string.Format("\\configuration\\servers\\server[@ip={0}", inf.Ip));
            if (node == null)
            {

                XmlNode parentNode = doc.SelectSingleNode("\\configuration\\servers");
                XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "server", "");
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
        public void ServerTurnOff(string ip)
        {
            ServerInfo info = this.FindServer(ip);
            if (info != null)
            {
                info.TurnOffLine();

            }

        }

    }
 
}

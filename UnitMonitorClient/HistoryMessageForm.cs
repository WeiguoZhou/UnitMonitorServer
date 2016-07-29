using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitMonitorCommunication;

namespace UnitMonitorClient
{
    public partial class HistoryMessageForm : Form
    {
        private string selectedMessageType = "系统,消息,报警,警告,危险,考核";
        private List<MessageInfo> dataList;
        public HistoryMessageForm()
        {
            InitializeComponent();
            dataList = new List<MessageInfo>();
        }

        private void HistoryMessageForm_Load(object sender, EventArgs e)
        {
            cbOnlineServers.Items.Clear();
            foreach (var item in Servers.Instance.OnlineServers())
            {
                cbOnlineServers.Items.Add(item.Ip);

            }

        }

        private void cbOnlineServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbHistoryFile.Items.Clear();
            if (cbOnlineServers.SelectedItem != null)
            {
                string ip = (string)cbOnlineServers.SelectedItem;
                if (!string.IsNullOrEmpty(ip))
                {
                    ServerInfo server = Servers.Instance.FindServer(ip);
                    if (server != null)
                    {

                        string[] messageFiles = server.HistoryMessageFiles();
                        if ((messageFiles != null) && (messageFiles.Length > 0))
                        {
                            foreach (var item in messageFiles)
                            {
                                cbHistoryFile.Items.Add(item);
                            }
                        }
                    }

                }
            }
        }
        /// <summary>
        /// 根据选择的服务器和选定的历史消息文件名读取历史消息并填充数据表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewMessages_Click(object sender, EventArgs e)
        {
            dataList.Clear();
            dgvMessages.DataSource = null;
            if (cbOnlineServers.SelectedItem == null)
                return;
            if (cbHistoryFile.SelectedItem == null)
                return;


            ServerInfo server = Servers.Instance.FindServer((string)cbOnlineServers.SelectedItem);
            if ((server == null) || (!server.IsOnline))
            {
                MessageBox.Show("服务器未找到或服务器不在线");
                return;
            }
            Stream stream = server.HistoryMessage((string)cbHistoryFile.SelectedItem);
            if (stream == null)
                return;
            try
            {
                using (StreamReader sr = new StreamReader(stream))
                {

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] messageItem = line.Split(new char[] { ',' });
                        string messageType = ClientMessageCenter.MessageTypeString((MessageType)Enum.Parse(typeof(MessageType), messageItem[1]));
                        MessageInfo inf = new MessageInfo();
                        inf.OccurTime = Convert.ToDateTime(messageItem[0]);
                        inf.MessageType = (MessageType)Enum.Parse(typeof(MessageType), messageItem[1]);
                        inf.Message = messageItem[2].Replace("&comma;", ",");
                        inf.SenderUrl = messageItem[3];
                        inf.TaskPath = messageItem[4];
                        dataList.Add(inf);
                    }

                }

            }
            catch
            {

                MessageBox.Show("读取历史消息时出错，文档格式不正确,内容未完全显示！");
            }
            DataFilter();
        }
        private void DataFilter()
        {
            if ((!string.IsNullOrEmpty(selectedMessageType)) && (dataList != null))
            {
                var query = from data in dataList
                            where selectedMessageType.Contains(ClientMessageCenter.MessageTypeString(data.MessageType))
                            select data;
                if (query != null)
                {
                    messageInfoBindingSource.DataSource = query.AsEnumerable<MessageInfo>().ToList();
                }

            }
            else
                messageInfoBindingSource.DataSource = dataList;


        }

        private void btnSelectMessageType_Click(object sender, EventArgs e)
        {
            MessageTypeSelectorDialog frm = new MessageTypeSelectorDialog(selectedMessageType);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                selectedMessageType = frm.SelectedMessageType;
            }
            DataFilter();
        }
    }
}


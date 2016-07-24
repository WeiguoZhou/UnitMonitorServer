using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using UnitMonitorCommunication;

namespace UnitMonitorClient
{
    public partial class ServersForm : Form
    {
        public ServersForm()
        {
            InitializeComponent();
        }



        private void ServersForm_Load(object sender, EventArgs e)
        {
            serversBindingSource.DataSource = Servers.Instance();

        }



        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dgvServers.SelectedRows.Count > 0)
            {
                if (dgvServers.SelectedRows[0].Index == dgvServers.NewRowIndex)
                {
                    e.Cancel = true;
                    return;
                }

                toolDel.Enabled = true;
                ServerInfo info = Servers.Instance()[dgvServers.SelectedRows[0].Index];


                if (info.IsOnline)
                {
                    toolLink.Enabled = false;
                    toolCancelLink.Enabled = true;
                    toolTasks.Enabled = true;
                }
                else
                {
                    toolLink.Enabled = true;
                    toolCancelLink.Enabled = false;
                    toolTasks.Enabled = false;
                }
            }


        }



        private void toolDel_Click(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows[0].Index == dgvServers.NewRowIndex)
                return;
            ServerInfo info = Servers.Instance()[dgvServers.SelectedRows[0].Index];
            info.TurnOffLine();
            Servers.Instance().Remove(info);
            Servers.Instance().DelServer(info.Ip);
        }

        private void toolNew_Click(object sender, EventArgs e)
        {


        }

        private void toolCancelLink_Click(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows[0].Index == dgvServers.NewRowIndex)
                return;
            ServerInfo info = Servers.Instance()[dgvServers.SelectedRows[0].Index];
            info.TurnOffLine();
        }

        private void toolTasks_Click(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count > 0)
            {
                try
                {
                    ServerInfo info = Servers.Instance()[dgvServers.SelectedRows[0].Index];
                    if ((info != null) && (info.Channel != null))
                    {
                        TaskInfo[] tasks = info.Channel.Tasks();
                        TasksForm frm = new TasksForm(tasks);
                        frm.ShowDialog();

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(string.Format("打开任务列表时错误，错误消息{10}", ex.Message));
                }


            }
        }

        private void toolLink_Click(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count > 0)
            {
                try
                {
                    ServerInfo info = Servers.Instance()[dgvServers.SelectedRows[0].Index];
                    if (info != null)
                    {
                        info.TryLink();
                        if (info.IsOnline)
                            MessageBox.Show("连接成功");
                        else
                            MessageBox.Show("连接失败");

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(string.Format("尝试连接到服务器时失败，错误消息：{0}", ex.Message));
                }

            }
        }
    }

}


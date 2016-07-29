using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitMonitorCommunication;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Threading;

namespace UnitMonitorClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ClientMessageCenter.Instance.RecievedSubscribedMessage += OnRecievedSubscribedMessage;
           
   

        }

        private void OnRecievedSubscribedMessage(MessageInfo info)
        {
            if (this.InvokeRequired)
                this.Invoke(new MessageEventHandler(AddMessage), info);
            else
                AddMessage(info);
        }
        private void AddMessage(MessageInfo info)
        {
            int index = dgvMessages.Rows.Add();
            DataGridViewRow row = dgvMessages.Rows[index];
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            switch (info.MessageType)
            {
                case MessageType.Danger:
                    style.BackColor = Color.Red;
                    break;
                case MessageType.Warn:
                    style.BackColor = Color.OrangeRed;
                    break;
                case MessageType.Alarm:
                    style.BackColor = Color.Orange;
                    break;
                default:
                    style.BackColor = Color.White;
                    break;
            }
            row.DefaultCellStyle = style;
            row.Cells[0].Value = DateTime.Now.ToString();
            row.Cells[1].Value = ClientMessageCenter.MessageTypeString(info.MessageType);
            row.Cells[2].Value = info.Message;
            row.Cells[3].Value = info.SenderUrl;
            row.Cells[4].Value = info.TaskPath;
            row.Selected = true;

            notifyIcon1.BalloonTipTitle= ClientMessageCenter.MessageTypeString(info.MessageType);
            notifyIcon1.BalloonTipText = info.Message;
            notifyIcon1.ShowBalloonTip(3000000);

        }

        private void toolLinkedServers_Click(object sender, EventArgs e)
        {
            ServersForm frm = new ServersForm();
           
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;

        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            ClientCommunication.Instance.StopService();
            Application.Exit();
        }

        private void toolDelMessage_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvMessages.SelectedRows)
            {
                dgvMessages.Rows.Remove(item);
            }
        }

        private void toolClearMessages_Click(object sender, EventArgs e)
        {
            dgvMessages.Rows.Clear();
        }

 

        private void toolDelSelected_Click(object sender, EventArgs e)
        {
            if (dgvMessages.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvMessages.SelectedRows)
                {
                    dgvMessages.Rows.Remove(row);
                }
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void toolHistoryMessages_Click(object sender, EventArgs e)
        {
            HistoryMessageForm frm = new HistoryMessageForm();
            frm.ShowDialog();
        }

        private void toolTasks_Click(object sender, EventArgs e)
        {
            TasksForm frm = new TasksForm();
            frm.ShowDialog();
        }
    }
}

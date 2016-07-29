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

namespace UnitMonitorClient
{
    public partial class TasksForm : Form
    {
        List<TaskInfo> tasks=null;
        string filterString = "";
        public TasksForm()
        {
            
            InitializeComponent();
        }

        private void TasksForm_Load(object sender, EventArgs e)
        {
            cbOnlineServers.Items.Clear();
            foreach (var item in Servers.Instance.OnlineServers())
            {
                cbOnlineServers.Items.Add(item.Ip);

            }
            cbAllTaskSelector.SelectedIndex = 0;
        }
        private void DataFilter()
        {
            if ((!string.IsNullOrEmpty(filterString)) && (tasks!= null))
            {
                var query = from data in tasks
                            where data.Name.Contains(filterString)
                            select data;
                if (query != null)
                {
                    taskInfoBindingSource.DataSource = query.AsEnumerable<TaskInfo>().ToList();
                }
            }
            else
                taskInfoBindingSource.DataSource = tasks;

        }
        private void btnViewData_Click(object sender, EventArgs e)
        {
            if (cbOnlineServers.SelectedItem == null)
                return;
            ServerInfo server = Servers.Instance.FindServer((string)cbOnlineServers.SelectedItem);
            if ((server == null) || (!server.IsOnline))
            {
                MessageBox.Show("服务器未找到或服务器不在线");
                return;
            }
            if (cbAllTaskSelector.SelectedIndex == 1)
                tasks = server.RunningTasks();
            else
                tasks = server.AllTasks();
            DataFilter();
        }

        private void btnDataFilter_Click(object sender, EventArgs e)
        {
            MessageBox.Show("尚未实现");
        }
    }
}

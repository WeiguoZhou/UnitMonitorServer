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
using UnitMonitorCommon;
namespace UnitMonitorServer
{
    public partial class TaskContainerForm : Form
    {
        private delegate void UpdatetData();
        public TaskContainerForm()
        {
            InitializeComponent();

        }

        private void TaskContainerForm_Load(object sender, EventArgs e)
        {
            tasksContainerBindingSource.DataSource = TasksContainer.Instance;
            PaintTreeView();
            RefreshToolButton();
            TasksContainer.Instance.StopRun += OnTaskContainerStateChanged;
            TasksContainer.Instance.BeginRun += OnTaskContainerStateChanged;
            _UpdateStatus();
            TasksContainer.Instance.RunComplete += UpdateStatus;

        }

        private void UpdateStatus(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdatetData(_UpdateStatus));
            else
                _UpdateStatus();


        }
        private void _UpdateStatus()
        {

            StatusTasksContainer.Text = string.Format("状态：运行次数{0}，上次花费时间{1}毫秒", TasksContainer.Instance.RunCount, TasksContainer.Instance.LastSpendTime);
        }
        private void OnTaskContainerStateChanged(object sender,EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdatetData(RefreshToolButton));
            else
                RefreshToolButton();
        }
        private void RefreshToolButton()
        {
            TasksContainer instance = TasksContainer.Instance;
            switch (TasksContainer.Instance.Mode)
            {
                case DataMode.RealTime:
                    cbMode.SelectedItem = "实时模式";
                    break;
                case DataMode.History:
                    cbMode.SelectedItem = "历史模式";
                    break;
                case DataMode.Debug:
                    cbMode.SelectedItem = "调试模式";
                    break;
            }

            cbPeriod.SelectedItem = TasksContainer.Instance.Period.ToString();
            if (instance.IsRunning)
            {
                btnRun.Enabled = false;
                btnStop.Enabled = true;
                cbMode.Enabled = false;
                cbPeriod.Enabled = false;
                btnHistorySetting.Enabled = false;
                if (instance.Mode == DataMode.Debug)
                    btnStep.Enabled = true;

            }
            else
            {
                btnRun.Enabled = true;
                btnStop.Enabled = false;
                btnStep.Enabled = false;
                cbMode.Enabled = true;
                cbPeriod.Enabled = true;
                if (TasksContainer.Instance.Mode == DataMode.History)
                    btnHistorySetting.Enabled = true;
                else
                    btnHistorySetting.Enabled = false;
            }

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (TasksContainer.Instance.Mode == DataMode.History)
            {
                if ((TasksContainer.Instance.BeginTime == DateTime.MinValue) || (TasksContainer.Instance.HistoryEndTime == DateTime.MinValue))
                {
                    MessageBox.Show("必须先设定历史模式的开始时间和结束时间后才能再启动任务！");
                    return;
                }
            }

            TasksContainer.Instance.Start();
            RefreshToolButton();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            TasksContainer.Instance.StepRun();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (TasksContainer.Instance.IsRunning)
                TasksContainer.Instance.Stop();
            RefreshToolButton();
        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!TasksContainer.Instance.IsRunning)
            {
                switch (cbMode.SelectedItem.ToString())
                {
                    case "实时模式":
                        TasksContainer.Instance.Mode = DataMode.RealTime;
                        break;
                    case "历史模式":
                        TasksContainer.Instance.Mode = DataMode.History;
                        break;
                    case "调试模式":
                        TasksContainer.Instance.Mode = DataMode.Debug;
                        break;
                }
            }
        }

        private void cbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!TasksContainer.Instance.IsRunning)
            {
                TasksContainer.Instance.Period = Convert.ToInt32(cbPeriod.SelectedItem);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                toolDebugData.Enabled = true;
                toolStopSelectedTask.Enabled = true;
            }

            else
            {
                toolDebugData.Enabled = false;
                toolStopSelectedTask.Enabled = false;
            }

            //if (TasksContainer.Instance.IsRunning && (TasksContainer.Instance.Mode == DataMode.Debug) && (dgvTasks.SelectedRows.Count>0))
            //    toolDebugData.Enabled = true;
            //else
            //    toolDebugData.Enabled = false;
        }

        private void toolDebugData_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                TaskBase task = TasksContainer.Instance[dgvTasks.SelectedRows[0].Index];
                if (task != null)
                {
                    DebugDataForm form = new DebugDataForm(task);
                    form.ShowDialog();
                }

            }

        }

        private void PaintTreeView()
        {
            treeView1.Nodes.Clear();
            TreeNode node = treeView1.Nodes.Add("全部");
            string fullPath = Directory.GetCurrentDirectory() + "\\" + "Tasks";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            if (GetMutiNode(node, fullPath))
                node.SelectedImageIndex = 1;
            else

                node.SelectedImageIndex = 0;
        }
        private bool GetMutiNode(TreeNode treeNode, string fullPath)
        {
            if (!Directory.Exists(fullPath))
                return false;
            DirectoryInfo dir = new DirectoryInfo(fullPath);
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();
            foreach (DirectoryInfo item in dirs)
            {
                TreeNode subNode = treeNode.Nodes.Add(item.Name);
                string pathNode = fullPath + "\\" + item.Name;

                if (GetMutiNode(subNode, pathNode))

                    subNode.SelectedImageIndex = 1;
                else

                    subNode.SelectedImageIndex = 0;
            }
            foreach (FileInfo item in files)
            {
                if (item.Extension.ToLower().Contains("config"))
                {
                    TreeNode itemNode = treeNode.Nodes.Add(item.Name);
                    itemNode.SelectedImageIndex = 2;
                    itemNode.Tag = item;
                   
                    if (TasksContainer.Instance.FindTask(item.FullName) != null)
                        itemNode.Checked = true;
                }


            }

            return ((dirs.Count() > 0) || (files.Count() > 0));
        }
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        private void node_AfterCheck(object sender, TreeViewEventArgs e)
        {

            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        private void toolStartSelectedTask_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
                StartCheckedNodes(treeView1.Nodes[0]);
        }
        private void StartCheckedNodes(TreeNode treeNode)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Checked && node.Tag != null)
                {
                    FileInfo inf = (FileInfo)node.Tag;
                   
                    if (TasksContainer.Instance.FindTask(inf.FullName) == null)
                    {
                        TaskBase task = TasksContainer.LoadTask(inf);
                        TasksContainer.Instance.AddTask(task);

                    }

                }
                StartCheckedNodes(node);
            }

        }

        private void toolStopSelectedTask_Click(object sender, EventArgs e)
        {
            while (dgvTasks.SelectedRows.Count > 0)
            {
                TasksContainer.Instance.RemoveAt(dgvTasks.SelectedRows[0].Index);
            }
        }
    }

}

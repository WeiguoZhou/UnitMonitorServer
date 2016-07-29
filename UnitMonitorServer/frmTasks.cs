using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using UnitMonitorCommon;

namespace UnitMonitorServer
{
    public partial class frmTasks : Form
    {
        public frmTasks()
        {
            InitializeComponent();
        }

       private void PaintTreeView()
        {
            treeView1.Nodes.Clear();
           TreeNode node= treeView1.Nodes.Add("全部");
            string fullPath = Environment.CurrentDirectory + "\\" + "Tasks";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            if (GetMutiNode(node, fullPath))
                node.SelectedImageIndex = 1;
            else

                node.SelectedImageIndex = 0;
        }
        private bool GetMutiNode(TreeNode treeNode,string fullPath)
        {
            if (!Directory.Exists(fullPath))
                return false;
            DirectoryInfo dir = new DirectoryInfo(fullPath);
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();
            foreach (DirectoryInfo item in dirs)
            {
                TreeNode subNode=treeNode.Nodes.Add(item.Name);
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
                   TreeNode itemNode= treeNode.Nodes.Add(item.Name);
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
       
        private void frmTasks_Load(object sender, EventArgs e)
        {
            PaintTreeView();
            switch (TasksContainer.Instance.Mode)
            {
                case DataMode.RealTime:
                    toolDataMode.SelectedItem = "实时模式";
                    break;
                case DataMode.History:
                    toolDataMode.SelectedItem = "历史模式";
                    break;
                case DataMode.Debug:
                    toolDataMode.SelectedItem = "调试模式";
                    break;
            }
        }
    


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Tag == null)
                {
                    dgvPoints.Rows.Clear();
                    lvFuns.Clear();
                    lvParams.Items.Clear();
                    rtxtTaskModuleReadme.Text = "";
                    dgvPoints.Rows.Clear();
                }
                else
                {
                    FileInfo inf = (FileInfo)e.Node.Tag;
                    LoadTaskFunSettings(inf);
                    LoadTaskParamSettings(inf);
                    LoadTaskModuleReadme(inf);
                    LoadTaskPoints(inf);
                }
                   


            }
        }

   

        #region 绑定任务信息
        public  void LoadTaskModuleReadme(FileInfo configFile)
        {

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFile.FullName);

                XmlNode taskInfo = doc.DocumentElement.SelectSingleNode("taskinfo");
                if (taskInfo != null)
                {
                    Assembly assem = Assembly.Load(taskInfo.Attributes["assembly"].Value);
                    Type t = assem.GetType(taskInfo.Attributes["class"].Value);
                    string fileFullname = Directory.GetCurrentDirectory() + "\\ModuleSettings\\" + t.Name + ".rtf";
                     rtxtTaskModuleReadme.LoadFile(fileFullname);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("任务容器加载任务时出错。任务名称：{0}，错误消息{1}", configFile.Name, ex.Message);
                rtxtTaskModuleReadme.Text = message;

            }
        }
        public void LoadTaskPoints(FileInfo configFile)
        {
            dgvPoints.Rows.Clear();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFile.FullName);
                
                foreach (XmlNode point in doc.DocumentElement.SelectNodes("points")[0].ChildNodes)
                {
                    DataGridViewRow row = dgvPoints.Rows[dgvPoints.Rows.Add()];
                    row.Cells[0].Value = point.Attributes["name"].Value;
                    row.Cells[1].Value = point.Attributes["id"].Value;
                    row.Cells[2].Value = point.Attributes["type"].Value;
                    row.Cells[4].Value = point.Attributes["description"].Value;
                }
            }
            catch 
            {

            }
        }
        private void RefreshPointValue(object sender,EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPoints.Rows)
            {
              
            }

        }

        public void LoadTaskFunSettings(FileInfo configFile)
        {
            lvFuns.Items.Clear();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFile.FullName);
                foreach (XmlNode fun in doc.DocumentElement.SelectNodes("funs")[0].ChildNodes)
                {
                    ListViewItem item = new ListViewItem(fun.Attributes["name"].Value);
                    item.Checked = Convert.ToBoolean(fun.Attributes["value"].Value);
                    item.SubItems.Add(fun.Attributes["name"].Value);
                    item.SubItems.Add(fun.Attributes["description"].Value);
                    lvFuns.Items.Add(item);
                }
            }
            catch
            {

            }
        }
        public void LoadTaskParamSettings(FileInfo configFile)
        {
            lvParams.Items.Clear();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFile.FullName);
                foreach (XmlNode param in doc.DocumentElement.SelectNodes("params")[0].ChildNodes)
                {
                    ListViewItem item = new ListViewItem(param.Attributes["name"].Value);
                    item.SubItems.Add(param.Attributes["name"].Value);
                    item.SubItems.Add(param.Attributes["type"].Value);
                    item.SubItems.Add(param.Attributes["value"].Value);
                    item.SubItems.Add(param.Attributes["description"].Value);
                    lvParams.Items.Add(item);
                }
            }

            catch
            {

            }
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolPointValues_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {

        }
    }
}

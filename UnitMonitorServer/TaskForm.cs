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
    public partial class TaskForm : Form
    {
        private delegate void RefreshView();
        private RefreshView RefreshViewFun;
        Dictionary<string, string> taskParamDescription;
        Dictionary<string, string> funParamDescription;

        private TaskBase task;
        public TaskForm(TaskBase t)
        {
            InitializeComponent();
            task = t;
            taskParamDescription = ParamSetting.CustomTaskDescriptions(task);
            funParamDescription = ParamSetting.CustomFunDescriptions(task);
   
        }


     
       
        private void frmTasks_Load(object sender, EventArgs e)
        {
            LoadTaskModuleReadme(task.ConfigFileInfo);
            if ((task.FunGroups != null) && (task.FunGroups.Length > 0))
            {
                funGroupBindingSource.DataSource = task.FunGroups.ToList();
            }

            RefreshViews();
            RefreshViewFun = new RefreshView(RefreshViews);
            task.RunComplete += OnTaskRunComplete;

        }

        private void OnTaskRunComplete(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(RefreshViewFun);
            }
            else
            {
                RefreshViews();
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

    
        #endregion
       
        private void RefreshViews()
        {
            
        }
        private void RefreshTaskParamsView()
        {
            //将任务中的字段信息用键和值的形式在表中显示
            dgvTaskParams.Rows.Clear();
            AddTaskParamsRow("taskName", task.TaskName);
            AddTaskParamsRow("taskPath", task.TaskPath);
            AddTaskParamsRow("taskModuleName",task.GetType().FullName);
            AddTaskParamsRow("configFileInfo", task.ConfigFileInfo.FullName);
            AddTaskParamsRow("isRunning", task.IsRunning.ToString());
            AddTaskParamsRow("beginTime", task.BeginTime == DateTime.MinValue ? "": task.BeginTime.ToString());
            AddTaskParamsRow("period", task.Period.ToString());
            AddTaskParamsRow("runCount", task.RunCount.ToString());
            //显示自定义的参数信息
            Dictionary<string, object> customParams = task.CustomParams();
            if (customParams != null)
            {
                foreach (var item in customParams.Keys)
                {
                    AddTaskParamsRow(item, customParams[item].ToString());
                }
            }
        }
        private void RefreshPointsView()
        {
            pointBindingSource.DataSource = null;
            FunGroup fun=null;
            if (dgvFuns.SelectedRows.Count > 0)
                fun = ((List<FunGroup>)funGroupBindingSource.DataSource)[dgvFuns.SelectedRows[0].Index];
            if((fun!=null) && (fun.Points!=null))
                pointBindingSource.DataSource = fun.Points.ToList();
            

        }
        private void RefreshFunParamsView()
        {
            dgvFunParams.Rows.Clear();
            FunGroup fun = null;
            if (dgvFuns.SelectedRows.Count > 0)
                fun = ((List<FunGroup>)funGroupBindingSource.DataSource)[dgvFuns.SelectedRows[0].Index];
            if (fun != null)
            {
                AddFunParamsRow("index", fun.Index.ToString());
                AddFunParamsRow("description", fun.Description);
                AddFunParamsRow("used", fun.Used.ToString());
                AddFunParamsRow("allowUse", fun.AllowUse.ToString());
                AddFunParamsRow("allowUnUse", fun.AllowUnUse.ToString());
                if(fun.ParamSettings !=null)
                {
                    foreach (var item in fun.ParamSettings.Keys)
                    {
                        AddFunParamsRow(item, fun.ParamSettings[item]);
                    }
                }
            }
        }
        private void AddTaskParamsRow(string key ,string value)
        {
            DataGridViewRow row = dgvTaskParams.Rows[dgvTaskParams.Rows.Add()];
            row.Cells[0].Value = key;
            row.Cells[1].Value = value;
            if (taskParamDescription.ContainsKey(key))
                row.Cells[2].Value = taskParamDescription[key];
            else
                row.Cells[2].Value = "";
        }
        private void AddFunParamsRow(string key, string value)
        {
            DataGridViewRow row = dgvFunParams.Rows[dgvTaskParams.Rows.Add()];
            row.Cells[0].Value = key;
            row.Cells[1].Value = value;
            if (funParamDescription.ContainsKey(key))
                row.Cells[2].Value = funParamDescription[key];
            else
                row.Cells[2].Value = "";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolPointValues_Click(object sender, EventArgs e)
        {

        }

 

        private void tcTask_Selected(object sender, TabControlEventArgs e)
        {
            if(tcTask.SelectedTab==tabReadme)
            {
                if (rtxtTaskModuleReadme.Text == "")
                    LoadTaskModuleReadme(task.ConfigFileInfo);
            }

        }

        private void dgvFuns_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}

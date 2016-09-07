using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitMonitorCommon;

namespace UnitMonitorServer
{
    public partial class WelcomeForm : Form
    {
        private SetText setText;

        public WelcomeForm()
        {
            InitializeComponent();
            setText = new SetText(SetStatusText);
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            label4.Text = Application.ProductVersion;
            Program.SetStatusText += this.SetStatus;
            TasksContainer.Instance.TaskAdded += OnTaskContainerLoadTask;
        }
        public void SetStatus(string statusText)
        {
            if (this.InvokeRequired)
                this.Invoke(setText, statusText);
            else
                SetStatusText(statusText);

        }
        private void SetStatusText(string statusText)
        {
            label3.Text = statusText;
        }
        private  void OnTaskContainerLoadTask(object sender, EventArgs e)
        {
            TaskBase task = (TaskBase)sender;
            SetStatus(String.Format("正在加载任务：{0}", task.TaskName));
        }
    }
  
    public delegate void SetText(string text);
}

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
        TaskInfo[] _tasks;
        public TasksForm(TaskInfo[] tasks)
        {
            _tasks = tasks;
            InitializeComponent();
        }

        private void TasksForm_Load(object sender, EventArgs e)
        {
            if(_tasks !=null )
                dgvTasks.DataSource = _tasks;
        }
    }
}

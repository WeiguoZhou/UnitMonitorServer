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
    public partial class TaskTempValueForm : Form
    {
        private delegate void UpdatetData();
        TaskBase _task;
        public TaskTempValueForm(TaskBase task)
        {
            _task=task;
            InitializeComponent();
        }

        private void TaskTempValueForm_Load(object sender, EventArgs e)
        {
            FreshData();
            _task.RunComplete += AfterTaskProcessHander;
        }
        private  void AfterTaskProcessHander(TaskBase task)
        {
            if (dgvTempValues.InvokeRequired)
                dgvTempValues.Invoke(new UpdatetData(FreshData));
            else
                FreshData();
        }
        private void FreshData()
        {
            dgvTempValues.Rows.Clear();
            if (_task != null)
            {
                foreach (string item in _task.TempValue.Keys)
                {
                    DataGridViewRow row = dgvTempValues.Rows[dgvTempValues.Rows.Add()];
                    row.Cells[0].Value = item;
                    row.Cells[1].Value = _task.TempValue[item];
                }
            }
      
        }
    }
}

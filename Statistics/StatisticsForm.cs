using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitMonitorServer.Components.statistics
{
    public partial class StatisticsForm : Form
    {
        public StatisticsManager Manager { set; get; }
        public StatisticsForm(StatisticsManager manager)

        {
            Manager = manager;
            InitializeComponent();
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Manager.TaskStatisticsList;
        }
    }
}

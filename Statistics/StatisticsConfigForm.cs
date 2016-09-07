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
    public partial class StatisticsConfigForm : Form
    {
        public StatisticsManager Mananger { set; get; }
        public StatisticsConfigForm(StatisticsManager mananger)
        {
            InitializeComponent();
            Mananger = mananger;
        }

        private void StatisticsConfigForm_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Mananger.TaskStatisticsUsed;
            checkBox2.Checked = Mananger.TasksContainerStatisticsUsed;
        }
    }
}

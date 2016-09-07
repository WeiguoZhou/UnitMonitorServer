using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunicationBase;

namespace UnitMonitorCommon
{
    public partial class ComponentsForm : Form
    {
        public List<ComponentInfo> Components { set; get; }
        public ComponentsForm()
        {
            Components = ComponentContainer.Instance.LoadComponentInfos();
            InitializeComponent();

        }
        public ComponentsForm(List<ComponentInfo> componentsList)
        {
            Components = componentsList;
            InitializeComponent();

        }

        private void ComponentsForm_Load(object sender, EventArgs e)
        {
            componentInfoBindingSource.DataSource = Components;
    
            
        }

      
    }
}

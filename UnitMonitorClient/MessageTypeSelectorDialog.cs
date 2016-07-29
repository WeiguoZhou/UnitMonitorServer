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

namespace UnitMonitorClient
{
    public partial class MessageTypeSelectorDialog : Form
    {
        public string SelectedMessageType {  private set; get; }
        public MessageTypeSelectorDialog(string selectedMessageType)
        {
            InitializeComponent();
            SelectedMessageType = selectedMessageType;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

                for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                {
                    this.checkedListBox1.SetItemChecked(i, checkBox1.Checked);
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedMessageType = "";
            foreach (var item in checkedListBox1.SelectedItems)
            {
                if (SelectedMessageType == "")
                    SelectedMessageType = (string)item;
                else
                    SelectedMessageType += "," + (string)item;
            }
        }

        private void MessageTypeSelectorForm_Load(object sender, EventArgs e)
        {
            if(SelectedMessageType.Contains(MessageCenter.SysMessageType))
                checkedListBox1.SetItemChecked(0, true);
            if (SelectedMessageType.Contains(MessageCenter.InfoMessageType))
                checkedListBox1.SetItemChecked(1, true);
            if (SelectedMessageType.Contains(MessageCenter.AlarmMessageType))
                checkedListBox1.SetItemChecked(2, true);
            if (SelectedMessageType.Contains(MessageCenter.WarnMessageType))
                checkedListBox1.SetItemChecked(3, true);
            if (SelectedMessageType.Contains(MessageCenter.DangerMessageType))
                checkedListBox1.SetItemChecked(4, true);
            if (SelectedMessageType.Contains(MessageCenter.ExaminationMessageType))
                checkedListBox1.SetItemChecked(5, true);
        }
    }
}

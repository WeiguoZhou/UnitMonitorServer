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

        public const string InfoMessageType = "消息";
        public const string SysMessageType = "系统";
        public const string AlarmMessageType = "报警";
        public const string WarnMessageType = "警告";
        public const string DangerMessageType = "危险";
        public const string ExaminationMessageType = "考核";
        public const string UnknownMessageType = "未知";
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
            if(SelectedMessageType.Contains(SysMessageType))
                checkedListBox1.SetItemChecked(0, true);
            if (SelectedMessageType.Contains(InfoMessageType))
                checkedListBox1.SetItemChecked(1, true);
            if (SelectedMessageType.Contains(AlarmMessageType))
                checkedListBox1.SetItemChecked(2, true);
            if (SelectedMessageType.Contains(WarnMessageType))
                checkedListBox1.SetItemChecked(3, true);
            if (SelectedMessageType.Contains(DangerMessageType))
                checkedListBox1.SetItemChecked(4, true);
            if (SelectedMessageType.Contains(ExaminationMessageType))
                checkedListBox1.SetItemChecked(5, true);
        }
    }
}

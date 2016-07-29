using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using UnitMonitorCommon;
using System.IO;

namespace UnitMonitorServer
{

    public partial class DebugDataForm : Form
    {
        private delegate void UpdatetData();
        TaskBase task;
        public DebugDataForm(TaskBase t)
        {
            task = t;
            InitializeComponent();
            this.Text = string.Format("调试窗口({0})", task.TaskName);
        }

        private void DebugDataForm_Load(object sender, EventArgs e)
        {
                    dgvPoints.Rows.Clear();
            try
            {
                XmlDocument doc = task.Config;
                foreach (XmlNode point in doc.DocumentElement.SelectNodes("points")[0].ChildNodes)
                {
                    string[] values = new string[6];
                    values[0] = point.Attributes["name"].Value;
                    values[1] = point.Attributes["id"].Value;
                    values[2] = point.Attributes["type"].Value;
                    values[3] = task.AnalogValue(point.Attributes["name"].Value).ToString();
                    values[4] = task.FindPointByAlais(point.Attributes["name"].Value).Used.ToString();
                    values[5] = point.Attributes["description"].Value;
                    dgvPoints.Rows.Add(values);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("加载点表时出错，错误消息：{0}", ex.Message));
            }
            
            task.AfterInitData += UpdateView;
        }

        private void UpdateView(object sender,EventArgs e)
        {
            if (dgvPoints.InvokeRequired)
                dgvPoints.Invoke(new UpdatetData(UpdateDataView));
            else
                UpdateDataView();
        }
        private void UpdateDataView()
        {
            statusLabel.Text = string.Format("任务状态:运行数:{0}，上次用时{1}毫秒，连续成功：{2}，连续失败{3}", task.RunCount, task.LastSpendTime, task.PersistantSuccess, task.PersistantFail);
            foreach (DataGridViewRow row in dgvPoints.Rows)
            {
               
                if (row.Cells[0].Value!=null)
                {
                    string alias = row.Cells[0].Value.ToString();
                   UnitMonitorCommon.Point point = task.FindPointByAlais(alias);
                    if (point != null)
                        row.Cells[4].Value = point.Used.ToString();
                }
                   
            }
        }


        private void dgvPoints_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 3)
                return;
            DataGridViewRow row = dgvPoints.Rows[e.RowIndex];
            string value = row.Cells[3].Value.ToString();
            switch (row.Cells[2].Value.ToString())
            {
                case "int":
                    int intresult;
                    if (!int.TryParse(value, out intresult))
                        e.Cancel = true;
                    break;
                case "bool":case "boolean":
                    if(!(value=="1" || value=="0"))
                        e.Cancel = true;
                    break;
                case "double":
                    double doubleResult;
                    if (!double.TryParse(value, out doubleResult))
                        e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TasksContainer.Instance.StepRun();
        }

        private void toolTempValuesForm_Click(object sender, EventArgs e)
        {
            TaskTempValueForm frm = new TaskTempValueForm(task);
            frm.Show();
        }

        private void dgvPoints_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex != 3)
                return;
            DataGridViewRow row = dgvPoints.Rows[e.RowIndex];
            task.SetPointData(row.Cells[1].Value.ToString(), Convert.ToDouble(row.Cells[3].Value));
        }

        private void btnLoadValues_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string dir = Directory.GetCurrentDirectory() + "\\DebugData";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            dialog.InitialDirectory = dir;
            dialog.Filter = "points files (*.points)|*.points|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(file);
                    foreach (XmlNode item in doc.GetElementsByTagName("point"))
                    {
                        string id= item.Attributes["id"].Value;
                        double value= Convert.ToDouble(item.Attributes["value"].Value);
                        DataGridViewRow row = GetRowByPointId(id);
                        if (row != null)
                            row.Cells[3].Value = value;
                        task.SetPointData(id, value);
                    }
                }
                catch (Exception ex )
                {

                    MessageBox.Show(ex.Message);
                }

            }
        }
        private DataGridViewRow GetRowByPointId(string id)
        {
            foreach (DataGridViewRow row in dgvPoints.Rows)
            {
                if (row.Cells[1].Value.ToString() == id)
                    return row;
            }
            return null;
        }

        private void btnSaveValues_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<points></points>");
            XmlNode rootNode = doc.GetElementsByTagName("points")[0];
            foreach (DataGridViewRow row in dgvPoints.Rows)
            {
                string id = row.Cells[1].Value.ToString();
                string value = row.Cells[3].Value==null?"": row.Cells[3].Value.ToString();
                XmlNode node= doc.CreateNode(XmlNodeType.Element, "point", null);
                XmlAttribute attrId = doc.CreateAttribute("id");
                attrId.Value = id;
                node.Attributes.Append(attrId);
                XmlAttribute attrValue = doc.CreateAttribute("value");
                attrValue.Value= value;
                node.Attributes.Append(attrValue);
                rootNode.AppendChild(node);
            }
            string dir = Directory.GetCurrentDirectory() + "\\DebugData";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
 
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = dir;
            dialog.Filter= "points files (*.points)|*.points|All files (*.*)|*.*";
            dialog.FileName = task.TaskName;
            dialog.DefaultExt = "points";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                doc.Save(dialog.FileName);
            }
        }
    }
}

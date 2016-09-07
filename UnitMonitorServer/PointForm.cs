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
    public partial class PointForm : Form
    {
        private delegate void UpdatetData();
        UnitMonitorCommon.Point point;
        Dictionary<string, string> paramDescs;
        bool isDirty = false;
        //指示点的需要保存到文档中的参数是否已更改
       public bool IsDirty
        {
          private   set
            {

                toolSaveChange.Enabled = value;
                toolReLoadPoint.Enabled = value;
                isDirty = value;
            }
            get
            {
                return isDirty;
            }
        }
        public PointForm(UnitMonitorCommon.Point p)
        {
            point = p;
            InitializeComponent();
            point.FunGroup.Task.RunComplete += RefreshData;
        }

        private void RefreshData(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            { }

        }

        private void TaskTempValueForm_Load(object sender, EventArgs e)
        {
            paramDescs = ParamSetting.CustomPointDescriptions(point.FunGroup.Task);
        }
     private void UpdateTempValueView()
        {
            dgvTempValue.Rows.Clear();
            foreach (var item in point.TempValues.Keys)
            {
                DataGridViewRow row = dgvTempValue.Rows[dgvTempValue.Rows.Add()];
                row.Cells[0].Value = item;
                row.Cells[1].Value = point.TempValues[item];
            }
        }
        private void UpdateParamView()
        {
           
            dgvParams.Rows.Clear();
            DataGridViewRow row = dgvParams.Rows[dgvTempValue.Rows.Add()];
            row.Cells[0].Value = "id";
            row.Cells[1].Value = point.Id;
            Dictionary<string, string> paramDescs = ParamSetting.CustomPointDescriptions(point.FunGroup.Task);
            row.Cells[2].Value = GetParamDesc("id");
            row = dgvParams.Rows[dgvTempValue.Rows.Add()];
            row.Cells[0].Value = "value";
            row.Cells[1].Value = point.Value.HasValue? point.Value.Value.ToString() :"";
            row.Cells[2].Value = GetParamDesc("allowAlarm");
            row = dgvParams.Rows[dgvTempValue.Rows.Add()];
            row.Cells[0].Value = "allowAlarm";
            row.Cells[1].Value = point.AllowAlarm;
            row.Cells[2].Value = GetParamDesc("allowAlarm");

            row = dgvParams.Rows[dgvTempValue.Rows.Add()];
            row.Cells[0].Value = "index";
            row.Cells[1].Value = point.Index;
            row.Cells[2].Value = GetParamDesc("index");
            foreach (var item in point.ParamSettings.Keys)
            {
                row = dgvParams.Rows[dgvTempValue.Rows.Add()];
                row.Cells[0].Value = item;
                row.Cells[1].Value = point.ParamSettings[item];
                row.Cells[2].Value = GetParamDesc(item);
            }
        }
        private string GetParamDesc(string key)
        {
            return paramDescs.ContainsKey(key) ? paramDescs[key] : "";
        }

        private void dgvParams_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //仅第二列允许更改
            if (e.ColumnIndex != 1)
            {
                e.Cancel = true;
                return;
            }

            string key = dgvParams.Rows[e.RowIndex].Cells[0].Value.ToString();
            string value = e.FormattedValue.ToString();
            string errorText = "";
            switch (key)
            {
                case "id":
                    if (!value.StartsWith("CSDC."))
                    {
                        errorText = "这里必须输入edna点地址";
                        e.Cancel = true;
                    }

                    break;
                case "value":
                    if (!point.ValidateValue(value))
                    {
                        errorText = "输入的数据类型与设定的数据类型不符";
                        e.Cancel = true;
                    }

                    break;
                case "allowAlarm":
                    bool bValue;
                    if(!bool.TryParse(value,out bValue))
                    {
                        errorText = "这里必须输入布尔值(true/false)";
                        e.Cancel = true;
                    }

                    break;
                case "index":
                    errorText = "序号不允许更改";
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
            if (e.Cancel == true)
            {
                DataGridViewCell cell = dgvParams.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ErrorText = errorText;
            }
        }

        private void dgvParams_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dgvParams.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.ErrorText = string.Empty;
            string key = dgvParams.Rows[e.RowIndex].Cells[0].Value.ToString();
            object value = cell.Value;
            switch (key)
            {
                case "id":
                    if(point.Id != value.ToString())
                    {
                       point.Id = value.ToString();
                        IsDirty = true;
                    }

                    break;
                case "value":
                    point.Value = Convert.ToInt32(value);
                    break;
                case "allowAlarm":
                    if(point.AllowAlarm != Convert.ToBoolean(value))
                    {
                        point.AllowAlarm = Convert.ToBoolean(value);
                        IsDirty = true;
                    }

                    break;
                case "index":
                    break;
                default:
                    if(point.ParamSettings[key]!= value.ToString())
                    {
                        point.ParamSettings[key] = value.ToString();
                        IsDirty = true;
                    }

                    break;
            }
        }
    }
}

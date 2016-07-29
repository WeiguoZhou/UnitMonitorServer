namespace UnitMonitorServer
{
    partial class DebugDataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugDataForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolTempValuesForm = new System.Windows.Forms.ToolStripButton();
            this.dgvPoints = new System.Windows.Forms.DataGridView();
            this.clAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPointId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnLoadValues = new System.Windows.Forms.ToolStripButton();
            this.btnSaveValues = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(696, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolTempValuesForm,
            this.btnLoadValues,
            this.btnSaveValues});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(696, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "步进运行";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolTempValuesForm
            // 
            this.toolTempValuesForm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolTempValuesForm.Image = ((System.Drawing.Image)(resources.GetObject("toolTempValuesForm.Image")));
            this.toolTempValuesForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolTempValuesForm.Name = "toolTempValuesForm";
            this.toolTempValuesForm.Size = new System.Drawing.Size(23, 22);
            this.toolTempValuesForm.Text = "临时数据";
            this.toolTempValuesForm.Click += new System.EventHandler(this.toolTempValuesForm_Click);
            // 
            // dgvPoints
            // 
            this.dgvPoints.AllowUserToAddRows = false;
            this.dgvPoints.AllowUserToDeleteRows = false;
            this.dgvPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clAlias,
            this.clPointId,
            this.clDataType,
            this.clValue,
            this.clUsed,
            this.clDescription});
            this.dgvPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPoints.Location = new System.Drawing.Point(0, 49);
            this.dgvPoints.Name = "dgvPoints";
            this.dgvPoints.RowTemplate.Height = 23;
            this.dgvPoints.Size = new System.Drawing.Size(696, 374);
            this.dgvPoints.TabIndex = 2;
            this.dgvPoints.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPoints_RowValidated);
            this.dgvPoints.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvPoints_RowValidating);
            // 
            // clAlias
            // 
            this.clAlias.HeaderText = "点别名";
            this.clAlias.Name = "clAlias";
            this.clAlias.ReadOnly = true;
            // 
            // clPointId
            // 
            this.clPointId.HeaderText = "点地址";
            this.clPointId.Name = "clPointId";
            this.clPointId.ReadOnly = true;
            // 
            // clDataType
            // 
            this.clDataType.HeaderText = "数据类型";
            this.clDataType.Name = "clDataType";
            this.clDataType.ReadOnly = true;
            // 
            // clValue
            // 
            this.clValue.HeaderText = "值";
            this.clValue.Name = "clValue";
            // 
            // clUsed
            // 
            this.clUsed.HeaderText = "使用中";
            this.clUsed.Name = "clUsed";
            this.clUsed.ReadOnly = true;
            // 
            // clDescription
            // 
            this.clDescription.HeaderText = "点描述";
            this.clDescription.Name = "clDescription";
            this.clDescription.ReadOnly = true;
            this.clDescription.Width = 300;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 401);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(696, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(44, 17);
            this.statusLabel.Text = "状态：";
            // 
            // btnLoadValues
            // 
            this.btnLoadValues.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoadValues.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadValues.Image")));
            this.btnLoadValues.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadValues.Name = "btnLoadValues";
            this.btnLoadValues.Size = new System.Drawing.Size(23, 22);
            this.btnLoadValues.Text = "加载点值";
            this.btnLoadValues.Click += new System.EventHandler(this.btnLoadValues_Click);
            // 
            // btnSaveValues
            // 
            this.btnSaveValues.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveValues.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveValues.Image")));
            this.btnSaveValues.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveValues.Name = "btnSaveValues";
            this.btnSaveValues.Size = new System.Drawing.Size(23, 22);
            this.btnSaveValues.Text = "保存点值";
            this.btnSaveValues.Click += new System.EventHandler(this.btnSaveValues_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // DebugDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 423);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvPoints);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DebugDataForm";
            this.Text = "DebugDataForm";
            this.Load += new System.EventHandler(this.DebugDataForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgvPoints;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolTempValuesForm;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPointId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clUsed;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDescription;
        private System.Windows.Forms.ToolStripButton btnLoadValues;
        private System.Windows.Forms.ToolStripButton btnSaveValues;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
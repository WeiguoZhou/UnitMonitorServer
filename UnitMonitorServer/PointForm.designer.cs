namespace UnitMonitorServer
{
    partial class PointForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PointForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolDebugStep = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSaveChange = new System.Windows.Forms.ToolStripButton();
            this.toolReLoadPoint = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPointParam = new System.Windows.Forms.TabPage();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.clParamKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clParamDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabTempValue = new System.Windows.Forms.TabPage();
            this.dgvTempValue = new System.Windows.Forms.DataGridView();
            this.clTempValueKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTempValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPointParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            this.tabTempValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTempValue)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDebugStep,
            this.toolStripSeparator1,
            this.toolSaveChange,
            this.toolReLoadPoint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(632, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolDebugStep
            // 
            this.toolDebugStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDebugStep.Image = ((System.Drawing.Image)(resources.GetObject("toolDebugStep.Image")));
            this.toolDebugStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDebugStep.Name = "toolDebugStep";
            this.toolDebugStep.Size = new System.Drawing.Size(23, 22);
            this.toolDebugStep.Text = "toolStripButton1";
            this.toolDebugStep.ToolTipText = "调试模式步进运行";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolSaveChange
            // 
            this.toolSaveChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSaveChange.Image = ((System.Drawing.Image)(resources.GetObject("toolSaveChange.Image")));
            this.toolSaveChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSaveChange.Name = "toolSaveChange";
            this.toolSaveChange.Size = new System.Drawing.Size(23, 22);
            this.toolSaveChange.Text = "toolStripButton2";
            this.toolSaveChange.ToolTipText = "保存到配置文件";
            // 
            // toolReLoadPoint
            // 
            this.toolReLoadPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolReLoadPoint.Image = ((System.Drawing.Image)(resources.GetObject("toolReLoadPoint.Image")));
            this.toolReLoadPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReLoadPoint.Name = "toolReLoadPoint";
            this.toolReLoadPoint.RightToLeftAutoMirrorImage = true;
            this.toolReLoadPoint.Size = new System.Drawing.Size(23, 22);
            this.toolReLoadPoint.Text = "重新载入";
            this.toolReLoadPoint.ToolTipText = "从配置文件中重新载入点的参数";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPointParam);
            this.tabControl1.Controls.Add(this.tabTempValue);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(632, 321);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPointParam
            // 
            this.tabPointParam.Controls.Add(this.dgvParams);
            this.tabPointParam.Location = new System.Drawing.Point(4, 22);
            this.tabPointParam.Name = "tabPointParam";
            this.tabPointParam.Padding = new System.Windows.Forms.Padding(3);
            this.tabPointParam.Size = new System.Drawing.Size(624, 295);
            this.tabPointParam.TabIndex = 0;
            this.tabPointParam.Text = "点参数";
            this.tabPointParam.UseVisualStyleBackColor = true;
            // 
            // dgvParams
            // 
            this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clParamKey,
            this.clParamValue,
            this.clParamDescription});
            this.dgvParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvParams.Location = new System.Drawing.Point(3, 3);
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RowTemplate.Height = 23;
            this.dgvParams.Size = new System.Drawing.Size(618, 289);
            this.dgvParams.TabIndex = 0;
            this.dgvParams.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvParams_CellValidating);
            this.dgvParams.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvParams_RowValidated);
            // 
            // clParamKey
            // 
            this.clParamKey.HeaderText = "参数名称";
            this.clParamKey.Name = "clParamKey";
            this.clParamKey.ReadOnly = true;
            this.clParamKey.Width = 200;
            // 
            // clParamValue
            // 
            this.clParamValue.HeaderText = "参数值";
            this.clParamValue.Name = "clParamValue";
            // 
            // clParamDescription
            // 
            this.clParamDescription.HeaderText = "参数描述";
            this.clParamDescription.Name = "clParamDescription";
            this.clParamDescription.ReadOnly = true;
            this.clParamDescription.Width = 400;
            // 
            // tabTempValue
            // 
            this.tabTempValue.Controls.Add(this.dgvTempValue);
            this.tabTempValue.Location = new System.Drawing.Point(4, 22);
            this.tabTempValue.Name = "tabTempValue";
            this.tabTempValue.Padding = new System.Windows.Forms.Padding(3);
            this.tabTempValue.Size = new System.Drawing.Size(624, 295);
            this.tabTempValue.TabIndex = 1;
            this.tabTempValue.Text = "临时数据";
            this.tabTempValue.UseVisualStyleBackColor = true;
            // 
            // dgvTempValue
            // 
            this.dgvTempValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTempValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clTempValueKey,
            this.clTempValue});
            this.dgvTempValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTempValue.Location = new System.Drawing.Point(3, 3);
            this.dgvTempValue.Name = "dgvTempValue";
            this.dgvTempValue.RowTemplate.Height = 23;
            this.dgvTempValue.Size = new System.Drawing.Size(618, 289);
            this.dgvTempValue.TabIndex = 0;
            // 
            // clTempValueKey
            // 
            this.clTempValueKey.HeaderText = "临时数据键名";
            this.clTempValueKey.Name = "clTempValueKey";
            this.clTempValueKey.ReadOnly = true;
            this.clTempValueKey.Width = 200;
            // 
            // clTempValue
            // 
            this.clTempValue.HeaderText = "临时数据值";
            this.clTempValue.Name = "clTempValue";
            this.clTempValue.ReadOnly = true;
            // 
            // PointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 370);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PointForm";
            this.Text = "点窗口";
            this.Load += new System.EventHandler(this.TaskTempValueForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPointParam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            this.tabTempValue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTempValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPointParam;
        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn clParamKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn clParamValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clParamDescription;
        private System.Windows.Forms.TabPage tabTempValue;
        private System.Windows.Forms.DataGridView dgvTempValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTempValueKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTempValue;
        private System.Windows.Forms.ToolStripButton toolDebugStep;
        private System.Windows.Forms.ToolStripButton toolSaveChange;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolReLoadPoint;
    }
}
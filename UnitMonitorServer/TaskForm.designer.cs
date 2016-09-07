namespace UnitMonitorServer
{
    partial class TaskForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolRun = new System.Windows.Forms.ToolStripButton();
            this.toolRunNext = new System.Windows.Forms.ToolStripButton();
            this.toolStop = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tcTask = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvTaskParams = new System.Windows.Forms.DataGridView();
            this.clTaskParamKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTaskParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTaskParamDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgvFuns = new System.Windows.Forms.DataGridView();
            this.funGroupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabReadme = new System.Windows.Forms.TabPage();
            this.rtxtTaskModuleReadme = new System.Windows.Forms.RichTextBox();
            this.tcFun = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvPoints = new System.Windows.Forms.DataGridView();
            this.indexDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allowAlarmDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.descriptionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvFunParams = new System.Windows.Forms.DataGridView();
            this.clFunParamKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clFunParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clFunParamDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolPointValues = new System.Windows.Forms.ToolStripMenuItem();
            this.全部引用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.indexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tcTask.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskParams)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFuns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.funGroupBindingSource)).BeginInit();
            this.tabReadme.SuspendLayout();
            this.tcFun.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(769, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRun,
            this.toolRunNext,
            this.toolStop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(769, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolRun
            // 
            this.toolRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRun.Image = ((System.Drawing.Image)(resources.GetObject("toolRun.Image")));
            this.toolRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRun.Name = "toolRun";
            this.toolRun.Size = new System.Drawing.Size(23, 22);
            this.toolRun.Text = "运行";
            // 
            // toolRunNext
            // 
            this.toolRunNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRunNext.Image = ((System.Drawing.Image)(resources.GetObject("toolRunNext.Image")));
            this.toolRunNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRunNext.Name = "toolRunNext";
            this.toolRunNext.Size = new System.Drawing.Size(23, 22);
            this.toolRunNext.Text = "步进";
            // 
            // toolStop
            // 
            this.toolStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStop.Image")));
            this.toolStop.ImageTransparentColor = System.Drawing.Color.MediumAquamarine;
            this.toolStop.Name = "toolStop";
            this.toolStop.Size = new System.Drawing.Size(23, 22);
            this.toolStop.Text = "停止";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tcTask);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tcFun);
            this.splitContainer1.Size = new System.Drawing.Size(769, 390);
            this.splitContainer1.SplitterDistance = 329;
            this.splitContainer1.TabIndex = 2;
            // 
            // tcTask
            // 
            this.tcTask.Controls.Add(this.tabPage2);
            this.tcTask.Controls.Add(this.tabPage5);
            this.tcTask.Controls.Add(this.tabReadme);
            this.tcTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTask.Location = new System.Drawing.Point(0, 0);
            this.tcTask.Name = "tcTask";
            this.tcTask.SelectedIndex = 0;
            this.tcTask.Size = new System.Drawing.Size(329, 390);
            this.tcTask.TabIndex = 0;
            this.tcTask.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcTask_Selected);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvTaskParams);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(321, 364);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "任务参数";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvTaskParams
            // 
            this.dgvTaskParams.AllowUserToAddRows = false;
            this.dgvTaskParams.AllowUserToDeleteRows = false;
            this.dgvTaskParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clTaskParamKey,
            this.clTaskParamValue,
            this.clTaskParamDescription});
            this.dgvTaskParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaskParams.Location = new System.Drawing.Point(3, 3);
            this.dgvTaskParams.Name = "dgvTaskParams";
            this.dgvTaskParams.ReadOnly = true;
            this.dgvTaskParams.RowTemplate.Height = 23;
            this.dgvTaskParams.Size = new System.Drawing.Size(315, 358);
            this.dgvTaskParams.TabIndex = 0;
            // 
            // clTaskParamKey
            // 
            this.clTaskParamKey.HeaderText = "参数名称";
            this.clTaskParamKey.Name = "clTaskParamKey";
            this.clTaskParamKey.ReadOnly = true;
            this.clTaskParamKey.Width = 150;
            // 
            // clTaskParamValue
            // 
            this.clTaskParamValue.HeaderText = "设定值";
            this.clTaskParamValue.Name = "clTaskParamValue";
            this.clTaskParamValue.ReadOnly = true;
            // 
            // clTaskParamDescription
            // 
            this.clTaskParamDescription.HeaderText = "参数描述";
            this.clTaskParamDescription.Name = "clTaskParamDescription";
            this.clTaskParamDescription.ReadOnly = true;
            this.clTaskParamDescription.Width = 400;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgvFuns);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(321, 364);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "功能一览";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgvFuns
            // 
            this.dgvFuns.AutoGenerateColumns = false;
            this.dgvFuns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFuns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
            this.dgvFuns.DataSource = this.funGroupBindingSource;
            this.dgvFuns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFuns.Location = new System.Drawing.Point(3, 3);
            this.dgvFuns.Name = "dgvFuns";
            this.dgvFuns.RowTemplate.Height = 23;
            this.dgvFuns.Size = new System.Drawing.Size(315, 358);
            this.dgvFuns.TabIndex = 0;
            this.dgvFuns.SelectionChanged += new System.EventHandler(this.dgvFuns_SelectionChanged);
            // 
            // funGroupBindingSource
            // 
            this.funGroupBindingSource.DataSource = typeof(UnitMonitorCommon.FunGroup);
            // 
            // tabReadme
            // 
            this.tabReadme.Controls.Add(this.rtxtTaskModuleReadme);
            this.tabReadme.Location = new System.Drawing.Point(4, 22);
            this.tabReadme.Name = "tabReadme";
            this.tabReadme.Size = new System.Drawing.Size(321, 364);
            this.tabReadme.TabIndex = 2;
            this.tabReadme.Text = "任务概述";
            this.tabReadme.UseVisualStyleBackColor = true;
            // 
            // rtxtTaskModuleReadme
            // 
            this.rtxtTaskModuleReadme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtTaskModuleReadme.Location = new System.Drawing.Point(0, 0);
            this.rtxtTaskModuleReadme.Name = "rtxtTaskModuleReadme";
            this.rtxtTaskModuleReadme.Size = new System.Drawing.Size(321, 364);
            this.rtxtTaskModuleReadme.TabIndex = 0;
            this.rtxtTaskModuleReadme.Text = "";
            // 
            // tcFun
            // 
            this.tcFun.Controls.Add(this.tabPage1);
            this.tcFun.Controls.Add(this.tabPage3);
            this.tcFun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFun.Location = new System.Drawing.Point(0, 0);
            this.tcFun.Name = "tcFun";
            this.tcFun.SelectedIndex = 0;
            this.tcFun.Size = new System.Drawing.Size(436, 390);
            this.tcFun.TabIndex = 0;
            this.tcFun.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvPoints);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(428, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "引用点表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvPoints
            // 
            this.dgvPoints.AutoGenerateColumns = false;
            this.dgvPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexDataGridViewTextBoxColumn1,
            this.idDataGridViewTextBoxColumn,
            this.allowAlarmDataGridViewCheckBoxColumn,
            this.descriptionDataGridViewTextBoxColumn1,
            this.valueDataGridViewTextBoxColumn});
            this.dgvPoints.DataSource = this.pointBindingSource;
            this.dgvPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPoints.Location = new System.Drawing.Point(3, 3);
            this.dgvPoints.Name = "dgvPoints";
            this.dgvPoints.RowTemplate.Height = 23;
            this.dgvPoints.Size = new System.Drawing.Size(422, 358);
            this.dgvPoints.TabIndex = 0;
            // 
            // indexDataGridViewTextBoxColumn1
            // 
            this.indexDataGridViewTextBoxColumn1.DataPropertyName = "Index";
            this.indexDataGridViewTextBoxColumn1.HeaderText = "序号";
            this.indexDataGridViewTextBoxColumn1.Name = "indexDataGridViewTextBoxColumn1";
            this.indexDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "点地址";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // allowAlarmDataGridViewCheckBoxColumn
            // 
            this.allowAlarmDataGridViewCheckBoxColumn.DataPropertyName = "AllowAlarm";
            this.allowAlarmDataGridViewCheckBoxColumn.HeaderText = "允许报警";
            this.allowAlarmDataGridViewCheckBoxColumn.Name = "allowAlarmDataGridViewCheckBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn1
            // 
            this.descriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn1.HeaderText = "点描述";
            this.descriptionDataGridViewTextBoxColumn1.Name = "descriptionDataGridViewTextBoxColumn1";
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            // 
            // pointBindingSource
            // 
            this.pointBindingSource.DataSource = typeof(UnitMonitorCommon.Point);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvFunParams);
            this.tabPage3.Controls.Add(this.dataGridView4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(428, 364);
            this.tabPage3.TabIndex = 5;
            this.tabPage3.Text = "功能参数";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvFunParams
            // 
            this.dgvFunParams.AllowUserToAddRows = false;
            this.dgvFunParams.AllowUserToDeleteRows = false;
            this.dgvFunParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFunParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clFunParamKey,
            this.clFunParamValue,
            this.clFunParamDescription});
            this.dgvFunParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFunParams.Location = new System.Drawing.Point(0, 0);
            this.dgvFunParams.Name = "dgvFunParams";
            this.dgvFunParams.RowTemplate.Height = 23;
            this.dgvFunParams.Size = new System.Drawing.Size(428, 364);
            this.dgvFunParams.TabIndex = 1;
            // 
            // clFunParamKey
            // 
            this.clFunParamKey.HeaderText = "参数名称";
            this.clFunParamKey.Name = "clFunParamKey";
            this.clFunParamKey.ReadOnly = true;
            this.clFunParamKey.Width = 200;
            // 
            // clFunParamValue
            // 
            this.clFunParamValue.HeaderText = "参数设定值";
            this.clFunParamValue.Name = "clFunParamValue";
            // 
            // clFunParamDescription
            // 
            this.clFunParamDescription.HeaderText = "参数描述";
            this.clFunParamDescription.Name = "clFunParamDescription";
            this.clFunParamDescription.ReadOnly = true;
            this.clFunParamDescription.Width = 400;
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4.Location = new System.Drawing.Point(0, 0);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowTemplate.Height = 23;
            this.dataGridView4.Size = new System.Drawing.Size(428, 364);
            this.dataGridView4.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolPointValues,
            this.全部引用ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // toolPointValues
            // 
            this.toolPointValues.Name = "toolPointValues";
            this.toolPointValues.Size = new System.Drawing.Size(124, 22);
            this.toolPointValues.Text = "全部数据";
            this.toolPointValues.Click += new System.EventHandler(this.toolPointValues_Click);
            // 
            // 全部引用ToolStripMenuItem
            // 
            this.全部引用ToolStripMenuItem.Name = "全部引用ToolStripMenuItem";
            this.全部引用ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.全部引用ToolStripMenuItem.Text = "全部引用";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ACCICONS_00038.ico");
            this.imageList1.Images.SetKeyName(1, "explorer_00014.ico");
            this.imageList1.Images.SetKeyName(2, "GROOVEEX_00001.ico");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "msenv_00043.ico");
            // 
            // indexDataGridViewTextBoxColumn
            // 
            this.indexDataGridViewTextBoxColumn.DataPropertyName = "Index";
            this.indexDataGridViewTextBoxColumn.HeaderText = "序号";
            this.indexDataGridViewTextBoxColumn.Name = "indexDataGridViewTextBoxColumn";
            this.indexDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "功能描述";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descriptionDataGridViewTextBoxColumn.Width = 500;
            // 
            // TaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 439);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TaskForm";
            this.Text = "frmTasks";
            this.Load += new System.EventHandler(this.frmTasks_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tcTask.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskParams)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFuns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.funGroupBindingSource)).EndInit();
            this.tabReadme.ResumeLayout(false);
            this.tcFun.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointBindingSource)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton toolRun;
        private System.Windows.Forms.ToolStripButton toolRunNext;
        private System.Windows.Forms.ToolStripButton toolStop;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolPointValues;
        private System.Windows.Forms.ToolStripMenuItem 全部引用ToolStripMenuItem;
        private System.Windows.Forms.TabControl tcFun;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox rtxtTaskModuleReadme;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.BindingSource funGroupBindingSource;
        private System.Windows.Forms.TabControl tcTask;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvTaskParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTaskParamKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTaskParamValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTaskParamDescription;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dgvFuns;
        private System.Windows.Forms.TabPage tabReadme;
        private System.Windows.Forms.DataGridView dgvPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowAlarmDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource pointBindingSource;
        private System.Windows.Forms.DataGridView dgvFunParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn clFunParamKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn clFunParamValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clFunParamDescription;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    }
}
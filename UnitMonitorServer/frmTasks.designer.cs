namespace UnitMonitorServer
{
    partial class frmTasks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTasks));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolRun = new System.Windows.Forms.ToolStripButton();
            this.toolRunNext = new System.Windows.Forms.ToolStripButton();
            this.toolStop = new System.Windows.Forms.ToolStripButton();
            this.toolDataMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolDataPeriod = new System.Windows.Forms.ToolStripComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lvFuns = new System.Windows.Forms.ListView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lvParams = new System.Windows.Forms.ListView();
            this.colParamName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValueType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colParamDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.dgvPoints = new System.Windows.Forms.DataGridView();
            this.clAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPointId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPointType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPointValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPointDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rtxtTaskModuleReadme = new System.Windows.Forms.RichTextBox();
            this.clFunName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFunDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolPointValues = new System.Windows.Forms.ToolStripMenuItem();
            this.全部引用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).BeginInit();
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
            this.toolStop,
            this.toolDataMode,
            this.toolDataPeriod});
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
            // toolDataMode
            // 
            this.toolDataMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolDataMode.Items.AddRange(new object[] {
            "实时模式",
            "历史模式",
            "调试模式"});
            this.toolDataMode.Name = "toolDataMode";
            this.toolDataMode.Size = new System.Drawing.Size(121, 25);
            // 
            // toolDataPeriod
            // 
            this.toolDataPeriod.Items.AddRange(new object[] {
            "5",
            "6",
            "10",
            "15",
            "20",
            "30",
            "60",
            "120",
            "240",
            "300",
            "600"});
            this.toolDataPeriod.Name = "toolDataPeriod";
            this.toolDataPeriod.Size = new System.Drawing.Size(121, 25);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(769, 390);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(191, 390);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCheck);
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.node_AfterCheck);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(574, 390);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtxtTaskModuleReadme);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(566, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "任务概述";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvPoints);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(566, 364);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "引用点";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(560, 358);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lvFuns);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(566, 364);
            this.tabPage3.TabIndex = 5;
            this.tabPage3.Text = "功能开关";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lvFuns
            // 
            this.lvFuns.CheckBoxes = true;
            this.lvFuns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clFunName,
            this.clFunDescription});
            this.lvFuns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFuns.FullRowSelect = true;
            this.lvFuns.GridLines = true;
            this.lvFuns.Location = new System.Drawing.Point(0, 0);
            this.lvFuns.Name = "lvFuns";
            this.lvFuns.Size = new System.Drawing.Size(566, 364);
            this.lvFuns.TabIndex = 0;
            this.lvFuns.UseCompatibleStateImageBehavior = false;
            this.lvFuns.View = System.Windows.Forms.View.Details;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lvParams);
            this.tabPage4.Controls.Add(this.dataGridView3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(566, 364);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "参数设置";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lvParams
            // 
            this.lvParams.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colParamName,
            this.colValueType,
            this.colValue,
            this.colParamDescription});
            this.lvParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvParams.FullRowSelect = true;
            this.lvParams.GridLines = true;
            this.lvParams.Location = new System.Drawing.Point(0, 0);
            this.lvParams.Name = "lvParams";
            this.lvParams.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lvParams.Size = new System.Drawing.Size(566, 364);
            this.lvParams.TabIndex = 1;
            this.lvParams.UseCompatibleStateImageBehavior = false;
            this.lvParams.View = System.Windows.Forms.View.Details;
            // 
            // colParamName
            // 
            this.colParamName.Text = "参数名称";
            this.colParamName.Width = 108;
            // 
            // colValueType
            // 
            this.colValueType.Text = "数据类型";
            this.colValueType.Width = 98;
            // 
            // colValue
            // 
            this.colValue.Text = "设定值";
            this.colValue.Width = 70;
            // 
            // colParamDescription
            // 
            this.colParamDescription.Text = "参数说明";
            this.colParamDescription.Width = 383;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(566, 364);
            this.dataGridView3.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.txtCode);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(566, 364);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "代码参考";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Location = new System.Drawing.Point(0, 0);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(566, 364);
            this.txtCode.TabIndex = 0;
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
            // dgvPoints
            // 
            this.dgvPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clAlias,
            this.clPointId,
            this.clPointType,
            this.clPointValue,
            this.clPointDescription});
            this.dgvPoints.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPoints.Location = new System.Drawing.Point(3, 3);
            this.dgvPoints.Name = "dgvPoints";
            this.dgvPoints.RowTemplate.Height = 23;
            this.dgvPoints.Size = new System.Drawing.Size(560, 358);
            this.dgvPoints.TabIndex = 1;
            // 
            // clAlias
            // 
            this.clAlias.Frozen = true;
            this.clAlias.HeaderText = "点别名";
            this.clAlias.Name = "clAlias";
            this.clAlias.ReadOnly = true;
            this.clAlias.Width = 150;
            // 
            // clPointId
            // 
            this.clPointId.Frozen = true;
            this.clPointId.HeaderText = "点地址";
            this.clPointId.Name = "clPointId";
            this.clPointId.ReadOnly = true;
            this.clPointId.Width = 150;
            // 
            // clPointType
            // 
            this.clPointType.HeaderText = "点类型";
            this.clPointType.Name = "clPointType";
            this.clPointType.ReadOnly = true;
            // 
            // clPointValue
            // 
            this.clPointValue.HeaderText = "点值";
            this.clPointValue.Name = "clPointValue";
            // 
            // clPointDescription
            // 
            this.clPointDescription.HeaderText = "点描述";
            this.clPointDescription.Name = "clPointDescription";
            this.clPointDescription.Width = 500;
            // 
            // rtxtTaskModuleReadme
            // 
            this.rtxtTaskModuleReadme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtTaskModuleReadme.Location = new System.Drawing.Point(3, 3);
            this.rtxtTaskModuleReadme.Name = "rtxtTaskModuleReadme";
            this.rtxtTaskModuleReadme.Size = new System.Drawing.Size(560, 358);
            this.rtxtTaskModuleReadme.TabIndex = 0;
            this.rtxtTaskModuleReadme.Text = "";
            // 
            // clFunName
            // 
            this.clFunName.Text = "功能名称键值";
            this.clFunName.Width = 99;
            // 
            // clFunDescription
            // 
            this.clFunDescription.Text = "功能描述";
            this.clFunDescription.Width = 441;
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
            // frmTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 439);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmTasks";
            this.Text = "frmTasks";
            this.Load += new System.EventHandler(this.frmTasks_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView lvParams;
        private System.Windows.Forms.ColumnHeader colParamName;
        private System.Windows.Forms.ColumnHeader colValueType;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.ColumnHeader colParamDescription;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView lvFuns;
        private System.Windows.Forms.ToolStripButton toolRun;
        private System.Windows.Forms.ToolStripButton toolRunNext;
        private System.Windows.Forms.ToolStripButton toolStop;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripComboBox toolDataMode;
        private System.Windows.Forms.ToolStripComboBox toolDataPeriod;
        private System.Windows.Forms.DataGridView dgvPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn clAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPointId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPointType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPointValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPointDescription;
        private System.Windows.Forms.RichTextBox rtxtTaskModuleReadme;
        private System.Windows.Forms.ColumnHeader clFunName;
        private System.Windows.Forms.ColumnHeader clFunDescription;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolPointValues;
        private System.Windows.Forms.ToolStripMenuItem 全部引用ToolStripMenuItem;
    }
}
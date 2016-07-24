namespace UnitMonitorServer
{
    partial class TaskContainerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskContainerForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.btnStep = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbPeriod = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStartSelectedTask = new System.Windows.Forms.ToolStripButton();
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolDebugData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStopSelectedTask = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.taskNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastSpendTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastSuccessDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.successCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.persistantSuccessDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.persistantFailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.failCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tasksContainerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusTasksContainer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasksContainerBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(722, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRun,
            this.btnStep,
            this.btnStop,
            this.toolStripLabel1,
            this.cbMode,
            this.toolStripLabel2,
            this.cbPeriod,
            this.toolStripSeparator1,
            this.toolStartSelectedTask});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(722, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRun
            // 
            this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(23, 22);
            this.btnRun.Text = "toolStripButton1";
            this.btnRun.ToolTipText = "运行";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStep
            // 
            this.btnStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStep.Image = ((System.Drawing.Image)(resources.GetObject("btnStep.Image")));
            this.btnStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(23, 22);
            this.btnStep.Text = "步进运行";
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 22);
            this.btnStop.Text = "停止";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel1.Text = "运行模式：";
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.Items.AddRange(new object[] {
            "实时模式",
            "历史模式",
            "调试模式"});
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(121, 25);
            this.cbMode.SelectedIndexChanged += new System.EventHandler(this.cbMode_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel2.Text = "运行周期：";
            // 
            // cbPeriod
            // 
            this.cbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPeriod.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "10",
            "15",
            "30"});
            this.cbPeriod.Name = "cbPeriod";
            this.cbPeriod.Size = new System.Drawing.Size(121, 25);
            this.cbPeriod.SelectedIndexChanged += new System.EventHandler(this.cbPeriod_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStartSelectedTask
            // 
            this.toolStartSelectedTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStartSelectedTask.Image = ((System.Drawing.Image)(resources.GetObject("toolStartSelectedTask.Image")));
            this.toolStartSelectedTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStartSelectedTask.Name = "toolStartSelectedTask";
            this.toolStartSelectedTask.Size = new System.Drawing.Size(23, 22);
            this.toolStartSelectedTask.Text = "启动选定任务";
            this.toolStartSelectedTask.Click += new System.EventHandler(this.toolStartSelectedTask_Click);
            // 
            // dgvTasks
            // 
            this.dgvTasks.AllowUserToAddRows = false;
            this.dgvTasks.AllowUserToDeleteRows = false;
            this.dgvTasks.AutoGenerateColumns = false;
            this.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.taskNameDataGridViewTextBoxColumn,
            this.taskPathDataGridViewTextBoxColumn,
            this.runCountDataGridViewTextBoxColumn,
            this.periodDataGridViewTextBoxColumn,
            this.lastSpendTimeDataGridViewTextBoxColumn,
            this.lastSuccessDataGridViewCheckBoxColumn,
            this.successCountDataGridViewTextBoxColumn,
            this.persistantSuccessDataGridViewTextBoxColumn,
            this.persistantFailDataGridViewTextBoxColumn,
            this.failCountDataGridViewTextBoxColumn});
            this.dgvTasks.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvTasks.DataSource = this.tasksContainerBindingSource;
            this.dgvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTasks.Location = new System.Drawing.Point(0, 0);
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.ReadOnly = true;
            this.dgvTasks.RowTemplate.Height = 23;
            this.dgvTasks.Size = new System.Drawing.Size(478, 338);
            this.dgvTasks.TabIndex = 2;
            this.dgvTasks.VirtualMode = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDebugData,
            this.toolStopSelectedTask});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolDebugData
            // 
            this.toolDebugData.Name = "toolDebugData";
            this.toolDebugData.Size = new System.Drawing.Size(124, 22);
            this.toolDebugData.Text = "调试数据";
            this.toolDebugData.Click += new System.EventHandler(this.toolDebugData_Click);
            // 
            // toolStopSelectedTask
            // 
            this.toolStopSelectedTask.Name = "toolStopSelectedTask";
            this.toolStopSelectedTask.Size = new System.Drawing.Size(124, 22);
            this.toolStopSelectedTask.Text = "停止运行";
            this.toolStopSelectedTask.Click += new System.EventHandler(this.toolStopSelectedTask_Click);
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
            this.splitContainer1.Panel2.Controls.Add(this.dgvTasks);
            this.splitContainer1.Size = new System.Drawing.Size(722, 338);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(240, 338);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.node_AfterCheck);
            // 
            // taskNameDataGridViewTextBoxColumn
            // 
            this.taskNameDataGridViewTextBoxColumn.DataPropertyName = "TaskName";
            this.taskNameDataGridViewTextBoxColumn.HeaderText = "任务名称";
            this.taskNameDataGridViewTextBoxColumn.Name = "taskNameDataGridViewTextBoxColumn";
            this.taskNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.taskNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // taskPathDataGridViewTextBoxColumn
            // 
            this.taskPathDataGridViewTextBoxColumn.DataPropertyName = "TaskPath";
            this.taskPathDataGridViewTextBoxColumn.HeaderText = "任务路径";
            this.taskPathDataGridViewTextBoxColumn.Name = "taskPathDataGridViewTextBoxColumn";
            this.taskPathDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // runCountDataGridViewTextBoxColumn
            // 
            this.runCountDataGridViewTextBoxColumn.DataPropertyName = "RunCount";
            this.runCountDataGridViewTextBoxColumn.HeaderText = "运行次数";
            this.runCountDataGridViewTextBoxColumn.Name = "runCountDataGridViewTextBoxColumn";
            this.runCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.runCountDataGridViewTextBoxColumn.Width = 60;
            // 
            // periodDataGridViewTextBoxColumn
            // 
            this.periodDataGridViewTextBoxColumn.DataPropertyName = "Period";
            this.periodDataGridViewTextBoxColumn.HeaderText = "运行周期";
            this.periodDataGridViewTextBoxColumn.Name = "periodDataGridViewTextBoxColumn";
            this.periodDataGridViewTextBoxColumn.ReadOnly = true;
            this.periodDataGridViewTextBoxColumn.Width = 60;
            // 
            // lastSpendTimeDataGridViewTextBoxColumn
            // 
            this.lastSpendTimeDataGridViewTextBoxColumn.DataPropertyName = "LastSpendTime";
            this.lastSpendTimeDataGridViewTextBoxColumn.HeaderText = "上次花费时间";
            this.lastSpendTimeDataGridViewTextBoxColumn.Name = "lastSpendTimeDataGridViewTextBoxColumn";
            this.lastSpendTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastSpendTimeDataGridViewTextBoxColumn.Width = 80;
            // 
            // lastSuccessDataGridViewCheckBoxColumn
            // 
            this.lastSuccessDataGridViewCheckBoxColumn.DataPropertyName = "LastSuccess";
            this.lastSuccessDataGridViewCheckBoxColumn.HeaderText = "上次成功运行";
            this.lastSuccessDataGridViewCheckBoxColumn.Name = "lastSuccessDataGridViewCheckBoxColumn";
            this.lastSuccessDataGridViewCheckBoxColumn.ReadOnly = true;
            this.lastSuccessDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lastSuccessDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.lastSuccessDataGridViewCheckBoxColumn.Width = 50;
            // 
            // successCountDataGridViewTextBoxColumn
            // 
            this.successCountDataGridViewTextBoxColumn.DataPropertyName = "SuccessCount";
            this.successCountDataGridViewTextBoxColumn.HeaderText = "成功总次数";
            this.successCountDataGridViewTextBoxColumn.Name = "successCountDataGridViewTextBoxColumn";
            this.successCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.successCountDataGridViewTextBoxColumn.Width = 80;
            // 
            // persistantSuccessDataGridViewTextBoxColumn
            // 
            this.persistantSuccessDataGridViewTextBoxColumn.DataPropertyName = "PersistantSuccess";
            this.persistantSuccessDataGridViewTextBoxColumn.HeaderText = "连续成功次数";
            this.persistantSuccessDataGridViewTextBoxColumn.Name = "persistantSuccessDataGridViewTextBoxColumn";
            this.persistantSuccessDataGridViewTextBoxColumn.ReadOnly = true;
            this.persistantSuccessDataGridViewTextBoxColumn.Width = 80;
            // 
            // persistantFailDataGridViewTextBoxColumn
            // 
            this.persistantFailDataGridViewTextBoxColumn.DataPropertyName = "PersistantFail";
            this.persistantFailDataGridViewTextBoxColumn.HeaderText = "连续失败次数";
            this.persistantFailDataGridViewTextBoxColumn.Name = "persistantFailDataGridViewTextBoxColumn";
            this.persistantFailDataGridViewTextBoxColumn.ReadOnly = true;
            this.persistantFailDataGridViewTextBoxColumn.Width = 80;
            // 
            // failCountDataGridViewTextBoxColumn
            // 
            this.failCountDataGridViewTextBoxColumn.DataPropertyName = "FailCount";
            this.failCountDataGridViewTextBoxColumn.HeaderText = "失败总次数";
            this.failCountDataGridViewTextBoxColumn.Name = "failCountDataGridViewTextBoxColumn";
            this.failCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.failCountDataGridViewTextBoxColumn.Width = 80;
            // 
            // tasksContainerBindingSource
            // 
            this.tasksContainerBindingSource.DataSource = typeof(UnitMonitorCommon.TasksContainer);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusTasksContainer});
            this.statusStrip1.Location = new System.Drawing.Point(0, 365);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(722, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusTasksContainer
            // 
            this.StatusTasksContainer.Name = "StatusTasksContainer";
            this.StatusTasksContainer.Size = new System.Drawing.Size(44, 17);
            this.StatusTasksContainer.Text = "状态：";
            // 
            // TaskContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 387);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TaskContainerForm";
            this.Text = "任务中心";
            this.Load += new System.EventHandler(this.TaskContainerForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tasksContainerBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripButton btnStep;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbMode;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cbPeriod;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.BindingSource tasksContainerBindingSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolDebugData;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripButton toolStartSelectedTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStopSelectedTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn runCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn periodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastSpendTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastSuccessDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn successCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn persistantSuccessDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn persistantFailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn failCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusTasksContainer;
    }
}
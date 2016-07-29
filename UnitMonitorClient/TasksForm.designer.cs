namespace UnitMonitorClient
{
    partial class TasksForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TasksForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbOnlineServers = new System.Windows.Forms.ToolStripComboBox();
            this.cbAllTaskSelector = new System.Windows.Forms.ToolStripComboBox();
            this.btnDataFilter = new System.Windows.Forms.ToolStripButton();
            this.btnViewData = new System.Windows.Forms.ToolStripButton();
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.订阅全部消息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.订阅InfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消订阅消息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.订阅WarnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.订阅DangerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消订阅InfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消订阅AlarmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消订阅WarnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消订阅DangerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消订阅全部消息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moduleNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clSubscribe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isRunningDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastRunTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beginTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastSpendTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastSuccessDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.successCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.persistantSuccessDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.persistantFailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.failCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(628, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cbOnlineServers,
            this.cbAllTaskSelector,
            this.btnDataFilter,
            this.btnViewData});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(628, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel1.Text = "选择服务器：";
            // 
            // cbOnlineServers
            // 
            this.cbOnlineServers.Name = "cbOnlineServers";
            this.cbOnlineServers.Size = new System.Drawing.Size(121, 25);
            // 
            // cbAllTaskSelector
            // 
            this.cbAllTaskSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAllTaskSelector.Items.AddRange(new object[] {
            "显示全部任务",
            "只显示运行任务"});
            this.cbAllTaskSelector.Name = "cbAllTaskSelector";
            this.cbAllTaskSelector.Size = new System.Drawing.Size(121, 25);
            // 
            // btnDataFilter
            // 
            this.btnDataFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDataFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnDataFilter.Image")));
            this.btnDataFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDataFilter.Name = "btnDataFilter";
            this.btnDataFilter.Size = new System.Drawing.Size(23, 22);
            this.btnDataFilter.Text = "筛选数据";
            this.btnDataFilter.Click += new System.EventHandler(this.btnDataFilter_Click);
            // 
            // btnViewData
            // 
            this.btnViewData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewData.Image = ((System.Drawing.Image)(resources.GetObject("btnViewData.Image")));
            this.btnViewData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewData.Name = "btnViewData";
            this.btnViewData.Size = new System.Drawing.Size(23, 22);
            this.btnViewData.Text = "显示内容";
            this.btnViewData.Click += new System.EventHandler(this.btnViewData_Click);
            // 
            // dgvTasks
            // 
            this.dgvTasks.AllowUserToAddRows = false;
            this.dgvTasks.AllowUserToDeleteRows = false;
            this.dgvTasks.AutoGenerateColumns = false;
            this.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.pathDataGridViewTextBoxColumn,
            this.moduleNameDataGridViewTextBoxColumn,
            this.clSubscribe,
            this.isRunningDataGridViewCheckBoxColumn,
            this.lastRunTimeDataGridViewTextBoxColumn,
            this.beginTimeDataGridViewTextBoxColumn,
            this.runCountDataGridViewTextBoxColumn,
            this.periodDataGridViewTextBoxColumn,
            this.lastSpendTimeDataGridViewTextBoxColumn,
            this.lastSuccessDataGridViewCheckBoxColumn,
            this.successCountDataGridViewTextBoxColumn,
            this.persistantSuccessDataGridViewTextBoxColumn,
            this.persistantFailDataGridViewTextBoxColumn,
            this.failCountDataGridViewTextBoxColumn});
            this.dgvTasks.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvTasks.DataSource = this.taskInfoBindingSource;
            this.dgvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTasks.Location = new System.Drawing.Point(0, 49);
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.ReadOnly = true;
            this.dgvTasks.RowTemplate.Height = 23;
            this.dgvTasks.Size = new System.Drawing.Size(628, 333);
            this.dgvTasks.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.订阅全部消息ToolStripMenuItem,
            this.订阅InfoToolStripMenuItem,
            this.取消订阅消息ToolStripMenuItem,
            this.订阅WarnToolStripMenuItem,
            this.订阅DangerToolStripMenuItem,
            this.取消订阅InfoToolStripMenuItem,
            this.取消订阅AlarmToolStripMenuItem,
            this.取消订阅WarnToolStripMenuItem,
            this.取消订阅DangerToolStripMenuItem,
            this.取消订阅全部消息ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 224);
            // 
            // 订阅全部消息ToolStripMenuItem
            // 
            this.订阅全部消息ToolStripMenuItem.Name = "订阅全部消息ToolStripMenuItem";
            this.订阅全部消息ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.订阅全部消息ToolStripMenuItem.Text = "订阅全部消息";
            // 
            // 订阅InfoToolStripMenuItem
            // 
            this.订阅InfoToolStripMenuItem.Name = "订阅InfoToolStripMenuItem";
            this.订阅InfoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.订阅InfoToolStripMenuItem.Text = "订阅Info";
            // 
            // 取消订阅消息ToolStripMenuItem
            // 
            this.取消订阅消息ToolStripMenuItem.Name = "取消订阅消息ToolStripMenuItem";
            this.取消订阅消息ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.取消订阅消息ToolStripMenuItem.Text = "订阅Alarm";
            // 
            // 订阅WarnToolStripMenuItem
            // 
            this.订阅WarnToolStripMenuItem.Name = "订阅WarnToolStripMenuItem";
            this.订阅WarnToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.订阅WarnToolStripMenuItem.Text = "订阅Warn";
            // 
            // 订阅DangerToolStripMenuItem
            // 
            this.订阅DangerToolStripMenuItem.Name = "订阅DangerToolStripMenuItem";
            this.订阅DangerToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.订阅DangerToolStripMenuItem.Text = "订阅Danger";
            // 
            // 取消订阅InfoToolStripMenuItem
            // 
            this.取消订阅InfoToolStripMenuItem.Name = "取消订阅InfoToolStripMenuItem";
            this.取消订阅InfoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.取消订阅InfoToolStripMenuItem.Text = "取消订阅Info";
            // 
            // 取消订阅AlarmToolStripMenuItem
            // 
            this.取消订阅AlarmToolStripMenuItem.Name = "取消订阅AlarmToolStripMenuItem";
            this.取消订阅AlarmToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.取消订阅AlarmToolStripMenuItem.Text = "取消订阅Alarm";
            // 
            // 取消订阅WarnToolStripMenuItem
            // 
            this.取消订阅WarnToolStripMenuItem.Name = "取消订阅WarnToolStripMenuItem";
            this.取消订阅WarnToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.取消订阅WarnToolStripMenuItem.Text = "取消订阅Warn";
            // 
            // 取消订阅DangerToolStripMenuItem
            // 
            this.取消订阅DangerToolStripMenuItem.Name = "取消订阅DangerToolStripMenuItem";
            this.取消订阅DangerToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.取消订阅DangerToolStripMenuItem.Text = "取消订阅Danger";
            // 
            // 取消订阅全部消息ToolStripMenuItem
            // 
            this.取消订阅全部消息ToolStripMenuItem.Name = "取消订阅全部消息ToolStripMenuItem";
            this.取消订阅全部消息ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.取消订阅全部消息ToolStripMenuItem.Text = "取消订阅全部消息";
            // 
            // taskInfoBindingSource
            // 
            this.taskInfoBindingSource.DataSource = typeof(UnitMonitorCommunication.TaskInfo);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.Frozen = true;
            this.nameDataGridViewTextBoxColumn.HeaderText = "任务名称";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pathDataGridViewTextBoxColumn
            // 
            this.pathDataGridViewTextBoxColumn.DataPropertyName = "Path";
            this.pathDataGridViewTextBoxColumn.HeaderText = "任务路径";
            this.pathDataGridViewTextBoxColumn.Name = "pathDataGridViewTextBoxColumn";
            this.pathDataGridViewTextBoxColumn.ReadOnly = true;
            this.pathDataGridViewTextBoxColumn.Width = 200;
            // 
            // moduleNameDataGridViewTextBoxColumn
            // 
            this.moduleNameDataGridViewTextBoxColumn.DataPropertyName = "ModuleName";
            this.moduleNameDataGridViewTextBoxColumn.HeaderText = "模块名称";
            this.moduleNameDataGridViewTextBoxColumn.Name = "moduleNameDataGridViewTextBoxColumn";
            this.moduleNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // clSubscribe
            // 
            this.clSubscribe.HeaderText = "订阅的消息";
            this.clSubscribe.Name = "clSubscribe";
            this.clSubscribe.ReadOnly = true;
            this.clSubscribe.Width = 200;
            // 
            // isRunningDataGridViewCheckBoxColumn
            // 
            this.isRunningDataGridViewCheckBoxColumn.DataPropertyName = "IsRunning";
            this.isRunningDataGridViewCheckBoxColumn.HeaderText = "是否运行";
            this.isRunningDataGridViewCheckBoxColumn.Name = "isRunningDataGridViewCheckBoxColumn";
            this.isRunningDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isRunningDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isRunningDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lastRunTimeDataGridViewTextBoxColumn
            // 
            this.lastRunTimeDataGridViewTextBoxColumn.DataPropertyName = "LastSuccessTime";
            this.lastRunTimeDataGridViewTextBoxColumn.HeaderText = "上次运行成功时间";
            this.lastRunTimeDataGridViewTextBoxColumn.Name = "lastRunTimeDataGridViewTextBoxColumn";
            this.lastRunTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // beginTimeDataGridViewTextBoxColumn
            // 
            this.beginTimeDataGridViewTextBoxColumn.DataPropertyName = "BeginTime";
            this.beginTimeDataGridViewTextBoxColumn.HeaderText = "开始运行时间";
            this.beginTimeDataGridViewTextBoxColumn.Name = "beginTimeDataGridViewTextBoxColumn";
            this.beginTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // runCountDataGridViewTextBoxColumn
            // 
            this.runCountDataGridViewTextBoxColumn.DataPropertyName = "RunCount";
            this.runCountDataGridViewTextBoxColumn.HeaderText = "运行次数";
            this.runCountDataGridViewTextBoxColumn.Name = "runCountDataGridViewTextBoxColumn";
            this.runCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // periodDataGridViewTextBoxColumn
            // 
            this.periodDataGridViewTextBoxColumn.DataPropertyName = "Period";
            this.periodDataGridViewTextBoxColumn.HeaderText = "运行周期（秒）";
            this.periodDataGridViewTextBoxColumn.Name = "periodDataGridViewTextBoxColumn";
            this.periodDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastSpendTimeDataGridViewTextBoxColumn
            // 
            this.lastSpendTimeDataGridViewTextBoxColumn.DataPropertyName = "LastSpendTime";
            this.lastSpendTimeDataGridViewTextBoxColumn.HeaderText = "上次花费时间（毫秒）";
            this.lastSpendTimeDataGridViewTextBoxColumn.Name = "lastSpendTimeDataGridViewTextBoxColumn";
            this.lastSpendTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastSuccessDataGridViewCheckBoxColumn
            // 
            this.lastSuccessDataGridViewCheckBoxColumn.DataPropertyName = "LastSuccess";
            this.lastSuccessDataGridViewCheckBoxColumn.HeaderText = "上次运行成功";
            this.lastSuccessDataGridViewCheckBoxColumn.Name = "lastSuccessDataGridViewCheckBoxColumn";
            this.lastSuccessDataGridViewCheckBoxColumn.ReadOnly = true;
            this.lastSuccessDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lastSuccessDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // successCountDataGridViewTextBoxColumn
            // 
            this.successCountDataGridViewTextBoxColumn.DataPropertyName = "SuccessCount";
            this.successCountDataGridViewTextBoxColumn.HeaderText = "总成功数";
            this.successCountDataGridViewTextBoxColumn.Name = "successCountDataGridViewTextBoxColumn";
            this.successCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // persistantSuccessDataGridViewTextBoxColumn
            // 
            this.persistantSuccessDataGridViewTextBoxColumn.DataPropertyName = "PersistantSuccess";
            this.persistantSuccessDataGridViewTextBoxColumn.HeaderText = "连续成功次数";
            this.persistantSuccessDataGridViewTextBoxColumn.Name = "persistantSuccessDataGridViewTextBoxColumn";
            this.persistantSuccessDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // persistantFailDataGridViewTextBoxColumn
            // 
            this.persistantFailDataGridViewTextBoxColumn.DataPropertyName = "PersistantFail";
            this.persistantFailDataGridViewTextBoxColumn.HeaderText = "连续失败次数";
            this.persistantFailDataGridViewTextBoxColumn.Name = "persistantFailDataGridViewTextBoxColumn";
            this.persistantFailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // failCountDataGridViewTextBoxColumn
            // 
            this.failCountDataGridViewTextBoxColumn.DataPropertyName = "FailCount";
            this.failCountDataGridViewTextBoxColumn.HeaderText = "总失败次数";
            this.failCountDataGridViewTextBoxColumn.Name = "failCountDataGridViewTextBoxColumn";
            this.failCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // TasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 382);
            this.Controls.Add(this.dgvTasks);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TasksForm";
            this.Text = "任务列表";
            this.Load += new System.EventHandler(this.TasksForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.taskInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.BindingSource taskInfoBindingSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 订阅全部消息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消订阅消息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 订阅WarnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 订阅DangerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 订阅InfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消订阅InfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消订阅AlarmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消订阅WarnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消订阅DangerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消订阅全部消息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbOnlineServers;
        private System.Windows.Forms.ToolStripComboBox cbAllTaskSelector;
        private System.Windows.Forms.ToolStripButton btnDataFilter;
        private System.Windows.Forms.ToolStripButton btnViewData;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn moduleNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clSubscribe;
        private System.Windows.Forms.DataGridViewTextBoxColumn isRunningDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastRunTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn beginTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn runCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn periodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastSpendTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastSuccessDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn successCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn persistantSuccessDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn persistantFailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn failCountDataGridViewTextBoxColumn;
    }
}
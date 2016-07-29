namespace UnitMonitorClient
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLinkedServers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolHistoryMessages = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.消息订阅ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.客户端设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolDelSelected = new System.Windows.Forms.ToolStripButton();
            this.toolDelAll = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dgvMessages = new System.Windows.Forms.DataGridView();
            this.clOccurTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clMessageType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clSenderServer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clSenderTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolClearMessages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDelMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolHisMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolService = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExit = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTasks = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.状态ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(685, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "文件";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打印ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打印ToolStripMenuItem
            // 
            this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            this.打印ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打印ToolStripMenuItem.Text = "打印";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 状态ToolStripMenuItem
            // 
            this.状态ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLinkedServers,
            this.toolHistoryMessages,
            this.toolTasks});
            this.状态ToolStripMenuItem.Name = "状态ToolStripMenuItem";
            this.状态ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.状态ToolStripMenuItem.Text = "视图";
            // 
            // toolLinkedServers
            // 
            this.toolLinkedServers.Name = "toolLinkedServers";
            this.toolLinkedServers.Size = new System.Drawing.Size(160, 22);
            this.toolLinkedServers.Text = "连接的服务";
            this.toolLinkedServers.Click += new System.EventHandler(this.toolLinkedServers_Click);
            // 
            // toolHistoryMessages
            // 
            this.toolHistoryMessages.Name = "toolHistoryMessages";
            this.toolHistoryMessages.Size = new System.Drawing.Size(160, 22);
            this.toolHistoryMessages.Text = "历史消息";
            this.toolHistoryMessages.Click += new System.EventHandler(this.toolHistoryMessages_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.消息订阅ToolStripMenuItem,
            this.客户端设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 消息订阅ToolStripMenuItem
            // 
            this.消息订阅ToolStripMenuItem.Name = "消息订阅ToolStripMenuItem";
            this.消息订阅ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.消息订阅ToolStripMenuItem.Text = "消息订阅";
            // 
            // 客户端设置ToolStripMenuItem
            // 
            this.客户端设置ToolStripMenuItem.Name = "客户端设置ToolStripMenuItem";
            this.客户端设置ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.客户端设置ToolStripMenuItem.Text = "客户端设置";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDelSelected,
            this.toolDelAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(685, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolDelSelected
            // 
            this.toolDelSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDelSelected.Image = ((System.Drawing.Image)(resources.GetObject("toolDelSelected.Image")));
            this.toolDelSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelSelected.Name = "toolDelSelected";
            this.toolDelSelected.Size = new System.Drawing.Size(23, 22);
            this.toolDelSelected.Text = "删除选定";
            this.toolDelSelected.Click += new System.EventHandler(this.toolDelSelected_Click);
            // 
            // toolDelAll
            // 
            this.toolDelAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDelAll.Image = ((System.Drawing.Image)(resources.GetObject("toolDelAll.Image")));
            this.toolDelAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelAll.Name = "toolDelAll";
            this.toolDelAll.Size = new System.Drawing.Size(23, 22);
            this.toolDelAll.Text = "删除全部";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 386);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(685, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dgvMessages
            // 
            this.dgvMessages.AllowUserToDeleteRows = false;
            this.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clOccurTime,
            this.clMessageType,
            this.clMessage,
            this.clSenderServer,
            this.clSenderTask});
            this.dgvMessages.ContextMenuStrip = this.contextMenuStrip2;
            this.dgvMessages.Location = new System.Drawing.Point(0, 50);
            this.dgvMessages.Name = "dgvMessages";
            this.dgvMessages.ReadOnly = true;
            this.dgvMessages.RowTemplate.Height = 23;
            this.dgvMessages.Size = new System.Drawing.Size(685, 336);
            this.dgvMessages.TabIndex = 3;
            // 
            // clOccurTime
            // 
            this.clOccurTime.HeaderText = "时间";
            this.clOccurTime.Name = "clOccurTime";
            this.clOccurTime.ReadOnly = true;
            // 
            // clMessageType
            // 
            this.clMessageType.HeaderText = "消息类型";
            this.clMessageType.Name = "clMessageType";
            this.clMessageType.ReadOnly = true;
            // 
            // clMessage
            // 
            this.clMessage.HeaderText = "消息内容";
            this.clMessage.Name = "clMessage";
            this.clMessage.ReadOnly = true;
            this.clMessage.Width = 500;
            // 
            // clSenderServer
            // 
            this.clSenderServer.HeaderText = "发送服务器";
            this.clSenderServer.Name = "clSenderServer";
            this.clSenderServer.ReadOnly = true;
            // 
            // clSenderTask
            // 
            this.clSenderTask.HeaderText = "所属任务";
            this.clSenderTask.Name = "clSenderTask";
            this.clSenderTask.ReadOnly = true;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolClearMessages,
            this.toolDelMessage});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 48);
            // 
            // toolClearMessages
            // 
            this.toolClearMessages.Name = "toolClearMessages";
            this.toolClearMessages.Size = new System.Drawing.Size(124, 22);
            this.toolClearMessages.Text = "清空消息";
            this.toolClearMessages.Click += new System.EventHandler(this.toolClearMessages_Click);
            // 
            // toolDelMessage
            // 
            this.toolDelMessage.Name = "toolDelMessage";
            this.toolDelMessage.Size = new System.Drawing.Size(124, 22);
            this.toolDelMessage.Text = "删除";
            this.toolDelMessage.Click += new System.EventHandler(this.toolDelMessage_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMessage,
            this.ToolHisMessage,
            this.toolService,
            this.toolSetting,
            this.toolExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 114);
            // 
            // toolMessage
            // 
            this.toolMessage.Name = "toolMessage";
            this.toolMessage.Size = new System.Drawing.Size(124, 22);
            this.toolMessage.Text = "消息";
            // 
            // ToolHisMessage
            // 
            this.ToolHisMessage.Name = "ToolHisMessage";
            this.ToolHisMessage.Size = new System.Drawing.Size(124, 22);
            this.ToolHisMessage.Text = "历史消息";
            // 
            // toolService
            // 
            this.toolService.Name = "toolService";
            this.toolService.Size = new System.Drawing.Size(124, 22);
            this.toolService.Text = "服务端";
            // 
            // toolSetting
            // 
            this.toolSetting.Name = "toolSetting";
            this.toolSetting.Size = new System.Drawing.Size(124, 22);
            this.toolSetting.Text = "设置";
            // 
            // toolExit
            // 
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(124, 22);
            this.toolExit.Text = "退出";
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolTasks
            // 
            this.toolTasks.Name = "toolTasks";
            this.toolTasks.Size = new System.Drawing.Size(160, 22);
            this.toolTasks.Text = "任务列表及订阅";
            this.toolTasks.Click += new System.EventHandler(this.toolTasks_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 408);
            this.Controls.Add(this.dgvMessages);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dgvMessages;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 状态ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolLinkedServers;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolMessage;
        private System.Windows.Forms.ToolStripMenuItem ToolHisMessage;
        private System.Windows.Forms.ToolStripMenuItem toolService;
        private System.Windows.Forms.ToolStripMenuItem toolSetting;
        private System.Windows.Forms.ToolStripMenuItem toolExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clOccurTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMessageType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn clSenderServer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clSenderTask;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolClearMessages;
        private System.Windows.Forms.ToolStripMenuItem toolDelMessage;
        private System.Windows.Forms.ToolStripMenuItem toolHistoryMessages;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 消息订阅ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 客户端设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolDelSelected;
        private System.Windows.Forms.ToolStripButton toolDelAll;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem toolTasks;
    }
}
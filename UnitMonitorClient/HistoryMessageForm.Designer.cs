namespace UnitMonitorClient
{
    partial class HistoryMessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryMessageForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbOnlineServers = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbHistoryFile = new System.Windows.Forms.ToolStripComboBox();
            this.btnViewMessages = new System.Windows.Forms.ToolStripButton();
            this.dgvMessages = new System.Windows.Forms.DataGridView();
            this.btnSelectMessageType = new System.Windows.Forms.ToolStripButton();
            this.occurTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.senderUrlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(740, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cbOnlineServers,
            this.toolStripLabel2,
            this.cbHistoryFile,
            this.btnSelectMessageType,
            this.btnViewMessages});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(740, 25);
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
            this.cbOnlineServers.SelectedIndexChanged += new System.EventHandler(this.cbOnlineServers_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(116, 22);
            this.toolStripLabel2.Text = "选择历史记录文件：";
            // 
            // cbHistoryFile
            // 
            this.cbHistoryFile.Name = "cbHistoryFile";
            this.cbHistoryFile.Size = new System.Drawing.Size(121, 25);
            // 
            // btnViewMessages
            // 
            this.btnViewMessages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewMessages.Image = ((System.Drawing.Image)(resources.GetObject("btnViewMessages.Image")));
            this.btnViewMessages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewMessages.Name = "btnViewMessages";
            this.btnViewMessages.Size = new System.Drawing.Size(23, 22);
            this.btnViewMessages.Text = "显示记录";
            this.btnViewMessages.Click += new System.EventHandler(this.btnViewMessages_Click);
            // 
            // dgvMessages
            // 
            this.dgvMessages.AutoGenerateColumns = false;
            this.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.occurTimeDataGridViewTextBoxColumn,
            this.messageTypeDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn,
            this.senderUrlDataGridViewTextBoxColumn,
            this.taskPathDataGridViewTextBoxColumn});
            this.dgvMessages.DataSource = this.messageInfoBindingSource;
            this.dgvMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMessages.Location = new System.Drawing.Point(0, 49);
            this.dgvMessages.Name = "dgvMessages";
            this.dgvMessages.RowTemplate.Height = 23;
            this.dgvMessages.Size = new System.Drawing.Size(740, 300);
            this.dgvMessages.TabIndex = 2;
            // 
            // btnSelectMessageType
            // 
            this.btnSelectMessageType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectMessageType.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectMessageType.Image")));
            this.btnSelectMessageType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectMessageType.Name = "btnSelectMessageType";
            this.btnSelectMessageType.Size = new System.Drawing.Size(23, 22);
            this.btnSelectMessageType.Text = "筛选";
            this.btnSelectMessageType.Click += new System.EventHandler(this.btnSelectMessageType_Click);
            // 
            // occurTimeDataGridViewTextBoxColumn
            // 
            this.occurTimeDataGridViewTextBoxColumn.DataPropertyName = "OccurTime";
            this.occurTimeDataGridViewTextBoxColumn.HeaderText = "发生时间";
            this.occurTimeDataGridViewTextBoxColumn.Name = "occurTimeDataGridViewTextBoxColumn";
            this.occurTimeDataGridViewTextBoxColumn.Width = 150;
            // 
            // messageTypeDataGridViewTextBoxColumn
            // 
            this.messageTypeDataGridViewTextBoxColumn.DataPropertyName = "MessageType";
            this.messageTypeDataGridViewTextBoxColumn.HeaderText = "消息类型";
            this.messageTypeDataGridViewTextBoxColumn.Name = "messageTypeDataGridViewTextBoxColumn";
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "消息内容";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.Width = 300;
            // 
            // senderUrlDataGridViewTextBoxColumn
            // 
            this.senderUrlDataGridViewTextBoxColumn.DataPropertyName = "SenderUrl";
            this.senderUrlDataGridViewTextBoxColumn.HeaderText = "发送服务器";
            this.senderUrlDataGridViewTextBoxColumn.Name = "senderUrlDataGridViewTextBoxColumn";
            // 
            // taskPathDataGridViewTextBoxColumn
            // 
            this.taskPathDataGridViewTextBoxColumn.DataPropertyName = "TaskPath";
            this.taskPathDataGridViewTextBoxColumn.HeaderText = "任务路径";
            this.taskPathDataGridViewTextBoxColumn.Name = "taskPathDataGridViewTextBoxColumn";
            // 
            // messageInfoBindingSource
            // 
            this.messageInfoBindingSource.DataSource = typeof(UnitMonitorCommunication.MessageInfo);
            // 
            // HistoryMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 349);
            this.Controls.Add(this.dgvMessages);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "HistoryMessageForm";
            this.Text = "历史消息";
            this.Load += new System.EventHandler(this.HistoryMessageForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbOnlineServers;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cbHistoryFile;
        private System.Windows.Forms.ToolStripButton btnViewMessages;
        private System.Windows.Forms.DataGridView dgvMessages;
        private System.Windows.Forms.DataGridViewTextBoxColumn occurTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn senderUrlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource messageInfoBindingSource;
        private System.Windows.Forms.ToolStripButton btnSelectMessageType;
    }
}
namespace UnitMonitorClient
{
    partial class ServersForm
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLink = new System.Windows.Forms.ToolStripMenuItem();
            this.toolCancelLink = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvServers = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ipDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOnlineDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sendMessageCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onlineTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serversBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serversBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNew,
            this.toolDel,
            this.toolLink,
            this.toolCancelLink});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolNew
            // 
            this.toolNew.Name = "toolNew";
            this.toolNew.Size = new System.Drawing.Size(152, 22);
            this.toolNew.Text = "添加";
            this.toolNew.Click += new System.EventHandler(this.toolNew_Click);
            // 
            // toolDel
            // 
            this.toolDel.Name = "toolDel";
            this.toolDel.Size = new System.Drawing.Size(152, 22);
            this.toolDel.Text = "删除";
            this.toolDel.Click += new System.EventHandler(this.toolDel_Click);
            // 
            // toolLink
            // 
            this.toolLink.Name = "toolLink";
            this.toolLink.Size = new System.Drawing.Size(152, 22);
            this.toolLink.Text = "连接";
            this.toolLink.Click += new System.EventHandler(this.toolLink_Click);
            // 
            // toolCancelLink
            // 
            this.toolCancelLink.Name = "toolCancelLink";
            this.toolCancelLink.Size = new System.Drawing.Size(152, 22);
            this.toolCancelLink.Text = "取消连接";
            this.toolCancelLink.Click += new System.EventHandler(this.toolCancelLink_Click);
            // 
            // dgvServers
            // 
            this.dgvServers.AutoGenerateColumns = false;
            this.dgvServers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.ipDataGridViewTextBoxColumn,
            this.portDataGridViewTextBoxColumn,
            this.IsOnlineDataGridViewTextBoxColumn,
            this.sendMessageCountDataGridViewTextBoxColumn,
            this.onlineTimeDataGridViewTextBoxColumn});
            this.dgvServers.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvServers.DataSource = this.serversBindingSource;
            this.dgvServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServers.Location = new System.Drawing.Point(0, 0);
            this.dgvServers.Name = "dgvServers";
            this.dgvServers.ReadOnly = true;
            this.dgvServers.RowTemplate.Height = 23;
            this.dgvServers.Size = new System.Drawing.Size(523, 313);
            this.dgvServers.TabIndex = 1;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "主机名称";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // ipDataGridViewTextBoxColumn
            // 
            this.ipDataGridViewTextBoxColumn.DataPropertyName = "Ip";
            this.ipDataGridViewTextBoxColumn.HeaderText = "IP地址";
            this.ipDataGridViewTextBoxColumn.Name = "ipDataGridViewTextBoxColumn";
            this.ipDataGridViewTextBoxColumn.ReadOnly = true;
            this.ipDataGridViewTextBoxColumn.Width = 150;
            // 
            // portDataGridViewTextBoxColumn
            // 
            this.portDataGridViewTextBoxColumn.DataPropertyName = "Port";
            this.portDataGridViewTextBoxColumn.HeaderText = "端口";
            this.portDataGridViewTextBoxColumn.Name = "portDataGridViewTextBoxColumn";
            this.portDataGridViewTextBoxColumn.ReadOnly = true;
            this.portDataGridViewTextBoxColumn.Width = 60;
            // 
            // IsOnlineDataGridViewTextBoxColumn
            // 
            this.IsOnlineDataGridViewTextBoxColumn.DataPropertyName = "IsOnline";
            this.IsOnlineDataGridViewTextBoxColumn.HeaderText = "是否在线";
            this.IsOnlineDataGridViewTextBoxColumn.Name = "IsOnlineDataGridViewTextBoxColumn";
            this.IsOnlineDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sendMessageCountDataGridViewTextBoxColumn
            // 
            this.sendMessageCountDataGridViewTextBoxColumn.DataPropertyName = "SendMessageCount";
            this.sendMessageCountDataGridViewTextBoxColumn.HeaderText = "消息数";
            this.sendMessageCountDataGridViewTextBoxColumn.Name = "sendMessageCountDataGridViewTextBoxColumn";
            this.sendMessageCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.sendMessageCountDataGridViewTextBoxColumn.Width = 80;
            // 
            // onlineTimeDataGridViewTextBoxColumn
            // 
            this.onlineTimeDataGridViewTextBoxColumn.DataPropertyName = "OnlineTime";
            this.onlineTimeDataGridViewTextBoxColumn.HeaderText = "上线时间";
            this.onlineTimeDataGridViewTextBoxColumn.Name = "onlineTimeDataGridViewTextBoxColumn";
            this.onlineTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serversBindingSource
            // 
            this.serversBindingSource.DataSource = typeof(UnitMonitorClient.Servers);
            // 
            // ServersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 313);
            this.Controls.Add(this.dgvServers);
            this.Name = "ServersForm";
            this.Text = "连接服务器";
            this.Load += new System.EventHandler(this.ServersForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serversBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolDel;
        private System.Windows.Forms.ToolStripMenuItem toolLink;
        private System.Windows.Forms.ToolStripMenuItem toolNew;
        private System.Windows.Forms.DataGridView dgvServers;
       
        private System.Windows.Forms.BindingSource serversBindingSource;
        private System.Windows.Forms.ToolStripMenuItem toolCancelLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ipDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn portDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsOnlineDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sendMessageCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn onlineTimeDataGridViewTextBoxColumn;
    }
}
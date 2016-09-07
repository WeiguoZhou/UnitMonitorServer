namespace UnitMonitorServer.Components.WCFService
{
    partial class ClientsForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dgvClients = new System.Windows.Forms.DataGridView();
            this.ipDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onlineTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sendMessageCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolDelClient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLink = new System.Windows.Forms.ToolStripMenuItem();
            this.toolUnLink = new System.Windows.Forms.ToolStripMenuItem();
            this.clientsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(672, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(672, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // dgvClients
            // 
            this.dgvClients.AutoGenerateColumns = false;
            this.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ipDataGridViewTextBoxColumn,
            this.portDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.onlineTimeDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.sendMessageCountDataGridViewTextBoxColumn});
            this.dgvClients.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvClients.DataSource = this.clientsBindingSource;
            this.dgvClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClients.Location = new System.Drawing.Point(0, 25);
            this.dgvClients.Name = "dgvClients";
            this.dgvClients.ReadOnly = true;
            this.dgvClients.RowTemplate.Height = 23;
            this.dgvClients.Size = new System.Drawing.Size(672, 391);
            this.dgvClients.TabIndex = 2;
            this.dgvClients.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvClients_CellFormatting);
            this.dgvClients.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvClients_CellValueNeeded);
            this.dgvClients.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvClients_RowValidating);
            this.dgvClients.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvClients_UserAddedRow);
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
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "主机名称";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // onlineTimeDataGridViewTextBoxColumn
            // 
            this.onlineTimeDataGridViewTextBoxColumn.DataPropertyName = "OnlineTime";
            this.onlineTimeDataGridViewTextBoxColumn.HeaderText = "上线时间";
            this.onlineTimeDataGridViewTextBoxColumn.Name = "onlineTimeDataGridViewTextBoxColumn";
            this.onlineTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.onlineTimeDataGridViewTextBoxColumn.Width = 150;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "服务状态";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sendMessageCountDataGridViewTextBoxColumn
            // 
            this.sendMessageCountDataGridViewTextBoxColumn.DataPropertyName = "SendMessageCount";
            this.sendMessageCountDataGridViewTextBoxColumn.HeaderText = "消息数";
            this.sendMessageCountDataGridViewTextBoxColumn.Name = "sendMessageCountDataGridViewTextBoxColumn";
            this.sendMessageCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDelClient,
            this.toolLink,
            this.toolUnLink});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolDelClient
            // 
            this.toolDelClient.Name = "toolDelClient";
            this.toolDelClient.Size = new System.Drawing.Size(124, 22);
            this.toolDelClient.Text = "删除";
            this.toolDelClient.Click += new System.EventHandler(this.toolDelClient_Click);
            // 
            // toolLink
            // 
            this.toolLink.Name = "toolLink";
            this.toolLink.Size = new System.Drawing.Size(124, 22);
            this.toolLink.Text = "连接";
            this.toolLink.Click += new System.EventHandler(this.toolLink_Click);
            // 
            // toolUnLink
            // 
            this.toolUnLink.Name = "toolUnLink";
            this.toolUnLink.Size = new System.Drawing.Size(124, 22);
            this.toolUnLink.Text = "断开连接";
            this.toolUnLink.Click += new System.EventHandler(this.toolUnLink_Click);
            // 
            // clientsBindingSource
            // 
            this.clientsBindingSource.DataSource = typeof(Clients);
            // 
            // ClientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 416);
            this.Controls.Add(this.dgvClients);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ClientsForm";
            this.Text = "连接客户端";
            this.Load += new System.EventHandler(this.ClientsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clientsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgvClients;
        private System.Windows.Forms.DataGridViewTextBoxColumn ipDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn portDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn onlineTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isOnLineDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sendMessageCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource clientsBindingSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolDelClient;
        private System.Windows.Forms.ToolStripMenuItem toolLink;
        private System.Windows.Forms.ToolStripMenuItem toolUnLink;
    }
}
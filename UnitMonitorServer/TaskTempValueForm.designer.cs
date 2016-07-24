namespace UnitMonitorServer
{
    partial class TaskTempValueForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dgvTempValues = new System.Windows.Forms.DataGridView();
            this.clKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTempValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTempValues)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(632, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // dgvTempValues
            // 
            this.dgvTempValues.AllowUserToAddRows = false;
            this.dgvTempValues.AllowUserToDeleteRows = false;
            this.dgvTempValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTempValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clKey,
            this.clTempValue});
            this.dgvTempValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTempValues.Location = new System.Drawing.Point(0, 49);
            this.dgvTempValues.Name = "dgvTempValues";
            this.dgvTempValues.ReadOnly = true;
            this.dgvTempValues.RowTemplate.Height = 23;
            this.dgvTempValues.Size = new System.Drawing.Size(632, 321);
            this.dgvTempValues.TabIndex = 2;
            // 
            // clKey
            // 
            this.clKey.HeaderText = "临时数据键值";
            this.clKey.Name = "clKey";
            this.clKey.ReadOnly = true;
            this.clKey.Width = 300;
            // 
            // clTempValue
            // 
            this.clTempValue.HeaderText = "临时数据值";
            this.clTempValue.Name = "clTempValue";
            this.clTempValue.ReadOnly = true;
            this.clTempValue.Width = 200;
            // 
            // TaskTempValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 370);
            this.Controls.Add(this.dgvTempValues);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TaskTempValueForm";
            this.Text = "TaskTempValueForm";
            this.Load += new System.EventHandler(this.TaskTempValueForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTempValues)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgvTempValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn clKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTempValue;
    }
}
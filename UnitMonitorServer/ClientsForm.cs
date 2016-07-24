using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitMonitorServer
{
    public partial class ClientsForm : Form
    {
        public ClientsForm()
        {
            InitializeComponent();
        }

        private void ClientsForm_Load(object sender, EventArgs e)
        {
            clientsBindingSource.DataSource = Clients.Instance;

        }



        private void dgvClients_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string value = dgvClients.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                try
                {
                    IPAddress ip = IPAddress.Parse(value);
                }
                catch
                {
                    e.Cancel = true;
                }

            }
            if (e.ColumnIndex == 1)
            {
                string value = dgvClients.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                int port;
                if (!int.TryParse(value, out port))
                    e.Cancel = true;
                else
                {
                    if (port < 21 || port >= 65535)
                        e.Cancel = true;
                }


            }
        }



        private void dgvClients_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.ColumnIndex == 1 && dgvClients.IsCurrentCellInEditMode)
            {
                if (string.IsNullOrEmpty(e.Value.ToString()))
                    e.Value = "1904";
            }
        }

        private void dgvClients_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                string ip = e.Row.Cells[0].Value.ToString();
                int port = int.Parse(e.Row.Cells[1].Value.ToString());              
                Clients.Instance.RegClient(ip, port);
               

            }
            catch { }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                ClientInfo inf = Clients.Instance[dgvClients.SelectedRows[0].Index];
                if (inf != null)
                {
                    toolDelClient.Enabled = true;
                    toolLink.Enabled = !inf.IsOnline;
                    toolUnLink.Enabled = inf.IsOnline;
                }
                else
                    toolDelClient.Enabled = false;
            }
            else
                e.Cancel = true;
        }

        private void toolDelClient_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvClients.SelectedRows)
                {
                    ClientInfo inf = Clients.Instance[row.Index];
                    if (inf != null)
                    {
                        Clients.Instance.DelClient(inf.Ip);
                        Clients.Instance.Remove(inf);
                    }
                }

            }
        }

        private void toolLink_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvClients.SelectedRows)
                {
                    ClientInfo inf = Clients.Instance[row.Index];
                    if (inf != null && !inf.IsOnline)
                    {
                        inf.TryLink();
                        if (inf.IsOnline)
                            MessageBox.Show("连接成功！");
                        else
                            MessageBox.Show("连接失败！");
                    }


                }
            }
        }

        private void toolUnLink_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvClients.SelectedRows)
                {
                    ClientInfo inf = Clients.Instance[row.Index];
                    if (inf != null && !inf.IsOnline)
                    {
                        inf.TurnOffLine();
                    }
                }
            }
        }

        private void dgvClients_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           if(e.ColumnIndex==3) 
            {
                if(((DateTime)e.Value == DateTime.MinValue))
                    e.Value = "";
            }
        }
    }
}

        
    


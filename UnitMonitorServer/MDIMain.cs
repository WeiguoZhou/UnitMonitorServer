using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitMonitorCommon;


namespace UnitMonitorServer
{
    public partial class MDIMain : Form
    {
        private int childFormNumber = 0;

        public MDIMain()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// 暴露组件菜单，使一些组件能自动添加菜单项
        /// </summary>
        public ToolStripMenuItem ComponentMenu {
            get
            {
                return toolComponents;
            }
        }
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "窗口 " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void menuTask_Click(object sender, EventArgs e)
        {

        }

        private void MDIMain_Load(object sender, EventArgs e)
        {




        }

        private void toolTaskContainerForm_Click(object sender, EventArgs e)
        {
            TaskContainerForm frm = new TaskContainerForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void toolClientsForm_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 合并工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDIMain_MdiChildActivate(object sender, EventArgs e)
        {
            ToolStripManager.RevertMerge(toolStrip);

            if (this.ActiveMdiChild == null) return;
            if (!(ActiveMdiChild is IMergeToolStrip))
                return;
            if ((ActiveMdiChild as IMergeToolStrip).MergeToolStrip == null) return;

            ToolStripManager.Merge((ActiveMdiChild as IMergeToolStrip).MergeToolStrip, toolStrip);

            if (toolStrip.Items.Count > 0)
                toolStrip.Visible = true;
            else
                toolStrip.Visible = false;
        }
    }
}

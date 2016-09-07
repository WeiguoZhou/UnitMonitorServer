using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using UnitMonitorCommon;

namespace UnitMonitorServer.Components.statistics
{
    public class StatisticsManager : ComponentBase
    {



        public bool TaskStatisticsUsed { set; get; } = false;
        public bool TasksContainerStatisticsUsed { set; get; } = false;
        public List<TaskStatistics> TaskStatisticsList { private set; get; }
        public TasksContainerStatistics TasksContainerStatistics { private set; get; }



        public override void Init()
        {


                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigFile);
                XmlNode node = doc.GetElementsByTagName("taskStatistics")[0];
                TaskStatisticsUsed = Convert.ToBoolean(node.Attributes["enabled"].Value);
                if (TaskStatisticsUsed)
                {
                    if (TaskStatisticsList == null)
                        TaskStatisticsList = new List<TaskStatistics>();
                    foreach (var item in TasksContainer.Instance)
                    {
                        TaskStatistics statistics = new TaskStatistics(item);
                        TaskStatisticsList.Add(statistics);
                    }
                    TasksContainer.Instance.TaskAdded += this.OnTaskAdded;
                    TasksContainer.Instance.TaskRemoved += this.OnTaskRemoved;
                }
                node = doc.GetElementsByTagName("tasksContainerStatistics")[0];
                TasksContainerStatisticsUsed = Convert.ToBoolean(node.Attributes["enabled"].Value);
                if (TasksContainerStatisticsUsed)
                    TasksContainerStatistics = new TasksContainerStatistics(TasksContainer.Instance);


        }

        private void OnTaskRemoved(object sender, EventArgs e)
        {
            TaskBase task = (TaskBase)sender;
            TaskStatistics statistics = FindTaskStatistics(task);
            if (statistics != null)
            {
                statistics.UnLoad();
                TaskStatisticsList.Remove(statistics);
            }

        }
        public TaskStatistics FindTaskStatistics(TaskBase task)
        {
            if ((TaskStatisticsList == null) || (task == null))
                return null;
            foreach (var item in TaskStatisticsList)
            {
                if (item.Task == task)
                    return item;
            }
            return null;
        }
        private void OnTaskAdded(object sender, EventArgs e)
        {
            TaskBase task = (TaskBase)sender;
            TaskStatistics statistics = new TaskStatistics(task);
            TaskStatisticsList.Add(statistics);
        }
        private void TaskStatisticsUnload()
        {
            TasksContainer.Instance.TaskAdded -= this.OnTaskAdded;
            TasksContainer.Instance.TaskRemoved -= this.OnTaskRemoved;
            foreach (var item in TaskStatisticsList)
            {
                item.UnLoad();
            }
            TaskStatisticsList.Clear();
        }
        public override void UnLoad()
        {
            TaskStatisticsUnload();

        }
        public override void SetMenu(ToolStripMenuItem compMenu)
        {
            ToolStripMenuItem configMenu = this.Container.CompConfigMenu;
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = "统计组件配置";
            menuItem.Click += new System.EventHandler(this.ShowStatisticsConfig);
            configMenu.DropDownItems.Add(menuItem);

        }

        private void ShowStatisticsConfig(object sender, EventArgs e)
        {
            StatisticsConfigForm form = new StatisticsConfigForm(this);
            if (this.Container.MDIForm != null)
                form.MdiParent = this.Container.MDIForm;
            form.Show();

        }
    }
}

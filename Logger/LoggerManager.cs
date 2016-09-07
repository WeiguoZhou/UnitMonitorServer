using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace UnitMonitorServer.Components.Logger
{
    class LoggerManager : ComponentBase
    {
        bool taskMessageEnabled = false;
        bool taskDebugEnabled = false;
        bool componentDebugEnabled = false;
        /// <summary>
        /// 是否开启记录任务消息
        /// </summary>
        public bool TaskMessageEnabled
        {
            private set
            {
                if (value != taskMessageEnabled)
                {
                    taskMessageEnabled = value;
                    if (taskMessageEnabled)
                    {
                        foreach (var item in TasksContainer.Instance)
                        {
                            item.SendMessage += LogTaskMessage;
                        }
                        TasksContainer.Instance.TaskAdded += SubscribeTaskMessage;
                        TasksContainer.Instance.TaskRemoved += UnSubscribeTaskMessage;
                    }
                    else
                    {
                        foreach (var item in TasksContainer.Instance)
                        {
                            item.SendMessage -= LogTaskMessage;
                        }
                        TasksContainer.Instance.TaskAdded -= SubscribeTaskMessage;
                        TasksContainer.Instance.TaskRemoved -= UnSubscribeTaskMessage;
                    }
                }

            }
            get
            {
                return taskMessageEnabled;
            }
        }
        /// <summary>
        /// 是否开启记录任务错误
        /// </summary>
        public bool TaskDebugEnabled
        {
            private set
            {
                if (taskDebugEnabled != value)
                {
                    taskDebugEnabled = value;
                    if (taskDebugEnabled)
                    {
                        foreach (var item in TasksContainer.Instance)
                        {
                            item.AfterPreInitFailed += OnTaskPreInitFailed;
                            item.AfterInitDataFailed += OnTaskInitDataFailed;
                            item.AfterProcessFailed += OnTaskProcessFailed;
                        }
                        TasksContainer.Instance.TaskAdded += SubscribeTaskDebug;
                        TasksContainer.Instance.TaskRemoved += UnSubscribeTaskDebug;
                    }
                    else
                    {
                        foreach (var item in TasksContainer.Instance)
                        {
                            item.AfterPreInitFailed -= OnTaskPreInitFailed;
                            item.AfterInitDataFailed -= OnTaskInitDataFailed;
                            item.AfterProcessFailed -= OnTaskProcessFailed;
                        }
                        TasksContainer.Instance.TaskAdded -= SubscribeTaskDebug;
                        TasksContainer.Instance.TaskRemoved -= UnSubscribeTaskDebug;
                    }
                }
            }

            get
            {
                return taskDebugEnabled;
            }

        }
        /// <summary>
        /// 是否开启记录组件错误
        /// </summary>
        public bool ComponentDebugEnabled
        {
            private set
            {
                if (componentDebugEnabled != value)
                {
                    componentDebugEnabled = value;
                    if (componentDebugEnabled)
                        ComponentContainer.Instance.ComponentFailed += OnComponentFailed;
                    else
                        ComponentContainer.Instance.ComponentFailed -= OnComponentFailed;
                }
            }

            get
            {
                return componentDebugEnabled;

            }
        }

        public override void Init()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.ConfigFile);
            XmlNode loggerNode = doc.GetElementsByTagName("logger")[0];

            TaskMessageEnabled = Convert.ToBoolean(loggerNode.Attributes["taskMessageEnabled"].Value);
            TaskDebugEnabled = Convert.ToBoolean(loggerNode.Attributes["taskDebugEnabled"].Value);
            ComponentDebugEnabled = Convert.ToBoolean(loggerNode.Attributes["componentDebugEnabled"].Value);


        }

        private void OnComponentFailed(string componentName, Exception ex)
        {
            ComponentDebugLogger logger = new ComponentDebugLogger();
            logger.LogComponentDebug(componentName, ex);
        }

        private void UnSubscribeTaskDebug(object sender, EventArgs e)
        {
            TaskBase task = (TaskBase)sender;
            task.AfterPreInitFailed -= OnTaskPreInitFailed;
            task.AfterInitDataFailed -= OnTaskInitDataFailed;
            task.AfterProcessFailed -= OnTaskProcessFailed;
        }

        private void SubscribeTaskDebug(object sender, EventArgs e)
        {
            TaskBase task = (TaskBase)sender;
            task.AfterPreInitFailed += OnTaskPreInitFailed;
            task.AfterInitDataFailed += OnTaskInitDataFailed;
            task.AfterProcessFailed += OnTaskProcessFailed;
        }

        private void OnTaskProcessFailed(TaskBase task, Exception ex)
        {
            TaskDebugLogger logger = new TaskDebugLogger();
            logger.LogTaskDebug(task, ex, "Process");
        }

        private void OnTaskInitDataFailed(TaskBase task, Exception ex)
        {
            TaskDebugLogger logger = new TaskDebugLogger();
            logger.LogTaskDebug(task, ex, "InitData");
        }

        private void OnTaskPreInitFailed(TaskBase task, Exception ex)
        {
            TaskDebugLogger logger = new TaskDebugLogger();
            logger.LogTaskDebug(task, ex, "PreInit");
        }

        private void UnSubscribeTaskMessage(object sender, EventArgs e)
        {
            TaskBase task = (TaskBase)sender;
            task.SendMessage -= LogTaskMessage;
        }

        private void SubscribeTaskMessage(object sender, EventArgs e)
        {
            TaskBase task = (TaskBase)sender;
            task.SendMessage += LogTaskMessage;
        }

        public override void UnLoad()
        {
            base.UnLoad();

            foreach (ToolStripMenuItem item in this.Container.CompConfigMenu.DropDownItems)
            {
                if (item.Text == "Logger组件配置")
                {
                    this.Container.CompConfigMenu.DropDownItems.Remove(item);
                    break;
                }
            }
 

        }
        private void LogTaskMessage(MessageInfo info)
        {
            TaskMessageLogger logger = new TaskMessageLogger();
            logger.LogMessageInfo(info);
        }
        public override void SetMenu(ToolStripMenuItem compMenu)
        {
            ToolStripMenuItem configMenu = this.Container.CompConfigMenu;
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = "Logger组件配置";
            menuItem.Click += new System.EventHandler(this.ShowLoggerConfig);
            configMenu.DropDownItems.Add(menuItem);
        }

        private void ShowLoggerConfig(object sender, EventArgs e)
        {
            LoggerConfigForm form = new LoggerConfigForm();
            if (this.Container.MDIForm != null)
                form.MdiParent = this.Container.MDIForm;
            form.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitMonitorCommon;

namespace UnitMonitorServer
{
    static class Program
    {
        public static event SetText SetStatusText;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TasksContainer.Init();
            Thread thread = new Thread(new ThreadStart(ShowSplash));
            thread.Start();
            RaiseSetStatusText("正在加载主窗体，请稍候...");
            MDIMain mainForm = new MDIMain();
            RaiseSetStatusText("正在加载组件，请稍候...");
            ComponentContainer.Init(mainForm);
            ComponentContainer.Instance.SetMenu(mainForm.ComponentMenu);
           RaiseSetStatusText("正在加载并启动任务，请稍候...");
            //加载所有任务并启动TasksContainer
            TasksContainer.Instance.LoadTasks();
            thread.Abort();
            thread.Join();
            thread = null;
            Application.Run(mainForm);
        }
        private static void RaiseSetStatusText(string statusText)
        {
            if (SetStatusText != null)
                SetStatusText(statusText);
        }
        private static void ShowSplash()
        {
            Form sForm;
            try
            {
                sForm = new WelcomeForm();
                sForm.ShowDialog();
                //等待两秒，使主线程充分显示(是否有必要?)
                //Thread.Sleep(2000);
            }
            catch (ThreadAbortException e)
            {
                throw e;
            }
            finally
            {
                sForm = null;
            }
        }

    }

}

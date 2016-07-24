using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitMonitorCommon;
namespace UnitMonitorServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MessageCenter.Init();
            TasksContainer.Init();
            Logger.Init();
            Clients.Init();
            ServerCommunication.Init();
  
            Application.Run(new MDIMain());
        }
    }
}

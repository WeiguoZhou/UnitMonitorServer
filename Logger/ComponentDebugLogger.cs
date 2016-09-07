using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommon;

namespace UnitMonitorServer.Components.Logger
{
    class ComponentDebugLogger:LoggerBase
    {
        public override string LogFile
        {
            get
            {
                return "ComponentDebug.log";
            }
        }
        public void LogComponentDebug(string componentName, Exception ex)
        {
            string message = string.Format("{0},{1},{2}", DateTime.Now, componentName, CommUtil.Transfer(ex.Message));
            LogMessage(message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommon;

namespace UnitMonitorServer.Components.Logger
{
    class TaskDebugLogger:LoggerBase
    {
        public override string LogFile
        {
            get
            {
                return "TaskDebug.log";
            }
        }
        public void LogTaskDebug(TaskBase task, Exception ex,string taskMothed)
        {
            string message = string.Format("{0},{1},{2},{3},{4}", DateTime.Now, task.TaskPath, task.TaskName, taskMothed, CommUtil.Transfer(ex.Message));
            LogMessage(message);
        }
    }
}

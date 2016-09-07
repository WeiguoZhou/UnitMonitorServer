using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace UnitMonitorServer.Components.Logger
{
    class TaskMessageLogger:LoggerBase
    {
        public override string LogFile
        {
            get
            {
                DateTime occurTime = DateTime.Now;
                return string.Format("{0:yyyy-MM-dd}{1}{2}.log", occurTime, CommUtil.BanC(occurTime), CommUtil.ZhiC(occurTime));
            }
        }
        public void LogMessageInfo(MessageInfo info)
        {
            string message = string.Format("{0},{1},{2},{3},{4},{5},{6}", info.OccurTime, info.SenderUrl, info.TaskPath, info.TaskName, info.MessageType, CommUtil.Transfer(info.Message));
            LogMessage(message);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace UnitMonitorCommon
{
  public  class Logger
    {
        private static Logger instance;
        AutoResetEvent areDebugWriting = new AutoResetEvent(true);
        AutoResetEvent areMessageWriting = new AutoResetEvent(true);
        private Logger() { }

        public static Logger Instance
        {
            get
            {
                return instance;
            }

        }
        public static void Init()
        {
            if (instance == null)
            {
                instance = new Logger();
                MessageCenter.Init();
                MessageCenter.Instance.SendMessageEvent += instance.LogMessage;
            }
        }
        /// <summary>
        /// 记录发送的消息
        /// 因为这里设计成按每个班分别记录的形式，所以一开始要计算文件名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LogMessage(MessageInfo inf)
        {
            Thread thread = new Thread(_LogMessage);
            thread.Start(inf);

        }
        private void _LogMessage(object state)
        {
            areMessageWriting.WaitOne();
            try
            {
                MessageInfo info = (MessageInfo)state;
                string directoryPath = Environment.CurrentDirectory + "\\Log";
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                string fileName = directoryPath + "\\" + LogFileName(info.OccurTime);
                if (!File.Exists(fileName))
                    File.Create(fileName);
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(string.Format("{0},{1},{2},{3}", info.OccurTime.ToString(), info.MessageType, CommUtil.Transfer(info.Message), info.TaskPath));
                    sw.Close();
                }
            }
            catch 
            {

            }
            areMessageWriting.Set();
        }
        public void LogDebug(string message)
        {
            Thread thread = new Thread(_LogDebug);
            thread.Start(message);
        }
        private void _LogDebug(object state)
        {
            string message = (string)state;
            areDebugWriting.WaitOne();
            try
            {
               string directoryPath = Environment.CurrentDirectory + "\\Log";
                if (!Directory.Exists(directoryPath))
                      Directory.CreateDirectory(directoryPath);
               string fileName = directoryPath + "\\debug.log" ;
                if (!File.Exists(fileName))
                    File.Create(fileName);
 
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(string.Format("{0},{1}", DateTime.Now,message));
                    sw.Close();
 
                }
            }
            catch 
            {


            }
            areDebugWriting.Set();
        }
        public string LogFileName(DateTime occurTime)
        {
            return string.Format("{0:yyyy-MM-dd}{1}{2}.log", occurTime, CommUtil.BanC(occurTime), CommUtil.ZhiC(occurTime));
        }
    }
}

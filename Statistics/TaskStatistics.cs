using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommon;
using System.Diagnostics;
using UnitMonitorCommunication;

namespace UnitMonitorServer.Components.statistics
{
   public  class TaskStatistics
    {
       public TaskBase Task { private set; get; }
        Stopwatch sw ;
        private long frequency= Stopwatch.Frequency;
        /// <summary>
        /// 上次处理成功的时间
        /// </summary>
        public DateTime LastProcessSuccessTime { set; get; } = DateTime.MinValue;
        /// <summary>
        /// 上次处理失败的时间
        /// </summary>
        public DateTime LastProcessFailedTime { set; get; } = DateTime.MinValue;
        /// <summary>
        /// PreInit失败次数
        /// </summary>
        public int PreInitFailCount { set; get; }
        /// <summary>
        /// PreInit成功次数
        /// </summary>
        public int PreInitSuccessCount { set; get; }
        /// <summary>
        /// 上次PreInit花费的时间（毫秒）
        /// </summary>
        public long LastPreInitSpendTime { set; get; }
        /// <summary>
        /// 上次PreInit的开始时刻
        /// </summary>
        public long LastPreInitBeginTicks { set; get; }
        /// <summary>
        /// PreInit平均花费时间
        /// </summary>
        public double PreInitAvgSpendTime { set; get; }
        /// <summary>
        /// InitData成功次数
        /// </summary>
        public int InitDataSuccessCount { set; get; }
        /// <summary>
        /// InitData失败次数
        /// </summary>
        public int InitDataFailCount { set; get; }
        /// <summary>
        /// 上次InitData花费的时间（毫秒）
        /// </summary>
        public long LastInitDataSpendTime { set; get; }
        /// <summary>
        /// 上次InitData开始时刻
        /// </summary>
        public long LastInitDataBeginTicks { set; get; }
        /// <summary>
        /// InitData平均花费时间
        /// </summary>
        public double InitDataAvgSpendTime { set; get; }

        /// <summary>
        /// Process成功次数
        /// </summary>
        public int ProcessSuccessCount { set; get; }
        /// <summary>
        /// Process失败次数
        /// </summary>
        public int ProcessFailCount { set; get; }
        /// <summary>
        /// 上次Process花费的时间（毫秒）
        /// </summary>
        public long LastProcessSpendTime { set; get; }
        /// <summary>
        /// 上次Process开始时刻
        /// </summary>
        public long LastProcessBeginTicks { set; get; }
        /// <summary>
        /// Process平均花费时间
        /// </summary>
        public double ProcessAvgSpendTime { set; get; }
        public long LastBeginRunTicks { set; get; }

        public int SendMessageCount { set; get; }
        public DateTime LastSendMessageTime { set; get; } = DateTime.MinValue;
        public TaskStatistics(TaskBase t)
        {
            
            Task = t;
            sw =  Stopwatch.StartNew();
            Task.AfterInitDataFailed += this.OnAfterInitDataFailed;
            Task.AfterInitDataSuccess += this.OnAfterInitDataSuccess;
            Task.AfterPreInitFailed += this.OnAfterPreInitFailed;
            Task.AfterPreInitSuccess += this.OnAfterPreInitSuccess;
            Task.AfterProcessFailed+= this.OnAfterProcessFailed;
            Task.AfterProcessSuccess += this.onAfterProcessSuccess;
            Task.BeginInitData += this.OnBeginInitData;
            Task.BeginProcess += this.OnBeginProcess;
            Task.BeginRun += this.OnBeginRun;
            Task.RunComplete += this.OnRunComplete;
            Task.SendMessage += this.OnSendMessage;

            
        }

        private void OnSendMessage(MessageInfo message)
        {
            SendMessageCount += 1;
            LastSendMessageTime = DateTime.Now;
        }

        private void OnRunComplete(object sender, EventArgs e)
        {
            
        }

        private void OnBeginRun(object sender, EventArgs e)
        {
            LastBeginRunTicks = sw.ElapsedTicks;
        }

        private void OnBeginProcess(object sender, EventArgs e)
        {
            LastProcessBeginTicks= sw.ElapsedTicks;
        }

        private void OnBeginInitData(object sender, EventArgs e)
        {
            LastInitDataBeginTicks= sw.ElapsedTicks;
        }

        private void onAfterProcessSuccess(object sender, EventArgs e)
        {
            LastProcessSpendTime = (sw.ElapsedTicks - LastProcessBeginTicks) * 1000 / frequency;
            ProcessAvgSpendTime = (ProcessAvgSpendTime * ProcessSuccessCount + LastProcessSpendTime) / (ProcessSuccessCount + 1);
            ProcessSuccessCount += 1;
            
        }

        private void OnAfterProcessFailed(TaskBase task, Exception ex)
        {
            LastProcessSpendTime = (sw.ElapsedTicks - LastProcessBeginTicks) * 1000 / frequency;
            ProcessFailCount += 1;
        }

        private void OnAfterPreInitSuccess(object sender, EventArgs e)
        {
            LastPreInitSpendTime= (sw.ElapsedTicks - LastPreInitBeginTicks) * 1000 / frequency;
            PreInitAvgSpendTime = (PreInitAvgSpendTime * PreInitSuccessCount + LastPreInitSpendTime) / (PreInitSuccessCount + 1);
            PreInitSuccessCount += 1;
        }

        private void OnAfterPreInitFailed(TaskBase task, Exception ex)
        {
            LastPreInitSpendTime = (sw.ElapsedTicks - LastPreInitBeginTicks) * 1000 / frequency;           
            PreInitFailCount += 1;
        }

        private void OnAfterInitDataSuccess(object sender, EventArgs e)
        {
            LastInitDataSpendTime = (sw.ElapsedTicks - LastInitDataBeginTicks) * 1000 / frequency;
            InitDataAvgSpendTime = (InitDataAvgSpendTime * InitDataSuccessCount + LastInitDataSpendTime) / (InitDataSuccessCount + 1);
            InitDataSuccessCount += 1;
        }

        private void OnAfterInitDataFailed(TaskBase task, Exception ex)
        {
            LastInitDataSpendTime = (sw.ElapsedTicks - LastInitDataBeginTicks) * 1000 / frequency;           
            InitDataFailCount += 1;
        }
        public void UnLoad()
        {
            Task.AfterInitDataFailed -= this.OnAfterInitDataFailed;
            Task.AfterInitDataSuccess -= this.OnAfterInitDataSuccess;
            Task.AfterPreInitFailed -= this.OnAfterPreInitFailed;
            Task.AfterPreInitSuccess -= this.OnAfterPreInitSuccess;
            Task.AfterProcessFailed -= this.OnAfterProcessFailed;
            Task.AfterProcessSuccess -= this.onAfterProcessSuccess;
            Task.BeginInitData -= this.OnBeginInitData;
            Task.BeginProcess -= this.OnBeginProcess;
            Task.BeginRun -= this.OnBeginRun;
            Task.RunComplete -= this.OnRunComplete;
            Task.SendMessage -= this.OnSendMessage;
        }
    }
}

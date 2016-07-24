using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommunication;

namespace UnitMonitorCommon
{
  public static  class TaskBoolExtensions
    {
        public static bool BoolTdOn(this TaskBase task,string pointAlias,int tdOnSec,bool outputAlarm=true)
        {
            bool returnValue = false;
            string preKey = pointAlias + "_BoolTdOn_" + tdOnSec.ToString();

            bool value = task.BoolValue(pointAlias);
            if (value)
            {
 
                string alarmStateKey = preKey + "_AlarmState";
                if(!task.TempValue.ContainsKey(alarmStateKey))
                {
                   string beginTimeKey = preKey + "_BeginTime";
                   DateTime currentTime = TasksContainer.Instance().CurrentTime;
                    if (!task.TempValue.ContainsKey(beginTimeKey))
                        task.TempValue.Add(beginTimeKey, currentTime);
                    DateTime beginTime = (DateTime)task.TempValue[beginTimeKey];
                    //如果已达报警延时时间，则报警
                    if (beginTime.AddSeconds(tdOnSec) <= currentTime)
                    {
                        task.TempValue.Add(alarmStateKey, 1);
                        returnValue = true;
                        if (outputAlarm)
                        {
                            int continueCount = Convert.ToInt32(CommUtil.TimeSecondSpan(beginTime, currentTime));
                            task.SendMessge(MessageType.Alarm, string.Format("{0}投入超{1}", task.PointNameDescription(pointAlias), continueCount));

                        }
                    }
                }
 
            }
            else
            {
                task.RemoveRelatedKey(preKey);
            }
            return returnValue;
        }

        public static bool BoolTdOff(this TaskBase task, string pointAlias, int tdOffSec, bool outputAlarm=true)
        {
            bool returnValue = false;
            string preKey = pointAlias + "_BoolTdOff_" + tdOffSec.ToString();

            bool value = task.BoolValue(pointAlias);
            if (!value)
            {

                string alarmStateKey = preKey + "_AlarmState";
                if (!task.TempValue.ContainsKey(alarmStateKey))
                {
                    string beginTimeKey = preKey + "_BeginTime";
                    DateTime currentTime = TasksContainer.Instance().CurrentTime;
                    if (!task.TempValue.ContainsKey(beginTimeKey))
                        task.TempValue.Add(beginTimeKey, currentTime);
                    DateTime beginTime = (DateTime)task.TempValue[beginTimeKey];
                    //如果已达报警延时时间，则报警
                    if (beginTime.AddSeconds(tdOffSec) <= currentTime)
                    {
                        task.TempValue.Add(alarmStateKey, 1);
                        returnValue = true;
                        if (outputAlarm)
                        {
                            int continueCount = Convert.ToInt32(CommUtil.TimeSecondSpan(beginTime, currentTime));
                            task.SendMessge(MessageType.Alarm, string.Format("{0}退出超{1}", task.PointNameDescription(pointAlias), continueCount));

                        }
                    }
                }

            }
            else
            {
                task.RemoveRelatedKey(preKey);
            }
            return returnValue;
        }

        /// <summary>
        /// 开关量首次由0变1时输出true(上升沿)，此后输出false，直到变0后复位报警信号才能再次起作用
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias"></param>
        /// <param name="preKey"></param>
        /// <returns></returns>
        public static bool RisingEdge(this TaskBase task, string pointAlias, string preKey = "")
        {
            bool returnValue = false;
            if (preKey == "")
                preKey = pointAlias + "_RisingEdge";
            string alarmStateKey = preKey + "_AlarmState";
            if (task.BoolValue(pointAlias))
            {
                if (!task.TempValue.ContainsKey(alarmStateKey))
                {
                    task.TempValue.Add(alarmStateKey, 1);
                    if (task.RunCount > 0)
                        returnValue = true;
                }
            }
            else
            {
                task.TempValue.Remove(alarmStateKey);
            }
            return returnValue;
        }
        /// <summary>
        /// 开关量首次由1变0时输出true(下降沿)，此后输出false，直到变1后复位报警信号才能再次起作用
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias"></param>
       /// <param name="preKey"></param>
        /// <returns></returns>
        public static bool TrailingEdge(this TaskBase task, string pointAlias,string preKey="")
        {
            bool returnValue = false;
            if (preKey == "")
                preKey = pointAlias + "_TrailingEdge";
            string alarmStateKey = preKey + "_AlarmState";
            if (!task.BoolValue(pointAlias))
            {
                if (!task.TempValue.ContainsKey(alarmStateKey))
                {
                    task.TempValue.Add(alarmStateKey, 1);
                    if(task.RunCount>0)
                        returnValue = true;
                }
            }
            else
            {
                task.TempValue.Remove(alarmStateKey);
            }
            return returnValue;
        }
        /// <summary>
        /// 切手动报警
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias"></param>
        /// <param name="outputAlarm"></param>
        /// <returns></returns>
        public static bool ToMan(this TaskBase task, string pointAlias,bool outputAlarm=true)
        {
            bool returnValue = false;

            if (TrailingEdge(task, pointAlias, pointAlias +"_ToMan"))
            {
                returnValue = true;
                if (outputAlarm)
                    task.SendMessge(MessageType.Alarm, string.Format("{0}切手动"));
            }
            return returnValue;

        }
    }
}

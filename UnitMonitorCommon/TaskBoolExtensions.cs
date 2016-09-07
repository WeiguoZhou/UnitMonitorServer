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
        /// <summary>
        /// 开关量由0变1时延迟指定时间后输出报警
        /// 相关参数从点的配置中读取，点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool BoolTdOn(this Point point,string key="")
        {
            if (!point.AllowAlarm || !point.Value.HasValue)
                return false;
            int tdOnSec= Convert.ToInt32(point.GetParamSetting(ParamSetting.OnTimeDelay));
            string alarmString = point.GetParamSetting(ParamSetting.OnDelayAlarm);
            if (point.TdOn(point.BoolValue.Value, tdOnSec, key))
            {
                if (!string.IsNullOrEmpty(alarmString))
                    point.SendMessage(MessageType.Alarm, alarmString);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 开关量由1变0时延迟指定时间后输出报警
        /// 相关参数从点的配置中读取，点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool BoolTdOff(this Point point, string key = "")
        {
            if (!point.AllowAlarm || !point.Value.HasValue)
                return false;
            int tdOnSec = Convert.ToInt32(point.GetParamSetting(ParamSetting.OffTimeDelay));
            string alarmString = point.GetParamSetting(ParamSetting.OffDelayAlarm);
            if (point.TdOn(!point.BoolValue.Value, tdOnSec, key))
            {
                if (!string.IsNullOrEmpty(alarmString))
                    point.SendMessage(MessageType.Alarm, alarmString);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 开关量由1变0时输出true
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool BoolNewOff(this Point point)
        {
            if (!point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            return ((point.Value != point.OldValue) && (point.Value == 0));
        }
        /// <summary>
        /// 开关量由0变1时输出true
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool BoolNewOn(this Point point)
        {
            if (!point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            return ((point.Value != point.OldValue) && (point.Value == 1));
        }
        /// <summary>
        /// 开关量值发生变化时输出报警
        /// 此算法根据点的AlarmString作不同的监视，当有onAlarmString时监视由0变1，当有offAlarmString时监视由1变0，当都有时都监视
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool BoolChanged(this Point point)
        {
            if (!point.AllowAlarm || !point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            if (point.Value != point.OldValue)
            {
                string alarmString = "";
                if (point.BoolValue.Value)
                {
                    alarmString= point.GetParamSetting(ParamSetting.ChangedOnAlarm);
                    if (!string.IsNullOrEmpty(alarmString))
                    {
                        point.SendMessage(MessageType.Alarm, alarmString);
                        return true;
                    }
                }
                else
                {
                    alarmString = point.GetParamSetting(ParamSetting.ChangedOffAlarm);
                    if (!string.IsNullOrEmpty(alarmString))
                    {
                        point.SendMessage(MessageType.Alarm, alarmString);
                        return true;
                    }
                }
            }
          
            return false;
        }
 
    }
}

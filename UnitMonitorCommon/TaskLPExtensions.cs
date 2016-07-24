using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommunication;

namespace UnitMonitorCommon
{
   public static class TaskLPExtensions
    {
        /// <summary>
        /// 检测电源是否有报警
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias"></param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool PowerFault(this TaskBase task,string pointAlias, bool outputAlarm = true)
        {
            bool returnValue = false;
            int value = task.LpValue(pointAlias);
            string oldValueKey = pointAlias + "_PowerFault_OldValue"; //考虑到每次执行都会新值替换旧值，键值加上PowerFault防止影响其它检测
            if (!task.TempValue.ContainsKey(oldValueKey))
            {
                task.TempValue.Add(oldValueKey, value);
                if (value.PowerFault())
                {
                    returnValue = true;
                    if(outputAlarm)
                        task.SendMessge(MessageType.Alarm, string.Format("{0}{1}", task.PointNameDescription(pointAlias), PowerFaultString(value)));
                }
            }

            else
            {
                int oldValue = (int)task.TempValue[oldValueKey];
                if (value.NewPowerFault(oldValue))
                {
                    returnValue = true;
                    if (outputAlarm)
                        task.SendMessge(MessageType.Alarm, string.Format("{0}{1}", task.PointNameDescription(pointAlias), PowerFaultString(value)));
                }                  
                task.TempValue[oldValueKey] = value;
            }
            return returnValue;
        }
        public static bool ValveFault(this TaskBase task, string pointAlias,bool outputAlarm = true)
        {
            bool returnValue = false;
            int value = task.LpValue(pointAlias);
            string oldValueKey = pointAlias + "_ValveFault_OldValue";
            if (!task.TempValue.ContainsKey(oldValueKey))
            {
                task.TempValue.Add(oldValueKey, value);
                if (value.ValveFault())
                {
                    returnValue = true;
                    if (outputAlarm)
                        task.SendMessge(MessageType.Alarm, string.Format("{0}{1}", task.PointNameDescription(pointAlias), ValveFaultString(value)));
                }
            }

            else
            {
                int oldValue = (int)task.TempValue[oldValueKey];

                if (value.NewValveFault(oldValue))
                {
                    returnValue = true;
                    if (outputAlarm)
                        task.SendMessge(MessageType.Alarm, string.Format("{0}{1}", task.PointNameDescription(pointAlias), ValveFaultString(value)));      
                }
                      task.TempValue[oldValueKey] = value;
            }
            return returnValue;
        }
        /// <summary>
        /// 检测电源或阀门是否刚停止或关闭
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias">开关量点别名</param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool NewOff(this TaskBase task, string pointAlias,bool outputAlarm=true)
        {
            bool returnValue = false;
            int value = task.LpValue(pointAlias);
            string oldValueKey = pointAlias + "_NewOff_OldValue";
            if (!task.TempValue.ContainsKey(oldValueKey))
                task.TempValue.Add(oldValueKey, value);
            else
            {
                int oldValue = (int)task.TempValue[oldValueKey];
                if (value.NewOff(oldValue))
                {
                    returnValue = true;
                    if(outputAlarm)
                        task.SendMessge(MessageType.Alarm, string.Format("{0}刚停止或关闭", task.PointNameDescription(pointAlias)));
                }

                task.TempValue[oldValueKey] = value;
            }
            return returnValue;
        }
        /// <summary>
        /// 检测电源或阀门是否刚启动或打开
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias">开关量点别名</param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool NewOn(this TaskBase task, string pointAlias, bool outputAlarm = true)
        {
            bool returnValue = false;
            int value = task.LpValue(pointAlias);
            string oldValueKey = pointAlias + "_NewOn_OldValue";
            if (!task.TempValue.ContainsKey(oldValueKey))
                task.TempValue.Add(oldValueKey, value);
            else
            {
                int oldValue = (int)task.TempValue[oldValueKey];
                if (value.NewOn(oldValue))
                {
                    returnValue = true;
                    if (outputAlarm)
                        task.SendMessge(MessageType.Alarm, string.Format("{0}刚启动或打开", task.PointNameDescription(pointAlias)));
                }

                task.TempValue[oldValueKey] = value;
            }
            return returnValue;
        }
        public static bool NewLostOn(this TaskBase task, string pointAlias, bool outputAlarm = true)
        {
            bool returnValue = false;
            int value = task.LpValue(pointAlias);
            string oldValueKey = pointAlias + "_NewLostOn_OldValue";
            if (!task.TempValue.ContainsKey(oldValueKey))
                task.TempValue.Add(oldValueKey, value);
            else
            {
                int oldValue = (int)task.TempValue[oldValueKey];
                if (oldValue.NewOn(value))
                {
                    returnValue = true;
                    if (outputAlarm)
                        task.SendMessge(MessageType.Alarm, string.Format("{0}刚失去启动或开足信号", task.PointNameDescription(pointAlias)));
                }

                task.TempValue[oldValueKey] = value;
            }
            return returnValue;
        }
        public static bool NewLostOff(this TaskBase task, string pointAlias, bool outputAlarm = true)
        {
            bool returnValue = false;
            int value = task.LpValue(pointAlias);
            string oldValueKey = pointAlias + "_NewLostOff_OldValue";
            if (!task.TempValue.ContainsKey(oldValueKey))
                task.TempValue.Add(oldValueKey, value);
            else
            {
                int oldValue = (int)task.TempValue[oldValueKey];
                if (oldValue.NewOff(value))
                {
                    returnValue = true;
                    if (outputAlarm)
                        task.SendMessge(MessageType.Alarm, string.Format("{0}刚失去停止或关闭信号", task.PointNameDescription(pointAlias)));
                }

                task.TempValue[oldValueKey] = value;
            }
            return returnValue;
        }
        /// <summary>
        /// 检查阀门是否为故障新报警
        /// </summary>
        /// <param name="newValue">阀门状态新值</param>
        /// <param name="oldValue">阀门状态旧值</param>
        /// <returns></returns>
        public static bool NewValveFault(this int newValue, int oldValue)
        {
            if ((newValue != oldValue)  && ValveFault(newValue) && !ValveFault(oldValue))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查电源是否为故障新报警
        /// </summary>
        /// <param name="newValue">电源状态新值</param>
        /// <param name="oldValue">电源状态旧值</param>
        /// <returns></returns>
        public static bool NewPowerFault(this int newValue, int oldValue)
        {
            if ((newValue != oldValue) && PowerFault(newValue) && !PowerFault(oldValue))
            {
                return true;
            }
            return false;
        }
        public static bool LPTdOn(this TaskBase task, string pointAlias,int tdOnSec)
        {
            bool returnValue = false;
            int value = task.LpValue(pointAlias);
            string preKey = pointAlias + "_TdOn_" + tdOnSec.ToString();
            if (value.IsOn())
            {
                string beginTimeKey = preKey + "_BeginTime";
                DateTime currentTime = TasksContainer.Instance().CurrentTime;
                if (!task.TempValue.ContainsKey(beginTimeKey))
                    task.TempValue.Add(beginTimeKey, currentTime);
                DateTime beginTime = (DateTime)task.TempValue[beginTimeKey];
                if (beginTime.AddSeconds(tdOnSec) <= currentTime)
                    returnValue = true;
            }
            else
            {
                task.TempValue.Remove(preKey);
            }

            return returnValue;

        }
        public static bool LPTdOff(this TaskBase task, string pointAlias, int tdOffSec)
        {
            bool returnValue = false;
            int value = task.LpValue(pointAlias);
            string preKey = pointAlias + "_TdOff_" + tdOffSec.ToString();
            if (value.IsOff())
            {
                string beginTimeKey = preKey + "_BeginTime";
                DateTime currentTime = TasksContainer.Instance().CurrentTime;
                if (!task.TempValue.ContainsKey(beginTimeKey))
                    task.TempValue.Add(beginTimeKey, currentTime);
                DateTime beginTime = (DateTime)task.TempValue[beginTimeKey];
                if (beginTime.AddSeconds(tdOffSec) <= currentTime)
                    returnValue = true;
            }
            else
            {
                task.TempValue.Remove(preKey);
            }

            return returnValue;

        }
        /// <summary>
        /// 打包点状态位比较
        /// 因为edna保存精度问题，如果32位都比较会出错
        /// </summary>
        /// <param name="rValue">实时值</param>
        /// <param name="state">要比较的位值，可以多个位给合</param>
        /// <param name="mask">掩码，默认值65535</param>
        /// <returns></returns>
        public static bool CheckState(this int rValue, int state, int mask = 65535)
        {
            return (rValue & mask) == state;
        }
        /// <summary>
        /// 检查打包点指定位的状态
        /// </summary>
        /// <param name="value">打包点值</param>
        /// <param name="bit">位的序号</param>
        /// <returns></returns>
        public static bool GetBitValue(this int value, int bit)
        {
            return (value & Convert.ToInt32(Math.Pow(2, bit))) > 0;
        }
        public static bool IsOn(this int value)
        {
            return GetBitValue(value, 9);
        }
        public static bool IsOff(this int value)
        {
            return GetBitValue(value, 10);
        }
        /// <summary>
        /// 检查阀门是否在故障状态
        /// </summary>
        /// <param name="value">阀门打包点值</param>
        /// <returns></returns>
        public static bool ValveFault(this int value)
        {
            return GetBitValue(value, 0) || GetBitValue(value, 1) || GetBitValue(value, 2);
        }

        /// <summary>
        /// 检查电源或阀门是否刚停止或关闭
        /// </summary>
        /// <returns></returns>
        public static bool NewOff(this int newValue,  int oldValue)
        {
            if ((newValue != oldValue) && IsOff(newValue) && !IsOff(oldValue))
                return true;
            return false;
        }
  
        /// <summary>
        /// 检查电源或阀门是否刚启动或打开
        /// </summary>
        /// <returns></returns>
        public static bool NewOn(this int newValue, int oldValue)
        {
            if ((newValue != oldValue) && IsOn(newValue) && !IsOn(oldValue))
                return true;
            return false;
        }
        /// <summary>
        /// 检查电源或阀门是否刚失去启动或开足信号
        /// </summary>

        /// <returns></returns>
        public static bool NewLostOn(this int newValue,int oldValue)
        {
            if ((newValue != oldValue) && !IsOn(newValue) && IsOn(oldValue))
                return true;
            return false;
        }
        /// <summary>
        /// 检查电源或阀门是否刚失去停止或关闭信号
        /// </summary>

        /// <returns></returns>
        public static bool NewLostOff(this int newValue,  int oldValue)
        {
            if ((newValue != oldValue) && !IsOff(newValue) && IsOff(oldValue))
              return true;
            return false;
        }
        /// <summary>
        /// 返回阀门故障说明字符串
        /// </summary>
        /// <param name="value">阀门打包点值</param>
        /// <returns></returns>
        public static string ValveFaultString(this int value)
        {
            string ret = "";
            if (GetBitValue(value, 0))
                ret += "阀门故障";
            if (GetBitValue(value, 1))
                ret += "开故障";
            if (GetBitValue(value, 2))
                ret += "关故障";
            return ret;
        }
        /// <summary>
        /// 检查电源是否在故障状态
        /// </summary>
        /// <param name="value">电源打包点值</param>
        /// <returns></returns>
        public static bool PowerFault(this int value)
        {
            return GetBitValue(value, 0) || GetBitValue(value, 1) || GetBitValue(value, 2) || GetBitValue(value, 7);
        }
        /// <summary>
        /// 返回电源故障说明字符串
        /// </summary>
        /// <param name="value">电源打包点值</param>
        /// <returns></returns>
        public static string PowerFaultString(this int value)
        {
            string ret = "";
            if (GetBitValue(value, 0))
                ret += "异常停止";
            if (GetBitValue(value, 1))
                ret += "启故障";
            if (GetBitValue(value, 2))
                ret += "停故障";
            if (GetBitValue(value, 7))
                ret += "电气故障";
            return ret;
        }
    }
}

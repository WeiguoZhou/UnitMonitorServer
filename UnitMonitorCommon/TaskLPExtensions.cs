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
        /// 监视电源是否出现新报警
        /// 当监视前本来就故障，不会发报警
        /// 点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool PowerNewFault(this Point point, bool outputAlarm = true)
        {
            if (!point.AllowAlarm || !point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            bool returnValue = false;
            int value = point.IntValue.Value;
            int oldValue = point.OldIntValue.Value;
            returnValue = value.NewPowerFault(oldValue);
            if (returnValue && outputAlarm)
                point.SendMessage(MessageType.Alarm, string.Format("{0}{1}", point.Description, PowerFaultString(value)));
            return returnValue;
        }

        /// <summary>
        /// 监视阀门是否出现新报警
        /// 当监视前本来就故障，不会发报警
        /// 点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <param name="outputAlarm"></param>
        /// <returns></returns>
        public static bool ValveNewFault(this Point point, bool outputAlarm = true)
        {
            if (!point.AllowAlarm || !point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            bool returnValue = false;
            int value = point.IntValue.Value;
            int oldValue = point.OldIntValue.Value;
            returnValue = value.NewValveFault(oldValue);
            if (returnValue && outputAlarm)
                point.SendMessage(MessageType.Alarm, string.Format("{0}{1}", point.Description, ValveFaultString(value)));
            return returnValue;
        }
        /// <summary>
        /// 监视电源或阀门是否刚停止或关闭
        /// </summary>
        /// <param name="point"></param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool NewOff(this Point point, bool outputAlarm = true)
        {
            if (!point.AllowAlarm || !point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            bool returnValue = false;
            int value = point.IntValue.Value;
            int oldValue = point.OldIntValue.Value;
            returnValue = value.NewOff(oldValue);
            if (returnValue && outputAlarm)
                point.SendMessage(MessageType.Alarm, string.Format("{0}刚停止或关闭", point.Description));
            return returnValue;

        }
        /// <summary>
        /// 监视电源或阀门是否刚启动或打开
        /// </summary>
        /// <param name="point"></param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool NewOn(this Point point, bool outputAlarm = true)
        {
            if (!point.AllowAlarm || !point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            bool returnValue = false;
            int value = point.IntValue.Value;
            int oldValue = point.OldIntValue.Value;
            returnValue = value.NewOn(oldValue);
            if (returnValue && outputAlarm)
                point.SendMessage(MessageType.Alarm, string.Format("{0}刚启动或开足", point.Description));
            return returnValue;
        }
        /// <summary>
        /// 监视电源或阀门是否刚失去启动或开足信号
        /// </summary>
        /// <param name="point"></param>
        /// <param name="outputAlarm"></param>
        /// <returns></returns>
        public static bool NewLostOn(this Point point, bool outputAlarm = true)
        {
            if (!point.AllowAlarm || !point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            bool returnValue = false;
            int value = point.IntValue.Value;
            int oldValue = point.OldIntValue.Value;
            returnValue = value.NewLostOn(oldValue);
            if (returnValue && outputAlarm)
                point.SendMessage(MessageType.Alarm, string.Format("{0}刚失去启动或开足信号", point.Description));
            return returnValue;
        }
        /// <summary>
        /// 打包点刚失去停止或关闭信号，脉冲输出。
        /// </summary>
        /// <param name="point"></param>
        /// <param name="outputAlarm"></param>
        /// <returns></returns>
        public static bool NewLostOff(this Point point,  bool outputAlarm = true)
        {
            if (!point.AllowAlarm || !point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            bool returnValue = false;
            int value = point.IntValue.Value;
            int oldValue = point.OldIntValue.Value;
            returnValue = value.NewLostOff(oldValue);        
            if (returnValue && outputAlarm)
                point.SendMessage(MessageType.Alarm, string.Format("{0}刚失去停止或关闭信号", point.Description));
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
            if ((newValue != oldValue) && ValveFault(newValue) && !ValveFault(oldValue))
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
        /// <summary>
        /// 表示设备已启动或阀门在开足位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsOn(this int value)
        {
            return GetBitValue(value, 9);
        }
        public static bool IsOn(this Point  point)
        {
            if (!point.Value.HasValue)
                return false;
            return GetBitValue(point.IntValue.Value, 9);
        }
        /// <summary>
        /// 表示设备已停止或阀门在关闭位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsOff(this int value)
        {
            return GetBitValue(value, 10);
        }
        public static bool IsOff(this Point point)
        {
            if (!point.Value.HasValue)
                return false;
            return GetBitValue(point.IntValue.Value, 10);
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
        public static bool NewOff(this int newValue, int oldValue)
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
        public static bool NewLostOn(this int newValue, int oldValue)
        {
            if ((newValue != oldValue) && !IsOn(newValue) && IsOn(oldValue))
                return true;
            return false;
        }
        /// <summary>
        /// 检查电源或阀门是否刚失去停止或关闭信号
        /// </summary>

        /// <returns></returns>
        public static bool NewLostOff(this int newValue, int oldValue)
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

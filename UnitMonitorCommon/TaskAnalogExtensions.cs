using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommunication;

namespace UnitMonitorCommon
{
    public static class TaskAnalogExtensions
    {
        /// <summary>
        /// 模拟量点低延时报警
        /// </summary>
        /// <param name="point"></param>
        /// <param name="lValue">低报警值</param>
        /// <param name="tdOnSec">延时时间</param>
        /// <param name="outputAlarm">是否输出报警，可选，默认为true</param>
        /// <param name="key">临时数据键值前缀</param>
        /// <returns></returns>
        public static bool AnalogLTdOn(this Point point, double lValue, int tdOnSec, bool outputAlarm = true, string key = "")
        {
            if (!point.Value.HasValue)
                return false;
            double value = point.Value.Value;
            string preKey = string.IsNullOrEmpty(key) ? "AnalogLTdOn" : key + "_AnalogLTdOn";
            if (TaskExtensions.TdOnPulse(point, value < lValue, tdOnSec, preKey))
            {

                if (outputAlarm)
                {

                    point.SendMessage(MessageType.Alarm, string.Format("{0}小于{1},持续{2}秒以上时间", point.Description, lValue, tdOnSec));
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 模拟量点低延时报警
        /// 相关参数从模拟量点的配置中读取，模拟量点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool AnalogLTdOn(this Point point)
        {
            if (!point.AllowAlarm || !point.Value.HasValue)
                return false;
            double lValue = Convert.ToDouble(point.GetParamSetting(ParamSetting.LowValue));
            int tdOnSec = Convert.ToInt32(point.GetParamSetting(ParamSetting.LowValueTimeDelay));
            //当设置alarmString时，会屏蔽函数默认的输出
            string alarmString = point.GetParamSetting(ParamSetting.LowAlarm);
            bool outputAlarm = (string.IsNullOrEmpty(alarmString));
            bool returnValue = TaskAnalogExtensions.AnalogLTdOn(point, lValue, tdOnSec, outputAlarm);
            if (returnValue && !outputAlarm)
                point.SendMessage(MessageType.Alarm, alarmString);
            return returnValue;
        }
        /// <summary>
        /// 模拟量高延时报警
        /// </summary>
        /// <param name="point">模拟量点</param>
        /// <param name="hValue">高报警值</param>
        /// <param name="tdOnSec">延时时间</param>
        /// <param name="outputAlarm">是否输出报警，可选，默认为true</param>
        /// <param name="key">临时数据键值前缀，可选</param>
        /// <returns></returns>
        public static bool AnalogHTdOn(this Point point, double hValue, int tdOnSec, bool outputAlarm = true, string key = "")
        {
            if (!point.Value.HasValue)
                return false;
            double value = point.Value.Value;
            string preKey = string.IsNullOrEmpty(key) ? "AnalogLTdOn" : key + "_AnalogLTdOn";
            if (TaskExtensions.TdOnPulse(point, value > hValue, tdOnSec, preKey))
            {
                if (outputAlarm)
                {

                    point.SendMessage(MessageType.Alarm, string.Format("{0}大于{1},持续{2}秒以上时间", point.Description, hValue, tdOnSec));
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 模拟量点高延时报警
        /// 相关参数从模拟量点的配置中读取，模拟量点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool AnalogHTdOn(this Point point)
        {
            if (!point.AllowAlarm || !point.Value.HasValue)
                return false;
            double hValue = Convert.ToDouble(point.GetParamSetting(ParamSetting.HighValue));
            int tdOnSec = Convert.ToInt32(point.GetParamSetting(ParamSetting.HighValueTimeDelay));
            string alarmString = point.GetParamSetting(ParamSetting.HighAlarm);
            bool outputAlarm = (string.IsNullOrEmpty(alarmString));
            bool returnValue = TaskAnalogExtensions.AnalogHTdOn(point, hValue, tdOnSec, outputAlarm);
            if (returnValue && !outputAlarm)
                point.SendMessage(MessageType.Alarm, alarmString);
            return returnValue;

        }

        /// <summary>
        /// 模拟量高低值延时报警,优先检查高报，如果有高报就不再检查低报
        /// </summary>
        /// <param name="point">模拟量点</param>
        /// <param name="hValue">高报值</param>
        /// <param name="lValue">低报值</param>
        /// <param name="tdOnSec">延时时间</param>
        /// <param name="outputAlarm">是否输出报警，可选，默认为true</param>
        /// <param name="key">临时数据键值前缀，可选</param>
        /// <returns></returns>
        public static bool AnalogTdOnHL(Point point, double hValue, double lValue, int tdOnSec, bool outputAlarm = true, string key = "")
        {

            if (AnalogHTdOn(point, hValue, tdOnSec, outputAlarm, key))
                return true;
            else
                return AnalogLTdOn(point, lValue, tdOnSec, outputAlarm, key);

        }
        /// <summary>
        /// 模拟量高低值延时报警。
        /// 相关参数从模拟量点的配置中读取，模拟量点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool AnalogTdOnHL(Point point)
        {
            if (!point.AllowAlarm)
                return false;
            return (AnalogHTdOn(point) | AnalogLTdOn(point));
        }
        /// <summary>
        /// 模拟量变化幅度小于一定值，延时一段时间后报警
        /// </summary>
        /// <param name="point">模拟量点</param>
        /// <param name="clValue">变化幅度低报警值</param>
        /// <param name="tdOnSec">延时时间</param>
        /// <param name="outputAlarm">是否输出报警，可选，默认为true</param>
        /// <param name="key">临时数据键值前缀，可选</param>
        /// <returns></returns>
        public static bool AnalogChangeLTdOn(this Point point, double clValue, int tdOnSec, bool outputAlarm = true, string key = "")
        {
            if (!point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            bool returnValue = false;
            string preKey = string.IsNullOrEmpty(key) ? "AnalogChangeLTdOn" : key + "_AnalogChangeLTdOn";
            double value = point.Value.Value;
            double oldValue = point.OldValue.Value;

            returnValue = TaskExtensions.TdOnPulse(point, Math.Abs(oldValue - value) < clValue, tdOnSec, preKey);
            if (returnValue && outputAlarm)
            {

                point.SendMessage(MessageType.Alarm, string.Format("{0}变化幅度低于{1},持续{2}秒以上时间", point.Description, clValue, tdOnSec));

            }


            return returnValue;
        }
        /// <summary>
        /// 模拟量变化幅度小于一定值，延时一段时间后报警。
        /// 相关参数从模拟量点的配置中读取，模拟量点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <param name="outputAlarm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool AnalogChangeLTdOn(this Point point)
        {
            if (!point.AllowAlarm)
                return false;
            double clValue = Convert.ToDouble(point.GetParamSetting(ParamSetting.ChangeLowValue));
            int tdOnSec = Convert.ToInt32(point.GetParamSetting(ParamSetting.ChangeLowTimeDelay));
            string alarmString = point.GetParamSetting(ParamSetting.ChangeLowAlarm);
            bool outputAlarm = string.IsNullOrEmpty(alarmString);
            bool returnValue = AnalogChangeLTdOn(point, clValue, tdOnSec, outputAlarm);
            if (returnValue && !outputAlarm)
                point.SendMessage(MessageType.Alarm, alarmString);
            return returnValue;
        }
        /// <summary>
        ///模拟量变化幅度高报警
        /// 此算法按统计周期循环计算，当发现最大值与最小值的差值达到一定幅度时报警
        /// 适用于平时比较稳定的值
        /// </summary>
        /// <param name="point">模拟量点</param>
        /// <param name="chValue">高报警值</param>
        /// <param name="statisticsSec">统计周期</param>
        /// <param name="outputAlarm">是否输出报警，可选，默认为true</param>
        /// <param name="key">临时数据键值前缀，可选</param>
        /// <returns></returns>

        public static bool AnalogChangeH(this Point point, double chValue, int statisticsSec, bool outputAlarm = true, string key = "")
        {
            if (!point.Value.HasValue)
                return false;
            bool returnValue = false;
            string preKey = string.IsNullOrEmpty(key) ? "AnalogChangeH" : key + "_AnalogChangeH";
            string statisticsBeginTimeKey = preKey + "_StatisticsBeginTime";
            double currentTime = TasksContainer.Instance.CurrentTotalSeconds;
            double statisticsBeginTime;
            //如果没有在临时数据中找到统计开始时间，则设置
            if (!point.TryGetTempValue(statisticsBeginTimeKey, out statisticsBeginTime))
                point.SetTempValue(statisticsBeginTimeKey, currentTime);
            else
            {
                //统计周期结束，删除键值，开始下一周期的运算
                if ((statisticsBeginTime + statisticsSec) <= currentTime)
                {
                    point.RemoveRelatedTempValue(preKey);
                }
                else
                {
                    double value = point.Value.Value;
                    string maxValueKey = preKey + "_MaxValue";
                    string minValueKey = preKey + "_MinValue";
                    double maxValue;
                    double minValue;
                    //没旧数据键值，表示第一次取数据
                    if (!point.TryGetTempValue(maxValueKey, out maxValue))
                    {
                        maxValue = value;
                        point.SetTempValue(maxValueKey, value);
                    }
                    if (!point.TryGetTempValue(minValueKey, out minValue))
                    {
                        minValue = value;
                        point.SetTempValue(minValueKey, value);
                    }

                    string alarmStateKey = preKey + "_AlarmState";
                    //如果统计周期内发现最大值和最小值之差大于设定值，则发报警
                    if ((maxValue - minValue) >= chValue)
                    {
                        if (!point.ContainsTempValue(alarmStateKey))
                        {
                            point.SetTempValue(alarmStateKey, 1);
                            returnValue = true;
                            if (outputAlarm)
                                point.SendMessage(MessageType.Alarm, string.Format("{0}在{1}秒内变化幅度大于{2}", point.Description, statisticsSec, chValue));
                        }
                    }
                }
            }



            return returnValue;
        }
        /// <summary>
        /// 模拟量变化幅度高报警
        /// 此算法按统计周期循环计算，当发现最大值与最小值的差值达到一定幅度时报警
        ///相关参数从模拟量点的配置中读取，模拟量点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool AnalogChangeH(this Point point)
        {
            if (!point.AllowAlarm)
                return false;
            double chValue = Convert.ToDouble(point.GetParamSetting(ParamSetting.ChangeHighValue));
            int statisticsSec = Convert.ToInt32(point.GetParamSetting(ParamSetting.ChangeHighStatisticsPeriod));
            string alarmString = point.GetParamSetting(ParamSetting.ChangeHighAlarm);
            bool outputAlarm = string.IsNullOrEmpty(alarmString);
            bool returnValue = AnalogChangeH(point, chValue, statisticsSec, outputAlarm);
            if (returnValue && !outputAlarm)
                point.SendMessage(MessageType.Alarm, alarmString);
            return returnValue;
        }
        /// <summary>
        /// 此算法连续监视上次的值和本次的值的差值，如果达到高设定值时输出报警
        /// </summary>
        /// <param name="point"></param>
        /// <param name="chValue">变化幅度高设定值</param>
        /// <param name="outputAlarm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool AnalogChangeConsecutiveH(this Point point, double chValue, bool outputAlarm = true)
        {
            if (!point.Value.HasValue || !point.OldValue.HasValue)
                return false;

            if (Math.Abs(point.Value.Value - point.OldValue.Value) > chValue)
            {
                if (outputAlarm)
                    point.SendMessage(MessageType.Alarm, string.Format("{0}变化幅度大于{1}", point.Description, chValue));
                return true;
            }
            return false;
        }
        /// <summary>
        /// 模拟量变化幅度高报警
        /// 此算法在一定的统计周期内监视上次的值和本次的值变化幅度超限的次数，当达到指定的次数时输出报警
        /// </summary>
        /// <param name="point">模拟量点</param>
        /// <param name="chValue">变化幅度高限设定值</param>
        /// <param name="chCount">超限次数设定值</param>
        /// <param name="statisticsSec">统计周期（秒）</param>
        /// <param name="outputAlarm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool AnalogChangeConsecutiveH(this Point point, double chValue, int chCount, int statisticsSec, bool outputAlarm = true, string key = "")
        {
            if (!point.Value.HasValue || !point.OldValue.HasValue)
                return false;
            bool returnValue = false;
            string preKey = string.IsNullOrEmpty(key) ? "AnalogChangeConsecutiveH" : key + "_AnalogChangeConsecutiveH";
            string statisticsBeginTimeKey = preKey + "_StatisticsBeginTime";
            double currentTime = TasksContainer.Instance.CurrentTotalSeconds;
            double statisticsBeginTime;
            //如果临时数据中无开始时间值，表示尚未开始
            if (!point.TryGetTempValue(statisticsBeginTimeKey, out statisticsBeginTime))
                point.SetTempValue(statisticsBeginTimeKey, currentTime);
            else
            {
                if ((currentTime - statisticsBeginTime) > statisticsSec)
                {
                    //已过本次统计周期，删除相关数据，开始下一统计周期
                    point.RemoveRelatedTempValue(preKey);
                    point.SetTempValue(statisticsBeginTimeKey, currentTime);
                }
                else
                {
                    //检查本次变化幅度是否超限
                    bool thisHigh = AnalogChangeConsecutiveH(point, chValue, false);

                    if (thisHigh)
                    {
                        //如果超限，读取之前的超限统计次数
                        string countKey = preKey + "_ChangeHighCount";
                        double count;
                        if (!point.TryGetTempValue(countKey, out count))
                            //如果之前没有就添加到临时数据
                            point.SetTempValue(countKey, 1);
                        else
                        {
                            point.SetTempValue(countKey, count + 1);
                            //检查增加一次后有没有达到报警条件，达到就输出报警
                            //仅在相等的那一次输出报警，防止频繁发报警
                            if ((count + 1) == chCount)
                            {
                                returnValue = true;
                                if (outputAlarm)
                                    point.SendMessage(MessageType.Alarm, string.Format("{0}变化幅度大(在{1}秒内变化幅度大于{2}达{3}次）", point.Description, statisticsSec, chValue, chCount));

                            }
                        }


                    }

                }
            }
            return returnValue;
        }
        /// <summary>
        /// 模拟量变化幅度高报警
        /// 此算法在一定的统计周期内监视上次的值和本次的值变化幅度超限的次数，当达到指定的次数时输出报警
        ///相关参数从模拟量点的配置中读取，模拟量点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <param name="outputAlarm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool AnalogChangeConsecutiveH(this Point point)
        {
            if (!point.AllowAlarm)
                return false;
            double chValue = Convert.ToDouble(point.GetParamSetting(ParamSetting.ChangeHighValue));
            int chCount = Convert.ToInt32(point.GetParamSetting(ParamSetting.ChangeHighCount));
            int statisticsSec = Convert.ToInt32(point.GetParamSetting(ParamSetting.ChangeHighStatisticsPeriod));
            string alarmString = point.GetParamSetting(ParamSetting.ChangeHighAlarm);
            bool outputAlarm = string.IsNullOrEmpty(alarmString);
            bool returnValue= AnalogChangeConsecutiveH(point, chValue, chCount, statisticsSec, outputAlarm);
            if (returnValue && !outputAlarm)
                point.SendMessage(MessageType.Alarm, alarmString);
            return returnValue;
        }
        /// <summary>
        /// 模拟量点低报警（可复位）
        /// </summary>
        /// <param name="point">模拟量点</param>
        /// <param name="lValue">低报警值</param>
        /// <param name="resetValue">报警复位值</param>
        /// <param name="outputAlarm">是否输出报警，可选，默认为true</param>
        /// <param name="key">临时数据键值前缀，可选</param>
        /// <returns></returns>
        public static bool AnalogL(this Point point, double lValue, double resetValue, bool outputAlarm = true, string key = "")
        {
            if (!point.Value.HasValue)
                return false;
            bool returnValue = false;
            string preKey = string.IsNullOrEmpty(key) ? "AnalogL" : key + "_AnalogL";
            double value = point.Value.Value;
            returnValue = TaskExtensions.RSPulse(point, value < lValue, value > resetValue, preKey);

            if (returnValue && outputAlarm)
                point.SendMessage(MessageType.Alarm, string.Format("{0}小于{1}报警", point.Description, lValue));

            return returnValue;
        }
        /// <summary>
        /// 模拟量点低报警（可复位）
        ///相关参数从模拟量点的配置中读取，模拟量点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point">模拟量点</param>
        /// <param name="outputAlarm">是否输出报警，可选，默认为true</param>
        /// <param name="key">临时数据键值前缀，可选</param>
        /// <returns></returns>
        public static bool AnalogL(this Point point)
        {
            if (!point.AllowAlarm)
                return false;
            double lValue = Convert.ToDouble(point.GetParamSetting(ParamSetting.LowValue));
            double resetValue = Convert.ToDouble(point.GetParamSetting(ParamSetting.LowResetValue));
            string alarmString = point.GetParamSetting(ParamSetting.LowAlarm);
            bool outputAlarm = string.IsNullOrEmpty(alarmString);
            bool returnValue= AnalogL(point, lValue, resetValue, outputAlarm);
            if (returnValue && !outputAlarm)
                point.SendMessage(MessageType.Alarm, alarmString);
            return returnValue;
        }
        /// <summary>
        /// 模拟量点高报警（可复位）
        /// </summary>
        /// <param name="point">模拟量点</param>
        /// <param name="lValue">高报警值</param>
        /// <param name="resetValue">报警复位值</param>
        /// <param name="outputAlarm">是否输出报警，可选，默认为true</param>
        /// <param name="key">临时数据键值前缀，可选</param>
        /// <returns>是否有报警</returns>
        public static bool AnalogH(this Point point, double hValue, double resetValue, bool outputAlarm = true, string key="")
        {
            if (!point.Value.HasValue)
                return false;
            bool returnValue = false;
            string preKey = string.IsNullOrEmpty(key) ? "AnalogH" : key + "_AnalogH";
            double value = point.Value.Value;
            returnValue = TaskExtensions.RSPulse(point, value > hValue, value < resetValue, preKey);

            if (returnValue && outputAlarm)
                point.SendMessage(MessageType.Alarm, string.Format("{0}大于{1}报警", point.Description, hValue));

            return returnValue;

        }
        /// <summary>
        /// 模拟量点高报警（可复位）
        ///相关参数从模拟量点的配置中读取，模拟量点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point">模拟量点</param>
        /// <param name="outputAlarm">是否输出报警，可选，默认为true</param>
        /// <param name="key">临时数据键值前缀，可选</param>
        /// <returns></returns>
        public static bool AnalogH(this Point point)
        {
            if (!point.AllowAlarm)
                return false;
            double hValue = Convert.ToDouble(point.GetParamSetting(ParamSetting.HighValue));
            double resetValue = Convert.ToDouble(point.GetParamSetting(ParamSetting.HighResetValue));
            string alarmString = point.GetParamSetting(ParamSetting.HighAlarm);
            bool outputAlarm = string.IsNullOrEmpty(alarmString);
            bool returnValue= AnalogH(point, hValue, resetValue, outputAlarm);
            if (returnValue && !outputAlarm)
                point.SendMessage(MessageType.Alarm, alarmString);
            return returnValue;
        }
        /// <summary>
        /// 模拟量高低值报警（可复位），优先检查高报，如果有高报就不再检查低报
        ///相关参数从模拟量点的配置中读取，模拟量点的AllowAlarm属性可屏蔽此算法
        /// </summary>
        /// <param name="point"></param>
        /// <param name="outputAlarm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool AnalogHL(this Point point)
        {
            if (AnalogH(point))
                return true;
            else
                return AnalogL(point);
           
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitMonitorCommon
{
    public static class TaskExtensions
    {
        /// <summary>
        /// 可以复位的条件判断，即首次满足置位条件时输出true，此后一直输出false，直到达到复位条件后再次满足置位条件才会再次输出true。
        /// </summary>
        /// <param name="task"></param>
        /// <param name="setCondition">置位条件</param>
        /// <param name="resetCondition">复位条件</param>
        /// <param name="preKey">临时值键值前缀</param>
        /// <returns></returns>
        public static bool CanReset(this TaskBase task, bool setCondition, bool resetCondition, string preKey)
        {
            bool returnValue = false;
            string alarmStateKey = preKey + "_AlarmState";
            if (setCondition)
            {
                if (!task.TempValue.ContainsKey(alarmStateKey))
                {
                    task.TempValue.Add(alarmStateKey, 1);
                    returnValue = true;
                }
            }
            else if (resetCondition)
            {
                task.TempValue.Remove(alarmStateKey);
            }
            return returnValue;
        }
        /// <summary>
        /// 可延时的条件判断，即满足置位条件并延迟指定时间后首次输出true，此后输出为false,一但条件不满足延迟计时清零并清除状态，此后才再次满足并达延时时输出true
        /// 注意和TdOn的输出的不同
        /// </summary>
        /// <param name="task"></param>
        /// <param name="setCondition"></param>
        /// <param name="tdOnSec"></param>
        /// <param name="preKey"></param>
        /// <returns></returns>
        public static bool CanTdOn(this TaskBase task, bool setCondition, int tdOnSec, string preKey)
        {
            bool returnValue = false;
            string alarmStateKey = preKey + "_AlarmState";
            string beginTimeKey = preKey + "_BeginTime";
            if (setCondition)
            {
                if (!task.TempValue.ContainsKey(alarmStateKey))
                {
                    //如果没有报警过，看有没有记录起始时间，没记录则记录
                    DateTime currentTime = TasksContainer.Instance().CurrentTime;
                    if (!task.TempValue.ContainsKey(beginTimeKey))
                        task.TempValue.Add(beginTimeKey, currentTime);
                    DateTime beginTime = (DateTime)task.TempValue[beginTimeKey];
                    //如果已达报警延时时间且未报警过，则报警
                    if ((beginTime.AddSeconds(tdOnSec) <= currentTime) && !task.TempValue.ContainsKey(alarmStateKey))
                    {
                        task.TempValue.Add(alarmStateKey, 1);
                        returnValue = true;
                    }
                }
            }
            else
            //如果值已不低于设定值，清除报警状态和起始时间
            {
                task.TempValue.Remove(beginTimeKey);
                task.TempValue.Remove(alarmStateKey);
            }
            return returnValue;
        }
        /// <summary>
        ///满足置位条件setCondition条件后输出true，此后保持true，直到复位条件resetCondition满足后输出false
        ///注意是set优先
        /// </summary>
        /// <param name="task"></param>
        /// <param name="setCondition">置位条件</param>
        /// <param name="resetCondition">复位条件</param>
        /// <param name="preKey"></param>
        /// <returns></returns>
        public static bool SetReset(this TaskBase task, bool setCondition, bool resetCondition, string preKey)
        {
            bool returnValue = false;
            string setStateKey = preKey + "_SetState";
            if (setCondition)
            {
                if (!task.TempValue.ContainsKey(setStateKey))
                {
                    task.TempValue.Add(setStateKey, 1);
                }
                returnValue = true;
            }
            else if (resetCondition)
            {
                task.TempValue.Remove(setStateKey);
                returnValue = false;
            }
            else
            {
                if (task.TempValue.ContainsKey(setStateKey))
                    returnValue = true;
                else
                    returnValue = false;
            }
            return returnValue;
        }
        public static bool TdOn(this TaskBase task, bool inputCondition, int tdOnSec, string preKey)
        {
            bool returnValue = false;
            string beginTimeKey = preKey + "_BeginTime";
            if (inputCondition)
            {
                DateTime currentTime = TasksContainer.Instance().CurrentTime;
                if (!task.TempValue.ContainsKey(beginTimeKey))
                    task.TempValue.Add(beginTimeKey, currentTime);
                DateTime beginTime = (DateTime)task.TempValue[beginTimeKey];
                if (beginTime.AddSeconds(tdOnSec) <= currentTime)
                    returnValue = true;

            }
            else
            {
                task.TempValue.Remove(beginTimeKey);
            }
            return returnValue;
        }
        public static bool TdOff(this TaskBase task, bool inputCondition, int tdOnSec, string preKey)
        {
            bool returnValue = true;
            string beginTimeKey = preKey + "_BeginTime";
            if (!inputCondition)
            {
                DateTime currentTime = TasksContainer.Instance().CurrentTime;
                if (!task.TempValue.ContainsKey(beginTimeKey))
                    task.TempValue.Add(beginTimeKey, currentTime);
                DateTime beginTime = (DateTime)task.TempValue[beginTimeKey];
                if (beginTime.AddSeconds(tdOnSec) <= currentTime)
                    returnValue = false;

            }
            else
            {
                task.TempValue.Remove(beginTimeKey);
            }
            return returnValue;
        }

        /// <summary>
        /// 条件满足后的累加时间，一但条件不满足则清零
        /// </summary>
        /// <param name="condition">检查的条件</param>
        /// <param name="second">以前累加的数值（秒）</param>
        /// <returns></returns>
        public static int ConditionSecSum(this TaskBase task, bool condition, int second)
        {
            if (condition && (task.RunCount > 0))
                return task.Period + second;
            return 0;
        }

        /// <summary>
        /// SR触发器逻辑判断,reset优先
        /// </summary>
        /// <param name="s">触发条件</param>
        /// <param name="r">复位条件</param>
        /// <param name="outValue">SR触发器输出值</param>
        /// <returns>当输出值由false变true时输出true，否则返回false</returns>
        public static bool SR(this TaskBase task, bool s, bool r, ref bool outValue)
        {
            if (r)
            {
                outValue = false;
                return false;
            }
            else
            {
                if (s)
                {
                    if (outValue)
                        return false;
                    else
                    {
                        outValue = true;
                        return true;
                    }
                }
                else
                    return false;
            }


        }
  


        /// <summary>
        /// 变化的限值，限值为一个Fx，字符串形式的Fx为x1:Y1;X2:y2
        /// </summary>
        /// <param name="x">x的值</param>
        /// <param name="y">y的值</param>
        /// <param name="fx">Fx的字符串形式</param>
        /// <param name="isHighLimit">是否为高限，默认为true，否则为低限</param>
        /// <returns></returns>
        public static bool FxLimit(this TaskBase task, double x, double y, string fx, bool isHighLimit = true)
        {
            FxSetting[] array = StrToFx(fx);
            int length = array.Length;
            if (length < 2)
                throw new Exception(string.Format("Fx设定错误：{0}", fx));
            for (int i = 1; i < length; i++)
            {
                if (array[i].X > x)
                {
                    double topX = array[i].X;
                    double topY = array[i].Y;
                    double bottomX = array[i - 1].X;
                    double bottomY = array[i - 1].Y;
                    double limitY = ((x - bottomX) * (topY - bottomY) / (topX - bottomX)) + bottomY;
                    if (isHighLimit)
                    {
                        if (y > limitY)
                            return true;
                    }
                    else
                    {
                        if (y < limitY)
                            return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 将字符串形式的Fx转换成FxSetting[]
        /// </summary>
        /// <param name="fx">字符串形式的Fx</param>
        /// <returns></returns>
        public static FxSetting[] StrToFx(string fx)
        {
            List<FxSetting> list = new List<FxSetting>();
            string[] array = fx.Split(new char[] { ';' });
            foreach (string item in array)
            {
                string[] strFx = item.Split(new char[] { ':' });
                if (strFx.Length == 2)
                {
                    FxSetting fxItem = new FxSetting();
                    fxItem.X = Convert.ToDouble(strFx[0]);
                    fxItem.Y = Convert.ToDouble(strFx[1]);
                }
            }
            return list.ToArray();
        }



    }
    /// <summary>
    /// 表示fx中的一组设定参数
    /// </summary>
    public class FxSetting
    {
        public double X { set; get; }
        public double Y { set; get; }
    }
    /// <summary>
    /// 高低限检测中的检测方式 
    /// </summary>
    public enum HLCkeckType
    {
        High,
        Low,
        Both
    }

}

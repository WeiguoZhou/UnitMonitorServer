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
        /// RS触发器条件判断，输出为首次发生的脉冲，即首次满足置位条件时输出true，此后一直输出false，直到达到复位条件后再次满足置位条件才会再次输出true。
        /// </summary>
        /// <param name="point"></param>
        /// <param name="setCondition">置位条件</param>
        /// <param name="resetCondition">复位条件</param>
        /// <param name="key">临时数据前缀，可选</param>
        /// <returns></returns>
        public static bool RSPulse(this Point point, bool setCondition, bool resetCondition,string key="")
        {
            bool returnValue = false;
            string alarmStateKey =string.IsNullOrEmpty(key)? "SRPulse_AlarmState": key +"_SRPulse_AlarmState";
            if (setCondition)
            {
                if (!point.ContainsTempValue(alarmStateKey))
                {
                    point.SetTempValue(alarmStateKey, 1);
                    returnValue = true;
                }
            }
            else if (resetCondition)
            {
                point.RemoveTempValue(alarmStateKey);
            }
            return returnValue;
        }
        /// <summary>
        ///  可延时的条件判断，即满足置位条件并延迟指定时间后首次输出true，此后输出为false,一但条件不满足延迟计时清零并清除状态，此后才再次满足并达延时时输出true
        /// 注意和TdOn的输出的不同
        /// </summary>
        /// <param name="point"></param>
        /// <param name="setCondition"></param>
        /// <param name="tdOnSec">延迟的时间</param>
        /// <param name="key">临时数据前缀，可选</param>
        /// <returns></returns>
        public static bool TdOnPulse(this Point point, bool setCondition, int tdOnSec, string key="")
        {
            bool returnValue = false;
            string preKey = string.IsNullOrEmpty(key) ? "TdOnPulse" : key + "_TdOnPulse";
            string alarmStateKey = preKey + "_AlarmState";
            string beginTimeKey = preKey + "_BeginTime";
            if (setCondition)
            {
                if (!point.ContainsTempValue(alarmStateKey))
                {
                    //如果没有报警过，看有没有记录起始时间，没记录则记录
                   double currentTime = TasksContainer.Instance.CurrentTotalSeconds;
                    double beginTime;

                    if (!point.TryGetTempValue(beginTimeKey, out beginTime))
                    {
                        beginTime = currentTime;
                        point.SetTempValue(beginTimeKey, beginTime);

                    }

                    //如果已达报警延时时间且未报警过，则报警
                    if (((beginTime + tdOnSec) <= currentTime) && !point.ContainsTempValue(alarmStateKey))
                    {
                        point.SetTempValue(alarmStateKey, 1);
                        returnValue = true;
                    }
                }
            }
            else
            //如果值已不低于设定值，清除报警状态和起始时间
            {
                point.RemoveTempValue(beginTimeKey);
                point.RemoveTempValue(alarmStateKey);
            }
            return returnValue;
        }
          /// <summary>
          /// RS触发器条件判断
          /// </summary>
          /// <param name="point"></param>
          /// <param name="setCondition">置位条件</param>
          /// <param name="resetCondition">复位条件</param>
          /// <param name="setFirst">是否是置位优先，可选，默认为true</param>
          /// <param name="key">临时数据键值前缀</param>
          /// <returns></returns>
        public static bool SR(this Point point, bool setCondition, bool resetCondition,bool setFirst=true, string key="")
        {
            bool returnValue = false;
                string setStateKey = string.IsNullOrEmpty(key) ? "SR_Output" : key + "_SR_Output";
            bool lastoutput = point.ContainsTempValue(setStateKey);
            if (setCondition)
            {
                returnValue = (resetCondition && (!setFirst)) ? false : true;
            }
            else 
            {
                returnValue = resetCondition? false : lastoutput;
            }
            if (lastoutput != returnValue)
            {
                if (lastoutput)
                    point.RemoveTempValue(setStateKey);
                else
                    point.SetTempValue(setStateKey, 1);
            }           
            return returnValue;
        }
        /// <summary>
        /// 延时On，即当输入条件满足时，等待一定时间输出为true，如中途输入条件不满足则重新计时
        /// </summary>
        /// <param name="point"></param>
        /// <param name="inputCondition">输入条件</param>
        /// <param name="tdOnSec">延时时间</param>
        /// <param name="key">临时数据前缀</param>
        /// <returns></returns>
        public static bool TdOn(this Point point, bool inputCondition, int tdOnSec, string key="")
        {
            bool returnValue = false;
            string beginTimeKey = string.IsNullOrEmpty(key) ? "TdOn_BeginTime":key + "_TdOn_BeginTime";
            if (inputCondition)
            {
                double currentTime = TasksContainer.Instance.CurrentTotalSeconds;
                double beginTime;

                if (!point.TryGetTempValue(beginTimeKey, out beginTime))
                {
                    beginTime = currentTime;
                    point.SetTempValue(beginTimeKey, beginTime);

                }
                if ((beginTime + tdOnSec) <= currentTime)
                    returnValue = true;
            }
            else
            {
                point.RemoveTempValue(beginTimeKey);
            }
            return returnValue;
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

}

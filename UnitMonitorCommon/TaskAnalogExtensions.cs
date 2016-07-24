using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommunication;

namespace UnitMonitorCommon
{
  public static  class TaskAnalogExtensions
    {
        /// <summary>
        /// 模拟量值低 延时报警
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias">模拟量点的别名</param>
        /// <param name="lValue">低设定值</param>
        /// <param name="tdOnSec">延时时间</param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool AnalogLTdOn(this TaskBase task,string pointAlias,double lValue,int tdOnSec,bool outputAlarm=true)
        {
            bool returnValue = false;
            double value = task.AnalogValue(pointAlias);
            //加这么长的前缀考虑当一个模拟量用了多个同一function时会冲突
            string preKey = pointAlias + "_AnalogLTdOn_" + lValue.ToString() + "_" + tdOnSec.ToString();
            if(TaskExtensions.CanTdOn(task,value<lValue,tdOnSec,preKey))
            {
                returnValue = true;
                if (outputAlarm)
                {
                    string beginTimeKey = preKey + "_BeginTime";
                    DateTime beginTime = (DateTime)task.TempValue[beginTimeKey];
                    DateTime currentTime = TasksContainer.Instance().CurrentTime;
                    int continueCount = Convert.ToInt32(CommUtil.TimeSecondSpan(beginTime, currentTime));
                    task.SendMessge(MessageType.Alarm, string.Format("{0}小于{1},持续{2}", task.PointNameDescription(pointAlias), lValue, continueCount));
                }
            }
            return returnValue;
         }
        /// <summary>
        /// 模拟量高延时报警
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias">模拟量点的别名</param>
        /// <param name="hValue">高报警值</param>
        /// <param name="tdOnSec">延迟时间</param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool AnalogHTdOn(this TaskBase task, string pointAlias, double hValue, int tdOnSec,bool outputAlarm=true)
        {
            bool returnValue = false;
            double value = task.AnalogValue(pointAlias);
            //加这么长的前缀考虑当一个模拟量用了多个同一function时会冲突
            string preKey = pointAlias + "_AnalogLTdOn_" + hValue.ToString() + "_" + tdOnSec.ToString();
            if (TaskExtensions.CanTdOn(task, value>hValue, tdOnSec, preKey))
            {
                returnValue = true;
                if (outputAlarm)
                {
                    string beginTimeKey = preKey + "_BeginTime";
                    DateTime beginTime = (DateTime)task.TempValue[beginTimeKey];
                    DateTime currentTime = TasksContainer.Instance().CurrentTime;
                    int continueCount = Convert.ToInt32(CommUtil.TimeSecondSpan(beginTime, currentTime));
                    task.SendMessge(MessageType.Alarm, string.Format("{0}小于{1},持续{2}", task.PointNameDescription(pointAlias), hValue, continueCount));
                }
            }
            return returnValue;
        }
    
    
        /// <summary>
        /// 模拟量高低延时报警
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias">模拟量点的别名</param>
        /// <param name="hValue">高报警值</param>
        /// <param name="lValue">低报警值</param>
        /// <param name="tdOnSec">延迟时间</param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool AnalogTdOnHL(this TaskBase task, string pointAlias, double hValue, double lValue, int tdOnSec,bool outputAlarm=true)
        {
            bool returnValue = false;
            if(AnalogHTdOn( task,  pointAlias,  hValue,  tdOnSec,outputAlarm))
                returnValue = true;
            if(AnalogLTdOn(task, pointAlias, lValue, tdOnSec,outputAlarm))
                returnValue = true;
            return returnValue;
        }

        /// <summary>
        /// 模拟量变化幅度低报警,用于检查调门有没有卡死，测点有没有死掉
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias">模拟量点的别名</param>
        /// <param name="clValue">模拟量变化幅度设定值</param>
        /// <param name="tdOnSec">延迟时间</param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool AnalogChangeLTdOn(this TaskBase task, string pointAlias, double clValue, int tdOnSec,bool outputAlarm=true)
        {
            bool returnValue = false;
            string preKey = pointAlias + "_AnalogChangeLTdOn_" + clValue.ToString() + "_" + tdOnSec.ToString();
            double value = task.AnalogValue(pointAlias);
            string oldValueKey = preKey + "_OldValue";
            //没旧数据键值，表示第一次取数据
            if (!task.TempValue.ContainsKey(oldValueKey))
            {
                task.TempValue.Add(oldValueKey, value);
            }
            else
            {
                double oldValue = (double)task.TempValue[oldValueKey];
                returnValue = TaskExtensions.CanTdOn(task, Math.Abs(oldValue - value) < clValue, tdOnSec, preKey);
                if(returnValue && outputAlarm)
                {
                    DateTime currentTime = TasksContainer.Instance().CurrentTime;
                    string beginTimeKey = preKey + "_BeginTime";
                    DateTime beginTime = (DateTime)task.TempValue[beginTimeKey];
                    int continueCount = Convert.ToInt32(CommUtil.TimeSecondSpan(beginTime, currentTime));
                    task.SendMessge(MessageType.Alarm, string.Format("{0}变化幅度低于{1},持续{2}", task.PointNameDescription(pointAlias), clValue, continueCount));

                }
            }
          
            return returnValue;
        }
        /// <summary>
        /// 模拟量变化幅度高报警
        /// 此算法按统计周期循环计算，当发现最大值与最小值的差值达到一定幅度时报警
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias">模拟量点的别名</param>
        /// <param name="chValue">模拟量变化幅度高设定值</param>
        /// <param name="statisticsSec">统计周期</param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>

        public static bool AnalogChangeH(this TaskBase task, string pointAlias, double chValue, int statisticsSec,bool outputAlarm=true)
        {
            bool returnValue = false;
            string preKey = pointAlias + "_AnalogChangeH_" + chValue.ToString() + "_" + statisticsSec.ToString();
            string statisticsBeginTimeKey = preKey + "_StatisticsBeginTime";
            DateTime currentTime = TasksContainer.Instance().CurrentTime;
            if (!task.TempValue.ContainsKey(statisticsBeginTimeKey))
                task.TempValue.Add(statisticsBeginTimeKey, currentTime);
            DateTime statisticsBeginTime = (DateTime)task.TempValue[statisticsBeginTimeKey];
            //如果小于，表示一个统计周期结束,清除所有临时数据
            if(statisticsBeginTime.AddSeconds(statisticsSec)<= currentTime)
            {
                task.RemoveRelatedKey( preKey);
            }
            else
            {
               double value = task.AnalogValue(pointAlias);
                string maxValueKey = preKey + "_MaxValue";
                string minValueKey = preKey + "_MinValue";
                //没旧数据键值，表示第一次取数据
                if (!task.TempValue.ContainsKey(maxValueKey))
                    task.TempValue.Add(maxValueKey, value);
                if (!task.TempValue.ContainsKey(minValueKey))
                    task.TempValue.Add(minValueKey, value);

                double maxValue = (double)task.TempValue[maxValueKey];
                double minValue = (double)task.TempValue[minValueKey];
                if (value > maxValue)
                    task.TempValue[maxValueKey] = value;
                if (value< minValue)
                    task.TempValue[minValueKey] = value;

                string alarmStateKey = preKey + "_AlarmState";

                if ((maxValue - minValue) >= chValue)
                {
                    if (!task.TempValue.ContainsKey(alarmStateKey))
                    {
                        task.TempValue.Add(alarmStateKey, 1);
                        returnValue = true;
                        if(outputAlarm)
                            task.SendMessge(MessageType.Alarm, string.Format("{0}在{1}秒内变化幅度大于{2}", task.PointNameDescription(pointAlias), statisticsSec, chValue));
                    }   
                }
            }
 
            return returnValue;
        }
        /// <summary>
        /// 模拟量点低报警（可复位）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias">模拟量点别名</param>
        /// <param name="lValue">低报警值</param>
        /// <param name="resetValue">低复位值</param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool AnalogL(this TaskBase task, string pointAlias, double lValue, double resetValue,bool outputAlarm=true)
        {
            bool returnValue = false;
            string preKey = pointAlias + "_AnalogL_" + lValue.ToString();
            double value = task.AnalogValue(pointAlias);
            if (TaskAnalogExtensions.AnalogL(task, value, lValue, resetValue, preKey))
            {
                returnValue = true;
                if(outputAlarm)
                    task.SendMessge(MessageType.Alarm, string.Format("{0}小于{1}", task.PointNameDescription(pointAlias), lValue));
            }
            return returnValue;
        }
        /// <summary>
        /// 模拟量高报警（可复位）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias">模拟量点别名</param>
        /// <param name="hValue">高设定值</param>
        /// <param name="resetValue">高复位值</param>
        /// <param name="resetValue">低复位值</param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool AnalogH(this TaskBase task, string pointAlias, double hValue, double resetValue,bool outputAlarm=true)
        {
            bool returnValue = false;
            string preKey = pointAlias + "_AnalogH_" + hValue.ToString();
            double value = task.AnalogValue(pointAlias);
            if (TaskAnalogExtensions.AnalogH(task, value, hValue, resetValue, preKey))
            {
                returnValue = true;
                if (outputAlarm)
                    task.SendMessge(MessageType.Alarm, string.Format("{0}大于{1}", task.PointNameDescription(pointAlias), hValue));
            }
            return returnValue;
        }
        /// <summary>
        /// 模拟量高低值报警（可复位）
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pointAlias">模拟量点别名</param>
        /// <param name="hValue">高设定值</param>
        /// <param name="resethValue">高复位值</param>
        /// <param name="lValue">低设定值</param>
        /// <param name="resetlValue">低复位值</param>
        /// <param name="outputAlarm">是否输出报警，默认为true</param>
        /// <returns>是否有报警</returns>
        public static bool AnalogHL(this TaskBase task, string pointAlias, double hValue, double resethValue, double lValue, double resetlValue,bool outputAlarm=true)
        {
            bool returnValue = false;
            if (AnalogH(task, pointAlias, hValue, resethValue, outputAlarm))
                returnValue = true;
            if (AnalogL(task, pointAlias, lValue, resetlValue, outputAlarm))
                returnValue = true;
            return returnValue;
        }
        public static bool AnalogL(this TaskBase task,double value,double lValue,double resetValue,string preKey)
        {
            return TaskExtensions.CanReset(task, value < lValue, value > resetValue, preKey);
        }
        public static bool AnalogH(this TaskBase task, double value, double hValue, double resetValue, string preKey)
        {
           
            return TaskExtensions.CanReset(task,value>hValue,value <resetValue,preKey);
        }
        /// <summary>
        /// 表示数个模拟量测点间偏差值大
        /// </summary>
        /// <param name="task"></param>
        /// <param name="value">数个模拟量测点的值</param>
        /// <param name="hValue">偏差设定值</param>
        /// <param name="tdOnSec">延时时间</param>
        /// <param name="preKey"></param>
        /// <returns></returns>

        public static bool AnalogDeviationH(this TaskBase task, double[] value, double hValue, int tdOnSec, string preKey)
        {
           double maxValue;
            double minValue ;
            double avgValue ;
            double sumValue ;
            MaxMinSumAvgValue(value, out maxValue, out minValue, out avgValue, out sumValue);
            string alarmStateKey = preKey + "_AlarmState";
            return TaskExtensions.CanTdOn(task, (maxValue - minValue) > hValue, tdOnSec, preKey);
        }
        /// <summary>
        /// 表示数个模拟量测点间偏差值大
        /// </summary>
        /// <param name="task"></param>
        /// <param name="alais">数个模拟量测点</param>
        /// <param name="hValue">偏差设定值</param>
        /// <param name="tdOnSec">延时时间</param>
        /// <param name="preKey"></param>
        /// <returns></returns>
        public static bool AnalogDeviationH(this TaskBase task, string[] alais, double hValue, int tdOnSec, string preKey)
        {
            double[] value = GetAnalogValues(task, alais);
            if (value != null)
                return AnalogDeviationH(task, value, hValue, tdOnSec, preKey);
            return false;

        }
        public static double[] GetAnalogValues(this TaskBase task, string[] alais)
        {
            if ((alais!=null) && (alais.Length > 0))
            {
                int nCount = alais.Length;
                double[] value = new double[nCount];
                for (int i = 0; i < nCount; i++)
                {
                    value[i] = task.AnalogValue(alais[i]);
                }
                return value;
            }
            return null;
        }

        /// <summary>
        /// 计算多个模拟量测点的最大值，最小值，平均值，和
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxValue"></param>
        /// <param name="minValue"></param>
        /// <param name="avgValue"></param>
        /// <param name="sumValue"></param>
        public static void MaxMinSumAvgValue(double[] value,out double maxValue,out double minValue,out double avgValue,out double sumValue)
        {
            maxValue = 0;
            minValue = 0;
            avgValue = 0;
            sumValue = 0;
            if(value != null)
            {
                int nCount = value.Length;
                if (nCount > 0)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        if (i == 0)
                        {
                            maxValue = value[i];
                            minValue = value[i];
                            sumValue= value[i];
                        }
                        else
                        {
                            sumValue+= value[i];
                            if(maxValue < value[i])
                                maxValue = value[i];
                            if(minValue > value[i])
                                minValue = value[i];
                        }
                    }

                    avgValue = sumValue/ nCount;
                }
   
            }
        }

 
    }
}

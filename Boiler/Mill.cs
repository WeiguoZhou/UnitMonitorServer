using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace Boiler
{
    public class Mill:TaskBase
    {
        #region "点号别名，防止拼写错误"
        const string  c_millRunState= "millRunState"; //磨煤机运行状态
        const string c_feederRunState = "feederRunState"; //给煤机运行状态
        const string c_feederCleanRunState = "feederCleanRunState"; //给煤机清扫链运行状态
        const string c_oilPumpState = "oilPumpState"; //油泵运行状态
        const string c_cycloneState = "cycloneState"; //旋转分离器运行状态
        const string c_heatterState = "heatterState"; //电加热运行状态
        const string c_heatterSLC = "heatterSLC"; //电加热SLC状态
 
        const string c_valveHotCut = "valveHotCut"; //磨煤机热风隔绝门
        const string c_valveColdCut = "valveColdCut"; //磨煤机冷风隔绝门
        const string c_valveInnerCut = "valveInnerCut"; //磨煤机进口快关门
        const string c_valveMillISeal = "valveMillSeal"; //磨煤机密封风门
        const string c_valveFireSteam = "valveFireSteam"; //磨煤机灭火蒸汽隔绝门
        const string c_valveFeederOut = "valveFeederOut"; //给煤机下阀
        const string c_valveFeederIn = "valveFeederIn"; //给煤机上阀
        const string c_valveFeederSeal = "valveFeederSeal"; //给煤机密封风门
        const string c_adjustColdAM = "adjustColdAM"; //冷风调门手自动
        const string c_adjustHotAM = "adjustHotAM"; //热风调门手自动
        const string c_adjustHotValue = "adjustHotValue"; //热风调门开度
        const string c_adjustColdValue = "adjustColdValue"; //冷风调门开度
        const string c_millCurrent = "millCurrent"; //磨煤机电流
        const string c_feederCurrent = "millCurrent"; //磨煤机电流
        const string c_tankTemp = "tankTemp"; //油箱油温
        const string c_oilP = "oilP"; //油压
        const string c_entranceWindP = "entranceWindP"; //磨进口风压
        const string c_outWindT1 = "outWindT1"; //磨出口温度
        const string c_windFlow1 = "windFlow1"; //磨风量1
        const string c_windFlow2 = "windFlow2"; //磨风量2
        const string c_windFlow3 = "windFlow3"; //磨风量3
        const string c_coalFlow = "coalFlow"; //给煤量
        const string c_feederSpeed = "feederSpeed"; //给煤机转速
        const string c_bearingTemp1 = "bearingTemp1"; //行星齿轮箱输入轴承温度1
        const string c_bearingTemp2 = "bearingTemp2"; //行星齿轮箱输入轴承温度2
        const string c_bearingTemp3 = "bearingTemp3"; //行星齿轮箱轴承温度1
        const string c_bearingTemp4 = "bearingTemp4"; //行星齿轮箱轴承温度2
        const string c_bearingTemp5 = "bearingTemp5"; //行星齿轮箱轴承温度3
        const string c_bearingTemp6 = "bearingTemp6"; //行星齿轮箱轴承温度4
        const string c_motorTemp1 = "motorTemp1"; //电机轴承温度1
        const string c_motorTemp2 = "motorTemp2"; //电机轴承温度2
        const string c_cycloneTemp1 = "cycloneTemp1"; //旋转分离器轴承温度1
        const string c_cycloneTemp2 = "cycloneTemp2"; //旋转分离器轴承温度2
        #endregion

        #region "在配置中查找设定值的键值"
        const string s_windFlowFx = "windFlowFx"; //煤量与风量的Fx
        
        #endregion
        #region "报警状态"

        #endregion
        #region "持续保存的数据"
        bool wTankTempH = false; //油箱油温大于50度连续报警，如果放到方法外就只报一次。
        int iHeatterRun_TankTH_Count = 0; //油箱油温如大于45度，电加热仍运行的累计运算次数
        int iHeatterRun_NoSLC_TCount = 0; //油箱电加热运行，SLC未投入的时间
        int iTankTL_NoHeatter_Count = 0; //油泵运行，油温小于40度，电加热未运行的累计运算次数
        int iTankTLL_OilRun_Count = 0; //油泵运行，油温小于25度的累计运算次数
        int iOilRun_PL_TCount = 0;  //油泵运行，油压低累计时间
        int vHeatterOldState; //电加热状态;
        #endregion
        protected override void Process()
        {
            //油箱油温


            //油箱油温高于50度报警
            this.AnalogHTdOn(c_tankTemp, 50, 300);
            //电加热故障报警
            this.PowerFault(c_heatterState);
            //电加热状态
            int vHeatterState = LpValue(c_heatterState);
            //电加热投入时
            if (vHeatterState.IsOn())
            {            
                //油箱油温如大于45度，电加热仍运行大于2分钟
                this.AnalogHTdOn(c_tankTemp, 45, 120);
                //油箱电加热运行，SLC未投入持续300秒以上报警
               if( this.CanTdOn(!BoolValue(c_heatterSLC),300, "HeatterOn_NoSLC"))
                    SendMessge(MessageType.Alarm, string.Format("{0}油箱电加热运行时，SLC未投入", this.TaskName));
            }


            //油泵运行状态
            int vOilPumpState = LpValue(c_oilPumpState);
            //如果油泵运行,电加热未运行，油箱油温低于35度大于2分钟报警
            if (vOilPumpState.IsOn() && !vHeatterState.IsOn())
            {
                if(this.AnalogLTdOn(c_tankTemp, 35, 120,false))
                    SendMessge(MessageType.Alarm, string.Format("{0}油泵运行，油温小于35度，电加热未运行", this.TaskName));
            }
  

            //油泵运行时，油箱油温小于25时报警
            if (vOilPumpState.IsOn())
            {
                if (this.AnalogL(c_tankTemp, 25, 30, false))
                    SendMessge(MessageType.Alarm, string.Format("{0}油泵运行，油温小于25度", this.TaskName));
            }
            //如果油泵运行大于2分钟，油压大于0.45或小于0.14报警
            if (this.LPTdOn(c_oilPumpState, 120))
                this.AnalogHL(c_oilP, 0.45, 4, 0.14, 0.16);

            //出口温度大于90度报警
            this.AnalogH(c_outWindT1, 90, 85);

            double vMillCurrent = AnalogValue(c_millCurrent);  //磨煤机电流
            int vMillRunState = LpValue(c_millRunState);  //磨煤机运行状态
           // bool millNewOn = this.NewOn(c_millRunState, false);

            //当磨煤机运行且电流大于35A时
            if(vMillRunState.IsOn() )
            {
                 if((vMillCurrent > 35) && !vOilPumpState.IsOn())
                      SendMessge(MessageType.Danger, string.Format("{0}磨煤机运行但油泵未运行", this.TaskName));  //连续报警
                DateTime currentTime = TasksContainer.Instance().CurrentTime;
                if (!this.TempValue.ContainsKey("MillRunTime"))
                    this.TempValue.Add("MillRunTime", currentTime);
                
                if (this.TempValue.ContainsKey("StopMonitorTemp"))
                {
                    this.SetPointUse(c_bearingTemp1);
                    this.SetPointUse(c_bearingTemp2);
                    this.SetPointUse(c_bearingTemp3);
                    this.SetPointUse(c_bearingTemp4);
                    this.SetPointUse(c_bearingTemp5);
                    this.SetPointUse(c_bearingTemp6);
                    this.SetPointUse(c_motorTemp1);
                    this.SetPointUse(c_motorTemp2);
                    this.SetPointUse(c_cycloneTemp1);
                    this.SetPointUse(c_cycloneTemp2);
                    this.TempValue.Remove("StopMonitorTemp");
                }
                 else
                {
                    //磨煤机电流大于85A持续3分钟发报警
                    this.AnalogHTdOn(c_millCurrent, 85, 180);
                    double runSecond = new TimeSpan(currentTime.Ticks - ((DateTime)this.TempValue["MillRunTime"]).Ticks).TotalSeconds;
                    //轴承温度监视，分为刚启动半小时内和半小时外两种情况
                    if (runSecond < 1800)
                    {
                        this.AnalogH(c_bearingTemp1, 60, 55);
                        this.AnalogH(c_bearingTemp2, 60, 55);
                        this.AnalogH(c_bearingTemp3, 60, 55);
                        this.AnalogH(c_bearingTemp4, 60, 55);
                        this.AnalogH(c_bearingTemp5, 60, 55);
                        this.AnalogH(c_bearingTemp6, 60, 55);
                        this.AnalogH(c_motorTemp1, 65, 60);
                        this.AnalogH(c_motorTemp2, 65, 60);
                        this.AnalogH(c_cycloneTemp1, 65, 60);
                        this.AnalogH(c_cycloneTemp2, 65, 60);
                        this.AnalogChangeH(c_bearingTemp1, 5, 300);
                        this.AnalogChangeH(c_bearingTemp2, 5, 300);
                        this.AnalogChangeH(c_bearingTemp3, 5, 300);
                        this.AnalogChangeH(c_bearingTemp4, 5, 300);
                        this.AnalogChangeH(c_bearingTemp5, 5, 300);
                        this.AnalogChangeH(c_bearingTemp6, 5, 300);
                        this.AnalogChangeH(c_motorTemp1, 5, 300);
                        this.AnalogChangeH(c_motorTemp2, 5, 300);
                        this.AnalogChangeH(c_cycloneTemp1, 5, 300);
                        this.AnalogChangeH(c_cycloneTemp2, 5, 300);
                    }
                    else
                    {
                        this.AnalogH(c_bearingTemp1, 65, 60);
                        this.AnalogH(c_bearingTemp2, 65, 60);
                        this.AnalogH(c_bearingTemp3, 65, 60);
                        this.AnalogH(c_bearingTemp4, 65, 60);
                        this.AnalogH(c_bearingTemp5, 65, 60);
                        this.AnalogH(c_bearingTemp6, 65, 60);
                        this.AnalogH(c_motorTemp1, 70, 65);
                        this.AnalogH(c_motorTemp2, 70, 65);
                        this.AnalogH(c_cycloneTemp1, 70, 65);
                        this.AnalogH(c_cycloneTemp2, 70, 65);
                        this.AnalogChangeH(c_bearingTemp1, 5, 600);
                        this.AnalogChangeH(c_bearingTemp2, 5, 600);
                        this.AnalogChangeH(c_bearingTemp3, 5, 600);
                        this.AnalogChangeH(c_bearingTemp4, 5, 600);
                        this.AnalogChangeH(c_bearingTemp5, 5, 600);
                        this.AnalogChangeH(c_bearingTemp6, 5, 600);
                        this.AnalogChangeH(c_motorTemp1, 5, 600);
                        this.AnalogChangeH(c_motorTemp2, 5, 600);
                        this.AnalogChangeH(c_cycloneTemp1, 5, 600);
                        this.AnalogChangeH(c_cycloneTemp2, 5, 600);
                    }

                }
            }
            else
            {
                if (!this.TempValue.ContainsKey("StopMonitorTemp"))
                {
                    this.SetPointUnUse(c_bearingTemp1);
                    this.SetPointUnUse(c_bearingTemp2);
                    this.SetPointUnUse(c_bearingTemp3);
                    this.SetPointUnUse(c_bearingTemp4);
                    this.SetPointUnUse(c_bearingTemp5);
                    this.SetPointUnUse(c_bearingTemp6);
                    this.SetPointUnUse(c_motorTemp1);
                    this.SetPointUnUse(c_motorTemp2);
                    this.SetPointUnUse(c_cycloneTemp1);
                    this.SetPointUnUse(c_cycloneTemp2);
                    this.TempValue.Add("StopMonitorTemp", 1);

                }
            }


            //风门检测

            int v_valveInnerCut = LpValue(c_valveInnerCut);
            //灭火蒸汽门开启发报警
            if (this.LPTdOff(c_valveFireSteam, 0))
                SendMessge(MessageType.Warn, string.Format("{0}灭火蒸汽门开启", this.TaskName));
            if (v_valveInnerCut.IsOn())
            {
                int v_valveHotCut = LpValue(c_valveHotCut);
                int v_valveColdCut = LpValue(c_valveColdCut);
                int v_valveFeederOut = LpValue(c_valveFeederOut);
                if ((!v_valveHotCut.IsOff())  || (!v_valveColdCut.IsOff()))
                {
                    //当进口快关门开启且冷热风门任一失去关闭信号时，如密封风门未开延时60秒发报警
                    if (this.LPTdOff(c_valveMillISeal,60))          
                            SendMessge(MessageType.Warn, string.Format("{0}风门开启时密封风门未开", this.TaskName));
                    //当进口快关门开启且冷热风门任一失去关闭信号时，如给煤机下阀开启但给煤机密封风门未开，延时60秒发报警
                    if(v_valveFeederOut.IsOn() && this.LPTdOff(c_valveFeederSeal, 60))
                        SendMessge(MessageType.Warn, string.Format("{0}风门开启时给煤机密封风门未开", this.TaskName));
                    //当磨未启且进口快关门开启且冷热风门任一失去关闭信号时（一般是通风或刚停），如入口风压大于6KPa延时60秒报警，通常是进口快关门未开出或测点坏。
                    if (vMillRunState.IsOff() && this.AnalogHTdOn(c_entranceWindP, 6, 60,false))
                        SendMessge(MessageType.Alarm, string.Format("{0}通风时入口风压高，通常是进口快关门未开出或测点坏", this.TaskName));
                }
                if (vMillRunState.IsOn())
                {
                    double maxWindFlow, minWindFlow, AvgWindFlow, sunWindFlow;
                    string[] c_windFlow = new string[] { c_windFlow1, c_windFlow2, c_windFlow3 };
                    TaskAnalogExtensions.MaxMinSumAvgValue(this.GetAnalogValues(c_windFlow), out maxWindFlow, out minWindFlow, out AvgWindFlow, out sunWindFlow);
                    if(this.CanReset((maxWindFlow-minWindFlow)>80, (maxWindFlow - minWindFlow) < 20, "WindFlow_Dev_High"))
                        SendMessge(MessageType.Alarm, string.Format("{0}三个风量偏差大于80", this.TaskName));
                    if(this.TdOn(AvgWindFlow<110,60, "WindFlow_TooLow"))
                        SendMessge(MessageType.Warn, string.Format("{0}运行时风量过低（小于110)", this.TaskName));

                }



            }
            int v_feederRunState = LpValue(c_feederRunState);
            if (v_feederRunState.IsOn())
            {
                //给煤量大于90T持续180秒报警
                this.AnalogHTdOn(c_coalFlow, 90, 180);
                //给煤机电流大于4.5A持续5分钟报警
                this.AnalogHTdOn(c_feederCurrent, 4.5, 300);
                //给煤机电流小于2.5A持续5分钟报警
                this.AnalogLTdOn(c_feederCurrent, 2.5, 300);
                double coalFlow = AnalogValue(c_coalFlow);
                if (coalFlow > 40)
                {
                    double speedPerCoal = AnalogValue(c_feederSpeed) / coalFlow;
                    if (this.TempValue.ContainsKey("FeederSpeedPerCoal"))
                    {
                        double olderSpeedPerCoal = (double)this.TempValue["FeederSpeedPerCoal"];
                        //当给煤机每吨煤需要的转速比最初记忆大1时，发煤层减薄报警
                        if (this.TdOn((speedPerCoal - olderSpeedPerCoal) >1,60, "SpeedPerCoal_Rising"))
                            SendMessge(MessageType.Alarm, string.Format("{0}给煤机皮带煤层减薄（有断煤倾向）", this.TaskName));
                        if (this.TdOn((speedPerCoal - olderSpeedPerCoal) <-1, 60, "SpeedPerCoal_Trailing"))
                            SendMessge(MessageType.Alarm, string.Format("{0}给煤机皮带煤层有加重倾向（煤水份增大或有异物）", this.TaskName));
                    }
                    else
                    {
                        this.TempValue.Add("FeederSpeedPerCoal", speedPerCoal);
                    }

                }
            }
        }
  
    }
}

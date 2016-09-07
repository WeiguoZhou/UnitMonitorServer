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
    public class Mill : TaskBase
    {
        #region "FunGroup的index"
        const int f_basic= 0; //必须监视项，不允许关闭
        const int f_oil = 1; //油系统
        const int f_bearingTemp = 2; //磨煤机、电机、旋转分离器轴承温度
        const int f_feeder = 3; //给煤机
        const int f_windValve = 4; //风门
        const int f_current = 5; //电流
        const int f_wind = 6; //磨煤机风量、风压、风温监视
        const int f_fireDetection = 7; //磨煤机火检
        #endregion
        #region "点表index，与配置文件中对应，防止引用错误"
        #region "FunGroup.Index=0,f_basic,必须监视项"
        const int p_millRunState = 0; //磨煤机运行状态
        const int p_oilPumpState = 1; //油泵运行状态
        const int p_heatterState = 2; //电加热运行状态
        const int p_valveInnerCut = 3; //磨煤机进口快关门
        const int p_valveFireSteam = 4; //磨煤机灭火蒸汽隔绝门

        #endregion
        #region "FunGroup.Index=1,f_oil,油系统"
        const int p_tankTemp = 0; //油箱油温
        const int p_oilP = 1; //油压
        const int p_heatterSLC = 2; //电加热SLC

        const int p_oilPL = 3; //油压低信号
        const int p_oilPLL1 = 4; //油压低低信号1
        const int p_oilPLL2 = 5; //油压低低信号2
        const int p_oilPLL3 = 6; //油压低低信号3
        const int p_oilFlowL = 7; //油流量低信号
        const int p_oilStrainerDPH = 8; //油滤网差压高信号
        const int p_oilTankLevelL = 9; //油箱油位低信号
        #endregion
        #region "FunGroup.Index=2,f_bearingTemp,磨煤机、电机、旋转分离器轴承温度"
        const int p_bearingTemp1 = 0; //行星齿轮箱输入轴承温度1
        const int p_bearingTemp2 = 1; //行星齿轮箱输入轴承温度2
        const int p_bearingTemp3 = 2; //行星齿轮箱轴承温度1
        const int p_bearingTemp4 = 3; //行星齿轮箱轴承温度2
        const int p_bearingTemp5 = 4; //行星齿轮箱轴承温度3
        const int p_bearingTemp6 = 5; //行星齿轮箱轴承温度4
        const int p_motorTemp1 = 6; //电机轴承温度1
        const int p_motorTemp2 = 7; //电机轴承温度2
        const int p_cycloneTemp1 = 8; //旋转分离器轴承温度1
        const int p_cycloneTemp2 = 9; //旋转分离器轴承温度2
        #endregion
        #region "FunGroup.Index=3,f_feeder,给煤机"
        const int p_feederRunState = 0; //给煤机运行状态
        const int p_feederCleanRunState = 1; //给煤机清扫链运行状态
        const int p_cycloneState = 2; //旋转分离器运行状态
        const int p_coalFlow = 3; //给煤量
        const int p_feederSpeed = 4; //给煤机转速
        const int p_coalInterruption = 5; //给煤机断煤信号
        const int p_coalClog = 6; //给煤机堵煤信号
        #endregion
        #region "FunGroup.Index=4,f_windValve ,风门"
        const int p_valveMillISeal = 0; //磨煤机密封风门
        const int p_valveFeederOut = 1; //给煤机下阀
        const int p_valveFeederIn = 2; //给煤机上阀
        const int p_valveFeederSeal = 3; //给煤机密封风门
        const int p_adjustColdAM = 4; //冷风调门手自动
        const int p_adjustHotAM = 5; //热风调门手自动
        const int p_adjustHotValue = 6; //热风调门开度
        const int p_adjustColdValue = 7; //冷风调门开度
        const int p_valveColdCut = 8; //磨煤机冷风隔绝门
        const int p_valveHotCut = 9; //磨煤机热风隔绝门
        #endregion
        #region "FunGroup.Index=5,f_current电流"
        const int p_millCurrent = 0; //磨煤机电流
        const int p_feederCurrent = 1; //给煤机电流
        const int p_cycloneCurrent = 2; //旋转分离器电流
        #endregion
        #region "FunGroup.Index=6,f_wind,磨煤机风量、风压、风温监视"
        const int p_entranceWindP = 0; //磨进口风压
        const int p_outWindP1 = 1; //磨出口风粉压力1
        const int p_outWindP2 = 2; //磨出口风粉压力2
        const int p_outWindT1 = 3; //磨出口温度1
        const int p_outWindT2 = 4; //磨出口温度2
        const int p_outWindT3 = 5; //磨出口温度3

        const int p_sealWindDp1 = 6; //密封风与一次风差压1
        const int p_sealWindDp2 = 7; //密封风与一次风差压2
        const int p_millBowlDp = 8; //磨碗差压
        const int p_windFlow1 = 9; //磨风量1
        const int p_windFlow2 = 10; //磨风量2
        const int p_windFlow3 = 11; //磨风量3
        #endregion
        #region "FunGroup.Index=7,f_fireDetection,磨煤机火检监视"
        const int p_fireDetection1 = 0; //#1角火检强度
        const int p_fireDetection2 = 1; //#2角火检强度
        const int p_fireDetection3 = 2; //#3角火检强度
        const int p_fireDetection4 = 3; //#4角火检强度
        const int p_fireDetectionSignal1 =4; //#1角火检信号
        const int p_fireDetectionSignal2 = 5; //#2角火检信号
        const int p_fireDetectionSignal3 = 6; //#3角火检信号
        const int p_fireDetectionSignal4 = 7; //#4角火检信号
        #endregion
        #endregion
    
   
        protected override void PreInit()
        {
            //RunCount == 0时尚未取到数据
            if (this.RunCount == 0)
                return;
            FunGroup fun = this.FunGroups[f_basic];
            //磨煤机运行状态
            Point pMillRunState = fun.Points[p_millRunState];
            if (pMillRunState.IsOn())
            {
                this.FunGroups[f_bearingTemp].Used = true;
                this.FunGroups[f_current].Used = true;
                this.FunGroups[f_feeder].Used = true;
                this.FunGroups[f_fireDetection].Used = true;              
            }
            else if (pMillRunState.IsOff())
            {
                this.FunGroups[f_bearingTemp].Stop();
                this.FunGroups[f_current].Stop();
                this.FunGroups[f_feeder].Stop();
                this.FunGroups[f_fireDetection].Stop();
            }
            Point pOilPumpState = fun.Points[p_oilPumpState];
            Point pHeatterState = fun.Points[p_heatterState];
            if(pOilPumpState.IsOn() || pHeatterState.IsOn())
            {
                this.FunGroups[f_oil].Used = true;
            }
            //油泵停止10分钟后且电加热停用时，终止监视油系统
            else if(pOilPumpState.TdOn(pOilPumpState.IsOff(),600) && pHeatterState.IsOff())
            {
                this.FunGroups[f_oil].Stop();
            }
            Point pValveInnerCut= fun.Points[p_valveInnerCut];
            if (!pValveInnerCut.IsOff())
            {
                this.FunGroups[f_wind].Used = true;
                this.FunGroups[f_windValve].Used = true;
            }
            else
            {
                this.FunGroups[f_wind].Stop();
                this.FunGroups[f_windValve].Stop();
            }

        }
        protected override void Process()
        {
            MonitorBasic();
            MonitorBearTemp();
            MonitorCurrent();
            MonitorFeeder();
            MonitorFireDetection();
            MonitorOil();
            MonitorWind();
            MonitorWindValve();
        }

        private void MonitorWindValve()
        {
            throw new NotImplementedException();
        }

        private void MonitorWind()
        {
            throw new NotImplementedException();
        }

        private void MonitorOil()
        {
            throw new NotImplementedException();
        }

        private void MonitorBasic()
        {
            FunGroup fun = this.FunGroups[f_basic];
            if (!fun.Used)
                return;
            fun.Points[p_millRunState].PowerNewFault();
            fun.Points[p_oilPumpState].PowerNewFault();
            fun.Points[p_heatterState].PowerNewFault();
            fun.Points[p_valveInnerCut].ValveNewFault();
            Point pValveFireSteam = fun.Points[p_valveFireSteam];
            //灭火蒸汽门
            if (pValveFireSteam.TdOn(!pValveFireSteam.IsOff(), 0))
                RaiseSendMessage(MessageType.Warn, string.Format("{0}未关闭", pValveFireSteam.Description));
        }

        private void MonitorFireDetection()
        {
            throw new NotImplementedException();
        }

        private void MonitorFeeder()
        {
            throw new NotImplementedException();
        }

        private void MonitorCurrent()
        {
            throw new NotImplementedException();
        }

        private void MonitorBearTemp()
        {
            FunGroup fun = this.FunGroups[f_bearingTemp];
            if (!fun.Used)
                return;
            fun.Points[p_bearingTemp1].AnalogH();
            fun.Points[p_bearingTemp2].AnalogH();
            fun.Points[p_bearingTemp3].AnalogH();
            fun.Points[p_bearingTemp4].AnalogH();
            fun.Points[p_bearingTemp5].AnalogH();
            fun.Points[p_bearingTemp6].AnalogH();
            fun.Points[p_motorTemp1].AnalogH();
            fun.Points[p_motorTemp2].AnalogH();
            fun.Points[p_cycloneTemp1].AnalogH();
            fun.Points[p_cycloneTemp1].AnalogH();
            Point pMillRunState = this.FunGroups[f_basic].Points[p_millRunState];
            string bearingChangeHighCheck = fun.GetParamSetting("bearingChangeHighCheck");
            bool isCheck = string.IsNullOrEmpty(bearingChangeHighCheck) ? false : Convert.ToBoolean(bearingChangeHighCheck);
            if (!isCheck)
                return;
            if (pMillRunState.TdOn(pMillRunState.IsOn(), 1800, "MillOn1800"))
            {
                //如果磨启动后超半小时
                fun.Points[p_bearingTemp1].AnalogChangeH(5,300);
                fun.Points[p_bearingTemp2].AnalogChangeH(5, 300);
                fun.Points[p_bearingTemp3].AnalogChangeH(5, 300);
                fun.Points[p_bearingTemp4].AnalogChangeH(5, 300);
                fun.Points[p_bearingTemp5].AnalogChangeH(5, 300);
                fun.Points[p_bearingTemp6].AnalogChangeH(5, 300);
                fun.Points[p_motorTemp1].AnalogChangeH(5, 300);
                fun.Points[p_motorTemp2].AnalogChangeH(5, 300);
                fun.Points[p_cycloneTemp1].AnalogChangeH(5, 300);
                fun.Points[p_cycloneTemp2].AnalogChangeH(5, 300);
            }
            else
            {
                //如果磨启动后半小时内
                fun.Points[p_bearingTemp1].AnalogChangeH(8, 300);
                fun.Points[p_bearingTemp2].AnalogChangeH(8, 300);
                fun.Points[p_bearingTemp3].AnalogChangeH(8, 300);
                fun.Points[p_bearingTemp4].AnalogChangeH(8, 300);
                fun.Points[p_bearingTemp5].AnalogChangeH(8, 300);
                fun.Points[p_bearingTemp6].AnalogChangeH(8, 300);
                fun.Points[p_motorTemp1].AnalogChangeH(8, 300);
                fun.Points[p_motorTemp2].AnalogChangeH(8, 300);
                fun.Points[p_cycloneTemp1].AnalogChangeH(8, 300);
                fun.Points[p_cycloneTemp2].AnalogChangeH(8, 300);

            }
        }

    

  
        /// <summary>
        /// 轴承温度监测
        /// </summary>
    
        /// <summary>
        /// 风门风量监测
        /// </summary>
       
        }
    }

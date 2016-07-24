using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace Boiler
{
    class MainSteam : TaskBase
    {
        #region "点号别名，防止拼写错误"
        const string c_valveHPA = "valveHPA"; //高旁减压阀A开度
        const string c_valveHPB = "valveHPB"; //高旁减压阀B开度
        const string c_valveHPC = "valveHPC"; //高旁减压阀C开度
        const string c_valveHPD = "valveHPD"; //高旁减压阀D开度
        const string c_valveHPWaterA = "valveHPWaterA"; //高旁减温调门A开度
        const string c_valveHPWaterB = "valveHPWaterB"; //高旁减温调门B开度
        const string c_valveHPWaterC = "valveHPWaterC"; //高旁减温调门C开度
        const string c_valveHPWaterD = "valveHPWaterD"; //高旁减温调门D开度
        const string c_HPQuickOpen = "HPQuickOpen"; //高旁快开动作
        const string c_HPAuto = "HPAuto"; //高旁控制自动

        const string c_valveHPAMA = "valveHPAMA"; //高旁减压阀A手自动
        const string c_valveHPBMA = "valveHPBMA"; //高旁减压阀B手自动
        const string c_valveHPCMA = "valveHPCMA"; //高旁减压阀C手自动
        const string c_valveHPDMA = "valveHPDMA"; //高旁减压阀D手自动
        const string c_valveHPWaterAMA = "valveHPWaterAMA"; //高旁减温调门A手自动
        const string c_valveHPWaterBMA = "valveHPWaterBMA"; //高旁减温调门B手自动
        const string c_valveHPWaterCMA = "valveHPWaterCMA"; //高旁减温调门C手自动
        const string c_valveHPWaterDMA = "valveHPWaterDMA"; //高旁减温调门D手自动
        const string c_valveHPWaterAStop = "valveHPWaterAStop"; //高旁A减温隔绝门
        const string c_valveHPWaterBStop = "valveHPWaterBStop"; //高旁B减温隔绝门
        const string c_valveHPWaterCStop = "valveHPWaterCStop"; //高旁C减温隔绝门
        const string c_valveHPWaterDStop = "valveHPWaterDStop"; //高旁D减温隔绝门

        const string c_HPATemp1 = "HPATemp1"; //高旁减压阀A阀后温度1
        const string c_HPATemp2 = "HPATemp2"; //高旁减压阀A阀后温度2
        const string c_HPBTemp1 = "HPBTemp1"; //高旁减压阀B阀后温度1
        const string c_HPBTemp2 = "HPBTemp2"; //高旁减压阀B阀后温度2
        const string c_HPCTemp1 = "HPCTemp1"; //高旁减压阀C阀后温度1
        const string c_HPCTemp2 = "HPCTemp2"; //高旁减压阀C阀后温度2
        const string c_HPDTemp1 = "HPDTemp1"; //高旁减压阀D阀后温度1
        const string c_HPDTemp2 = "HPDTemp2"; //高旁减压阀D阀后温度2

        const string c_valveLPA = "valveLPA"; //低旁减压阀A开度
        const string c_valveLPB = "valveLPB"; //低旁减压阀B开度
        const string c_valveLPWaterA = "valveLPWaterA"; //低旁减温调门A开度
        const string c_valveLPWaterB = "valveLPWaterB"; //低旁减温调门B开度

        const string c_valveLPAMA = "valveLPAMA"; //低旁减压阀A手自动
        const string c_valveLPBMA = "valveLPBMA"; //低旁减压阀B手自动
        const string c_valveLPWaterAMA = "valveLPWaterAMA"; //低旁减温调门A手自动
        const string c_valveLPWaterBMA = "valveLPWaterBMA"; //低旁减温调门B手自动
        const string c_valveLPWaterAStop = "valveLPWaterAStop"; //低旁A减温水隔绝门
        const string c_valveLPWaterBStop = "valveLPWaterBStop"; //低旁B减温水隔绝门

        const string c_LPAuto = "LPAuto"; //低旁控制自动
        const string c_LPQuickClose = "LPQuickClose"; //低旁快关动作
        const string c_LPPressureSP = "LPPressureSP"; //低旁压力设定值
        const string c_LPPressurePV = "LPPressurePV"; //低旁前压力实际值
        const string c_LPATemp = "LPATemp"; //低旁减压阀A阀后温度
        const string c_LPBTemp = "LPATemp"; //低旁减压阀B阀后温度

 
        const string c_unitLoad = "unitLoad"; //机组负荷

        #endregion

        #region "在配置中查找设定值的键值"

        #endregion
        protected override void Process()
        {
            //高旁快开动作报警
            if(this.RisingEdge(c_HPQuickOpen))
                SendMessge(MessageType.Danger, string.Format("{0}高旁快开动作", this.TaskName));
            //低旁快关动作报警
            if (this.RisingEdge(c_LPQuickClose))
                SendMessge(MessageType.Danger, string.Format("{0}低旁快关动作", this.TaskName));
            //高旁减温水隔绝门故障
            this.ValveFault(c_valveHPWaterAStop);
            this.ValveFault(c_valveHPWaterBStop);
            this.ValveFault(c_valveHPWaterCStop);
            this.ValveFault(c_valveHPWaterDStop);
            //低旁减温水隔绝门故障
            this.ValveFault(c_valveLPWaterAStop);
            this.ValveFault(c_valveLPWaterBStop);


            double unitLoad = AnalogValue(c_unitLoad);
            //3000MW以后监视,250MW后停止监视
            if(this.SetReset(unitLoad>300, unitLoad<250,"UnitLoad>300"))
            {
                //高旁控制切手动
                this.ToMan(c_HPAuto);
                //低旁控制切手动
                this.ToMan(c_LPAuto);
                   
                //监视高旁减压阀，开度大于3时报警，小于2时复位
                this.AnalogH(c_valveHPA, 3, 2);
                this.AnalogH(c_valveHPB, 3, 2);
                this.AnalogH(c_valveHPC, 3, 2);
                this.AnalogH(c_valveHPD, 3, 2);
                //监视高旁减温水调门，开度大于3时报警，小于2时复位
                this.AnalogH(c_valveHPWaterA, 3, 2);
                this.AnalogH(c_valveHPWaterB, 3, 2);
                this.AnalogH(c_valveHPWaterC, 3, 2);
                this.AnalogH(c_valveHPWaterD, 3, 2);

                //监视高旁减温后温度偏差大于5度报警，小于3度复位
                if (this.AnalogH(Math.Abs(AnalogValue(c_HPATemp1)-AnalogValue(c_HPATemp2)), 5, 3,"HPATempDev"))
                   SendMessge(MessageType.Alarm,string.Format("{0}A高旁减温后温度偏差大于5度",this.TaskName)) ;
                if (this.AnalogH(Math.Abs(AnalogValue(c_HPBTemp1) - AnalogValue(c_HPBTemp2)), 5, 3, "HPBTempDev"))
                    SendMessge(MessageType.Alarm, string.Format("{0}B高旁减温后温度偏差大于5度", this.TaskName));
                if (this.AnalogH(Math.Abs(AnalogValue(c_HPCTemp1) - AnalogValue(c_HPCTemp2)), 5, 3, "HPCTempDev"))
                    SendMessge(MessageType.Alarm, string.Format("{0}C高旁减温后温度偏差大于5度", this.TaskName));
                if (this.AnalogH(Math.Abs(AnalogValue(c_HPDTemp1) - AnalogValue(c_HPDTemp2)), 5, 3, "HPDTempDev"))
                    SendMessge(MessageType.Alarm, string.Format("{0}D高旁减温后温度偏差大于5度", this.TaskName));
                //监视高旁减温后温度如果5分钟内变化超5度发晃动大报警
                this.AnalogChangeH(c_HPATemp1, 5, 300);
                this.AnalogChangeH(c_HPATemp2, 5, 300);
                this.AnalogChangeH(c_HPBTemp1, 5, 300);
                this.AnalogChangeH(c_HPBTemp2, 5, 300);
                this.AnalogChangeH(c_HPCTemp1, 5, 300);
                this.AnalogChangeH(c_HPCTemp2, 5, 300);
                this.AnalogChangeH(c_HPDTemp1, 5, 300);
                this.AnalogChangeH(c_HPDTemp2, 5, 300);
                //监视高旁减温后温度如果大于280度报警,270度复置报警
                this.AnalogH(c_HPATemp1, 280, 270);
                this.AnalogH(c_HPATemp2, 280, 270);
                this.AnalogH(c_HPBTemp1, 280, 270);
                this.AnalogH(c_HPBTemp2, 280, 270);
                this.AnalogH(c_HPCTemp1, 280, 270);
                this.AnalogH(c_HPCTemp2, 280, 270);
                this.AnalogH(c_HPDTemp1, 280, 270);
                this.AnalogH(c_HPDTemp2, 280, 270);



                //监视高旁减温水隔绝门是否刚失去关闭信号
                this.NewLostOff(c_valveHPWaterAStop);
                this.NewLostOff(c_valveHPWaterBStop);
                this.NewLostOff(c_valveHPWaterCStop);
                this.NewLostOff(c_valveHPWaterDStop);

                //监视高旁减压阀切手动报警
                this.ToMan(c_valveHPAMA);
                this.ToMan(c_valveHPBMA);
                this.ToMan(c_valveHPCMA);
                this.ToMan(c_valveHPDMA);
                //监视高旁减温水调门切手动报警
                this.ToMan(c_valveHPWaterAMA);
                this.ToMan(c_valveHPWaterBMA);
                this.ToMan(c_valveHPWaterCMA);
                this.ToMan(c_valveHPWaterDMA);

                //监视低旁减压阀，开度大于3时报警，小于2时复位
                this.AnalogH(c_valveLPA, 3, 2);
                this.AnalogH(c_valveLPB, 3, 2);
                //监视低旁减温水调门，开度大于3时报警，小于2时复位
                this.AnalogH(c_valveLPWaterA, 3, 2);
                this.AnalogH(c_valveLPWaterB, 3, 2);

                //监视低旁阀后温度如果5分钟内变化超5度发晃动大报警
                this.AnalogChangeH(c_LPATemp, 5, 300);
                this.AnalogChangeH(c_LPBTemp, 5, 300);
                //监视低旁减压阀切手动报警
                this.ToMan(c_valveLPAMA);
                this.ToMan(c_valveLPBMA);
                //监视低旁减温水调门切手动报警
                this.ToMan(c_valveLPWaterAMA);
                this.ToMan(c_valveLPWaterBMA);
                //监视低旁减温后温度如果5分钟内变化超5度发晃动大报警
                this.AnalogChangeH(c_LPATemp, 5, 300);
                this.AnalogChangeH(c_LPBTemp, 5, 300);
                //监视低旁减温后温度如果大于80度报警,70度复置报警
                this.AnalogH(c_LPATemp, 80, 70);
                this.AnalogH(c_LPBTemp, 80, 70);

                double presLPSP = AnalogValue(c_LPPressureSP);
                double presLPPV = AnalogValue(c_LPPressurePV);
                //低旁压力设定值与实际值之差小于0.2报警
                if (this.AnalogL(presLPSP- presLPPV,0.2,0.4,"LPSPPVDev"))
                    SendMessge(MessageType.Warn, string.Format("{0}低旁压力设定值与实际值之差小于0.2Mpa", this.TaskName));
            }
            
        }
    }
}

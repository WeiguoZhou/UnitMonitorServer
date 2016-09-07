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
          
        }
    }
}

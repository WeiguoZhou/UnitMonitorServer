using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace Boiler
{
    class SCR : TaskBase
    {
        #region "点号别名，防止拼写错误"
        const string c_ChimneyOutNox = "chimneyOutNox"; //烟囱出口NOX浓度
        const string c_valveSCR = "valveSCR"; //脱硝喷氨隔绝门
        const string c_dilutPumpA = "dilutPumpA";//A稀释风机
        const string c_dilutPumpB = "dilutPumpB";//A稀释风机
        const string c_dilutPumpATemp1 = "dilutPumpATemp1";//A稀释风机轴承温度1
        const string c_dilutPumpATemp2 = "dilutPumpATemp2";//A稀释风机轴承温度2
        const string c_dilutPumpBTemp1 = "dilutPumpATemp1";//B稀释风机轴承温度1
        const string c_dilutPumpBTemp2 = "dilutPumpATemp2";//B稀释风机轴承温度2
        const string c_ScrEfficiency = "scrEfficiency"; //脱硝效率
        const string c_ScrAFault = "scrAFault"; //脱硝A侧烟气分析仪故障
        const string c_ScrBFault = "scrBFault"; //脱硝B侧烟气分析仪故障
        const string c_ScrInFault = "scrInFault"; //脱硝入口烟气分析仪故障
        const string c_NH3P = "NH3P"; //氨气压力
        const string c_sootBlowingSLC = "sootBlowingSLC"; //声波吹灰SLC
        #endregion
        #region "在配置中查找设定值的键值"
        #endregion
        #region "持续保存的数据"

        #endregion


        protected override void Process()
        {

        }
    }
}



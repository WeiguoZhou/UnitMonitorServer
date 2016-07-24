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
        const string c_sootBlowingSLC  = "sootBlowingSLC"; //声波吹灰SLC
        #endregion
        #region "在配置中查找设定值的键值"
        #endregion
        #region "持续保存的数据"

        #endregion


        protected override void Process()
        {
            //检测喷氨隔绝门故障
            this.ValveFault(c_valveSCR);
            int valveSCR = LpValue(c_valveSCR);
            //检测喷氨隔绝门关闭
            this.NewOff(c_valveSCR);
            //如果喷氨隔绝门打开(延时600秒)
            if (this.LPTdOn(c_valveSCR,600))
            {
                //烟囱出口NOX浓度高低报警，高50低10延时300秒
                this.AnalogTdOnHL(c_ChimneyOutNox, 50, 10, 300);
                //脱硝效率低于60%延时300秒报警
                this.AnalogLTdOn(c_ScrEfficiency, 60, 300);
                double chimneyOutNoxValue = AnalogValue(c_ChimneyOutNox);
                string chimneyOutNoxOldValueKey = c_ChimneyOutNox + "_OldValue";
                if (this.TempValue.ContainsKey(chimneyOutNoxOldValueKey))
                {
                    double chimneyOutNoxOldValue = (double)this.TempValue[chimneyOutNoxOldValueKey];
                    if (this.TdOn(Math.Abs(chimneyOutNoxOldValue - chimneyOutNoxValue) < 0.01, 600, "ChimneyOutNox_OldDev"))
                        this.SendMessge(MessageType.Warn, string.Format("{0}烟囱出口NOx无变化持续10分钟"));
                    this.TempValue[chimneyOutNoxOldValueKey] = chimneyOutNoxValue;
                }
                else
                    this.TempValue.Add(chimneyOutNoxOldValueKey, chimneyOutNoxValue);

                if (this.TdOn(BoolValue(c_ScrAFault), 300, "ScrAFault"))
                     this.SendMessge(MessageType.Warn, string.Format("{0}A侧出口烟气分析仪故障"));
                if (this.TdOn(BoolValue(c_ScrBFault), 300, "ScrBFault"))
                    this.SendMessge(MessageType.Warn, string.Format("{0}B侧出口烟气分析仪故障"));
                if (this.TdOn(BoolValue(c_ScrInFault), 300, "ScrInFault"))
                    this.SendMessge(MessageType.Warn, string.Format("{0}入口烟气分析仪故障"));
            }
            //如果脱硝投用，氨 气压力大于0.48报警，小于0.15报警
            if (valveSCR.IsOn())
            {
                this.AnalogHL(c_NH3P, 0.48, 0.4, 0.15, 0.2);
            }
            //稀释风机
            int dilutPumpA= LpValue(c_dilutPumpA);
            int dilutPumpB = LpValue(c_dilutPumpB);
            //如果稀释风机启动，轴承温度升到60度报警
            if (dilutPumpA.IsOn())
            {
                this.AnalogH(c_dilutPumpATemp1, 60, 55);
                this.AnalogH(c_dilutPumpATemp2, 60, 55);              
            }
            if (dilutPumpB.IsOn())
            {
                this.AnalogH(c_dilutPumpBTemp1, 60, 55);
                this.AnalogH(c_dilutPumpBTemp2, 60, 55);
            }
            //如果稀释风机启动10分钟后，半小时内轴承温升大于5度报警
            if (this.LPTdOn(c_dilutPumpA, 600))
            {
                this.AnalogChangeH(c_dilutPumpATemp1, 5, 1800);
                this.AnalogChangeH(c_dilutPumpATemp2, 5, 1800);

            }
            if (this.LPTdOn(c_dilutPumpB, 600))
            {
                this.AnalogChangeH(c_dilutPumpBTemp1, 5, 1800);
                this.AnalogChangeH(c_dilutPumpBTemp2, 5, 1800);

            }
            //声波吹灰SLC退出,延时600秒发消息
            if (this.CanTdOn(BoolValue(c_sootBlowingSLC), 600, c_sootBlowingSLC))
                SendMessge(MessageType.Info, string.Format("{0}声波吹灰SLC退出（已延时500秒）", this.TaskName));
        }

        }
    }


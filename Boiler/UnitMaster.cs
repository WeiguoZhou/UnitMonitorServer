using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommon;
using UnitMonitorCommunication;

namespace Boiler
{
    class UnitMaster : TaskBase
    {
        #region "点号别名，防止拼写错误"
        const string c_MFTAct = "MFTAct"; //MFT动作
        const string c_TurbineTrip = "turbineTrip"; //汽机跳闸
        const string c_genTrip = "genTrip"; //发变组跳闸
        const string c_uldRB = "uldRB"; //RB动作
        const string c_smoothP = "smoothP"; //定滑压
        const string c_oneThough = "oneThough"; //干湿态
        const string c_unitPressureSP = "unitPressureSP"; //机组压力设定值
        const string c_unitPressurePV = "unitPressurePV"; //机组主汽压力
        const string c_unitLoad = "unitLoad"; //机组负荷
        const string c_avc = "avc"; //AVC

        const string c_feederAState = "feederAState"; //给煤机A运行状态
        const string c_feederBState = "feederBState"; //给煤机B运行状态
        const string c_feederCState = "feederCState"; //给煤机C运行状态
        const string c_feederDState = "feederDState"; //给煤机D运行状态
        const string c_feederEState = "feederEState"; //给煤机E运行状态
        const string c_feederFState = "feederFState"; //给煤机F运行状态
        const string c_realTotalCoal = "realTotalCoal"; //实际总煤量
        const string c_TMax = "TMax"; //TMax
        const string c_TMax2 = "TMax2"; //TMax2
        #endregion

        #region "在配置中查找设定值的键值"

        #endregion
        protected override void Process()
        {
            double unitLoad = AnalogValue(c_unitLoad);
            //机组汽压设定值
            double unitPressureSP = AnalogValue(c_unitPressureSP);
            double unitPressurePV = AnalogValue(c_unitPressurePV);
            if (this.AnalogH(unitPressurePV - unitPressureSP, 1.0, 0.6, "UnitPressureSPPVDev"))
                SendMessge(MessageType.Warn, string.Format("{0}主汽压力设定值与实际值偏差大于1.0MPa", this.TaskName));
            if (this.AnalogH(unitPressurePV, 28.0, 27, "UnitPressurePVHigh"))
                SendMessge(MessageType.Warn, string.Format("{0}大于28.0MPa，大于28.5高旁将快开", this.PointNameDescription(c_unitPressurePV)));
            if (this.AnalogH(unitPressureSP, 28.0, 27, "UnitPressureSPHigh"))
                SendMessge(MessageType.Warn, string.Format("{0}大于28.0MPa，大于28.5高旁将快开", this.PointNameDescription(c_unitPressureSP)));
            if (this.TrailingEdge(c_smoothP, c_smoothP))
                SendMessge(MessageType.Warn, string.Format("{0}机组切至定压模式", this.TaskName));
            if(this.RunCount==0 && BoolValue(c_smoothP))
                SendMessge(MessageType.Warn, string.Format("{0}机组处于定压模式", this.TaskName));
            if (this.TrailingEdge(c_oneThough))
                SendMessge(MessageType.Warn, string.Format("{0}机组转至湿态", this.TaskName));
            if (this.RisingEdge(c_oneThough))
                SendMessge(MessageType.Info, string.Format("{0}机组转至干态", this.TaskName));

            if (this.RisingEdge(c_MFTAct))
                SendMessge(MessageType.Danger, string.Format("{0}MFT动作", this.TaskName));
            if (this.RisingEdge(c_TurbineTrip))
                SendMessge(MessageType.Danger, string.Format("{0}汽机跳闸", this.TaskName));
            if (this.RisingEdge(c_genTrip))
                SendMessge(MessageType.Danger, string.Format("{0}发变组跳闸", this.TaskName));
            if (this.RisingEdge(c_uldRB))
                SendMessge(MessageType.Danger, string.Format("{0}RB动作", this.TaskName));

            if((unitLoad>350) && this.BoolTdOff(c_avc,120,false))
                SendMessge(MessageType.Warn, string.Format("{0}机组负荷大于350MW，AVC未投入", this.TaskName));

            int totalFeeder = 0;
            if (LpValue(c_feederAState).IsOn())
                totalFeeder += 1;
            if (LpValue(c_feederBState).IsOn())
                totalFeeder += 1;
            if (LpValue(c_feederCState).IsOn())
                totalFeeder += 1;
            if (LpValue(c_feederDState).IsOn())
                totalFeeder += 1;
            if (LpValue(c_feederEState).IsOn())
                totalFeeder += 1;
            if (LpValue(c_feederFState).IsOn())
                totalFeeder += 1;
            if(totalFeeder>=5)
            {
                double avgFeederCoal = AnalogValue(c_realTotalCoal) / totalFeeder;
               if (this.AnalogL(avgFeederCoal,50,60, "avgFeederCoal"))
                    SendMessge(MessageType.Warn, string.Format("{0}给煤机平均煤量小于50T，可考虑停磨", this.TaskName));
            }
            if (totalFeeder == 4)
            {
                double avgFeederCoal = AnalogValue(c_realTotalCoal) / totalFeeder;
                if (this.AnalogH(avgFeederCoal, 85, 72, "avgFeederCoal"))
                    SendMessge(MessageType.Warn, string.Format("{0}给煤机平均煤量大于85T，可考虑启磨", this.TaskName));
            }
            if (unitLoad > 300)
            {
                if(this.AnalogL(AnalogValue(c_TMax2)- AnalogValue(c_TMax),8,18,"TMaxDev"))
                    SendMessge(MessageType.Warn, string.Format("{0}TMax与Tmax2偏差仅有8度", this.TaskName));
            }

        }


    }
}

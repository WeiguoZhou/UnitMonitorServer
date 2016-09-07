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
            
        }


    }
}

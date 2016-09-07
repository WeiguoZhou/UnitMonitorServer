using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommon;
namespace Turbine
{
    class VacuumSystem:TaskBase
    {
        #region "点号别名，防止拼写错误"
        const string c_vacuumPmpAState = "vacuumPmpAState"; //真空泵A运行状态
        const string c_vacuumPmpBState = "vacuumPmpBState"; //真空泵B运行状态
        const string c_vacuumPmpCState = "vacuumPmpCState"; //真空泵C运行状态
        const string c_vacuumPmpABearingTemp1 = "vacuumPmpABearingTemp1"; //真空泵A轴承温度1
        const string c_vacuumPmpABearingTemp2 = "vacuumPmpABearingTemp2"; //真空泵A轴承温度2
        const string c_vacuumPmpBBearingTemp1 = "vacuumPmpBBearingTemp1"; //真空泵A轴承温度1
        const string c_vacuumPmpBBearingTemp2 = "vacuumPmpBBearingTemp2"; //真空泵A轴承温度2
        const string c_vacuumPmpCBearingTemp1 = "vacuumPmpCBearingTemp1"; //真空泵A轴承温度1
        const string c_vacuumPmpCBearingTemp2 = "vacuumPmpCBearingTemp2"; //真空泵A轴承温度2
        const string c_vacuumPmpAWaterLevelL = "vacuumPmpAWaterLevelL"; //真空泵A分离器水位低
        const string c_vacuumPmpAWaterLevelH = "vacuumPmpAWaterLevelH"; //真空泵A分离器水位高
        const string c_vacuumPmpBWaterLevelL = "vacuumPmpBWaterLevelL"; //真空泵B分离器水位低
        const string c_vacuumPmpBWaterLevelH = "vacuumPmpBWaterLevelH"; //真空泵B分离器水位高
        const string c_vacuumPmpCWaterLevelL = "vacuumPmpCWaterLevelL"; //真空泵C分离器水位低
        const string c_vacuumPmpCWaterLevelH = "vacuumPmpCWaterLevelH"; //真空泵C分离器水位高
        const string c_vacuumPmpAWaterValve = "vacuumPmpAWaterValve"; //真空泵A补水电磁阀
        const string c_vacuumPmpBWaterValve = "vacuumPmpBWaterValve"; //真空泵B补水电磁阀
        const string c_vacuumPmpCWaterValve = "vacuumPmpCWaterValve"; //真空泵C补水电磁阀
        const string c_vacuumPmpAMotorHeater = "vacuumPmpAMotorHeater"; //真空泵A电机电加热
        const string c_vacuumPmpBMotorHeater = "vacuumPmpBMotorHeater"; //真空泵B电机电加热
        const string c_vacuumPmpCMotorHeater = "vacuumPmpCMotorHeater"; //真空泵C电机电加热
        const string c_vacuumPmpAInjectorHeater = "vacuumPmpAInjectorHeater"; //真空泵A喷射器电加热
        const string c_vacuumPmpBInjectorHeater = "vacuumPmpBInjectorHeater"; //真空泵B喷射器电加热
        const string c_vacuumPmpCInjectorHeater = "vacuumPmpCInjectorHeater"; //真空泵C喷射器电加热
        const string c_vacuumPmpAStandBy = "vacuumPmpAStandBy"; //真空泵A备用状态
        const string c_vacuumPmpBStandBy = "vacuumPmpBStandBy"; //真空泵A备用状态
        const string c_vacuumPmpCStandBy = "vacuumPmpCStandBy"; //真空泵A备用状态
        const string c_cndAVacuum1 = "cndAVacuum1"; //凝汽器A真空1
        const string c_cndAVacuum2 = "cndAVacuum2"; //凝汽器A真空2
        const string c_cndAVacuum3 = "cndAVacuum3"; //凝汽器A真空3
        const string c_cndBVacuum1 = "cndBVacuum1"; //凝汽器B真空1
        const string c_cndBVacuum2 = "cndBVacuum2"; //凝汽器B真空2
        const string c_vacuumBrokenValveA = "vacuumBrokenValveA"; //真空破坏门A
        const string c_vacuumBrokenValveB = "vacuumBrokenValveB"; //真空破坏门B

        const string c_unitLoad = "unitLoad"; //机组负荷
        #endregion

        #region "功能开关"
        const string s_checkPmpBearingTemp = "checkPmpBearingTemp"; //监视真空泵轴承温度
        #endregion
        protected override void Process()
        {

            



        }
    }
}

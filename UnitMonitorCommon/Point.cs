using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnitMonitorCommunication;

namespace UnitMonitorCommon
{
    public class Point
    {
       public static int? NullIntValue = null;
        public static bool? NullBoolValue = null;
        public static double? NullDoubleValue = null;
        //点所属的FunGroup
        private FunGroup funGroup;
        public Dictionary<string, string> ParamSettings { private set; get; }
        /// <summary>
        /// 临时变量保存处
        /// </summary>
        public Dictionary<string, double> TempValues { private set; get; }
        /// <summary>
        /// 点地址
        /// </summary>
        public string Id { set; get; }
        /// <summary>
        /// 此Point在FunGroup中点表的索引号
        /// </summary>
        public int Index { private set; get; }
        /// <summary>
        /// 是否允许输出报警
        /// 如果关闭输出报警，该测点的预定义报警算法直接返回不再计算，使用Point.SendMessage的将不再输出
        /// 但因可能其它测点中会引用此测点，因此数据仍会更新
        /// </summary>
        public bool AllowAlarm { set; get; } = true;
        /// <summary>
        /// 点描述
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 点的值
        /// 虽然加了ValueIndex可以在实时模式中直接查到值，但考虑其它模式需要还是保存一下此值
        /// </summary>
        public double? Value { set; get; }
        //点的旧值，因在很多点的算法中都用到oldValue，所以加了此项来提高性能
        public double? OldValue { set; get; }


        public bool? BoolValue
        {
            get
            {
                return (Value.HasValue)?Convert.ToBoolean(Value): NullBoolValue;
            }
        }
        public bool? OldBoolValue
        {
            get
            {
                return (OldValue.HasValue) ? Convert.ToBoolean(OldValue) : NullBoolValue;
            }
        }
        public int? OldIntValue
        {
            get
            {
                return (OldValue.HasValue) ? Convert.ToInt32(OldValue) : NullIntValue;
            }
        }
        public FunGroup FunGroup
        {
            get
            {
                return funGroup;
            }
        }
        public int? IntValue
        {
            get
            {
                return (Value.HasValue) ? Convert.ToInt32(Value):NullIntValue;
            }
        }
        
        /// <summary>
        /// 临时数据中是否包含指定的键值
        /// </summary>
        /// <param name="key">临时数据的键</param>
        /// <returns></returns>
        public bool ContainsTempValue(string key)
        {
            if (TempValues == null)
                return false;          
            return TempValues.ContainsKey(key);
        }
        public bool TryGetTempValue(string key,out double value)
        {
            value = 0;
            if (ContainsTempValue(key))
            {
                value = TempValues[key];
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 设置临时数据值
        /// </summary>
        /// <param name="key">临时数据的键</param>
        /// <param name="value">临时数据的值</param>
        public void SetTempValue(string key, double value)
        {
            if (TempValues == null)
            {
                TempValues = new Dictionary<string, double>();
            }
            //如果找不到指定的键，get 操作便会引发 KeyNotFoundException，而 set 操作会创建一个具有指定键的新元素
            TempValues[key] = value;
        }
        public double GetTempValue(string key)
        {
            return TempValues[key];
        }
      /// <summary>
      /// 清空所有临时数据
      /// </summary>
        public void ClearTempValues()
        {
            if (TempValues != null)
            {
                TempValues.Clear();
            }

        }
        public void ClearValue()
        {
            Value = NullDoubleValue;
            OldValue = NullDoubleValue;
        }
        /// <summary>
        /// 删除指定的临时数据
        /// </summary>
        /// <param name="key"></param>
        public void RemoveTempValue(string key)
        {
            if (TempValues != null)
            {
                TempValues.Remove(key);
            }

        }
        /// <summary>
        /// 删除相关的临时数据
        /// </summary>
        /// <param name="relatedKey">相关的临时数据键值前缀</param>
        public void RemoveRelatedTempValue(string relatedKey)
        {
            if (TempValues == null)
                return;
            List<string> removedKeys = new List<string>();
            foreach (var item in TempValues.Keys)
            {
                if (item.StartsWith(relatedKey))
                    removedKeys.Add(item);
            }
            foreach (var item in removedKeys)
            {
                TempValues.Remove(item);
            }
        }

        private  Point()
        {
            
        }
        public static  Point  Load(FunGroup fun,XmlNode node)
        {
            Point point = new Point();
            point.funGroup = fun;
            foreach (XmlAttribute item in node.Attributes)
            {
                switch (item.Name)
                {
                    case "id":
                        point.Id = item.Value;
                        break;
                    case "description":
                        point.Description = item.Value;
                        break;
                    case "allowAlarm":
                        point.AllowAlarm = Convert.ToBoolean(item.Value);
                        break;
                    case "index":
                       point.Index = Convert.ToInt32(item.Value);
                        if (fun.Points[point.Index] == null)
                            fun.Points[point.Index] = point;
                        else
                            throw new Exception(string.Format("{0}加载index为{1}的funGroup的点表中的index为{2}的项重复", fun.Task.TaskName, fun.Index, point.Index));
                        break;
                    case "type":
                        break;
                    default:
                        point.SetParamSetting(item.Name, item.Value);
                        break;
                }
            }
            return point;
        }
        public string GetParamSetting( string key)
        {
            if ((ParamSettings == null) || (!ParamSettings.ContainsKey(key)))
                return "";
            return ParamSettings[key];
        }
        public void SetParamSetting(string key,string value)
        {
            if (ParamSettings == null)
                ParamSettings = new Dictionary<string, string>();
            ParamSettings[key] = value;
        }
        public void SendMessage(MessageType messageType, string messge)
        {
            if (this.AllowAlarm)
                this.FunGroup.Task.RaiseSendMessage(messageType, messge);
        }
        //手动输入数据时验证值是否符合规则
        public bool ValidateValue( string value)
        {
            //因为允许空值，所以当是空字符串时转换为空值
            if (string.IsNullOrEmpty(value))
                return true;
            switch (ParamSettings["type"])
            {
                case "double":
                    double dOutValue;
                    return double.TryParse(value,out  dOutValue);
                //TODO:可以考虑在这里加入验证规则，可以是在模块配置文件中预设好的。
                //大概的思路如以下：
                //TaskRules rules=TaskRules.Load(this.FunGroup.Task);
                //string rule=ParamSettings["validateRule"];
                //return rules.Validate(rule,value);
                case "int":
                    int iOutValue;
                    return int.TryParse(value, out iOutValue);
                case "bool":
                    bool bOutValue;
                    return bool.TryParse(value, out bOutValue);
                case "boolean": //上次忘记两个Case并起来怎么写
                    bool bValue;
                    return bool.TryParse(value, out bValue);
                default:
                    return false;
            }
        }
    }
}

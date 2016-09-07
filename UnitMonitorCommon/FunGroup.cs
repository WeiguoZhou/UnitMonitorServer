using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UnitMonitorCommon
{
    public class FunGroup
    {
        private bool used = false;
        public Dictionary<string, string> ParamSettings { private set; get; }
        //是否允许使用，如果不允许使用无法将Used改为true
        public bool AllowUse { set; get; } = true;
        //是否允许不使用，如果为false时无法将Used改为false
        public bool AllowUnUse { set; get; } = true;
        public int Index { private set; get; }
        public bool Used
        {
            set
            {

                if (value != used)
                {
                    if (value && !AllowUse)
                        return;
                    if (!value && !AllowUnUse)
                        return;
                    used = value;
                    this.Task.RaiseFunGroupUsedChanged(this);
                }
            }

            get
            {
                return used;
            }

        }
        public Point[] Points { private set; get; }
        public TaskBase Task { private set; get; }


        public string Description { get; private set; }

        private FunGroup() { }
        public static FunGroup Load(TaskBase container, XmlNode node)
        {
            FunGroup fun = new FunGroup();
            fun.Task = container;
            foreach (XmlAttribute item in node.Attributes)
            {
                switch (item.Name)
                {
                    case "defaultUsed":
                        fun.used = Convert.ToBoolean(item.Value);
                        break;
                    case "allowUse":
                        fun.AllowUse = Convert.ToBoolean(item.Value);
                        break;
                    case "allowUnUse":
                        fun.AllowUnUse = Convert.ToBoolean(item.Value);
                        break;
                    case "index":
                        fun.Index = Convert.ToInt32(item.Value);
                        if (container.FunGroups[fun.Index] == null)
                            container.FunGroups[fun.Index] = fun;
                        else
                            throw new Exception(string.Format("{0}加载FunGroup时发现index为{1}的项有重复", container.TaskName, fun.Index));
                        break;
                    case "description":
                        fun.Description = item.Value;
                        break;
                    default:
                        fun.SetParamSetting(item.Name, item.Value);
                        break;
                }
            }
            XmlNodeList pointsList = node.ChildNodes;
            int count = pointsList.Count;
            if (count > 0)
            {
                fun.Points = new Point[count];
                for (int i = 0; i < count; i++)
                {

                    Point.Load(fun, pointsList[i]);
                }

            }



            return fun;
        }
        public void ClearPointsTempValue()
        {

            foreach (var item in Points)
            {
                item.ClearTempValues();
            }

        }
        /// <summary>
        /// 终止此FunGroup的运行，清除点表中的临时数据和值（防止恢复运行时造成干扰
        /// </summary>
        public void Stop()
        {
            this.Used = false;
            foreach (var item in this.Points)
            {
                item.ClearTempValues();
                item.ClearValue();
            }
        }
        public string GetParamSetting(string key)
        {
            if ((ParamSettings == null) || (!ParamSettings.ContainsKey(key)))
                return "";
            return ParamSettings[key];
        }
        public void SetParamSetting(string key, string value)
        {
            if (ParamSettings == null)
                ParamSettings = new Dictionary<string, string>();
            ParamSettings[key] = value;
        }
    }
}

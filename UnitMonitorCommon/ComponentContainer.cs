using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using CommunicationBase;

namespace UnitMonitorCommon
{
   public  class ComponentContainer
    {
        /// <summary>
        /// 组件容器的运行时实例
        /// </summary>
        public static ComponentContainer Instance { private set; get; }
        /// <summary>
        /// 组件列表
        /// </summary>
        private List<ComponentBase> components;
        /// <summary>
        /// 组件容器的配置
        /// </summary>
        public XmlDocument ConfigDoc { private set; get; }
        /// <summary>
        /// 保存对主窗体的引用
        /// 组件对象打开的窗口要将它作为父窗口
        /// </summary>
        public Form MDIForm { set; get; }
        /// <summary>
        /// 组件菜单项，有的组件向此菜单项中添加菜单
        /// </summary>
        public ToolStripMenuItem CompMenu { set; get; }
        /// <summary>
        /// 组件配置菜单项，所有的组件向此菜单项中添加配置菜单
        /// </summary>
        public ToolStripMenuItem CompConfigMenu { set; get; }

        /// <summary>
        /// 加载组件后的事件通知
        /// </summary>
        public event ComponentEventHandler ComponentLoaded;
        /// <summary>
        /// 组件移除后的通知
        /// </summary>
        public event ComponentEventHandler ComponentRemoved;
        /// <summary>
        /// 组件出错的通知
        /// </summary>
        public event ComponentFailedEventHandler ComponentFailed;
        public static void Init(Form form)
        {
            if (Instance == null)
                Instance = new ComponentContainer();
            Instance.MDIForm = form;
            Instance.ConfigDoc = new XmlDocument();
            Instance.ConfigDoc.Load(Instance.GetConfigFile());
            Instance.LoadComponents();



        }
        private ComponentContainer()
        {
            components = new List<ComponentBase>();
        }
        public string ComponentDirectory
        {
            get
            {
                return Directory.GetCurrentDirectory() + "\\Components";
            }
        }
        private void LoadComponents()
        {
            foreach (XmlNode node in ConfigDoc.GetElementsByTagName("component"))
            {
                //仅加载默认启动的组件
                if (Convert.ToBoolean(node.Attributes["defaultUsed"].Value))
                {
                    ComponentBase component=LoadComponent(node);
                    if (component != null)
                    {
                        components.Add(component);
                        if (ComponentLoaded != null)
                            ComponentLoaded(component);
                    }

                }
            }
        }
        /// <summary>
        /// 从XmlNode配置信息中加载组件
        /// </summary>
        /// <param name="node">XmlNode配置信息</param>
        public ComponentBase LoadComponent(XmlNode node)
        {
            ComponentBase component = null;
            string name= node.Attributes["name"].Value;
            try
            {
                Assembly assem = Assembly.Load(node.Attributes["assembly"].Value);
                 component = (ComponentBase)assem.CreateInstance(node.Attributes["class"].Value);
                component.ComponentName = name;
                component.Container = this;
                component.Description = node.Attributes["description"].Value;
                string configFile= ComponentDirectory + "\\" + node.Attributes["configFile"].Value;
                component.ConfigFile = configFile;
                component.Init();


            }
            catch(Exception ex)
            {
                component = null;
                if (ComponentFailed != null)
                    ComponentFailed(name, ex);
            }
            return component;
        }
        public ComponentBase LoadComponent(string name)
        {
            ComponentBase comp = GetComponentByName(name);
            if (comp != null)
                return comp;
            foreach (XmlNode node in ConfigDoc.GetElementsByTagName("component"))
            {
                if (node.Attributes["name"].Value==name)
                {
                    comp=  LoadComponent(node);
                    if (comp != null)
                    {
                        components.Add(comp);
                        if (ComponentLoaded != null)
                            ComponentLoaded(comp);
                        return comp;
                    }
                }
            }
            return comp;
        }
        /// <summary>
        /// 保存对默认启动项的修改
        /// </summary>
        /// <param name="name">组件名称</param>
        /// <param name="defaultUsed">是否默认启动</param>
        public void SaveDefaultUsed(string name,bool defaultUsed)
        {
            foreach (XmlNode node in ConfigDoc.GetElementsByTagName("component"))
            {
                if (node.Attributes["name"].Value == name)
                {
                    node.Attributes["defaultUsed"].Value = defaultUsed.ToString();
                    ConfigDoc.Save(GetConfigFile());
                    return;
                }
            }
        }
        /// <summary>
        /// 加载所有组件信息
        /// </summary>
        /// <returns></returns>
        public List<ComponentInfo> LoadComponentInfos()
        {
            List<ComponentInfo> list = new List<ComponentInfo>();
            foreach (XmlNode node in ConfigDoc.GetElementsByTagName("component"))
            {
                ComponentInfo info = new ComponentInfo();
                info.Name = node.Attributes["name"].Value;
                info.Description= node.Attributes["description"].Value;
                info.DefaultUsed= Convert.ToBoolean(node.Attributes["defaultUsed"].Value);
                info.Enabled = (GetComponentByName(info.Name) != null);
                info.Assembly= node.Attributes["assembly"].Value;
                info.configFile= node.Attributes["configFile"].Value;
                info.Class = node.Attributes["class"].Value;
                list.Add(info);
            }
            return list;
        }
        /// <summary>
        /// 从组件容器中移除指定的组件
        /// </summary>
        /// <param name="componentName">组件名称</param>
        public void RemoveComponent(string componentName)
        {
            ComponentBase component = GetComponentByName(componentName);
            if (component != null)
                RemoveComponent(component);
        }
        /// <summary>
        /// 从组件容器中移除指定的组件
        /// </summary>
        /// <param name="component">要移除的组件</param>
        public void RemoveComponent(ComponentBase component)
        {
            component.UnLoad();
            components.Remove(component);
            if (ComponentRemoved != null)
                ComponentRemoved(component);
        }

        public ComponentBase GetComponentByName(string name)
        {
            foreach (ComponentBase item in components)
            {
                if (item.ComponentName == name)
                    return item;
            }
            return null;
        }
        private string GetConfigFile()
        {
            string filename = ComponentDirectory + "\\Components.config";
            if (!File.Exists(filename))
                throw new Exception(string.Format("组件配置文件{0}未找到", filename));
            return filename;
        }
        /// <summary>
        /// 引发ComponentFailed事件，由组件出现异常时调用
        /// </summary>
        /// <param name="component"></param>
        /// <param name="ex"></param>
        public void RaiseComponentFailed(ComponentBase component,Exception ex)
        {
            if (this.ComponentFailed != null)
                this.ComponentFailed(component.ComponentName, ex);
        }

        public void SetMenu(ToolStripMenuItem componentMenu)
        {
            CompMenu = componentMenu;
            ToolStripMenuItem componentListMenu = new ToolStripMenuItem();
           // componentListMenu.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            componentListMenu.ImageTransparentColor = System.Drawing.Color.Black;
            componentListMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            //componentListMenu.Size = new System.Drawing.Size(165, 22);
            componentListMenu.Text = "组件清单(&L)";
            componentListMenu.Click += new System.EventHandler(this.ShowComponentList);
            componentMenu.DropDownItems.Add(componentListMenu);

            ToolStripMenuItem componentConfigMenu = new ToolStripMenuItem();
            // componentListMenu.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            componentConfigMenu.ImageTransparentColor = System.Drawing.Color.Black;
            componentConfigMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            // componentListMenu.Size = new System.Drawing.Size(165, 22);
            componentConfigMenu.Text = "组件配置(&C)";
            componentMenu.DropDownItems.Add(componentConfigMenu);
            CompConfigMenu = componentConfigMenu;
            foreach (var item in this.components)
            {
                item.SetMenu(componentMenu);
            }
        }

        private void ShowComponentList(object sender, EventArgs e)
        {
            ComponentsForm form = new ComponentsForm();
            if (MDIForm != null)
                form.MdiParent = MDIForm;
            form.Show();
        }
    }
    public delegate void ComponentEventHandler(ComponentBase component);
    public delegate void ComponentFailedEventHandler(string componentName, Exception ex);

}

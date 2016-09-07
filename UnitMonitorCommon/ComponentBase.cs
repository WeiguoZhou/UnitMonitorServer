using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace UnitMonitorCommon
{
   public  class ComponentBase
    {
        /// <summary>
        /// 组件配置文件名称
        /// </summary>
        public string ConfigFile { set; get; }
        /// <summary>
        /// 组件描述
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 组件名称
        /// </summary>
        public string ComponentName { set; get; }
        /// <summary>
        /// 组件容器
        /// </summary>
        public ComponentContainer Container { set; get; }
        /// <summary>
        /// 组件初始化
        /// </summary>
        public virtual void Init()
        {

        }
        /// <summary>
        /// 组件停用时的消毁操作
        /// </summary>
        public virtual void UnLoad()
        {

        }
        /// <summary>
        /// 当组件出现异常时引发组件容器的事件，以便进一步处理
        /// </summary>
        /// <param name="ex"></param>
        protected void RaiseComponentFailed(Exception ex)
        {
            this.Container.RaiseComponentFailed(this, ex);
        }
        /// <summary>
        /// 组件设置菜单项
        /// </summary>
        /// <param name="compMenu"></param>
        public virtual void SetMenu(ToolStripMenuItem compMenu)
        {
            
        }
    }
}

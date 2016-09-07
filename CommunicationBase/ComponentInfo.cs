using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBase
{
  public  class ComponentInfo
    {
      public  string  Name { set; get; }
        public string Description { set; get; }
        public bool Enabled { set; get; }
        public bool DefaultUsed { set; get; }
        public string Assembly { set; get; }
        public string Class { set; get; }
        public string configFile { set; get; }

    }
}

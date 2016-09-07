using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace UnitMonitorCommunication
{
    [DataContract]
 public   class TaskInfo
    {
        [DataMember]
        public string Name { set;get;}
        [DataMember]
        public string Path { set; get; }
        [DataMember]
        public string ModuleName { set; get; }
        [DataMember]
        public bool IsRunning { set; get; }
        [DataMember]
        public DateTime BeginTime { set; get; }
        [DataMember]
        public int RunCount { set; get; }
        [DataMember]
        public int Period { set; get; }
  
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitMonitorCommunication;

namespace UnitMonitorClient
{
  public  class ClientService:IClientService
    {
        //服务器端向客户端发送消息
      
      public  bool SendMessage(MessageInfo message)
        {

            ClientCommunication.Instance.RaiseRecievedMessage(message);
            return true;
            
        }
        //读取客户端程序的版本
      
      public  Version ApiVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }
        //向客户端注册服务端信息

      public  bool RegService(string ip,string port)
        {
            Servers.Instance.RegServer(ip, Convert.ToInt32(port));
            return true;

        }
        public bool TestOnline()
        {
            return true;
        }

        //通知客户端服务端已关闭

        public bool ServiceShutOff(string ip)
        {
            Servers.Instance.ServerTurnOff(ip);
            return true;
        }
    }
}

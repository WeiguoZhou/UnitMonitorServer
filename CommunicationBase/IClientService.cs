using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace UnitMonitorCommunication
{

    /// <summary>
    /// 放在客户端的供服务器端调用的接口
    /// </summary>
    [ServiceContract]
   public interface IClientService
    {
        //读取客户端程序的版本
        [OperationContract]
        [WebGet(UriTemplate = "ApiVersion", BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        Version ApiVersion();
        //向客户端注册服务端信息
        [OperationContract]
        [WebInvoke(UriTemplate = "RegService/{ip}/{port}")]
        bool RegService(string ip, string port);
        //测试客户端是否在线
        [OperationContract]
        [WebInvoke(UriTemplate = "TestOnline")]
        bool TestOnline();
        //通知客户端服务端已关闭
        [OperationContract]
        [WebInvoke(UriTemplate = "ServiceShutOff/{ip}")]
        bool ServiceShutOff(string ip);
        //服务器端向客户端发送消息
        [OperationContract]
        [WebInvoke(UriTemplate = "SendMessage", BodyStyle = WebMessageBodyStyle.Wrapped)]
       bool SendMessage(MessageInfo message);
       
  

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
namespace UnitMonitorCommunication
{
    [ServiceContract]
    public interface IServerService
    {
        //读取客户端程序的版本
        [OperationContract]
        [WebGet(UriTemplate = "ApiVersion", BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        Version ApiVersion();
        //向客户端注册服务端信息
        [OperationContract]
        [WebInvoke(UriTemplate = "RegService/{ip}/{port}")]
       bool RegService(string ip, string port);
        //通知客户端服务端已关闭
        [OperationContract]
        [WebInvoke(UriTemplate = "ServiceShutOff/{ip}")]
        bool ServiceShutOff(string ip);
        //服务器端服务的名称
        [OperationContract]
        [WebInvoke(UriTemplate = "ServiceName")]
        string ServiceName();

        [OperationContract]
        [WebGet(UriTemplate = "Tasks")]
        TaskInfo[] Tasks();
    }

    
}

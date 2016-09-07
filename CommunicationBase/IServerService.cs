using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.IO;

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
        //测试嗠端是否在线
        [OperationContract]
        [WebInvoke(UriTemplate = "TestOnline")]
        bool TestOnline();
        [OperationContract]
        [WebGet(UriTemplate = "AllTasks")]
        List<TaskInfo> AllTasks();
        [OperationContract]
        [WebGet(UriTemplate = "RunningTasks")]
        List<TaskInfo> RunningTasks();
        [OperationContract]
        [WebGet(UriTemplate = "HistoryMessageFiles")]
        string[] HistoryMessageFiles();
        [OperationContract]
        [WebGet(UriTemplate = "HistoryMessage/{messageFile}")]
        Stream HistoryMessage(string messageFile);
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Lib.ServiceContracts
{
    [ServiceContract(CallbackContract=typeof(ISubscriberCallback))]
    public interface IPublishService
    {
        [OperationContract]
        void Regist(string clientMac);

        [OperationContract]
        void Unregist(string clientMac);

        [OperationContract]
        void MsgReceiveTest(string msg);

        /// <summary>
        /// 广播到所有客户端
        /// </summary>
        /// <param name="msg"></param>
        [OperationContract(Name = "BroadcastAllClient")]
        void Broadcast(string msg);

        /// <summary>
        /// 广播到指定客户端
        /// </summary>
        /// <param name="clientMac"></param>
        /// <param name="msg"></param>
        
        [OperationContract(Name = "BroadcastToClient")]
        void Broadcast(string clientMac ,string msg);


        /// <summary>
        /// 广播到指定客户端
        /// </summary>
        /// <param name="clientMacs"></param>
        /// <param name="msg"></param>
        [OperationContract(Name = "BroadcastToClients")]
        void Broadcast(IEnumerable<string> clientMacs, string msg);


    }
}

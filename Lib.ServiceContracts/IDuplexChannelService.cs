using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Lib.ServiceContracts
{
    [ServiceContract(CallbackContract = typeof(IDuplexChannelCallback))]
    public interface IDuplexChannelService
    {
        /// <summary>
        /// 客户端上线方法
        /// </summary>
        /// <param name="Mac"></param>
        /// <param name="isManager"></param>
        [OperationContract(IsOneWay = true)]
        void Online(string Mac, bool isManager);

        /// <summary>
        /// 客户端主动离线方法
        /// </summary>
        /// <param name="Mac"></param>
        [OperationContract(IsOneWay = true)]
        void Offline(string Mac);

        /// <summary>
        /// 心跳监测
        /// </summary>
        /// <param name="b"></param>
        [OperationContract(IsOneWay = true)]
        void HeartBeat(byte b);


        /// <summary>
        /// 广播到所有在线客户端
        /// </summary>
        /// <param name="msg"></param>
        [OperationContract(Name = "BroadcastAllClient", IsOneWay = true)]
        void Broadcast(string msg);

        /// <summary>
        /// 广播到指定客户端
        /// </summary>
        /// <param name="clientMac"></param>
        /// <param name="msg"></param>

        [OperationContract(Name = "BroadcastToClient", IsOneWay = true)]
        void Broadcast(string clientMac, string msg);


        /// <summary>
        /// 广播到指定客户端
        /// </summary>
        /// <param name="clientMacs"></param>
        /// <param name="msg"></param>
        [OperationContract(Name = "BroadcastToClients", IsOneWay = true)]
        void Broadcast(IEnumerable<string> clientMacs, string msg);

        /// <summary>
        /// 获取订阅者
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void GetClients();
    }
}

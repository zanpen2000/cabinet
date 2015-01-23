using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Lib.ServiceContracts
{

    public interface IDuplexChannelCallback
    {
       
        [OperationContract(IsOneWay=true)]
        void Broadcast(string message);

        [OperationContract(IsOneWay = true)]
        void ReturnHeartBeat(byte b);

        [OperationContract(IsOneWay = true)]
        void ReturnClients(IEnumerable<ISubscriber> clientMacs);

        [OperationContract(IsOneWay = true)]
        void ReturnOnlineResult(string mac, OnlineState state);

        [OperationContract(IsOneWay = true)]
        void ReturnOfflineResult(string mac, OnlineState state);

    }
}

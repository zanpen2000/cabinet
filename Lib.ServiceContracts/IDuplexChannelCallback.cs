using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Lib.ServiceContracts
{
    public interface IDuplexChannelCallback
    {
        [OperationContract]
        void Broadcast(string message);

        [OperationContract]
        void ReturnHeartBeat(byte b);

        [OperationContract]
        void ReturnClients(IEnumerable<string> clientMacs);

        [OperationContract]
        void ReturnOnlineResult(string mac, OnlineState state);

        [OperationContract]
        void ReturnOfflineResult(string mac, OnlineState state);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Lib.ServiceContracts
{
    public interface ISubscriberCallback
    {
        [OperationContract(IsOneWay=true)]
        void Publish(string mac, string message);

        [OperationContract(IsOneWay = true)]
        void ReturnRegis(string mac, string msg);

        [OperationContract(IsOneWay = true)]
        void ReturnUnregis(string mac, string msg);

    }
}

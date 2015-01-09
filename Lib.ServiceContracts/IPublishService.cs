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
    }
}

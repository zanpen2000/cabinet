using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Lib.ServiceContracts
{
    public interface IManagerCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReturnSubscribers(IEnumerable<string> subscriberMacs);
    }
}

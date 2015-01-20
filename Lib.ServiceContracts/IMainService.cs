using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Lib.ServiceContracts
{
    [ServiceContract]
    public interface IMainService
    {
        [OperationContract]
        string Test(string msg);
    }
}

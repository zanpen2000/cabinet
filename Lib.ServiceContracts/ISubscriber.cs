using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lib.ServiceContracts
{
    
    public interface ISubscriber
    {
       
        string ClientMacAddress { get; }
        string ClientIPAddress { get; }
        int ClientPort { get; }

        ISubscriberCallback ClientCallback { get; }

        void Notify(string message);
    }
}

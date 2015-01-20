using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lib.ServiceContracts
{

    public interface ISubscriber
    {
        bool IsManager { get; }

        string Mac { get; }
        string IP { get; }
        int Port { get; }

        IDuplexChannelCallback Callback { get; }

        void Notify(string message);
    }
}

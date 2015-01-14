using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.ServiceContracts
{
    public interface IManager
    {
        string Mac { get; }
        string IP { get; }
        int Port { get; }
        void Notify(string message);

        IManagerCallback ManagerCallback;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.ServiceContracts
{
    public class SubscriberCallbackEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public SubscriberCallbackEventArgs(string message)
        {
            this.Message = message;
        }
    }
}

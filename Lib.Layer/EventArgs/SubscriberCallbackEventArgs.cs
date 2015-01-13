using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class SubscriberCallbackEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public string ClientMac { get; private set; }

        public SubscriberCallbackEventArgs(string clientMac, string message)
        {
            this.Message = message;
            this.ClientMac = clientMac;
        }
    }
}

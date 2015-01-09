using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.ServiceContracts
{
    public class SubscriberCallback : ISubscriberCallback
    {
        public event EventHandler<SubscriberCallbackEventArgs> OnPublish = delegate { };
        public void Publish(string message)
        {
            OnPublish(this, new SubscriberCallbackEventArgs(message));
        }
    }
}

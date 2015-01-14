using Lib.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class SubscriberCallback : ISubscriberCallback
    {
        public event EventHandler<SubscriberCallbackEventArgs> OnPublish = delegate { };
        public event EventHandler<SubscriberCallbackEventArgs> OnReturnRegis = delegate { };
        public event EventHandler<SubscriberCallbackEventArgs> OnReturnUnregis = delegate { };
        public event EventHandler<SubscribersCallbackEventArgs> OnReturnSubscribers = delegate { };

        public void Publish(string mac, string message)
        {
            OnPublish(this, new SubscriberCallbackEventArgs(mac, message));
        }


        public void ReturnRegis(string mac, string msg)
        {
            OnReturnRegis(this, new SubscriberCallbackEventArgs(mac,msg));
        }

        public void ReturnUnregis(string mac, string msg)
        {
            OnReturnUnregis(this, new SubscriberCallbackEventArgs(mac,msg));
        }

        public void ReturnSubscribers(IEnumerable<string> subs)
        {
            OnReturnSubscribers(this, new SubscribersCallbackEventArgs(subs));
        }
    }
}

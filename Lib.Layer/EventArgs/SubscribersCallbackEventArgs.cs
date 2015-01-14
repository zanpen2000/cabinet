using Lib.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class SubscribersCallbackEventArgs : EventArgs
    {
        public IEnumerable<string> Subscribers;

        public SubscribersCallbackEventArgs(IEnumerable<string> subs)
        {
            this.Subscribers = subs;
        }
    }
}

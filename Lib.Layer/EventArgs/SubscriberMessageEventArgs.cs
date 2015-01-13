using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class SubscriberMessageEventArgs : EventArgs
    {
        public ISubscriber Subscriber { get; private set; }

        public SubscriberMessageEventArgs(ISubscriber listener)
        {
            this.Subscriber = listener;
        }
    }
}

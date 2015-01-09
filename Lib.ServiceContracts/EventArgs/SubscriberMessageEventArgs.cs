using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.ServiceContracts
{
    public class SubscriberMessageEventArgs : EventArgs
    {
        public Subscriber Subscriber { get; private set; }

        public SubscriberMessageEventArgs(Subscriber listener)
        {
            this.Subscriber = listener;
        }
    }
}

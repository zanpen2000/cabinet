using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class MessageNotifyErrorEventArgs : EventArgs
    {
        public MessageNotifyErrorEventArgs(ISubscriber sub, Exception ex)
        {
            Subscriber = sub;
            Error = ex;
        }

        public ISubscriber Subscriber { get; private set; }

        public Exception Error { get; private set; }
    }
}

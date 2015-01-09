using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.ServiceContracts
{
    public class MessageNotifyErrorEventArgs : EventArgs
    {
        public MessageNotifyErrorEventArgs(Subscriber sub, Exception ex)
        {
            Subscriber = sub;
            Error = ex;
        }

        public Subscriber Subscriber { get; private set; }

        public Exception Error { get; private set; }
    }
}

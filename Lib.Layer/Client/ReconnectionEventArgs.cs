using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer.Client
{
    public class ReconnectionEventArgs : EventArgs
    {
        public ReconnectionEventArgs(int ctimes, int ttimes)
        {
            this.CurrentTimes = ctimes;
            this.TotalTimes = ttimes;
        }

        public int CurrentTimes { get; private set; }

        public int TotalTimes { get; private set; }
    }
}

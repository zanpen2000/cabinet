using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.ServiceContracts
{
    public class Subscriber
    {
        public string ClientMacAddress { get; private set; }
        public string ClientIPAddress { get; private set; }
        public int ClientPort { get; private set; }
        private ISubscriberCallback __clientCallback;
        public Subscriber(string cMac, string cIP, int cPort, ISubscriberCallback clientCallback)
        {
            this.ClientMacAddress = cMac; this.ClientIPAddress = cIP; this.ClientPort = cPort; this.__clientCallback = clientCallback;
        }

        public void Notify(string message)
        {
            __clientCallback.Publish(message);
        }

        public override bool Equals(object obj)
        {
            bool eq = base.Equals(obj);
            if (!eq)
            {
                Subscriber ls = obj as Subscriber;
                if (ls.__clientCallback.Equals(this.__clientCallback))
                {
                    eq = true;
                }
            }
            return eq;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}({1}:{2})", this.ClientMacAddress, this.ClientIPAddress, this.ClientPort);
        }
    }
}

using Castle.DynamicProxy;
using Lib.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class Subscriber : ISubscriber
    {
        public string ClientMacAddress { get; private set; }
        public string ClientIPAddress { get; private set; }
        public int ClientPort { get; private set; }
        public ISubscriberCallback ClientCallback
        {
            get;
            private set;
        }
        private Subscriber(string cMac, string cIP, int cPort, ISubscriberCallback clientCallback)
            : this()
        {
            this.ClientMacAddress = cMac; this.ClientIPAddress = cIP; this.ClientPort = cPort; this.ClientCallback = clientCallback;
        }

        public Subscriber()
        {

        }

        public static ISubscriber NewSubscriber(string cMac, string cIP, int cPort, ISubscriberCallback clientCallback)
        {
            ISubscriber suber = new Subscriber(cMac, cIP, cPort, clientCallback);
            ProxyGenerator generator = new ProxyGenerator();
            ISubscriber subscriber = (ISubscriber)generator.CreateInterfaceProxyWithTarget(typeof(ISubscriber), suber, new InterceptorProxy());
            suber = subscriber;
            return suber;
        }

        public void Notify(string message)
        {
            ClientCallback.Publish(ClientMacAddress,message);
        }

        public override bool Equals(object obj)
        {
            bool eq = base.Equals(obj);
            if (!eq)
            {
                Subscriber ls = obj as Subscriber;
                if (ls.ClientCallback.Equals(this.ClientCallback))
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

    public interface ISubscriber
    {
        string ClientMacAddress { get; }
        string ClientIPAddress { get; }
        int ClientPort { get; }
        ISubscriberCallback ClientCallback { get; }

        void Notify(string message);
    }
}

using Castle.DynamicProxy;
using Lib.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lib.Layer
{

    public class Subscriber : ISubscriber
    {
        public string Mac { get; private set; }

        public string IP { get; private set; }

        public int Port { get; private set; }

        public bool IsManager { get; private set; }

        public ISubscriberCallback Callback
        {
            get;
            private set;
        }
        private Subscriber(string cMac, string cIP, int cPort, ISubscriberCallback clientCallback, bool isManager = false)
            : this()
        {
            this.Mac = cMac; this.IP = cIP; this.Port = cPort; this.Callback = clientCallback; this.IsManager = isManager;
        }

        public Subscriber()
        {

        }

        public static ISubscriber NewSubscriber(string cMac, string cIP, int cPort, ISubscriberCallback clientCallback, bool isManager = false)
        {
            ISubscriber suber = new Subscriber(cMac, cIP, cPort, clientCallback, isManager);
            ProxyGenerator generator = new ProxyGenerator();
            ISubscriber subscriber = (ISubscriber)generator.CreateInterfaceProxyWithTarget(typeof(ISubscriber), suber, new InterceptorProxy());
            suber = subscriber;
            return suber;
        }

        public void Notify(string message)
        {
            Callback.Publish(Mac, message);
        }

        public override bool Equals(object obj)
        {
            bool eq = base.Equals(obj);
            if (!eq)
            {
                Subscriber ls = obj as Subscriber;
                if (ls.Callback.Equals(this.Callback))
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
            return string.Format("{0}({1}:{2})", this.Mac, this.IP, this.Port);
        }



    }


}

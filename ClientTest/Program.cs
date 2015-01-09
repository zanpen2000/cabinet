using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ClientTest
{
    using Lib.ServiceContracts;
    using Lib.Layer;

    class Program
    {
        static void Main(string[] args)
        {
            SubscriberCallback callback = new SubscriberCallback();
            callback.OnPublish += callback_OnPublish;
            InstanceContext context = new InstanceContext(callback);
            ServiceCaller.Execute<IPublishService>(context, net =>
            {
                net.Regist("Client Demo");
                net.MsgReceiveTest("hahahahahaah");
            });

            Console.WriteLine("here");

            Console.Read();
        }

        static void callback_OnPublish(object sender, SubscriberCallbackEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }


}

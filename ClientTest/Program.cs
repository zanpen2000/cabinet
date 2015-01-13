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
                net.Regist("mac1");
                net.Regist("mac2");
                net.Regist("mac3");
                net.Regist("mac4");
                net.MsgReceiveTest("hahahahahaah");
                Console.Read();
            });

            Console.WriteLine("here");

            Console.Read();
        }

        static void callback_OnPublish(object sender, SubscriberCallbackEventArgs e)
        {
            Console.WriteLine("接收到服务器消息： "+ e.Message);
        }
    }


}

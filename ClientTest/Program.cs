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

            Console.WriteLine("{0}\t程序已启动，连接服务器... ", DateTime.Now);

            SubscriberCallback callback = new SubscriberCallback();
            callback.OnPublish += callback_OnPublish;
            callback.OnReturnRegis += callback_OnReturnRegis;
            callback.OnReturnUnregis += callback_OnReturnUnregis;

            InstanceContext context = new InstanceContext(callback);
            ServiceCaller.Execute<IPublishService>(context, net =>
            {
                for (int i = 0; i < 20; i++)
                {
                    net.Regist("Mac" + i.ToString());
                }
                net.MsgReceiveTest("hahahahahaah");
                Console.Read();
            });

            Console.WriteLine("here");

            Console.Read();
        }

        static void callback_OnReturnUnregis(object sender, SubscriberCallbackEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString() + "\t客户端注销：" + e.ClientMac);
        }

        static void callback_OnReturnRegis(object sender, SubscriberCallbackEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString() + "\t客户端注册：" + e.ClientMac);
        }

        static void callback_OnPublish(object sender, SubscriberCallbackEventArgs e)
        {

            Console.WriteLine(e.ClientMac + " 接收到服务器消息： " + e.Message);
        }
    }


}

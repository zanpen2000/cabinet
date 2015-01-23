using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ClientTest
{
    using Lib.ServiceContracts;
    using Lib.Layer;
    using System.ServiceModel.Description;
    using Lib.Layer.Client;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0}\t程序已启动，连接服务器... ", DateTime.Now);

            var s2 = ProxyFactory.GetContext<IDuplexChannelService>();
            s2.OnConnectionChanged += (x, y) => { Console.WriteLine("s2 online"); };
            s2.OnBroadcast += (x, y) => { Console.WriteLine("接收广播：{0}", y.Content); };
            s2.OnGetOnlineClients += (x, y) =>
            {
                Console.WriteLine("在线客户端:");
                foreach (var item in y.Macs)
                {
                    Console.WriteLine("\t" + item);
                }
            };

            var s1 = ProxyFactory.GetContext<IMainService>();
            s1.OnConnectionChanged += (x, y) => { Console.WriteLine("s1 online"); };
   

            HeartBeat hb = new HeartBeat();

            hb.Regist<IDuplexChannelService>(s2, 5000);
            hb.Regist<IMainService>(s1);

            hb.Start();

            System.Threading.Thread.Sleep(3000);

            //s1.Invoke(s => s.Test("发送广播"));
            //s2.Invoke(s => s.GetClients());


            //s1.Invoke(s => s.Test("发送广播2"));

            //context.inverk(context => context.Test());

            //HeartBeat s = new HeartBeat();
            //s.register(context);


            //s.stop();
            ////ms.Online("", false);


            //////




            ////var dd = ms.Test("wef");







            //HeartBeat.OnSending += HeartBeat_OnSending;
            //HeartBeat.OnSent += HeartBeat_OnSent;
            //HeartBeat.Go(callback);

            //ServiceProxy.Call<IDuplexChannelService, IDuplexChannelCallback>(callback, net =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        net.Online("MMM" + i.ToString(), false);
            //    }

            //});



            Console.Read();
        }

        static void callback_OnOnlineStateChanged(object sender, OnlineStateEventArgs e)
        {
            Console.WriteLine("{0}:{1}", e.Mac, e.State);
        }

        static void HeartBeat_OnSent(object sender, HeartBeatEventArgs e)
        {
            Console.WriteLine("接收到心跳测试数据包：{0}", e.Byte);
        }

        static void HeartBeat_OnSending(object sender, HeartBeatEventArgs e)
        {
            Console.WriteLine("发送 心跳测试数据包：{0}", e.Byte);
        }

        static void ServiceProxy_OnClientReconnection(object sender, ReconnectionEventArgs e)
        {
            Console.WriteLine("重试连接：{0}/{1}", e.CurrentTimes, e.TotalTimes);
        }

        static void ProxyFactory_OnClientOffLine(object sender, EventArgs e)
        {
            Console.WriteLine("无法连接服务器，已超出重试次数，客户端已离线");
        }

        static void callback_OnBroadcast(object sender, BroadcastEventArgs e)
        {
            Console.WriteLine("接收到广播消息： " + e.Content);
        }
    }


}

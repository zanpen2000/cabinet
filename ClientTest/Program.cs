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

            DuplexChannelCallback callback = new DuplexChannelCallback();
            callback.OnBroadcast += callback_OnBroadcast;
            callback.OnHeartBeatCallback += callback_OnHeartBeatCallback;
            callback.OnOnlineStateChanged += callback_OnOnlineStateChanged;

            ServiceProxy.OnClientOffLine += ProxyFactory_OnClientOffLine;

            ServiceProxy.Call<IMainService>(net =>
            {
                string r = net.Test("单通道测试数据");
                Console.WriteLine(r);
            });


            ServiceProxy.Call<IDuplexChannelService, IDuplexChannelCallback>(callback, net =>
            {
                for (int i = 0; i < 20; i++)
                {
                    net.Online("Client" + i.ToString(), false);
                }
            });

            Console.Read();

            //InstanceContext context = new InstanceContext(callback);
            //ServiceCaller.Execute<IPublishService>(context, net =>
            //{
            //    for (int i = 0; i < 20; i++)
            //    {
            //        net.Regist("Mac" + i.ToString(), false);
            //    }
            //    net.MsgReceiveTest("hahahahahaah");
            //    Console.Read();

            //    for (int i = 0; i < 10; i++)
            //    {
            //        net.Unregist("Mac" + i.ToString());
            //    }

            //    Console.Read();
            //});


            //var serviceHost = new ServiceHost(typeof(Lib.ServiceImpl.ServiceImpl));


            //ServiceEndpoint main_endpoint = new ServiceEndpoint(
            //    ContractDescription.GetContract(typeof(Lib.ServiceContracts.IMainService),
            //    typeof(Lib.ServiceImpl.ServiceImpl)),
            //    new NetTcpBinding(),
            //    new EndpointAddress("net.tcp://localhost:5900/main"));

            //ServiceEndpoint duplex_endpoint = new ServiceEndpoint(
            //    ContractDescription.GetContract(typeof(Lib.ServiceContracts.IDuplexChannelService),
            //    typeof(Lib.ServiceImpl.ServiceImpl)),
            //    new NetTcpBinding(),
            //    new EndpointAddress("net.tcp://localhost:5900/duplex"));

            //serviceHost.AddServiceEndpoint(main_endpoint);
            //serviceHost.AddServiceEndpoint(duplex_endpoint);

            //SubscriberContainer.Instance.SubscriberAdded += Instance_SubscriberAdded;
            //SubscriberContainer.Instance.SubscriberRemoved += Instance_SubscriberRemoved;
            //SubscriberContainer.Instance.NotifyError += Instance_NotifyError;
            //serviceHost.Opened += serviceHost_Opened;
            //serviceHost.Open();


            Console.WriteLine("{0}\there", DateTime.Now);

            Console.Read();
        }

        static void ProxyFactory_OnClientOffLine(object sender, EventArgs e)
        {
            Console.WriteLine("无法连接服务器，已超出重试次数，客户端已离线");
        }

        static void callback_OnOnlineStateChanged(object sender, OnlineStateEventArgs e)
        {
            Console.WriteLine("客户端状态：{0}:{1}", e.Mac, e.State.ToString());
        }

        static void callback_OnHeartBeatCallback(object sender, HeartBeatEventArgs e)
        {
            Console.WriteLine("心跳返回数据： " + e.Byte.ToString());
        }

        static void callback_OnBroadcast(object sender, BroadcastEventArgs e)
        {
            Console.WriteLine("接收到广播消息： " + e.Content);
        }
    }


}

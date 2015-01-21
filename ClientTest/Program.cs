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
            callback.OnOnlineStateChanged += callback_OnOnlineStateChanged;

            ServiceProxy.OnClientOffLine += ProxyFactory_OnClientOffLine;
            ServiceProxy.OnClientReconnection += ServiceProxy_OnClientReconnection;
            ServiceProxy.Call<IMainService>(net =>
            {
                string r = net.Test("单通道测试数据");
                Console.WriteLine("单通道测试 服务器返回：{0}", r);
            });


            ServiceProxy.Call<IDuplexChannelService, IDuplexChannelCallback>(callback, net =>
            {
                for (int i = 0; i < 20; i++)
                {
                    net.Online("Client" + i.ToString(), false);
                }
            });

            HeartBeat.Go(callback);

            Console.Read();
        }

        static void ServiceProxy_OnClientReconnection(object sender, ReconnectionEventArgs e)
        {
            Console.WriteLine("重试连接：{0}/{1}", e.CurrentTimes, e.TotalTimes);
        }

        static void ProxyFactory_OnClientOffLine(object sender, EventArgs e)
        {
            Console.WriteLine("无法连接服务器，已超出重试次数，客户端已离线");
        }

        static void callback_OnOnlineStateChanged(object sender, OnlineStateEventArgs e)
        {
            Console.WriteLine("客户端状态：{0}:{1}", e.Mac, e.State.ToString());
        }



        static void callback_OnBroadcast(object sender, BroadcastEventArgs e)
        {
            Console.WriteLine("接收到广播消息： " + e.Content);
        }
    }

    public class HeartBeat
    {
        public static void Go(DuplexChannelCallback callback)
        {
            callback.OnHeartBeatCallback += callback_OnHeartBeatCallback; //心跳返回数据事件
            HeartBeating(callback);
        }

        private static void HeartBeating(DuplexChannelCallback callback)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (s, e) =>
            {
                timer.Stop();
                ServiceProxy.Call<IDuplexChannelService, IDuplexChannelCallback>(callback, net =>
                {
                    do
                    {
                        Random r = new Random();
                        byte b = (byte)r.Next(9);
                        Console.WriteLine("{0}\t发送心跳包：{1}", e.SignalTime.ToString(), b);
                        net.HeartBeat(b);
                        System.Threading.Thread.Sleep(3000);
                    } while (true);
                });
                timer.Start();
            };

            timer.Interval = 3000;
            timer.Start();
        }

        private static void callback_OnHeartBeatCallback(object sender, HeartBeatEventArgs e)
        {
            Console.WriteLine("{0}\t心跳返回数据： {1}", DateTime.Now.ToString(), e.Byte.ToString());
        }
    }
}

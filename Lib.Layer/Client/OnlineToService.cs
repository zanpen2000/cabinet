using Lib.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer.Client
{
    public class OnlineToService
    {
        //超出连接重试次数的事件处理
        public static event EventHandler OnClientOffLine = delegate { };
        //正在重试第N次
        public static event EventHandler<ReconnectionEventArgs> OnClientReconnection = delegate { };
        /// <summary>
        /// 总的重试次数
        /// </summary>
        static int RetryTimes = int.Parse(Librarys.AppSettings.Get("RetryTimes"));

        /// <summary>
        /// 当前重试次数
        /// </summary>
        static int _currentTime = 0;

        /// <summary>
        /// 客户端Mac地址或者标记
        /// </summary>
        static string Mac = Librarys.AppSettings.Get("Mac");

        public static void ReOnline<ISvc>(IChannelContext<ISvc> context)
        {
            if (_currentTime < RetryTimes)
            {
                _currentTime++;
                var binding = ProxyFactory.DefaultBinding();
                OnClientReconnection(null, new ReconnectionEventArgs(_currentTime, RetryTimes));

                try
                {
                    ProxyFactory.GetProxy<IDuplexChannelService, IDuplexChannelCallback>(
                        (IDuplexChannelCallback)context, binding).Online(Mac, false);
                    _currentTime = 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    System.Threading.Thread.Sleep(5000);
                    ReOnline<ISvc>(context);
                }
            }
            else
            {
                //超出重试次数，客户端或者服务器离线
                OnClientOffLine(null, null);
            }
        }

        public static void Offline<ISvc>(IChannelContext<ISvc> context)
        {
            ProxyFactory.GetProxy<IDuplexChannelService, IDuplexChannelCallback>(
                      (IDuplexChannelCallback)context, ProxyFactory.DefaultBinding()).Offline(Mac);
            OnClientOffLine(null, null);
        }


        public static void ReAction<ISvc, ICallback>(ICallback callback, Action<ISvc> ac)
        {
            if (_currentTime < RetryTimes)
            {
                _currentTime++;

                OnClientReconnection(null, new ReconnectionEventArgs(_currentTime, RetryTimes));
                ServiceProxy.Call<ISvc, ICallback>(callback, ac);
            }
            else
            {
                //超出重试次数，客户端或者服务器离线
                OnClientOffLine(null, null);
            }
        }

        public static void ReAction<ISvc>(Action<ISvc> ac)
        {
            if (_currentTime < RetryTimes)
            {
                _currentTime++;
                OnClientReconnection(null, new ReconnectionEventArgs(_currentTime, RetryTimes));
                ServiceProxy.Call<ISvc>(ac);
            }
            else
            {
                //超出重试次数，客户端或者服务器离线
                OnClientOffLine(null, null);
            }

        }
    }
}

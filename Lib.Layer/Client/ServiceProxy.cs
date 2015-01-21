using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer.Client
{
    public class ServiceProxy
    {
        //超出连接重试次数的事件处理
        public static event EventHandler OnClientOffLine = delegate { };
        //正在重试第N次
        public static event EventHandler<ReconnectionEventArgs> OnClientReconnection = delegate { };
        static int RetryTimes = int.Parse(Librarys.AppSettings.Get("RetryTimes"));
        static int _currentTime = 0;

        public static void Call<ISvc, ICallback>(ICallback callback, Action<ISvc> ac)
        {
            ISvc svc = ProxyFactory.GetProxy<ISvc, ICallback>(callback);

            try
            {
                ac(svc);
            }
            catch (Exception)
            {

                if (_currentTime < RetryTimes)
                {
                    _currentTime++;

                    OnClientReconnection(null, new ReconnectionEventArgs(_currentTime, RetryTimes));
                    Call<ISvc, ICallback>(callback, ac);
                }
                else
                {
                    //超出重试次数，客户端或者服务器离线
                    OnClientOffLine(null, null);
                }
            }
        }
        public static void Call<ISvc>(Action<ISvc> ac)
        {
            ISvc svc = ProxyFactory.GetProxy<ISvc>();

            try
            {
                ac(svc);
            }
            catch (Exception)
            {
                if (_currentTime < RetryTimes)
                {
                    _currentTime++;
                    OnClientReconnection(null, new ReconnectionEventArgs(_currentTime, RetryTimes));
                    Call<ISvc>(ac);
                }
                else
                {
                    //超出重试次数，客户端或者服务器离线
                    OnClientOffLine(null, null);
                }
            }
        }
    }
}

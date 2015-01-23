using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer.Client
{
    /// <summary>
    /// WCf服务用户调用入口
    /// </summary>
    public class ServiceProxy
    {
        /// <summary>
        /// 带有回调的服务调用
        /// </summary>
        /// <typeparam name="ISvc"></typeparam>
        /// <typeparam name="ICallback"></typeparam>
        /// <param name="callback"></param>
        /// <param name="ac"></param>
        public static void Call<ISvc, ICallback>(ICallback callback, Action<ISvc> ac)
        {
            ISvc svc = ProxyFactory.GetProxy<ISvc, ICallback>(callback);

            try
            {
                ac(svc);
            }
            catch (Exception)
            {
                
                OnlineToService.ReAction<ISvc, ICallback>(callback, ac);
            }
        }

        /// <summary>
        /// 无回调的服务调用（立即返回结果）
        /// </summary>
        /// <typeparam name="ISvc"></typeparam>
        /// <param name="ac"></param>
        public static void Call<ISvc>(Action<ISvc> ac)
        {
            ISvc svc = ProxyFactory.GetProxy<ISvc>();

            try
            {
                ac(svc);
            }
            catch (Exception)
            {
                OnlineToService.ReAction<ISvc>(ac);
            }
        }
    }
}

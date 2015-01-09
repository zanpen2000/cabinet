using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Lib.Layer
{
    public class ServiceCaller
    {
        private static string __getAddress<ISvc>()
        {
            string baseAddr = Librarys.AppSettings.Get("ServiceBaseAddress");

            if (string.IsNullOrEmpty(baseAddr))
            {
                throw new ArgumentNullException("未能获取Web Service地址");
            }

            string svrName = typeof(ISvc).ToString().Split('.')[2].Substring(1);

            if (string.IsNullOrEmpty(svrName))
            {
                throw new ArgumentNullException("未能获取服务地址");
            }

            return "net.tcp://" + baseAddr + "/" + svrName ;
        }

        public static void Execute<ISvc>(InstanceContext context, Action<ISvc> ac)
        {
            string address = __getAddress<ISvc>();

            NetTcpBinding binding = new NetTcpBinding();

            using (var factory = new DuplexChannelFactory<ISvc>(context, binding))
            {
                ISvc proxy = factory.CreateChannel(new EndpointAddress(address));
                try
                {
                    ac(proxy);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}

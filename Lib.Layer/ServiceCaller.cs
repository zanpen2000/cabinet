using Lib.ServiceContracts;
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

            return "net.tcp://" + baseAddr + "/" + svrName;
        }

        public static void Execute<ISvc>(InstanceContext context, Action<ISvc> ac)
        {
            string address = __getAddress<ISvc>();

            NetTcpBinding binding = __getBinding();

            using (var factory = new DuplexChannelFactory<ISvc>(context, binding))
            {
                ISvc proxy = factory.CreateChannel(new EndpointAddress(address));
                try
                {
                    ac(proxy);
                }
                catch (EndpointNotFoundException)
                {
                    (proxy as ICommunicationObject).Abort();
                }
                catch (CommunicationException)
                {
                    (proxy as ICommunicationObject).Abort();
                }
                catch (TimeoutException)
                {
                    (proxy as ICommunicationObject).Abort();
                }
                catch (Exception)
                {
                    (proxy as ICommunicationObject).Close();
                }
                finally
                {
                    (proxy as ICommunicationObject).Close();
                }
            }
        }

        public static void Execute<ISvc>(EventHandler<SubscriberCallbackEventArgs> cb , Action<ISvc> ac)
        {
            string address = __getAddress<ISvc>();

            SubscriberCallback sc = new SubscriberCallback();
            sc.OnPublish += cb;
            InstanceContext context = new InstanceContext(sc);
            NetTcpBinding binding = __getBinding();

            using (var factory = new DuplexChannelFactory<ISvc>(context, binding))
            {
                ISvc proxy = factory.CreateChannel(new EndpointAddress(address));
                try
                {
                    ac(proxy);
                }
                catch (EndpointNotFoundException)
                {
                    (proxy as ICommunicationObject).Abort();
                }
                catch (CommunicationException)
                {
                    (proxy as ICommunicationObject).Abort();
                }
                catch (TimeoutException)
                {
                    (proxy as ICommunicationObject).Abort();
                }
                catch (Exception)
                {
                    (proxy as ICommunicationObject).Close();
                }
                finally
                {
                    sc.OnPublish -= cb;
                    (proxy as ICommunicationObject).Close();
                }
            }
        }

        private static NetTcpBinding __getBinding()
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.ReliableSession.Enabled = true;
            binding.ReliableSession.Ordered = true;
            binding.ReliableSession.InactivityTimeout = new TimeSpan(0, 10, 0);
            
            //系统默认MaxRetryCount=8，超过8系统报错，若要自定义该值，需要使用CustomBinding
            //binding.ReliableSession.MaxRetryCount = 10;

            binding.TransactionFlow = false;
            binding.TransferMode = TransferMode.Buffered;
            binding.TransactionProtocol = TransactionProtocol.OleTransactions;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.ListenBacklog = 400;

            binding.MaxBufferPoolSize = 2147483647;
            binding.MaxReceivedMessageSize = 2147483647;
            binding.MaxBufferSize = 2147483647;
            binding.MaxConnections = 400;

            binding.CloseTimeout = new TimeSpan(0, 1, 0);
            binding.SendTimeout = new TimeSpan(0, 30, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 30, 0);
            binding.OpenTimeout = new TimeSpan(0, 1, 0);

            binding.ReaderQuotas.MaxDepth = 32;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            binding.ReaderQuotas.MaxArrayLength = 2147483647;
            binding.ReaderQuotas.MaxBytesPerRead = 2048;
            binding.ReaderQuotas.MaxNameTableCharCount = 16384;

            return binding;
        }

    }
}

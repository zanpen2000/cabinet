using Lib.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.IO;
using Lib.Layer;

namespace HostService
{
    public class PublishWindowService : ServiceBase
    {
        public ServiceHost serviceHost = null;

        public PublishWindowService()
        {
            ServiceName = "TSDYKJ_PublishService";
        
        }

        public static void Main()
        {
            ServiceBase.Run(new PublishWindowService());
        }

        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = new ServiceHost(typeof(Lib.ServiceImpl.PublishService));

            SubscriberContainer.Instance.SubscriberAdded += Instance_SubscriberAdded;
            SubscriberContainer.Instance.SubscriberRemoved += Instance_SubscriberRemoved;
            SubscriberContainer.Instance.NotifyError += Instance_NotifyError;
            serviceHost.Opened += serviceHost_Opened;
            serviceHost.Closed += serviceHost_Closed;
            serviceHost.Opening += serviceHost_Opening;
            serviceHost.Closing += serviceHost_Closing;
            serviceHost.Open();
        }

        void serviceHost_Closing(object sender, EventArgs e)
        {
            Logger.AppendInfo(ServiceName + " 正在停止...");
        }

        void serviceHost_Opening(object sender, EventArgs e)
        {
            Logger.AppendInfo(ServiceName + " 正在启动...");
        }

        void serviceHost_Closed(object sender, EventArgs e)
        {
            Logger.AppendInfo(ServiceName + " 已停止");
        }

        void serviceHost_Opened(object sender, EventArgs e)
        {
            Logger.AppendInfo(ServiceName + " 已启动");
        }

        private void Instance_NotifyError(object sender, MessageNotifyErrorEventArgs e)
        {
            Logger.AppendInfo(e.Subscriber.ToString(), e.Error);
        }

        private void Instance_SubscriberRemoved(object sender, SubscriberMessageEventArgs e)
        {
            Logger.AppendUserMessage(string.Format("客户端离线：{0}", e.Subscriber.ToString()));
            //Logger.AppendInfo(string.Format("客户端离线：{0}", e.Subscriber.ToString()));
        }

        private void Instance_SubscriberAdded(object sender, SubscriberMessageEventArgs e)
        {
            Logger.AppendUserMessage(string.Format("客户端上线：{0}", e.Subscriber.ToString()));
            //Logger.AppendInfo(string.Format("客户端上线：{0}", e.Subscriber.ToString()));
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}

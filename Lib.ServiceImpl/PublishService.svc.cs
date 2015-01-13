using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Lib.ServiceImpl
{
    using ServiceContracts;
    using System.ServiceModel.Channels;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class PublishService : IPublishService
    {
        public void Regist(string clientMac)
        {
            RemoteEndpointMessageProperty remote =
                OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            ISubscriberCallback callback = OperationContext.Current.GetCallbackChannel<ISubscriberCallback>();
            OperationContext.Current.Channel.Closing += (x, y) =>
            {
                SubscriberContainer.Instance.RemoveSubscriber(new Subscriber(clientMac, remote.Address, remote.Port, callback));
            };
            SubscriberContainer.Instance.AddSubscriber(new Subscriber(clientMac, remote.Address, remote.Port, callback));
        }

        public void Unregist(string clientMac)
        {
            RemoteEndpointMessageProperty remote =
               OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            ISubscriberCallback callback = OperationContext.Current.GetCallbackChannel<ISubscriberCallback>();
            SubscriberContainer.Instance.RemoveSubscriber(new Subscriber(clientMac, remote.Address, remote.Port, callback));
        }

        public void MsgReceiveTest(string msg)
        {
            Lib.Layer.Logger.AppendUserMessage("客户端消息: " + msg);
        }
    }
}

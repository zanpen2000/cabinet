using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Lib.ServiceImpl
{
    using Lib.Layer;
    using ServiceContracts;
    using System.ServiceModel.Channels;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class PublishService : IPublishService
    {
        [Log(LogType.ApplicationInfo)]
        public void Regist(string clientMac)
        {
            RemoteEndpointMessageProperty remote =
                OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            ISubscriberCallback callback = OperationContext.Current.GetCallbackChannel<ISubscriberCallback>();
            OperationContext.Current.Channel.Closing += (x, y) =>
            {
                SubscriberContainer.Instance.RemoveSubscriber(Subscriber.NewSubscriber(clientMac, remote.Address, remote.Port, callback));
            };
            SubscriberContainer.Instance.AddSubscriber(Subscriber.NewSubscriber(clientMac, remote.Address, remote.Port, callback));
            callback.ReturnRegis(clientMac, string.Format("客户端{0}注册成功", clientMac));
        }

        [Log(LogType.ApplicationInfo)]
        public void Unregist(string clientMac)
        {
            RemoteEndpointMessageProperty remote =
               OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            ISubscriberCallback callback = OperationContext.Current.GetCallbackChannel<ISubscriberCallback>();
            SubscriberContainer.Instance.RemoveSubscriber(Subscriber.NewSubscriber(clientMac, remote.Address, remote.Port, callback));
            callback.ReturnUnregis(clientMac, string.Format("客户端{0}取消注册", clientMac));
        }

        [Log(LogType.All)]
        public void MsgReceiveTest(string msg)
        {
            
        }

        [Log(LogType.UserMessage)]
        public void Broadcast(string msg)
        {
            SubscriberContainer.Instance.NotifyMessage(msg);
        }

        [Log(LogType.UserMessage)]
        public void Broadcast(string clientMac, string msg)
        {
            SubscriberContainer.Instance.NotifyMessage(clientMac, msg);
        }

        [Log(LogType.UserMessage)]
        public void Broadcast(IEnumerable<string> clientMacs, string msg)
        {
            SubscriberContainer.Instance.NotifyMessage(clientMacs, msg);

        }
    }
}

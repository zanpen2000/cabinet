using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.ServiceImpl
{
    using Lib.Layer;
    using ServiceContracts;
    using System.ServiceModel;
    using System.ServiceModel.Channels;


    public partial class ServiceImpl : IDuplexChannelService
    {
        public void Online(string Mac, bool isManager)
        {
            RemoteEndpointMessageProperty remote =
               OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            IDuplexChannelCallback callback = OperationContext.Current.GetCallbackChannel<IDuplexChannelCallback>();

            var sub = Subscriber.NewSubscriber(Mac, remote.Address, remote.Port, callback, isManager);

            OperationContext.Current.Channel.Closing += (x, y) =>
            {
                SubscriberContainer.Instance.RemoveSubscriber(sub);
            };
            SubscriberContainer.Instance.AddSubscriber(sub);

            callback.ReturnOnlineResult(Mac, SubscriberContainer.Instance.Exists(sub) ? OnlineState.Online : OnlineState.Offline);
        }

        public void Offline(string Mac)
        {
            RemoteEndpointMessageProperty remote =
              OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            IDuplexChannelCallback callback = OperationContext.Current.GetCallbackChannel<IDuplexChannelCallback>();
            SubscriberContainer.Instance.RemoveSubscriber(Subscriber.NewSubscriber(Mac, remote.Address, remote.Port, callback));
            callback.ReturnOfflineResult(Mac, SubscriberContainer.Instance.Subscribers.Count(x => x.Mac == Mac) > 0 ? OnlineState.Online : OnlineState.Offline);
        }

        public void HeartBeat(byte b)
        {
            IDuplexChannelCallback callback = OperationContext.Current.GetCallbackChannel<IDuplexChannelCallback>();
            callback.ReturnHeartBeat(b);
        }

        public void Broadcast(string msg)
        {
            SubscriberContainer.Instance.NotifyMessage(msg);
        }

        public void Broadcast(string clientMac, string msg)
        {
            SubscriberContainer.Instance.NotifyMessage(clientMac, msg);
        }

        public void Broadcast(IEnumerable<string> clientMacs, string msg)
        {
            SubscriberContainer.Instance.NotifyMessage(clientMacs, msg);
        }

        public void GetClients()
        {
            IDuplexChannelCallback callback = OperationContext.Current.GetCallbackChannel<IDuplexChannelCallback>();
            callback.ReturnClients((from n in SubscriberContainer.Instance.Subscribers select n.Mac).ToList());

        }
    }
}

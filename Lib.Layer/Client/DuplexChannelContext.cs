using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer.Client
{
    using System.ServiceModel;
    using Lib.ServiceContracts;
    using System.ServiceModel.Channels;
    using System.Collections;
    using System.ComponentModel;


    internal class DuplexChannelContext<TChannel>: IChannelContext<TChannel>,IDuplexChannelCallback
    {

        public DuplexChannelContext() { }


        public DuplexChannelContext(TChannel channel)
        {
            MainChannelType = channel;
        }

        public event EventHandler<BroadcastEventArgs> OnBroadcast = delegate { };
        public event EventHandler<OnlineStateEventArgs> OnConnectionChanged = delegate { };
        public event EventHandler<HeartBeatEventArgs> OnHeartBeat = delegate { };
        public event EventHandler<ClientsReturnEventArgs> OnGetOnlineClients = delegate { };

        public void Invoke(Action<TChannel> ac)
        {
            ac(MainChannelType);
        }

        public void SetChannel(TChannel channel)
        {
            MainChannelType = channel;
        }


        public TChannel MainChannelType
        {
            private set;
            get; 
        }

        public void Broadcast(string message)
        {
            OnBroadcast(this, new BroadcastEventArgs(message));
        }

        public void ReturnHeartBeat(byte b)
        {
            OnHeartBeat(this, new HeartBeatEventArgs(b));
        }

        public void ReturnClients(IEnumerable<ISubscriber> clientMacs)
        {
            OnGetOnlineClients(this, new ClientsReturnEventArgs(clientMacs));
        }

        public void ReturnOnlineResult(string mac, OnlineState state)
        {
            OnConnectionChanged(this, new OnlineStateEventArgs(mac, state));
        }

        public void ReturnOfflineResult(string mac, OnlineState state)
        {
            OnConnectionChanged(this, new OnlineStateEventArgs(mac, state));
        }
    }
}

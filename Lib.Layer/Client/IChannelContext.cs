using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer.Client
{
    public interface IChannelContext<T>
    {
        T MainChannelType { get; }
        event EventHandler<BroadcastEventArgs> OnBroadcast;
        event EventHandler<OnlineStateEventArgs> OnConnectionChanged;
        event EventHandler<HeartBeatEventArgs> OnHeartBeat;
        event EventHandler<ClientsReturnEventArgs> OnGetOnlineClients;

        void Invoke(Action<T> ac);
    }
}

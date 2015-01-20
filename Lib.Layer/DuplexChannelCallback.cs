using Lib.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class DuplexChannelCallback : IDuplexChannelCallback
    {
        public event EventHandler<HeartBeatEventArgs> OnHeartBeatCallback = delegate { };
        public event EventHandler<ClientsReturnEventArgs> OnClientsReturn = delegate { };
        public event EventHandler<OnlineStateEventArgs> OnOnlineStateChanged = delegate { };
        public event EventHandler<BroadcastEventArgs> OnBroadcast = delegate { };

        public void ReturnHeartBeat(byte b)
        {
            OnHeartBeatCallback(this, new HeartBeatEventArgs(b));
        }

        public void ReturnClients(IEnumerable<string> clientMacs)
        {
            OnClientsReturn(this, new ClientsReturnEventArgs(clientMacs));
        }

        public void ReturnOnlineResult(string mac, OnlineState state)
        {
            OnOnlineStateChanged(this, new OnlineStateEventArgs(mac, state));
        }

        public void ReturnOfflineResult(string mac, OnlineState state)
        {
            OnOnlineStateChanged(this, new OnlineStateEventArgs(mac, state));
        }

        public void Broadcast(string message)
        {
            OnBroadcast(this, new BroadcastEventArgs(message));
        }



    }

    #region EventArgs
    public class BroadcastEventArgs : EventArgs
    {
        public string Content { get; private set; }

        public BroadcastEventArgs(string content)
        {
            this.Content = content;
        }
    }

    public class HeartBeatEventArgs : EventArgs
    {
        public Byte Byte { get; private set; }

        public HeartBeatEventArgs(byte b)
        {
            this.Byte = b;
        }
    }

    public class ClientsReturnEventArgs : EventArgs
    {
        public IEnumerable<string> Macs { get; private set; }

        public ClientsReturnEventArgs(IEnumerable<string> clientMacs)
        {
            this.Macs = clientMacs;
        }
    }

    public class OnlineStateEventArgs : EventArgs
    {
        public OnlineState State { get; private set; }
        public string Mac { get; private set; }

        public OnlineStateEventArgs(string mac, OnlineState state)
        {
            this.Mac = mac;
            this.State = state;
        }
    }
    #endregion


}

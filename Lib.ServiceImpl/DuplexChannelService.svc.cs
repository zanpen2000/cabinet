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
    using Model;


    public partial class ServiceImpl : IDuplexChannelService
    {
        public void Online(string Mac, bool isManager)
        {
            DuplexChannelService.Instance.Online(Mac, isManager);
        }

        public void Offline(string Mac)
        {
            DuplexChannelService.Instance.Offline(Mac);
        }

        public void HeartBeat(byte b)
        {
            DuplexChannelService.Instance.HeartBeat(b);
        }

        public void Broadcast(string msg)
        {
            DuplexChannelService.Instance.Broadcast(msg);
        }

        public void Broadcast(string clientMac, string msg)
        {
            DuplexChannelService.Instance.Broadcast(clientMac, msg);
        }

        public void Broadcast(IEnumerable<string> clientMacs, string msg)
        {
            DuplexChannelService.Instance.Broadcast(clientMacs, msg);
        }

        public void GetClients()
        {
            DuplexChannelService.Instance.GetClients();
        }
    }
}

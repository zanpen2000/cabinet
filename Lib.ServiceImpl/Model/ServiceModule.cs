using Lib.Layer;
using Lib.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Lib.ServiceImpl.Model
{
    public class ServiceModule<T> : IServiceModule<T>
    {
        public ServiceModule(T t)
        {
            this.ChannelType = t;
        }

        public T ChannelType
        {
            get;
            private set;
        }

        public void Invoke(Action<T> ac)
        {
            ac(ChannelType);
        }
    }
}

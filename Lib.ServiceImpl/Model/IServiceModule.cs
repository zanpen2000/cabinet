using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.ServiceImpl.Model
{
    public interface IServiceModule<T>
    {
        T ChannelType { get; }

        void Invoke(Action<T> ac);
    }
}

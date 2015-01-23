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

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class ServiceImpl 

    {
    }
}

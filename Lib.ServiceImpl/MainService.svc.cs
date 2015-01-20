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

    public partial class ServiceImpl : IMainService
    {

        public string Test(string msg)
        {
            return msg;
        }
    }
}

using Lib.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace ArchivesServices.MEF
{
    /// <summary>
    /// MEF在WCF中的扩展
    /// </summary>
    public class MEFBehaviorAttribute : Attribute, IContractBehavior, IContractBehaviorAttribute
    {
        static object lockobject = new object();
        public Type TargetContract
        {
            get { return null; }
        }

        public void AddBindingParameters(ContractDescription description, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection parameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription description, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription description, ServiceEndpoint endpoint, DispatchRuntime dispatch)
        {
            dispatch.InstanceProvider = new MEFInstanceProvider();
            lock (lockobject)
            {
                if (IOC.Container == null)
                    IOC.Container = MEFInstanceProvider._container;
            }
        }

        public void Validate(ContractDescription description, ServiceEndpoint endpoint)
        {
        }
    }
}
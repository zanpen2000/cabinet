using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;

namespace BehaviorLibrarys
{
    public class IncomingMessageLogger : IDispatchMessageInspector, IEndpointBehavior
    {
        private ILog logger = null;
        private const string DefaultMessageLogFolder = @"c:\temp\";
        private static int messageLogFileIndex = 0;
        private string messageLogFolder;

        public IncomingMessageLogger() : this(DefaultMessageLogFolder) { }

        public IncomingMessageLogger(string messageLogFolder)
        {
            this.messageLogFolder = messageLogFolder;
            logger = LogManager.GetLogger("Global");
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            //channel.RemoteAddress.Uri.AbsolutePath
            //string messageFileName = Path.Combine(this.messageLogFolder, string.Format("Log{0:000}_Incoming.txt", Interlocked.Increment(ref messageLogFileIndex)));
            // rest of the method ommitted...
            logger.Info(channel.RemoteAddress.Uri.ToString());
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            logger.Info(reply.ToString());

            //string messageFileName = Path.Combine(this.messageLogFolder, string.Format("Log{0:000}_Outgoing.txt", Interlocked.Increment(ref messageLogFileIndex)));
            // rest of the method ommitted...
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }
    }
}

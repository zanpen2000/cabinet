using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace BehaviorLibrarys
{
    public class IncomingMessageLoggerBehaviorExtension : BehaviorExtensionElement
    {

        const string MyPropertyName = "logFolder";

        public override Type BehaviorType
        {
            get { return typeof(IncomingMessageLogger); }
        }

        [ConfigurationProperty(MyPropertyName)]
        public string LogFolder
        {
            get
            {
                return (string)base[MyPropertyName];
            }
            set
            {
                base[MyPropertyName] = value;
            }
        }

        protected override object CreateBehavior()
        {
            return new IncomingMessageLogger(this.LogFolder);
        }
    }
}

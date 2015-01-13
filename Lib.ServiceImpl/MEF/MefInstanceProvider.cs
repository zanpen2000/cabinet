using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace ArchivesServices.MEF
{

    public class MEFInstanceProvider : IInstanceProvider
    {
        public static CompositionContainer _container;


        static MEFInstanceProvider()
        {
            Compose();
        }

        public object GetInstance(System.ServiceModel.InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            var serviceType = instanceContext.Host.Description.ServiceType;
            var instance = Activator.CreateInstance(serviceType);
            return instance;
        }

        public object GetInstance(System.ServiceModel.InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(System.ServiceModel.InstanceContext instanceContext, object instance)
        {
            var disposable = instance as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }



        private static void Compose()
        {
            var catalog = new AggregateCatalog();
            var p = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            if (System.IO.Directory.Exists(p))
                catalog.Catalogs.Add(new DirectoryCatalog(p)); //Extensions
            catalog.Catalogs.Add(new DirectoryCatalog(@".")); //Extensions
            _container = new CompositionContainer(catalog, true);
        }
    }
}
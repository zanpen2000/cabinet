using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class IOC
    {
        public static CompositionContainer Container { set; get; }
        public static T Get<T>()
        {
            //var container = MefDependencySolver.GetContainer();
            var name = AttributedModelServices.GetContractName(typeof(T));
            //return (T)container.GetExportedValueOrDefault<object>(name);

            return (T)Container.GetExportedValueOrDefault<object>(name);
        }

        public static IEnumerable<Lazy<T, TMetadataView>> Get<T, TMetadataView>()
        {
            return Container.GetExports<T, TMetadataView>();
        }

        public static IEnumerable<Lazy<T>> GetModels<T>()
        {
            return Container.GetExports<T>();
        }

    }
}

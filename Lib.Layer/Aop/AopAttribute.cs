using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class AopAttribute : Attribute
    {
        public virtual object Execute(params object[] objs) { return null; }
    }
}

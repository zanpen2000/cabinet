using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class InterceptorProxy : IInterceptor
    {
        private void PreProceed(IInvocation invocation)
        {

        }
        private void PostProceed(IInvocation invocation)
        {
            var ass = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(AopAttribute), true);
            AopAttribute[] aaaa = ass as AopAttribute[];
            foreach (var item in aaaa)
            {
                item.Execute(invocation.InvocationTarget);
            }

        }

        public void Intercept(IInvocation invocation)
        {
            this.PreProceed(invocation);
            invocation.Proceed();
            this.PostProceed(invocation);
        }
    }
}

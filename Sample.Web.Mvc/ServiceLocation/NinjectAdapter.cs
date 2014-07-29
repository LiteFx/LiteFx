using Microsoft.Practices.ServiceLocation;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Web.Mvc.ServiceLocation
{

    public class NinjectServiceLocator : ServiceLocatorImplBase
    {
        public IKernel Kernel { get; private set; }

        public NinjectServiceLocator(IKernel kernel)
        {
            Kernel = kernel;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (key == null)
            {
                return Kernel.Get(serviceType);
            }
            return Kernel.Get(serviceType, key);
        }

        public override TService GetInstance<TService>()
        {
            return Kernel.Get<TService>();
        }

        public override TService GetInstance<TService>(string key)
        {
            return Kernel.Get<TService>(key);
        }
    }
}
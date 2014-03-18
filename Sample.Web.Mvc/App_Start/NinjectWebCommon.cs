[assembly: WebActivator.PreApplicationStartMethod(typeof(Sample.Web.Mvc.App_Start.NinjectWebCommon), "Start", Order=1)]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Sample.Web.Mvc.App_Start.NinjectWebCommon), "Stop")]

namespace Sample.Web.Mvc.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using LiteFx.Context.NHibernate;
    using Sample.Infrastructure;
    using Sample.Infrastructure.Repositories;
    using Sample.Domain.Repositories;
    using Microsoft.Practices.ServiceLocation;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            var ninjectServiceLocator = new NinjectAdapter.NinjectServiceLocator(kernel);
            ServiceLocator.SetLocatorProvider(() => ninjectServiceLocator);

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<SessionFactoryManager>().To<SampleSessionFactoryManager>().InRequestScope();

            kernel.Bind<ISampleContext>().To<SampleContext>().InRequestScope();

            kernel.Bind<IProductRepository>().To<ProductRepository>();                        
        }        
    }
}

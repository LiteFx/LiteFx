[assembly: WebActivator.PreApplicationStartMethod(typeof(Sample.Web.Mvc.App_Start.LiteFxWebNHibernate), "Start", Order=0)]

namespace Sample.Web.Mvc.App_Start
{
    using LiteFx.Web.NHibernate;
    using FluentNHibernate;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using FluentNHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    public static class LiteFxWebNHibernate
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(SessionPerRequestModule));

            LiteFx.Context.NHibernate.ConfigurationManager.AssemblyToConfigure = typeof(Sample.Infrastructure.SampleContext).Assembly;

            LiteFx.Context.NHibernate.ConfigurationManager.PosCustomConfiguration = (NHibernate.Cfg.Configuration configuration) =>
            {
                new SchemaUpdate(configuration).Execute(false, true);
                //new SchemaExport(configuration).Execute(false, true, true);
            };

            LiteFx.Context.NHibernate.ConfigurationManager.Initialize();
            LiteFx.Context.NHibernate.SessionFactoryManager.Initialize();
            LiteFx.Context.NHibernate.SessionFactoryManager.UseReadOnlySession = true;

            LiteFx.DomainEvents.DomainEvents.RegisterAllDomainEventHandlers(typeof(Sample.Domain.EntityBase).Assembly);

            //LiteFx.DomainEvents.DomainEvents.RegisterAsyncDomainEventHandler(new Sample.Domain.ProductSoldEventHandler());
            //LiteFx.DomainEvents.DomainEvents.RegisterAsyncDomainEventHandler(new Sample.Domain.BuyMoreProductsEventHandler());
            
            //LiteFx.Validation.ValidationHelper.ResourceManager = Siteware.Resources.Resource.ResourceManager;
        }
    }

}
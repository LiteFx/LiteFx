using System.Reflection;
using System.Threading;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using System;
using LiteFx.Context.NHibernate.Properties;
using LiteFx.Validation;

namespace LiteFx.Context.NHibernate
{
	public static class ConfigurationManager
	{
		private static Mutex _configMutex = new Mutex();

		private static Configuration configuration;
		public static Configuration Configuration
		{
			get
			{
                if (configuration == null)
                    throw new InvalidOperationException(ResourceHelper.GetString("YouHaveToCallConfigurationManagerInitializeAtLiteFxWebNHibernateStart"));

				return configuration;
			}
		}

        public static void Initialize() 
        {
            if (configuration != null)
                throw new InvalidOperationException(ResourceHelper.GetString("YouCanCallConfigurationManagerInitializeOnlyOnce"));

            try
            {
                _configMutex.WaitOne();
                                
                configuration = new Configuration();
                configuration.LinqToHqlGeneratorsRegistry<ExtendedLinqtoHqlGeneratorsRegistry>();

                if (PreCustomConfiguration != null)
                    PreCustomConfiguration(configuration);

                configuration = Fluently.Configure(configuration)
                        .Mappings(m =>
                        {
                            m.FluentMappings
                                .Conventions.Setup(s => s.Add(AutoImport.Never()))
                                .AddFromAssembly(AssemblyToConfigure);
                            m.HbmMappings
                                .AddFromAssembly(AssemblyToConfigure);
                        }).BuildConfiguration();

                if (PosCustomConfiguration != null)
                    PosCustomConfiguration(configuration);
            }
            finally
            {
                _configMutex.ReleaseMutex();
            }
        }

		public static Assembly AssemblyToConfigure { get; set; }

		public delegate void CustomConfigurationDelegate(Configuration configuration);

		public static CustomConfigurationDelegate PreCustomConfiguration { get; set; }

		public static CustomConfigurationDelegate PosCustomConfiguration { get; set; }

	}
}

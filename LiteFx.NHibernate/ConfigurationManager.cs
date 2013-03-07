using System.Reflection;
using System.Threading;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;


namespace LiteFx.Context.NHibernate
{
	public static class ConfigurationManager
	{
		private static Mutex _configMutex = new Mutex();

		private static Configuration configuration;
		/// <summary>
		/// Propriedade privada para fazer o cache da configuração do NHibernate.
		/// </summary>
		public static Configuration Configuration
		{
			get
			{
				if (configuration == null)
				{
					try
					{
						_configMutex.WaitOne();

						if (configuration == null)
						{
							configuration = new Configuration();
							configuration.LinqToHqlGeneratorsRegistry<ExtendedLinqtoHqlGeneratorsRegistry>();

							if (CustomConfiguration != null)
								CustomConfiguration(configuration);

							configuration = Fluently.Configure(configuration)
									.Mappings(m =>
									{
										m.FluentMappings
											.Conventions.Setup(s => s.Add(AutoImport.Never()))
											.AddFromAssembly(AssemblyToConfigure);
										m.HbmMappings
											.AddFromAssembly(AssemblyToConfigure);
									}).BuildConfiguration();
						}

					}
					finally
					{
						_configMutex.ReleaseMutex();
					}
				}

				return configuration;
			}
		}

		public static Assembly AssemblyToConfigure { get; set; }

		public delegate void CustomConfigurationDelegate(Configuration configuration);

		public static CustomConfigurationDelegate CustomConfiguration { get; set; }

	}
}

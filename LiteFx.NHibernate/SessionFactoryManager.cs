using System.Reflection;
using System.Threading;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Context;

namespace LiteFx.Context.NHibernate
{
	public abstract class SessionFactoryManager
	{
		private static SessionFactoryManager current;
		public static SessionFactoryManager Current
		{
			get
			{
				return current ?? (current = ServiceLocator.Current.GetInstance<SessionFactoryManager>());
			}
		}

		private static Mutex _configMutex = new Mutex();
		private static Mutex _sessionMutex = new Mutex();

		private Configuration configuration;
		/// <summary>
		/// Propriedade privada para fazer o cache da configuração do NHibernate.
		/// </summary>
		protected Configuration Configuration
		{
			get
			{
				if (configuration == null)
				{
					_configMutex.WaitOne();

					if (configuration == null)
					{
						configuration = new Configuration();
						configuration.LinqToHqlGeneratorsRegistry<ExtendedLinqtoHqlGeneratorsRegistry>();

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

					_configMutex.ReleaseMutex();
				}

				return configuration;
			}
		}

		/// <summary>
		/// Override to make custom configuration in NHibernate configuration class.
		/// </summary>
		/// <param name="configuration"></param>
		protected virtual void CustomConfiguration(Configuration configuration) { }

		/// <summary>
		/// Has to be setted on constructor.
		/// </summary>
		protected abstract Assembly AssemblyToConfigure { get; }

		/// <summary>
		/// Private sessionFactory.
		/// </summary>
		private ISessionFactory sessionFactory;

		/// <summary>
		/// Propriedade privada para fazer o cache do sessionFactory do NHibernate.
		/// </summary>
		protected ISessionFactory SessionFactory
		{
			get
			{
				if (sessionFactory == null)
				{
					sessionFactory = Configuration.BuildSessionFactory();
				}
				return sessionFactory;
			}
		}

		private static bool sessionOpened = false;

		public virtual ISession GetCurrentSession()
		{
			_sessionMutex.WaitOne();
			if (!CurrentSessionContext.HasBind(SessionFactoryManager.Current.SessionFactory))
			{
				ISession session = SessionFactory.OpenSession();
				session.BeginTransaction();
				CurrentSessionContext.Bind(session);
				sessionOpened = true;
			}
			_sessionMutex.ReleaseMutex();
			return SessionFactory.GetCurrentSession();
		}

		public virtual void DisposeSession()
		{
			if (sessionOpened)
			{
				var session = CurrentSessionContext.Unbind(SessionFactory);
				if (session != null)
				{
					session.Close();
					session.Dispose();
				}
				sessionOpened = false;
			}
		}

		public virtual void CommitTransaction()
		{
			if (sessionOpened)
			{
				var session = GetCurrentSession();
				if (session.Transaction.IsActive)
					session.Transaction.Commit();
			}
		}

		public virtual void RollbackTransaction()
		{
			if (sessionOpened)
			{
				var session = GetCurrentSession();
				if (session.Transaction.IsActive)
					session.Transaction.Rollback();
			}
		}

		public virtual void Flush()
		{
			if (sessionOpened)
			{
				var session = GetCurrentSession();
				session.Flush();
			}
		}
	}
}

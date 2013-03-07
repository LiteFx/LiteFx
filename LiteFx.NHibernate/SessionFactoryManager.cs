using System;
using System.Threading;
using Microsoft.Practices.ServiceLocation;
using NHibernate;

namespace LiteFx.Context.NHibernate
{
	public abstract class SessionFactoryManager
	{
		public static SessionFactoryManager Current
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SessionFactoryManager>();
			}
		}

		public SessionFactoryManager()
		{
			id = Guid.NewGuid();
		}

		private Guid id;
		public Guid Id { get { return id; } }


		private static Mutex _factoryMutex = new Mutex();

		/// <summary>
		/// Private sessionFactory.
		/// </summary>
		private static ISessionFactory sessionFactory;

		/// <summary>
		/// Propriedade privada para fazer o cache do sessionFactory do NHibernate.
		/// </summary>
		protected static ISessionFactory SessionFactory
		{
			get
			{
				if (sessionFactory == null)
				{
					_factoryMutex.WaitOne();
					if (sessionFactory == null)
					{
						sessionFactory = ConfigurationManager.Configuration.BuildSessionFactory();
					}
					_factoryMutex.ReleaseMutex();
				}
				return sessionFactory;
			}
		}

		private ISession session;

		public virtual ISession GetCurrentSession()
		{
			if (session == null)
			{
				session = SessionFactory.OpenSession();
				session.BeginTransaction();
				//CurrentSessionContext.Bind(session);
			}

			return session;
		}

		public virtual void DisposeSession()
		{
			if (session != null)
			{
				//CurrentSessionContext.Unbind(SessionFactory);
				session.Close();
				session.Dispose();
				session = null;
			}
		}

		public virtual void CommitTransaction()
		{
			if (session != null)
			{
				if (session.Transaction.IsActive)
					session.Transaction.Commit();
			}
		}

		public virtual void RollbackTransaction()
		{
			if (session != null)
			{
				if (session.Transaction.IsActive)
					session.Transaction.Rollback();
			}
		}

		public virtual void Flush()
		{
			if (session != null)
			{
				session.Flush();
			}
		}
	}
}

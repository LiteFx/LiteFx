using System;
using System.Threading;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using LiteFx.Context.NHibernate.Properties;
using System.Diagnostics;

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
                    throw new InvalidOperationException(Resources.YouHaveToCallSessionFactoryManagerInitializeAtLiteFxWebNHibernateStart);

				return sessionFactory;
			}
		}

        public static void Initialize() 
        {
            if (sessionFactory != null)
                throw new InvalidOperationException(Resources.YouCanCallSessionFactoryManagerInitializeOnlyOnce);
                
            try
            {
                _factoryMutex.WaitOne();

                sessionFactory = ConfigurationManager.Configuration.BuildSessionFactory();
            }
            finally
            {
                _factoryMutex.ReleaseMutex();
            }
        }

        public bool ReadOnly { get; set; }

		private ISession session;

		public virtual ISession GetCurrentSession()
		{
			if (session == null)
			{

                Trace.WriteLine("Opening NHibernate Session.", "LiteFx");
				session = SessionFactory.OpenSession();

                session.DefaultReadOnly = ReadOnly;

                if (!ReadOnly)
                {
                    Trace.WriteLine("Beggining NHibernate Transaction.", "LiteFx");
                    session.BeginTransaction();
                }
				//CurrentSessionContext.Bind(session);
			}

			return session;
		}

		public virtual void DisposeSession()
		{
			if (session != null)
			{

                Trace.WriteLine("Closing and Disposing NHibernate Session.", "LiteFx");
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
                {
                    Trace.WriteLine("Commiting NHibernate Transaction.", "LiteFx");
                    session.Transaction.Commit();
                }
			}
		}

		public virtual void RollbackTransaction()
		{
			if (session != null)
			{
                if (session.Transaction.IsActive)
                {
                    Trace.WriteLine("Rollingback NHibernate Transaction.", "LiteFx");
                    session.Transaction.Rollback();
                }
			}
		}

		public virtual void Flush()
		{
			if (session != null)
			{
                Trace.WriteLine("Flushing NHibernate Session.", "LiteFx");
				session.Flush();
			}
		}
	}
}

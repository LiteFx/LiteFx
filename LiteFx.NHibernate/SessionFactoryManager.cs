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

        private Guid id;
        public Guid Id { get { return id; } }

        public bool ReadOnly { get; set; }

        private ISession session;

        public bool IsSessionActive { get { return session != null; } }

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

        public SessionFactoryManager()
        {
            id = Guid.NewGuid();
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

        public virtual ISession GetCurrentSession()
        {
            if (!IsSessionActive)
            {

                Trace.WriteLine("Opening NHibernate Session.", getTraceCategory());
                session = SessionFactory.OpenSession();
                //CurrentSessionContext.Bind(session);

                if (ReadOnly)
                {
                    session.DefaultReadOnly = true;
                    session.FlushMode = FlushMode.Never;
                }
                else
                {
                    BeginTransaction();
                }
            }

            return session;
        }

        public ITransaction BeginTransaction()
        {
            if (IsSessionActive)
            {
                if (!session.Transaction.IsActive)
                {
                    Trace.WriteLine("Beggining NHibernate Transaction.", getTraceCategory());

                    if (ReadOnly)
                    {
                        session.DefaultReadOnly = false;
                        session.FlushMode = FlushMode.Auto;
                        ReadOnly = false;
                    }

                    return session.BeginTransaction();
                }
            }

            throw new InvalidOperationException(Resources.YouCantBeginATransactionWithoutAnActiveNHibernateSession);
        }

        public virtual void DisposeSession()
        {
            if (IsSessionActive)
            {

                Trace.WriteLine("Closing and Disposing NHibernate Session.", getTraceCategory());
                //CurrentSessionContext.Unbind(SessionFactory);
                session.Close();
                session.Dispose();
                session = null;
            }
        }

        public virtual void CommitTransaction()
        {
            if (IsSessionActive)
            {
                if (session.Transaction.IsActive)
                {
                    Flush();
                    Trace.WriteLine("Commiting NHibernate Transaction.", getTraceCategory());
                    session.Transaction.Commit();
                }
            }
        }

        public virtual void RollbackTransaction()
        {
            if (IsSessionActive)
            {
                if (session.Transaction.IsActive)
                {
                    Trace.WriteLine("Rollingback NHibernate Transaction.", getTraceCategory());
                    session.Transaction.Rollback();
                }
            }
        }

        public virtual void Flush()
        {
            if (IsSessionActive)
            {
                Trace.WriteLine("Flushing NHibernate Session.", getTraceCategory());
                session.Flush();
            }
        }

        string _traceCategory = string.Empty;

        private string getTraceCategory()
        {
            if(string.IsNullOrEmpty(_traceCategory))
                _traceCategory = string.Format("LiteFx - Session Id:{0}", Id.ToString().Substring(0, 8));

            return _traceCategory;
        }
    }
}
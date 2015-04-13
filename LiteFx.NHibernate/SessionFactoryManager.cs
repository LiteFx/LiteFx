using System;
using System.Threading;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using LiteFx.Context.NHibernate.Properties;
using System.Diagnostics;

namespace LiteFx.Context.NHibernate
{
    public abstract class SessionFactoryManager : IDisposable
    {
        public static SessionFactoryManager Current
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SessionFactoryManager>();
            }
        }

        public static bool UseReadOnlySession { get; set; }

        private Guid id;
        public Guid Id { get { return id; } }

        public bool ReadOnly { get; set; }

        private ISession session;

        public bool IsSessionActive { get { return session != null; } }

        private static Mutex _factoryMutex = new Mutex();

        private static ISessionFactory sessionFactory;

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

            DomainEvents.DomainEvents.AsyncDomainEventHandlerError += DomainEvents_AsyncDomainEventHandlerError;
            DomainEvents.DomainEvents.AsyncDomainEventHandlerExecuted += DomainEvents_AsyncDomainEventHandlerExecuted;

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

        private static void DomainEvents_AsyncDomainEventHandlerError(Exception exception, DomainEvents.IDomainEvent domainEvent, DomainEvents.IAsyncDomainEventHandler asyncDomainEventHandler)
        {
            SessionFactoryManager.Current.RollbackTransaction();
            SessionFactoryManager.Current.DisposeSession();
        }

        private static void DomainEvents_AsyncDomainEventHandlerExecuted(DomainEvents.IDomainEvent domainEvent, DomainEvents.IAsyncDomainEventHandler asyncDomainEventHandler)
        {
            try
            {
                SessionFactoryManager.Current.CommitTransaction();
                LiteFx.DomainEvents.DomainEvents.DispatchAsyncEvents();
            }
            catch (Exception ex)
            {
                DomainEvents.DomainEvents.OnAsyncDomainEventHandlerError(ex, domainEvent, asyncDomainEventHandler);
                SessionFactoryManager.Current.RollbackTransaction();
                throw;
            }
            finally
            {
                SessionFactoryManager.Current.DisposeSession();
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
                    Trace.WriteLine("Begining NHibernate Transaction.", getTraceCategory());

                    if (ReadOnly)
                    {
                        session.DefaultReadOnly = false;
                        session.FlushMode = FlushMode.Auto;
                        ReadOnly = false;
                    }

                    ITransaction transaction = session.BeginTransaction();

                    return transaction;
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

        #region IDisposable Members [Dispose pattern implementation]

        /// <summary>
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        private bool disposed;

        /// <summary>
        /// Libera todos os recursos utilizados pela classe.
        /// Implementação do Dispose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        /// <param name="disposing">Usado para verificar se a chamada esta sendo feita pelo <see cref="GC"/> ou pela aplicação.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                if (session != null)
                    session.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// Chamado pelo <see ref="GC" /> para liberar recursos que não estão sendo utilizados.
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        ~SessionFactoryManager()
        {
            Dispose(false);
        }

        /// <summary>
        /// Libera todos os recursos utilizados pela classe.
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        string _traceCategory = string.Empty;

        private string getTraceCategory()
        {
            if (string.IsNullOrEmpty(_traceCategory))
                _traceCategory = string.Format("LiteFx - Session Id:{0}", Id.ToString().Substring(0, 8));

            return _traceCategory;
        }
    }
}
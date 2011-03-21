using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using System.Reflection;
using FluentNHibernate.Cfg;
using NHibernate.Context;

namespace LiteFx.Bases.Context.NHibernate
{
    public static class SessionFactoryManager
    {
        private static Configuration configuration;
        /// <summary>
        /// Propriedade privada para fazer o cache da configuração do NHibernate.
        /// </summary>
        public static Configuration Configuration
        {
            get { return configuration ?? (configuration = new Configuration()); }
        }

        /// <summary>
        /// Has to be setted on constructor.
        /// </summary>
        public static Assembly AssemblyToConfigure { get; set; }

        /// <summary>
        /// Private sessionFactory.
        /// </summary>
        private static ISessionFactory sessionFactory;

        /// <summary>
        /// Propriedade privada para fazer o cache do sessionFactory do NHibernate.
        /// </summary>
        public static ISessionFactory SessionFactory
        {
            get
            {
                return sessionFactory ?? (sessionFactory = Fluently.Configure(Configuration)
                                        .Mappings(m =>
                                        {
                                            m.FluentMappings.AddFromAssembly(
                                                AssemblyToConfigure);
                                            m.HbmMappings.AddFromAssembly(
                                                AssemblyToConfigure);
                                        })
                                        .BuildSessionFactory());
            }
        }

        internal static ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(SessionFactoryManager.SessionFactory))
            {
                ISession session = SessionFactoryManager.SessionFactory.OpenSession();
                session.BeginTransaction();
                CurrentSessionContext.Bind(session);
            }
            return SessionFactory.GetCurrentSession();
        }

        public static void DisposeSession()
        {
            var session = GetCurrentSession();
            session.Close();
            session.Dispose();
        }

        public static void BeginTransaction()
        {
            GetCurrentSession().BeginTransaction();
        }

        public static void CommitTransaction()
        {
            var session = GetCurrentSession();
            if (session.Transaction.IsActive)
                session.Transaction.Commit();
        }

        public static void RollbackTransaction()
        {
            var session = GetCurrentSession();
            if (session.Transaction.IsActive)
                session.Transaction.Rollback();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;

namespace LiteFx.Bases
{
    public class NHibernateContextBase : IDBContext, IDisposable
    {
        #region NHibernate Configuration and SessionFactory Cache
        private static Configuration _cfg;
        /// <summary>
        /// Propriedade privada para fazer o cache da configuração do NHibernate.
        /// </summary>
        protected static Configuration cfg
        {
            get
            {
                if (_cfg == null)
                    _cfg = new NHibernate.Cfg.Configuration();

                return _cfg;
            }
        }

        private static ISessionFactory _sessionFactory;
        /// <summary>
        /// Propriedade privada para fazer o cache do sessionFactory do NHibernate.
        /// </summary>
        protected static ISessionFactory sessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    _sessionFactory = Fluently.Configure(cfg)
                        /* PODEMOS USAR ESTA FUNCIONALIDADE NO FUTURO
                        .ExposeConfiguration(config =>
                        {
                            SchemaExport se = new SchemaExport(config);
                            se.SetOutputFile(@"E:\SWFontes\PortalSim\Desenvolvimento\PortalSim.Web.Mvc\teste.txt");
                            se.Create(false, false);
                        })*/
                                              .Mappings(m =>
                                              {
                                                  m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly());
                                                  m.HbmMappings.AddFromAssembly(Assembly.GetExecutingAssembly());
                                              })
                                              .BuildSessionFactory();

                return _sessionFactory;
            }
        }
        #endregion

        /// <summary>
        /// The NHibernate Database Context Constructor.
        /// </summary>
        protected NHibernateContextBase()
        {
            OpenSession();
        }

        /// <summary>
        /// Open the session with the database.
        /// </summary>
        protected virtual void OpenSession()
        {
            currentSession = sessionFactory.OpenSession();
        }

        #region IDBContext Members

        /// <summary>
        /// Sessão com o banco de dados.
        /// </summary>
        protected ISession currentSession;

        /// <summary>
        /// Flag usado para identificar se há uma transação aberta.
        /// </summary>
        protected bool openTransaction = false;

        /// <summary>
        /// Variavel que mantem a transação.
        /// </summary>
        protected ITransaction transaction;

        /// <summary>
        /// Inicia uma transação e retorna a referência da transação como um IDisposable.
        /// </summary>
        /// <returns>Referência da transação como um IDisposable.</returns>
        public virtual IDisposable BeginTransaction()
        {
            if (openTransaction)
                return transaction;

            if (currentSession != null)
                currentSession.Dispose();

            OpenSession();

            transaction = currentSession.BeginTransaction();
            openTransaction = true;
            return transaction;
        }

        /// <summary>
        /// Salva as alterações realizadas sobre a transação aberta no banco de dados.
        /// </summary>
        public virtual void CommitTransaction()
        {
            if (!openTransaction)
                throw new Exception("Este método pode ser chamado somente após a chamada do método BeginTransaction.");

            try
            {
                transaction.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                transaction = null;
                openTransaction = false;
            }
        }

        /// <summary>
        /// Descarta as alterações realizadas sobre a transação aberta.
        /// </summary>
        public virtual void RollBackTransaction()
        {
            if (!openTransaction)
                throw new Exception("Este método pode ser chamado somente após a chamada do método BeginTransaction.");

            transaction.Rollback();
            transaction.Dispose();
            openTransaction = false;
        }

        /// <summary>
        /// Exclui uma entidade do contexto pelo seu Identificador.
        /// </summary>
        /// <typeparam name="T">Tipo do entidade.</typeparam>
        /// <param name="id">Identificador do entidade.</param>
        public virtual T Delete<T>(long id)
        {
            T obj = currentSession.Get<T>(id);
            Delete(obj);
            return obj;
        }

        /// <summary>
        /// Exclui uma entidade do contexto.
        /// </summary>
        /// <param name="entity">Entidade que será exlcuida.</param>
        public virtual void Delete(object entity)
        {
            BeginTransaction();
            currentSession.Delete(entity);
        }

        /// <summary>
        /// Salva uma entidade no contexto.
        /// </summary>
        /// <param name="entity">Entidade que será salva.</param>
        public virtual void Save(object entity)
        {
            BeginTransaction();
            currentSession.SaveOrUpdate(entity);
        }

        /// <summary>
        /// Remove o objeto do cache do contexto.
        /// </summary>
        /// <param name="entity">Objeto a ser removido do cache.</param>
        public virtual void RemoveFromCache(object entity)
        {
            currentSession.Evict(entity);
        }

        /// <summary>
        /// Salva as informações alteradas no contexto no banco de dados.
        /// </summary>
        public virtual void SaveContext()
        {
            try
            {
                CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IDisposable Members [Dispose pattern implementation]

        /// <summary>
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        private bool disposed = false;

        /// <summary>
        /// Libera todos os recursos utilizados pela classe.
        /// Implementação do Dispose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        /// <param name="disposing">Usado para verificar se a chamada esta sendo feita pelo <see cref="GC"/> ou pela aplicação.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (openTransaction)
                    {
                        RollBackTransaction();
                    }

                    if (currentSession != null)
                        currentSession.Dispose();
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Chamado pelo <see ref="GC" /> para liberar recursos que não estão sendo utilizados.
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        ~NHibernateContextBase()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Libera todos os recursos utilizados pela classe.
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

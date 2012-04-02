using System;
using System.Linq;
using Raven.Client;

namespace LiteFx.Raven
{
    public abstract class RavenContextAdapter<TId> : IContext<TId>, IDisposable
        where TId : IEquatable<TId>
    {

        protected IDocumentSession Session 
        {
            get { return SessionFactoryManager.Current.GetCurrentSession(); }
        }

        public T Delete<T>(TId id)
        {
            var doc = Session.Load<T>(id as ValueType);
            Session.Delete(doc);
            throw new NotImplementedException();
        }

        public IQueryable<T> GetQueryableObject<T>() where T : class
        {
            return Session.Query<T>();
        }

        public void SaveContext()
        {
            Session.SaveChanges();
        }

        public void Save(object entity)
        {
            Session.Store(entity);
        }

        public void Delete(object entity)
        {
            Session.Delete(entity);
        }

        public void Detach(object entity)
        {
            Session.Advanced.Evict(entity);
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
                if (Session != null)
                    Session.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// Chamado pelo <see ref="GC" /> para liberar recursos que não estão sendo utilizados.
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        ~RavenContextAdapter()
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteFx.Bases;

namespace LiteFx.Bases.Repository
{
    /// <summary>
    /// Repository base.
    /// </summary>
    /// <typeparam name="TEntity">Type that the repository will handle.</typeparam>
    /// <typeparam name="TId">Type of identificator.</typeparam>
    /// <typeparam name="TContext">Type of the Database Context.</typeparam>
    public abstract class RepositoryBase<TEntity, TId, TContext> : IRepository<TEntity, TId> , IDisposable
        where TEntity : EntityBase<TId>, new()
        where TContext : IContext<TId>, IDisposable, new()
        where TId : IEquatable<TId>
    {
        #region IRepository<T,IGerenciadorEventoDB> Members

        /// <summary>
        /// The database context.
        /// </summary>
        protected TContext Context { get; set; }

        /// <summary>
        /// Get all entities instances.
        /// </summary>
        /// <returns>List with entities instances.</returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return from e in Context.GetQueryableObject<TEntity>()
                   select e;
        }

        /// <summary>
        /// Get an entity by entity id.
        /// </summary>
        /// <param name="id">Entity identificator.</param>
        /// <returns>The entity instance.</returns>
        public virtual TEntity GetById(TId id)
        {
            return (from e in Context.GetQueryableObject<TEntity>()
                    where e.Id.Equals(id)
                    select e).FirstOrDefault();
        }

        /// <summary>
        /// Get a list of entities that satisfy the specificaton.
        /// </summary>
        /// <param name="specification">Specification filter.</param>
        /// <returns>List of entities.</returns>
        public virtual IQueryable<TEntity> GetBySpecification(ILambdaSpecification<TEntity> specification)
        {
            return Context.GetQueryableObject<TEntity>().Where(specification.Predicate);
        }

        /// <summary>
        /// Get the first entity that satisfy the specification.
        /// </summary>
        /// <param name="specification">Specification filter.</param>
        /// <returns>An entity instance.</returns>
        public virtual TEntity GetFirstBySpecification(ILambdaSpecification<TEntity> specification)
        {
            return Context.GetQueryableObject<TEntity>().Where(specification.Predicate).FirstOrDefault();
        }

        /// <summary>
        /// Save entity in the context.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public virtual void Save(TEntity entity)
        {
            Context.Save(entity);
            Context.SaveContext();
        }

        /// <summary>
        /// Delete a entity in the context.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        public virtual void Delete(TEntity entity)
        {
            Context.Delete(entity);
            Context.SaveContext();
        }

        /// <summary>
        /// Delete an entity by the identificator.
        /// </summary>
        /// <param name="id">Entity identificator.</param>
        public virtual void Delete(TId id)
        {
            Context.Delete<TEntity>(id);
            Context.SaveContext();
        }

        #endregion

        #region IDisposable Members

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
                    if (Context != null)
                        Context.Dispose();

                disposed = true;
            }
        }

        /// <summary>
        /// Chamado pelo <see ref="GC" /> para liberar recursos que não estão sendo utilizados.
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        ~RepositoryBase()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Libera todos os recursos utilizados pela classe.
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        /// <example>
        /// <code lang="cs" title="Utilizando a BaseWKR">
        /// <![CDATA[
        /// //Como implementar uma classe Worker (WRK)
        /// public class ProdutoRepository : RepositoryBase<Produto>, IDisposable
        /// {
        ///     public void Dispose()
        ///     {
        ///         base.Dispose();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// <code lang="cs" title="Liberando recursos">
        /// <![CDATA[
        /// //Melhor pratica para liberar recursos o mais breve possivel.
        /// using(ProdutoRepository repo = new  ProdutoRepository())
        /// {
        ///     // Seu codigo vem aqui.
        /// } //Neste ponto o objeto criado na clausula using será liberado da memoria.
        /// ]]>
        /// </code>
        /// </example>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using LiteFx.Specification;

namespace LiteFx.Repository
{
    /// <summary>
    /// Repository base.
    /// </summary>
    /// <typeparam name="TContext">Type of context.</typeparam>
    public abstract class RepositoryBase<TContext> where TContext : class, IContext
    {
        /// <summary>
        /// Method factory to be implemented.
        /// </summary>
        protected TContext Context { get; set; }

        public RepositoryBase(TContext context)
        {
            Context = context;
        }
    }

    /// <summary>
    /// Repository base.
    /// </summary>
    /// <typeparam name="TEntity">Type that the repository will handle.</typeparam>
    /// <typeparam name="TId">Type of identificator.</typeparam>
    /// <typeparam name="TContext">Type of the Database Context.</typeparam>
    public abstract class RepositoryBase<TEntity, TId, TContext> : RepositoryBase<TContext>, IRepository<TEntity, TId>
        where TEntity : EntityBase<TId>
        where TContext : class, IContext<TId>
        where TId : struct, IEquatable<TId>, IComparable<TId>
    {
        public RepositoryBase(TContext context) : base(context) { }

        /// <summary>
        /// Get all entities instances.
        /// </summary>
        /// <returns>List with entities instances.</returns>
        public virtual IEnumerable<TEntity> GetAll()
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
                    select e).SingleOrDefault();
        }

        /// <summary>
        /// Get a list of entities that satisfy the specificaton.
        /// </summary>
        /// <param name="specification">Specification filter.</param>
        /// <returns>List of entities.</returns>
        public virtual IEnumerable<TEntity> GetBySpecification(ILambdaSpecification<TEntity> specification)
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
        }

        /// <summary>
        /// Delete a entity in the context.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        public virtual void Delete(TEntity entity)
        {
            Context.Delete(entity);
        }

        /// <summary>
        /// Delete an entity by the identificator.
        /// </summary>
        /// <param name="id">Entity identificator.</param>
        public virtual void Delete(TId id)
        {
            Context.Delete<TEntity>(id);
        }
    }
}
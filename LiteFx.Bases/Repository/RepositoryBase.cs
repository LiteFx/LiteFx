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
    /// <typeparam name="TIdentificator">Type of identificator.</typeparam>
    /// <typeparam name="TDBContext">Type of the Database Context.</typeparam>
    public abstract class RepositoryBase<TEntity, TIdentificator, TDBContext> : IRepository<TEntity, TIdentificator, TDBContext> 
        where TEntity : EntityBase<TIdentificator>
        where TDBContext : IDBContext<TIdentificator>
        where TIdentificator : IEquatable<TIdentificator>
    {
        #region IRepository<T,IGerenciadorEventoDB> Members

        /// <summary>
        /// The data base context.
        /// </summary>
        public TDBContext DBContext { get; set; }

        /// <summary>
        /// Get all entities instances.
        /// </summary>
        /// <returns>List with entities instances.</returns>
        public IList<TEntity> GetAll()
        {
            return (from e in DBContext.GetQueryableObject<TEntity>()
                    select e).ToList();
        }

        /// <summary>
        /// Get an entity by entity id.
        /// </summary>
        /// <param name="id">Entity identificator.</param>
        /// <returns>The entity instance.</returns>
        public TEntity GetById(TIdentificator id)
        {
            return (from e in DBContext.GetQueryableObject<TEntity>()
                    where e.Identificador.Equals(id)
                    select e).FirstOrDefault();
        }

        /// <summary>
        /// Get a list of entities that satisfy the specificaton.
        /// </summary>
        /// <param name="specification">Specification filter.</param>
        /// <returns>List of entities.</returns>
        public IList<TEntity> GetBySpecification(ILambdaSpecification<TEntity> specification)
        {
            return DBContext.GetQueryableObject<TEntity>().Where(specification.Predicate).ToList();
        }

        /// <summary>
        /// Get the first entity that satisfy the specification.
        /// </summary>
        /// <param name="specification">Specification filter.</param>
        /// <returns>An entity instance.</returns>
        public TEntity GetFirstBySpecification(ILambdaSpecification<TEntity> specification)
        {
            return DBContext.GetQueryableObject<TEntity>().Where(specification.Predicate).FirstOrDefault();
        }

        /// <summary>
        /// Save entity in the context.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public void Save(TEntity entity)
        {
            DBContext.Save(entity);
        }

        /// <summary>
        /// Delete a entity in the context.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        public void Delete(TEntity entity)
        {
            DBContext.Delete(entity);
        }

        /// <summary>
        /// Delete an entity by the identificator.
        /// </summary>
        /// <param name="id">Entity identificator.</param>
        public void Delete(TIdentificator id)
        {
            DBContext.Delete<TEntity>(id);
        }

        #endregion
    }
}
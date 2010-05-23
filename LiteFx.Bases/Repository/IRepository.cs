using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteFx.Bases.Specification;

namespace LiteFx.Bases.Repository
{
    /// <summary>
    /// Repository interface.
    /// </summary>
    /// <typeparam name="TEntity">Type that the repository will handle.</typeparam>
    /// <typeparam name="TIdentificator">Type of identificator.</typeparam>
    /// <typeparam name="TDBContext">Type of the Database Context.</typeparam>
    public interface IRepository<TEntity, TIdentificator> 
        where TIdentificator : IEquatable<TIdentificator>
    {
        /// <summary>
        /// Get the entity instance by id.
        /// </summary>
        /// <param name="id">Entity identificator.</param>
        /// <returns>An entity instance.</returns>
        TEntity GetById(TIdentificator id);

        /// <summary>
        /// Get a list of entities that satisfy the specificaton.
        /// </summary>
        /// <param name="specification">Specification filter.</param>
        /// <returns>List of entities.</returns>
        IQueryable<TEntity> GetBySpecification(ILambdaSpecification<TEntity> specification);

        /// <summary>
        /// Get the first entity that satisfy the specification.
        /// </summary>
        /// <param name="specification">Specification filter.</param>
        /// <returns>An entity instance.</returns>
        TEntity GetFirstBySpecification(ILambdaSpecification<TEntity> specification);

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>List of entities.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Save entity in the context.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        void Save(TEntity entity);

        /// <summary>
        /// Delete a entity in the context.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete an entity by the identificator.
        /// </summary>
        /// <param name="id">Entity identificator.</param>
        void Delete(TIdentificator id);
    }
}
using System;
using System.Collections.Generic;
using LiteFx.Bases.Specification;

namespace LiteFx.Bases.Repository
{
    /// <summary>
    /// Repository interface.
    /// </summary>
    /// <typeparam name="TEntity">Type that the repository will handle.</typeparam>
    /// <typeparam name="TId">Type of identificator.</typeparam>
    public interface IRepository<TEntity, in TId> 
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Get the entity instance by id.
        /// </summary>
        /// <param name="id">Entity identificator.</param>
        /// <returns>An entity instance.</returns>
        TEntity GetById(TId id);

        /// <summary>
        /// Get a list of entities that satisfy the specificaton.
        /// </summary>
        /// <param name="specification">Specification filter.</param>
        /// <returns>List of entities.</returns>
        IEnumerable<TEntity> GetBySpecification(ILambdaSpecification<TEntity> specification);

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
        IEnumerable<TEntity> GetAll();

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
        void Delete(TId id);
    }
}
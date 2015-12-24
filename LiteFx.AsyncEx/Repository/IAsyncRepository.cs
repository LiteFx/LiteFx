using System;
using System.Collections.Generic;
using LiteFx.Specification;
using System.Threading.Tasks;

namespace LiteFx.Repository
{
	public interface IAsyncRepository<TEntity> : IRepository<TEntity>
	{
		/// <summary>
		/// Get a list of entities that satisfy the specificaton.
		/// </summary>
		/// <param name="specification">Specification filter.</param>
		/// <returns>List of entities.</returns>
		Task<IEnumerable<TEntity>> GetBySpecificationAsync(ILambdaSpecification<TEntity> specification);

		/// <summary>
		/// Get the first entity that satisfy the specification.
		/// </summary>
		/// <param name="specification">Specification filter.</param>
		/// <returns>An entity instance.</returns>
		Task<TEntity> GetFirstBySpecificationAsync(ILambdaSpecification<TEntity> specification);

		/// <summary>
		/// Get all entities.
		/// </summary>
		/// <returns>List of entities.</returns>
		Task<IEnumerable<TEntity>> GetAllAsync();

		/// <summary>
		/// Save entity in the context.
		/// </summary>
		/// <param name="entity">Entity to be saved.</param>
		Task SaveAsync(TEntity entity);

		/// <summary>
		/// Delete a entity in the context.
		/// </summary>
		/// <param name="entity">Entity to be deleted.</param>
		Task DeleteAsync(TEntity entity);
	}

	/// <summary>
	/// Repository interface.
	/// </summary>
	/// <typeparam name="TEntity">Type that the repository will handle.</typeparam>
	/// <typeparam name="TId">Type of identificator.</typeparam>
	public interface IAsyncRepository<TEntity, in TId> : IAsyncRepository<TEntity>, IRepository<TEntity, TId>
		where TId : IEquatable<TId>
	{
		/// <summary>
		/// Get the entity instance by id.
		/// </summary>
		/// <param name="id">Entity identificator.</param>
		/// <returns>An entity instance.</returns>
		Task<TEntity> GetByIdAsync(TId id);

		/// <summary>
		/// Delete an entity by the identificator.
		/// </summary>
		/// <param name="id">Entity identificator.</param>
		Task DeleteAsync(TId id);
	}
}
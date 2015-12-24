using LiteFx.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteFx.Repository
{
	public abstract class AsyncRepositoryBase<TEntity, TContext> : RepositoryBase<TEntity, TContext>, IAsyncRepository<TEntity>
		where TEntity : EntityBase
		where TContext : class, IContext
	{
		public AsyncRepositoryBase(TContext context) : base(context) { }

		/// <summary>
		/// Get all entities instances.
		/// </summary>
		/// <returns>List with entities instances.</returns>
		public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await Task.Run(() => from e in Context.GetQueryableObject<TEntity>()
				   select e);
		}

		/// <summary>
		/// Get a list of entities that satisfy the specificaton.
		/// </summary>
		/// <param name="specification">Specification filter.</param>
		/// <returns>List of entities.</returns>
		public virtual async Task<IEnumerable<TEntity>> GetBySpecificationAsync(ILambdaSpecification<TEntity> specification)
		{
			return await Task.Run(() => Context.GetQueryableObject<TEntity>().Where(specification.Predicate));
		}

		/// <summary>
		/// Get the first entity that satisfy the specification.
		/// </summary>
		/// <param name="specification">Specification filter.</param>
		/// <returns>An entity instance.</returns>
		public virtual async Task<TEntity> GetFirstBySpecificationAsync(ILambdaSpecification<TEntity> specification)
		{
			return await Task.Run(() => Context.GetQueryableObject<TEntity>().Where(specification.Predicate).FirstOrDefault());
		}

		/// <summary>
		/// Save entity in the context.
		/// </summary>
		/// <param name="entity">Entity to be saved.</param>
		public virtual async Task SaveAsync(TEntity entity)
		{
			await Task.Run(() => Context.Save(entity));
		}

		/// <summary>
		/// Delete a entity in the context.
		/// </summary>
		/// <param name="entity">Entity to be deleted.</param>
		public virtual async Task DeleteAsync(TEntity entity)
		{
			await Task.Run(() => Context.Delete(entity));
		}
	}

	/// <summary>
	/// Repository base.
	/// </summary>
	/// <typeparam name="TEntity">Type that the repository will handle.</typeparam>
	/// <typeparam name="TId">Type of identificator.</typeparam>
	/// <typeparam name="TContext">Type of the Database Context.</typeparam>
	public abstract class AsyncRepositoryBase<TEntity, TId, TContext> : AsyncRepositoryBase<TEntity, TContext>, IAsyncRepository<TEntity, TId>
		where TEntity : EntityBase<TId>
		where TContext : class, IContext
		where TId : IEquatable<TId>, IComparable<TId>
	{
		public AsyncRepositoryBase(TContext context) : base(context) { }

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
		/// Delete an entity by the identificator.
		/// </summary>
		/// <param name="id">Entity identificator.</param>
		public virtual void Delete(TId id)
		{
			Context.Delete<TEntity, TId>(id);
		}

		/// <summary>
		/// Get an entity by entity id.
		/// </summary>
		/// <param name="id">Entity identificator.</param>
		/// <returns>The entity instance.</returns>
		public virtual async Task<TEntity> GetByIdAsync(TId id)
		{
			return await Task.Run(() => (from e in Context.GetQueryableObject<TEntity>()
					where e.Id.Equals(id)
					select e).SingleOrDefault());
		}

		/// <summary>
		/// Delete an entity by the identificator.
		/// </summary>
		/// <param name="id">Entity identificator.</param>
		public virtual async Task DeleteAsync(TId id)
		{
			await Task.Run(() => Context.Delete<TEntity, TId>(id));
		}
	}

}

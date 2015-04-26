using System;
using System.Linq;

namespace LiteFx
{
    public interface IContext 
    {
        /// <summary>
        /// Get a queryable object of an especifique entity.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>A queryable object.</returns>
        IQueryable<T> GetQueryableObject<T>() where T : class;

        /// <summary>
        /// Save all modifications made over the context.
        /// </summary>
        void SaveContext();

        /// <summary>
        /// Save entity in context.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        void Save(object entity);

        /// <summary>
        /// Delete entity in context.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        void Delete(object entity);

		/// <summary>
		/// Delete an entity by id.
		/// </summary>
		/// <typeparam name="T">Entity type.</typeparam>
		/// <param name="id">Entity id.</param>
		T Delete<T, TId>(TId id) where TId : IEquatable<TId>;

        /// <summary>
        /// Detach entity from context.
        /// </summary>
        /// <param name="entity">Entity to be detached</param>
        void Detach(object entity);
    }
}

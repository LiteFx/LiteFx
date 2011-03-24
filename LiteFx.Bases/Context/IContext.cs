using System;
using System.Linq;

namespace LiteFx.Bases
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
    }

    /// <summary>
    /// Interface that will be implemented by classes that represent contexts.
    /// These contexts could persist the modifications in a DataBase, XML Files, memory and etc.
    /// </summary>
    /// <typeparam name="TId">Type of id.</typeparam>
    public interface IContext<TId> : IContext
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Delete an entity by id.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="id">Entity id.</param>
        T Delete<T>(TId id);
    }
}

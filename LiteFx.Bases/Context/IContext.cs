using System;
using System.Linq;

namespace LiteFx.Bases
{
    /// <summary>
    /// Interface that will be implemented by classes that represent contexts.
    /// These contexts could persist the modifications in a DataBase, XML Files, memory and etc.
    /// </summary>
    /// <typeparam name="TId">Type of id.</typeparam>
    public interface IContext<TId> where TId : IEquatable<TId>
    {
        /// <summary>
        /// Get a queryable object of an especifique entity.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>A queryable object.</returns>
        IQueryable<T> GetQueryableObject<T>() where T : EntityBase<TId>;

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
        T Delete<T>(TId id);

        /// <summary>
        /// Begins a transaction in context.
        /// </summary>
        /// <returns>Returns the transaction object as an <see cref="System.IDisposable"/></returns>
        IDisposable BeginTransaction();

        /// <summary>
        /// Commits the transaction and close all resources.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rollbacks the transaction and close all resources.
        /// </summary>
        void RollBackTransaction();
    }
}

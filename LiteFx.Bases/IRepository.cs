using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases
{
    /// <summary>
    /// Repository interface.
    /// </summary>
    /// <typeparam name="T">Type that the repository will handle.</typeparam>
    /// <typeparam name="TDBContext">Type of the Database Context.</typeparam>
    public interface IRepository<T, TDBContext> where TDBContext : IDBContext
    {
        /// <summary>
        /// Database Context.
        /// </summary>
        TDBContext DBContext { get; set; }

        /// <summary>
        /// Get the object instance by id.
        /// </summary>
        /// <param name="id">Object identificator.</param>
        /// <returns>A object instance.</returns>
        T GetById(int id);

        /// <summary>
        /// Get all objects.
        /// </summary>
        /// <returns>List of objects.</returns>
        IList<T> GetAll();

        /// <summary>
        /// Save object in the context.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        void Save(T entity);

        /// <summary>
        /// Delete a object in the context.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        void Delete(T entity);

        /// <summary>
        /// Delete an entity by the identificator.
        /// </summary>
        /// <param name="id">Entity identificator.</param>
        void Delete(int id);
    }
}

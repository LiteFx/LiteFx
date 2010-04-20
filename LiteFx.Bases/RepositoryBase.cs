using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases
{
    /// <summary>
    /// Repository base.
    /// </summary>
    /// <typeparam name="TEntity">Type that the repository will handle.</typeparam>
    /// <typeparam name="TIdentificator">Type of identificator.</typeparam>
    /// <typeparam name="TDBContext">Type of the Database Context.</typeparam>
    public class RepositoryBase<TEntity, TIdentificator, TDBContext> : IRepository<TEntity, TIdentificator, TDBContext> 
        where TEntity : EntityBase<TIdentificator>
        where TDBContext : IDBContext<TIdentificator>
        where TIdentificator : IEquatable<TIdentificator>
    {
        #region IRepository<T,IGerenciadorEventoDB> Members

        public TDBContext DBContext { get; set; }

        public TEntity GetById(TIdentificator id)
        {
            return (from e in DBContext.GetQueryableObject<TEntity>()
                    where e.Identificador.Equals(id)
                    select e).FirstOrDefault();
        }

        public IList<TEntity> GetAll()
        {
            return (from e in DBContext.GetQueryableObject<TEntity>()
                    select e).ToList();
        }

        public void Save(TEntity entity)
        {
            DBContext.Save(entity);
        }

        public void Delete(TEntity entity)
        {
            DBContext.Delete(entity);
        }

        public void Delete(TIdentificator id)
        {
            DBContext.Delete<TEntity>(id);
        }

        #endregion
    }
}

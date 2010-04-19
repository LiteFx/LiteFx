using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases
{
    public class RepositoryBase<T, TDBContext> : IRepository<T, TDBContext> 
        where T : EntityBase 
        where TDBContext : IDBContext
    {
        #region IRepository<T,IGerenciadorEventoDB> Members

        public TDBContext DBContext { get; set; }

        public T GetById(int id)
        {
            return (from e in DBContext.GetQueryableObject<T>()
                    where e.Identificador == id
                    select e).FirstOrDefault();
        }

        public IList<T> GetAll()
        {
            return (from e in DBContext.GetQueryableObject<T>()
                    select e).ToList();
        }

        public void Save(T entity)
        {
            DBContext.Save(entity);
        }

        public void Delete(T entity)
        {
            DBContext.Delete(entity);
        }

        public void Delete(int id)
        {
            DBContext.Delete<T>(id);
        }

        #endregion
    }
}

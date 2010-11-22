using System;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;
using System.Linq;

namespace LiteFx.Bases.Context.EntityFramework
{
    /// <summary>
    /// Entity Framework Context Base.
    /// </summary>
    /// <typeparam name="TId">Type of Entity identificador.</typeparam>
    public abstract class EntityFrameworkContextBase<TId> : ObjectContext, IContext<TId>
        where TId : IEquatable<TId>
    {
        public EntityFrameworkContextBase(string connectionString) : base(connectionString) { }

        #region IDBContext Members

        /// <summary>
        /// Flag usado para identificar se há uma transação aberta.
        /// </summary>
        protected bool openTransaction;

        /// <summary>
        /// Variavel que mantem a transação.
        /// </summary>
        protected IDbTransaction transaction;

        /// <summary>
        /// Inicia uma transação e retorna a referência da transação como um IDisposable.
        /// </summary>
        /// <returns>Referência da transação como um IDisposable.</returns>
        public virtual IDisposable BeginTransaction()
        {
            if (openTransaction)
                return transaction;

            if (Connection.State == ConnectionState.Closed)
                Connection.Open();

            transaction = Connection.BeginTransaction();
            openTransaction = true;
            return transaction;
        }

        /// <summary>
        /// Salva as alterações realizadas sobre a transação aberta no banco de dados.
        /// </summary>
        public virtual void CommitTransaction()
        {
            if (!openTransaction)
                throw new Exception("Este método pode ser chamado somente após a chamada do método BeginTransaction.");

            try
            {
                SaveChanges();
                transaction.Commit();
            }
            finally
            {
                transaction.Dispose();
                transaction = null;
                openTransaction = false;
            }
        }

        /// <summary>
        /// Descarta as alterações realizadas sobre a transação aberta.
        /// </summary>
        public virtual void RollBackTransaction()
        {
            if (!openTransaction)
                throw new Exception("Este método pode ser chamado somente após a chamada do método BeginTransaction.");

            transaction.Rollback();
            transaction.Dispose();
            openTransaction = false;
        }

        /// <summary>
        /// Get a queryable object of an especifique entity.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>Queryable object.</returns>
        public virtual IQueryable<T> GetQueryableObject<T>() where T : class
        {
            return CreateObjectSet<T>();
        }

        /// <summary>
        /// Exclui uma entidade do contexto pelo seu Identificador.
        /// </summary>
        /// <typeparam name="T">Tipo do entidade.</typeparam>
        /// <param name="id">Identificador do entidade.</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual T Delete<T>(TId id)
        {
            EntityKey entityKey = new EntityKey(buildEntitySetName(typeof(T).Name), "Id", id);

            T entity = (T)GetObjectByKey(entityKey);

            Delete(entity);

            return entity;
        }

        /// <summary>
        /// Exclui uma entidade do contexto.
        /// </summary>
        /// <param name="entity">Entidade que será exlcuida.</param>
        public virtual void Delete(object entity)
        {
            BeginTransaction();
            DeleteObject(entity);
        }

        private string buildEntitySetName(string entityName) 
        {
            return string.Format("{0}.{1}s", DefaultContainerName, entityName);
        }

        /// <summary>
        /// Salva uma entidade no contexto.
        /// </summary>
        /// <param name="entity">Entidade que será salva.</param>
        public virtual void Save(object entity)
        {
            BeginTransaction();
            if (entity is EntityBase<TId>)
            {
                EntityBase<TId> ent = (EntityBase<TId>)entity;

                if (ent.Id.Equals(default(TId)))
                    
                    AddObject(buildEntitySetName(entity.GetType().Name), entity);
                else
                {
                    AttachTo(buildEntitySetName(entity.GetType().Name), entity);
                    ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                }
            }
        }

        /// <summary>
        /// Remove o objeto do cache do contexto.
        /// </summary>
        /// <param name="entity">Objeto a ser removido do cache.</param>
        public virtual void RemoveFromCache(object entity)
        {
            Detach(entity);
        }

        /// <summary>
        /// Salva as informações alteradas no contexto no banco de dados.
        /// </summary>
        public virtual void SaveContext()
        {
            CommitTransaction();
        }

        #endregion
    }
}
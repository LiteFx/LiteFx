using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace LiteFx.Context.NHibernate
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class NHibernateContextAdapter : IContext
	{
		/// <summary>
		/// Current NHibernate Session.
		/// </summary>
		protected ISession CurrentSession
		{
			get
			{
				return SessionFactoryManager.GetCurrentSession();
			}
		}

		protected SessionFactoryManager SessionFactoryManager { get; set; }

		public NHibernateContextAdapter(SessionFactoryManager sessionFactoryManager)
		{
			SessionFactoryManager = sessionFactoryManager;
		}

		/// <summary>
		/// Get a queryable object of an especifique entity.
		/// </summary>
		/// <typeparam name="T">Entity type.</typeparam>
		/// <returns>Queryable object.</returns>
		public virtual IQueryable<T> GetQueryableObject<T>() where T : class
		{
			return CurrentSession.Query<T>();
		}


		/// <summary>
		/// Exclui uma entidade do contexto.
		/// </summary>
		/// <param name="entity">Entidade que será exlcuida.</param>
		public virtual void Delete(object entity)
		{
			CurrentSession.Delete(entity);
		}

		/// <summary>
		/// Salva uma entidade no contexto.
		/// </summary>
		/// <param name="entity">Entidade que será salva.</param>
		public virtual void Save(object entity)
		{
			CurrentSession.SaveOrUpdate(entity);
		}

		/// <summary>
		/// Remove o objeto do cache do contexto.
		/// </summary>
		/// <param name="entity">Objeto a ser removido do cache.</param>
		public void Detach(object entity)
		{
			CurrentSession.Evict(entity);
		}

		/// <summary>
		/// Salva as informações alteradas no contexto no banco de dados.
		/// </summary>
		public virtual void SaveContext()
		{
			SessionFactoryManager.Current.CommitTransaction();
		}

		/// <summary>
		/// Exclui uma entidade do contexto pelo seu Identificador.
		/// </summary>
		/// <typeparam name="T">Tipo do entidade.</typeparam>
		/// <param name="id">Identificador do entidade.</param>
		public T Delete<T, TId>(TId id) where TId : IEquatable<TId>
		{
			var obj = CurrentSession.Get<T>(id);
			Delete(obj);
			return obj;
		}
	}
}
using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace LiteFx.Context.NHibernate
{
	/// <summary>
	/// NHibernate base context.
	/// </summary>
	/// <typeparam name="TId"></typeparam>
	public abstract class NHibernateContextAdapter<TId> : IContext<TId>
		where TId : IEquatable<TId>
	{
		/// <summary>
		/// Current NHibernate Session.
		/// </summary>
		protected ISession CurrentSession
		{
			get
			{
				return SessionFactoryManager.Current.GetCurrentSession();
			}
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
		/// Exclui uma entidade do contexto pelo seu Identificador.
		/// </summary>
		/// <typeparam name="T">Tipo do entidade.</typeparam>
		/// <param name="id">Identificador do entidade.</param>
		public virtual T Delete<T>(TId id)
		{
			var obj = CurrentSession.Get<T>(id);
			Delete(obj);
			return obj;
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
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace LiteFx.Bases
{
    /// <summary>
    /// Interface que deve ser utilizada na classe que representará o contexto do banco de dados.
    /// </summary>
    /// <typeparam name="TIdentificator">Type of identificator.</typeparam>
    public interface IDBContext<TIdentificator> where TIdentificator : IEquatable<TIdentificator>
    {
        /// <summary>
        /// Get a queryable object of an especifique entity.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>A queryable object.</returns>
        IQueryable<T> GetQueryableObject<T>() where T : EntityBase<TIdentificator>;

        /// <summary>
        /// Reflete as modificações feitas no contexto para a base de dados.
        /// </summary>
        void SaveContext();

        /// <summary>
        /// Salva um objeto no contexto.
        /// </summary>
        /// <param name="entity">Objeto a ser salvo.</param>
        void Save(object entity);

        /// <summary>
        /// Exclui um objeto do contexto.
        /// </summary>
        /// <param name="entity">Objeto a ser excluido.</param>
        void Delete(object entity);

        /// <summary>
        /// Exclui um objeto do contexto pelo seu ID.
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto.</typeparam>
        /// <param name="id">Identificador do objeto.</param>
        T Delete<T>(TIdentificator id);

        /// <summary>
        /// Inicia uma transação no contexto.
        /// </summary>
        /// <returns>Objeto da transação como um IDisposable </returns>
        IDisposable BeginTransaction();

        /// <summary>
        /// Fecha a transação com sucesso. E salva as modificações no banco.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Finaliza a transação com falha. As modificações não são refletidas no banco.
        /// </summary>
        void RollBackTransaction();
    }
}

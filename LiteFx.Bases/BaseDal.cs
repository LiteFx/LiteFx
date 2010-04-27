using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace LiteFx.Bases
{
    /// <summary>
    /// Classe base para implementação de códigos para acesso a dados.
    /// </summary>
    /// <typeparam name="T">Tipo do contexto baseado no Entity Framework.</typeparam>
    public abstract class BaseDal<T> where T : IDisposable
    {
        /// <summary>
        /// Membro privado para o contexto do banco de dados.
        /// </summary>
        private T dbContext;

        /// <summary>
        /// Propriedade para encapsular o contexto.
        /// </summary>
        protected T DBContext
        {
            get { return dbContext; }
        }

        /// <summary>
        /// Construtor base da classe de acesso a dados.
        /// </summary>
        /// <param name="dbContext">Contexto do banco de dados baseado no Entity Framework.</param>
        /// <example>
        /// <code lang="cs" title="Utilizando a BaseDAL">
        /// <![CDATA[
        /// public class ClienteDAL : BaseDAL<MyEntity>
        /// {
        ///     public ClientesDAL(MyEntity dbContext) : base(dbContext)
        ///     {
        ///         //Alguma logica de construção pode ser adicionada aqui
        ///     }
        ///     
        ///     public Cliente GetCliente(int ideCli)
        ///     {
        ///         return DbContext.Cliente.First(c => c.IdeCli == ideCli);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        protected BaseDal(T dbContext)
        {
            this.dbContext = dbContext;

            /*IUnityContainer uc = new UnityContainer();
            uc.AddNewExtension<Interception>()
                .RegisterInstance<;*/
        }
    }
}
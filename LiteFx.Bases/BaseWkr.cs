using System;

namespace LiteFx.Bases
{

    /// <summary>
    /// Classe base para implementação de códigos de integração.
    /// </summary>
    /// <typeparam name="T">Tipo do contexto baseado no NHibernate.</typeparam>
    /// <typeparam name="TId">Type of Id.</typeparam>
    public abstract class BaseWkr<T, TId> : IDisposable 
        where T : class, IContext<TId>, IDisposable, new()
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        private bool disposed;

        /// <summary>
        /// Propriedade para encapsular o contexto.
        /// </summary>
        protected T DBContext { get; private set; }

        /// <summary>
        /// Constroi um objeto Worker.
        /// </summary>
        /// <example>
        /// <code lang="cs" title="Utilizando a BaseWKR">
        /// <![CDATA[
        /// //Como implementar uma classe Worker (WRK)
        /// public class ClienteWKR : BaseWKR<MyEntity>, IDisposable
        /// {
        ///     public Cliente GetCliente(int ideCli)
        ///     {
        ///         return (new ClienteBLL(DbContext)).GetCliente(ideCli);
        ///     }
        ///     
        ///     public void Dispose()
        ///     {
        ///         base.Dispose();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// <code lang="cs" title="Liberando recursos">
        /// <![CDATA[
        /// //Melhor pratica para liberar recursos o mais breve possivel.
        /// using(ClienteWKR cliWkr = new  ClienteWKR())
        /// {
        ///     Cliente cli = cliWkr.GetCliente(ideCli);
        ///     // Seu codigo vem aqui.
        /// } //Neste ponto o objeto criado na clausula using será liberado da memoria.
        /// ]]>
        /// </code>
        /// </example>
        protected BaseWkr()
        {
            DBContext = new T();
        }

        /// <summary>
        /// Salva as informações do contexto na base de dados.
        /// </summary>
        protected void SalvarContexto()
        {
            DBContext.SaveContext();
        }

        /// <summary>
        /// Inicia uma transação no contexto.
        /// </summary>
        /// <returns>Objeto da transação como um IDisposable </returns>
        protected IDisposable BeginTransaction() 
        {
            return DBContext.BeginTransaction();
        }

        /// <summary>
        /// Fecha a transação com sucesso. E salva as modificações no banco.
        /// </summary>
        protected void CommitTransaction() 
        {
            DBContext.CommitTransaction();
        }

        /// <summary>
        /// Finaliza a transação com falha. As modificações não são refletidas no banco.
        /// </summary>
        protected void RollBackTransaction() 
        {
            DBContext.RollBackTransaction();
        }


        #region Dispose pattern implementation
        /// <summary>
        /// Libera todos os recursos utilizados pela classe.
        /// Implementação do Dispose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        /// <param name="disposing">Usado para verificar se a chamada esta sendo feita pelo <see cref="GC"/> ou pela aplicação.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
                if (DBContext != null)
                    DBContext.Dispose();

            disposed = true;
        }

        /// <summary>
        /// Chamado pelo <see ref="GC" /> para liberar recursos que não estão sendo utilizados.
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        ~BaseWkr()
        {
            Dispose(false);
        }

        #region IDisposable Members

        /// <summary>
        /// Libera todos os recursos utilizados pela classe.
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        /// <example>
        /// <code lang="cs" title="Utilizando a BaseWKR">
        /// <![CDATA[
        /// //Como implementar uma classe Worker (WRK)
        /// public class ClienteWKR : BaseWKR<MyEntity>, IDisposable
        /// {
        ///     public void Dispose()
        ///     {
        ///         base.Dispose();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// <code lang="cs" title="Liberando recursos">
        /// <![CDATA[
        /// //Melhor pratica para liberar recursos o mais breve possivel.
        /// using(ClienteWKR cli = new  ClienteWKR())
        /// {
        ///     // Seu codigo vem aqui.
        /// } //Neste ponto o objeto criado na clausula using será liberado da memoria.
        /// ]]>
        /// </code>
        /// </example>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion
    }
}

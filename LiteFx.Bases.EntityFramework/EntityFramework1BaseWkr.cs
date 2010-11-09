using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Objects;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace LiteFx.Bases
{
    /// <summary>
    /// Classe base para implementação de códigos de integração.
    /// </summary>
    /// <typeparam name="T">Tipo do contexto baseado no Entity Framework.</typeparam>
    public abstract class BaseWkr<T> : IDisposable where T : ObjectContext, IDisposable
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
        /// Container para a criação do contexto.
        /// </summary>
        private UnityContainer container;

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
        /// <code lang="xml" title="Configurando o LiteFx.Bases">
        /// <![CDATA[
        /// <?xml version="1.0" encoding="utf-8" ?>
        /// <configuration>
        ///    <configSections>
        ///         <section name="entityConfig" type="Microsoft.Practices.Unity.Configuration.UnityConfigurationSection, Microsoft.Practices.Unity.Configuration, Version=1.2.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
        ///     </configSections>
        ///     <entityConfig>
        ///         <typeAliases>
        ///             <typeAlias alias="external" type="Microsoft.Practices.Unity.ContainerControlledLifetimeManager, Microsoft.Practices.Unity" />
        ///             <typeAlias alias="MyEntities" type="My.Data.MyEntities, My.Data" />
        ///         </typeAliases>
        ///         <containers>
        ///             <container name="entityModel">
        ///                 <types>
        ///                     <type type="MyEntities">
        ///                         <lifetime type="external" />
        ///                         <typeConfig extensionType="Microsoft.Practices.Unity.Configuration.TypeInjectionElement, Microsoft.Practices.Unity.Configuration">
        ///                             <constructor>
        ///                                 <param name="connectionString" parameterType="System.String">
        ///                                     <value value="name=MyEntitiesSQL"/>
        ///                                 </param>
        ///                             </constructor>
        ///                          </typeConfig>
        ///                      </type>
        ///                  </types>
        ///              </container>
        ///          </containers>
        ///      </entityConfig>
	    ///    <connectionStrings>
		///       <add name="MyEntitiesSQL" connectionString="metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=DOUGLASNOTE\SQLEXPRESS;Initial Catalog=School;Persist Security Info=True;User ID=sa;Password=sa;MultipleActiveResultSets=True&quot;" providerName="System.Data.EntityClient" />
		///       <add name="MyEntitiesOra" connectionString="metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=DOUGLASNOTE\SQLEXPRESS;Initial Catalog=School;Persist Security Info=True;User ID=sa;Password=sa;MultipleActiveResultSets=True&quot;" providerName="System.Data.EntityClient" />
        ///    </connectionStrings>
        /// </configuration>
        /// ]]>
        /// </code>
        /// </example>
        protected BaseWkr() 
        {
                container = new UnityContainer();

                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("entityConfig");
                section.Containers["entityModel"].Configure(container);

                this.DBContext = container.Resolve<T>();
                this.DBContext.SavingChanges += new EventHandler(dbContext_SavingChanges);
        }

        /// <summary>
        /// Antes de salvar os dados no banco a coluna UPDTME de todos os registros a serem salvos e setada para DateTime.Now.
        /// </summary>
        /// <param name="sender">Objeto chamador do evento.</param>
        /// <param name="e">argumentos do evento.</param>
        void dbContext_SavingChanges(object sender, EventArgs e)
        {
            ObjectContext context = (ObjectContext)sender;
            IEnumerable<ObjectStateEntry> changes = context.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added | System.Data.EntityState.Modified);

            foreach (ObjectStateEntry entry in changes) 
            {
                if (entry.EntityKey != null)
                {
                    int ordinal = entry.CurrentValues.GetOrdinal("UPDTME");
                    entry.CurrentValues.SetDateTime(ordinal, DateTime.Now);
                }
            }
        }

        /// <summary>
        /// Salva as informações do contexto na base de dados.
        /// </summary>
        protected void SalvarContexto() 
        {
            try
            {
                DBContext.SaveChanges();
            }
            catch (System.Data.UpdateException uex) 
            {
                throw new BusinessException("Não foi possivel excluir o registro.", uex);
            }
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
            if (!disposed) 
            {
                if (disposing)
                {
                    if (DBContext != null)
                    {
                        DBContext.Dispose();
                    }

                    container.Dispose();
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Chamado pelo <see ref="GC" /> para liberar recursos que não estão sendo utilizados.
        /// Implementação do Dipose Pattern.
        /// </summary>
        /// <remarks><a target="blank" href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Dispose Pattern</a>.</remarks>
        ~BaseWkr()
        {
            this.Dispose(false);
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
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion
    }
}
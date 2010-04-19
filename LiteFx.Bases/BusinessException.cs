using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Security.Permissions;

namespace LiteFx.Bases
{
    /// <summary>
    /// Esta classe deve ser utilizada para repassar exceções de regras de negócio.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
    [Serializable]
    public class BusinessException : System.Exception
    {
        private ValidationResults validationResults;

        /// <summary>
        /// Resultados dos erros encontrados na validação de um objeto.
        /// </summary>
        public ValidationResults ValidationResults
        {
            get { return validationResults; }
        }

        /// <summary>
        /// Constroi um BusinessException baseado nos resultados da validação.
        /// </summary>
        /// <param name="validationResults">Resultados da validação. O membro validationResults.IsValid precisa ser FALSO.</param>
        /// <example>
        /// <code lang="cs" title="Utilizando a BusinessException com o Validation Application Block">
        /// <![CDATA[
        /// public class ClienteBLL : LiteFx.Bases.BaseBLL<MyEntity>
        /// {
        ///     public ClienteBLL(MyEntity dbContext) : base(dbContext) { }
        /// 
        ///     public int Update(Cliente cliente) 
        ///     {
        ///         try
        ///         {
        ///             ValidationResults results = Validation.Validate<Cliente>(cliente);
        /// 
        ///             if (!results.IsValid)
        ///                 throw new BusinessException(results);
        /// 
        ///             //Código para salvar o registro
        ///         }
        ///         catch (Exception ex)
        ///         {
        ///             throw ex;
        ///         }
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public BusinessException(ValidationResults validationResults) : base(string.Empty)
        {
            //Nao há razao para repassar uma excecao se os resultados da validacao foram positivos
            if (validationResults.IsValid)
            {
                throw new ArgumentException(Properties.Resources.ParaCriarUmaBusinessExceptionOValidationResultsPrecisaEstarInvalido);
            }
            this.validationResults = validationResults;
        }

        /// <summary>
        /// Constroi um BusinessException baseado em uma mensagem.
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exceção.</param>
        /// <example>
        /// <code lang="cs" title="Utilizando a BusinessException">
        /// <![CDATA[
        /// public class ClienteBLL : LiteFx.Bases.BaseBLL<MyEntity>
        /// {
        ///     public ClienteBLL(MyEntity dbContext) : base(dbContext) { }
        /// 
        ///     public int Update(Cliente cliente) 
        ///     {
        ///         try
        ///         {
        ///             //Alguma validação que gere um exceção de regra de negócio
        ///             throw new BusinessException(Porperties.Resources.MyMessage);
        ///         }
        ///         catch (Exception ex)
        ///         {
        ///             throw ex;
        ///         }
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public BusinessException(string message) : base(message) { }

        /// <summary>
        /// Constroi um BusinessException baseado em uma mensagem e em uma exceção anterior.
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exceção.</param>
        /// <param name="innerException">Exceção que causou a exceção atual.</param>
        /// <example>
        /// <code lang="cs" title="Utilizando a BusinessException com Inner Exception">
        /// <![CDATA[
        /// public class ClienteBLL : LiteFx.Bases.BaseBLL<MyEntity>
        /// {
        ///     public ClienteBLL(MyEntity dbContext) : base(dbContext) { }
        /// 
        ///     public void Foo() 
        ///     {
        ///         try
        ///         {
        ///             //Algum código
        ///         }
        ///         catch (Exception ex)
        ///         {
        ///             throw new BusinessException(Porperties.Resources.MyMessage, ex);
        ///         }
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public BusinessException(string message, System.Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Adiciona uma mensagem de erro nos resultados da validação.
        /// </summary>
        /// <param name="mensagem">Mensagem de erro.</param>
        /// <param name="key">Chave.</param>
        public void AdicionarResultadoDeErro(string mensagem, string key)
        {
            if (validationResults == null)
                validationResults = new ValidationResults();

            validationResults.AddResult(new ValidationResult(mensagem, null, key, key, null));
        }
    }
}

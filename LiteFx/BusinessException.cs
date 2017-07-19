using LiteFx.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using System.Text;

namespace LiteFx
{
	/// <summary>
	/// Esta classe deve ser utilizada para repassar exceções de regras de negócio.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
	[Serializable]
	public class BusinessException : Exception
	{
		/// <summary>
		/// Resultados dos erros encontrados na validação de um objeto.
		/// </summary>
		public IList<ValidationResult> ValidationResults { get; private set; }

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
		public BusinessException(IList<ValidationResult> validationResults)
			: base(ResourceHelper.GetString("SomeBusinessRulesWasViolated"))
		{
			//Nao há razao para repassar uma excecao se os resultados da validacao foram positivos
			if (validationResults.Count == 0)
				throw new ArgumentException(ResourceHelper.GetString("ParaCriarUmaBusinessExceptionOValidationResultsPrecisaEstarInvalido"));

			ValidationResults = validationResults;
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
		///             throw new BusinessException(ResourceHelper.GetString("MyMessage"));
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
		public BusinessException(string message)
			: base(message)
		{
			ValidationResults = new List<ValidationResult>();
		}

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
		///             throw new BusinessException(ResourceHelper.GetString("MyMessage"), ex);
		///         }
		///     }
		/// }
		/// ]]>
		/// </code>
		/// </example>
		public BusinessException(string message, Exception innerException)
			: base(message, innerException)
		{
			ValidationResults = new List<ValidationResult>();
		}

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
			if (ValidationResults == null)
				ValidationResults = new List<ValidationResult>();

			ValidationResults.Add(new ValidationResult(mensagem, new string[] { key }));
		}

		public override string Message
		{
			get
			{
				var errorStringBuilder = new StringBuilder();

				foreach (var businessRuleViolation in ValidationResults)
				{
					errorStringBuilder.AppendFormat("{0} : {1}{2}", businessRuleViolation.MemberNames, businessRuleViolation.ErrorMessage, Environment.NewLine);
				}

				return base.Message + (errorStringBuilder.Length > 0 ? Environment.NewLine + errorStringBuilder : string.Empty);
			}
		}
	}
}

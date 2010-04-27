using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using LiteFx.Bases.Repository;

namespace LiteFx.Bases.Validation
{
    public static class ValidationHelper
    {
        #region Validações

        /// <summary>
        /// Verifica se uma expressão é valida.
        /// </summary>
        /// <param name="valorVerificacao">Valor a ser verificado.</param>
        /// <param name="expressao">Expressão a ser verificada. Se o resultado da expresão for true um resultado de validação é criado na coleção de resultados.</param>
        /// <param name="mensagem">Mensagem para caso o valor seja inválido.</param>
        /// <param name="key">Chave do campo que esta sendo validado.</param>
        /// <example>
        /// <code lang="cs" title="Utilizando a BaseBLL">
        /// <![CDATA[
        /// public class Cliente : BaseBLL<MyEntity>
        /// {
        ///     public void ValidaCliente(Cliente cliente)
        ///     {
        ///         
        ///         Verificar<Cliente>(cliente, 
        ///                          cli => cli.PessoaFisica && (cli.Idade < 18 && cli.Idade > 120), 
        ///                          Resources.IdadeInvalida, "idade");
        ///         
        ///         // Se os resultados não forem válidos uma exceção de negócio é disparada.
        ///         ValidarResultados();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static Assert IsSatisfied<TVal>(this Assert validation, TVal valorVerificacao, Func<TVal, bool> expressao, string mensagem, string key)
        {
            if (expressao(valorVerificacao))
            {
                validation.AddValidationResult(mensagem, key);
                validation.LastAssertionIsValid = false;
            }
            validation.LastAssertionIsValid = true;
            return validation;
        }

        /// <summary>
        /// Verifica se o valor ou a expressão booleana é verdadeira e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <param name="valorOuExpressaoBooleano"></param>
        /// <param name="mensagem"></param>
        /// <param name="key"></param>
        public static Assert IsTrue(this Assert validation, bool valorOuExpressaoBooleano, string mensagem, string key)
        {
            if (valorOuExpressaoBooleano)
            {
                validation.AddValidationResult(mensagem, key);
                validation.LastAssertionIsValid = false;
            }
            validation.LastAssertionIsValid = true;
            return validation;
        }

        /// <summary>
        /// Verifica se o valor ou a expressão booleana é falsa e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <param name="valorOuExpressaoBooleano"></param>
        /// <param name="mensagem"></param>
        /// <param name="key"></param>
        public static Assert IsFalse(this Assert validation, bool valorOuExpressaoBooleano, string mensagem, string key)
        {
            if (!valorOuExpressaoBooleano)
            {
                validation.AddValidationResult(mensagem, key);
                validation.LastAssertionIsValid = false;
            }
            validation.LastAssertionIsValid = true;
            return validation;
        }

        /// <summary>
        /// Verifica se o valor esperado e o valor atual não são iguais e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="esperado"></param>
        /// <param name="atual"></param>
        /// <param name="mensagem"></param>
        /// <param name="key"></param>
        public static Assert AreEqual<TVal>(this Assert validation, TVal esperado, TVal atual, string mensagem, string key)
        {
            if (!esperado.Equals(atual))
            {
                validation.AddValidationResult(mensagem, key);
                validation.LastAssertionIsValid = false;
            }
            validation.LastAssertionIsValid = true;
            return validation;
        }

        /// <summary>
        /// Verifica se o valor esperado e o valor atual são iguais e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="esperado"></param>
        /// <param name="atual"></param>
        /// <param name="mensagem"></param>
        /// <param name="key"></param>
        public static Assert AreNotEqual<TVal>(this Assert validation, TVal esperado, TVal atual, string mensagem, string key)
        {
            if (esperado.Equals(atual))
            {
                validation.AddValidationResult(mensagem, key);
                validation.LastAssertionIsValid = false;
            }
            validation.LastAssertionIsValid = true;
            return validation;
        }

        /// <summary>
        /// Verifica se o valor é nulo e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <param name="valor">Valor que será verificado.</param>
        /// <param name="mensagem">Mensagem para caso a verificação falhe.</param>
        /// <param name="key">Chave de verificação.</param>
        public static Assert IsNull(this Assert validation, object valor, string mensagem, string key)
        {
            if (valor == null)
            {
                validation.AddValidationResult(mensagem, key);
                validation.LastAssertionIsValid = false;
            }
            validation.LastAssertionIsValid = true;
            return validation;
        }

        /// <summary>
        /// Verifica se o valor não é nulo e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <param name="valor">Valor a ser verificado.</param>
        /// <param name="mensagem">Mensagem para caso a verificação falhe.</param>
        /// <param name="key">Chave de verificação.</param>
        public static Assert IsNotNull(this Assert validation, object valor, string mensagem, string key)
        {
            if (valor != null)
            {
                validation.AddValidationResult(mensagem, key);
                validation.LastAssertionIsValid = false;
            }
            validation.LastAssertionIsValid = true;
            return validation;
        }

        /// <summary>
        /// Verifica se é um valor valido.
        /// </summary>
        /// <param name="valorVerificacao">Valor a ser verificado.</param>
        /// <param name="mensagem">Mensagem para caso o valor seja inválido.</param>
        /// <param name="key">Chave do campo que esta sendo validado.</param>
        /// <example>
        /// <code lang="cs" title="Utilizando a BaseBLL">
        /// <![CDATA[
        /// public class ClienteBLL : BaseBLL<MyEntity>
        /// {
        ///     public void ValidaCliente(Cliente cliente)
        ///     {
        ///         VerificarStringNulaOuVazia(cliente.Nome, Properties.Resources.InformeNome, "nome");
        ///         
        ///         // Se os resultados não forem válidos uma exceção de negócio é disparada.
        ///         ValidarResultados();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static Assert IsNullOrEmpty(this Assert validation, string valorVerificacao, string mensagem, string key)
        {
            return validation.IsSatisfied(valorVerificacao, x => string.IsNullOrEmpty(x), mensagem, key);
        }

        /// <summary>
        /// Verifica se é um valor valido.
        /// </summary>
        /// <param name="valorVerificacao">Valor a ser verificado.</param>
        /// <param name="mensagem">Mensagem para caso o valor seja inválido.</param>
        /// <param name="key">Chave do campo que esta sendo validado.</param>
        public static Assert IsEqualOrLessThanZero(this Assert validation, int valorVerificacao, string mensagem, string key)
        {
            return validation.IsSatisfied(valorVerificacao, x => x <= 0, mensagem, key);
        }

        /// <summary>
        /// Verifica se é um valor valido.
        /// </summary>
        /// <param name="valorVerificacao">Valor a ser verificado.</param>
        /// <param name="mensagem">Mensagem para caso o valor seja inválido.</param>
        /// <param name="key">Chave do campo que esta sendo validado.</param>
        public static Assert IsEqualOrLessThanZero(this Assert validation, long valorVerificacao, string mensagem, string key)
        {
            return validation.IsSatisfied(valorVerificacao, x => x <= 0, mensagem, key);
        }

        /// <summary>
        /// Verifica se é um valor valido.
        /// </summary>
        /// <param name="valorVerificacao">Valor a ser verificado.</param>
        /// <param name="mensagem">Mensagem para caso o valor seja inválido.</param>
        /// <param name="key">Chave do campo que esta sendo validado.</param>
        public static Assert IsEqualOrLessThanZero(this Assert validation, decimal valorVerificacao, string mensagem, string key)
        {
            return validation.IsSatisfied(valorVerificacao, x => x <= 0, mensagem, key);
        }

        public static Assert IsSatisfiedBySpecification<T>(this Assert validation, T entity, ISpecification<T> specification, string mensagem, string key)
        {
            validation.LastAssertionIsValid = specification.IsSatisfiedBy(entity);
            return validation;
        }
        #endregion
    }
}

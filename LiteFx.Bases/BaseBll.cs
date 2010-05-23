using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using LiteFx.Bases.Validation;

namespace LiteFx.Bases
{
    /// <summary>
    /// Classe base para implementação de códigos de regras de negócio.
    /// </summary>
    /// <typeparam name="T">Tipo do contexto baseado no Entity Framework.</typeparam>
    public abstract class BaseBll<T>
        where T : IDisposable
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
        /// Construtor base da classe de regras de negócio.
        /// </summary>
        /// <param name="dbContext">Contexto do banco de dados baseado no Entity Framework.</param>
        /// <example>
        /// <code lang="cs" title="Utilizando a BaseBLL">
        /// <![CDATA[
        /// public class ClienteBLL : BaseBLL<MyEntity>
        /// {
        ///     public ClientesBLL(MyEntity dbContext) : base(dbContext)
        ///     {
        ///         //Alguma logica de construção pode ser adicionada aqui
        ///     }
        ///     
        ///     public Cliente GetCliente(int ideCli)
        ///     {
        ///         return (new ClienteBLL(DbContext)).GetCliente(ideCli);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        protected BaseBll(T dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Membro privado para os resultados das validações.
        /// </summary>
        private ValidationResults validationResults;

        /// <summary>
        /// Resultados das validações realizadas na Bll.
        /// </summary>
        public ValidationResults Results
        {
            get
            {
                //Se o validationResults estiver nulo cria uma nova instância.
                if (validationResults == null)
                    validationResults = new ValidationResults();
                return validationResults;
            }
        }

        /// <summary>
        /// Adiciona uma mensagem de erro nos resultados da validação.
        /// </summary>
        /// <param name="mensagem">Mensagem de erro.</param>
        /// <param name="key">Chave.</param>
        public void AddValidationResult(string mensagem, string key)
        {
            Results.AddResult(new ValidationResult(mensagem, null, key, key, null));
        }

        /// <summary>
        /// Adiciona uma mensagem de erro nos resultados da validação.
        /// </summary>
        /// <param name="mensagem">Mensagem de erro.</param>
        /// <param name="key">Chave.</param>
        public void AdicionarResultadoDeErro(string mensagem, string key)
        {
            AddValidationResult(mensagem, key);
        }

        /// <summary>
        /// Verifia se o membro Results do tipo ValidationResults esta inválido e dispara uma BusinessException.
        /// Se o membro Results do tipo ValidationResults estiver válido nada acontece.
        /// </summary>
        public void Validate()
        {
            if (!Results.IsValid)
                throw new BusinessException(Results);
        }

        /// <summary>
        /// Verifia se o membro Results do tipo ValidationResults esta inválido e dispara uma BusinessException.
        /// Se o membro Results do tipo ValidationResults estiver válido nada acontece.
        /// </summary>
        public void ValidarResultados()
        {
            Validate();
        }

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
        public bool Verificar<TVal>(TVal valorVerificacao, Func<TVal, bool> expressao, string mensagem, string key)
        {
            if (expressao(valorVerificacao))
            {
                AddValidationResult(mensagem, key);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se o valor ou a expressão booleana é verdadeira e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <param name="valorOuExpressaoBooleano"></param>
        /// <param name="mensagem"></param>
        /// <param name="key"></param>
        public bool VerificarSeEVerdadeiro(bool valorOuExpressaoBooleano, string mensagem, string key)
        {
            if (valorOuExpressaoBooleano)
            {
                AddValidationResult(mensagem, key);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se o valor ou a expressão booleana é falsa e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <param name="valorOuExpressaoBooleano"></param>
        /// <param name="mensagem"></param>
        /// <param name="key"></param>
        public bool VerificarSeEFalso(bool valorOuExpressaoBooleano, string mensagem, string key)
        {
            if (!valorOuExpressaoBooleano)
            {
                AddValidationResult(mensagem, key);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se o valor esperado e o valor atual não são iguais e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="esperado"></param>
        /// <param name="atual"></param>
        /// <param name="mensagem"></param>
        /// <param name="key"></param>
        public bool VerificarSeNaoSaoIguais<TVal>(TVal esperado, TVal atual, string mensagem, string key)
        {
            if (!esperado.Equals(atual))
            {
                AddValidationResult(mensagem, key);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se o valor esperado e o valor atual são iguais e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="esperado"></param>
        /// <param name="atual"></param>
        /// <param name="mensagem"></param>
        /// <param name="key"></param>
        public bool VerificarSeSaoIguais<TVal>(TVal esperado, TVal atual, string mensagem, string key)
        {
            if (esperado.Equals(atual))
            {
                AddValidationResult(mensagem, key);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se o valor é nulo e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <param name="valor">Valor que será verificado.</param>
        /// <param name="mensagem">Mensagem para caso a verificação falhe.</param>
        /// <param name="key">Chave de verificação.</param>
        public bool VerificarSeENulo(object valor, string mensagem, string key)
        {
            if (valor == null)
            {
                AddValidationResult(mensagem, key);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se o valor não é nulo e adiciona um erro nos resultados da validação.
        /// </summary>
        /// <param name="valor">Valor a ser verificado.</param>
        /// <param name="mensagem">Mensagem para caso a verificação falhe.</param>
        /// <param name="key">Chave de verificação.</param>
        public bool VerificarSeNaoENulo(object valor, string mensagem, string key)
        {
            if (valor != null)
            {
                AddValidationResult(mensagem, key);
                return false;
            }
            return true;
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
        public bool VerificarStringNulaOuVazia(string valorVerificacao, string mensagem, string key)
        {
            return Verificar(valorVerificacao, x => string.IsNullOrEmpty(x), mensagem, key);
        }

        /// <summary>
        /// Verifica se é um valor valido.
        /// </summary>
        /// <param name="valorVerificacao">Valor a ser verificado.</param>
        /// <param name="mensagem">Mensagem para caso o valor seja inválido.</param>
        /// <param name="key">Chave do campo que esta sendo validado.</param>
        public bool VerificarIntMenorIgualAZero(int valorVerificacao, string mensagem, string key)
        {
            return Verificar(valorVerificacao, x => x <= 0, mensagem, key);
        }

        /// <summary>
        /// Verifica se é um valor valido.
        /// </summary>
        /// <param name="valorVerificacao">Valor a ser verificado.</param>
        /// <param name="mensagem">Mensagem para caso o valor seja inválido.</param>
        /// <param name="key">Chave do campo que esta sendo validado.</param>
        public bool VerificarInt64MenorIgualAZero(long valorVerificacao, string mensagem, string key)
        {
            return Verificar(valorVerificacao, x => x <= 0, mensagem, key);
        }

        /// <summary>
        /// Verifica se é um valor valido.
        /// </summary>
        /// <param name="valorVerificacao">Valor a ser verificado.</param>
        /// <param name="mensagem">Mensagem para caso o valor seja inválido.</param>
        /// <param name="key">Chave do campo que esta sendo validado.</param>
        public bool VerificarDecimalMenorIgualAZero(decimal valorVerificacao, string mensagem, string key)
        {
            return Verificar(valorVerificacao, x => x <= 0, mensagem, key);
        }
        #endregion

    }
}
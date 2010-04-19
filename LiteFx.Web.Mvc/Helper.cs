using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using LiteFx.Bases;
using System.Collections;

namespace LiteFx.Web.Mvc
{
    /// <summary>
    /// Esta classe comportará métodos de auxilio na camada MVC da aplicação.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Metodo helper para montar o combo com base em um dicionario de valor
        /// </summary>
        /// <typeparam name="TKey">Chave que vai ser passada como parte do combo</typeparam>
        /// <typeparam name="TVal">Valor que vai ser passado para exibicao no combo</typeparam>
        /// <param name="data">Colecao de valores para serem tensformados propriamente para o combo</param>
        /// <returns></returns>
        public static SelectList RetornaCombo<TKey, TVal>(IDictionary<TKey, TVal> data)
        {
            SelectList lista = new SelectList(data, "Key", "Value");
            return lista;
        }

        /// <summary>
        /// Metodo helper para montar o combo com base em um dicionario de valor
        /// </summary>
        /// <typeparam name="TKey">Chave que vai ser passada como parte do combo</typeparam>
        /// <typeparam name="TVal">Valor que vai ser passado para exibicao no combo</typeparam>
        /// <param name="data">Colecao de valores para serem tensformados propriamente para o combo</param>
        /// <param name="selectedValue">Valor selecionado no combo.</param>
        /// <returns></returns>
        public static SelectList RetornaCombo<TKey, TVal>(IDictionary<TKey, TVal> data, object selectedValue)
        {
            SelectList lista = new SelectList(data, "Key", "Value", selectedValue);
            return lista;
        }

        /// <summary>
        /// Metodo helper para montar o listBox com base em um dicionario
        /// </summary>
        /// <typeparam name="TKey">Chave que vai ser passada como parte do combo</typeparam>
        /// <typeparam name="TVal">Valor que vai ser passado para exibicao no combo</typeparam>
        /// <param name="data">Colecao de valores que serão exibidos no listBox</param>
        /// <returns></returns>
        public static MultiSelectList RetornaMultiSelectList<TKey, TVal>(IDictionary<TKey, TVal> data)
        {
            return new MultiSelectList(data, "Key", "Value");
        }

        /// <summary>
        /// Metodo helper para montar o listBox com base em um dicionario
        /// </summary>
        /// <typeparam name="TKey">Chave que vai ser passada como parte do combo</typeparam>
        /// <typeparam name="TVal">Valor que vai ser passado para exibicao no combo</typeparam>
        /// <param name="data">Colecao de valores que serão exibidos no listBox</param>
        /// <param name="selectedValues">Valores selecionados.</param>
        /// <returns></returns>
        public static MultiSelectList RetornaMultiSelectList<TKey, TVal>(IDictionary<TKey, TVal> data, IEnumerable selectedValues)
        {
            return new MultiSelectList(data, "Key", "Value", selectedValues);
        }

        /// <summary>
        /// Transfere os erros encontrados na camada de negocio (Bll) para o ModelState.
        /// </summary>
        /// <param name="modelState">Estado do modelo.</param>
        /// <param name="ex">Exceção de negócio.</param>
        public static void TransferirErros(this ModelStateDictionary modelState, BusinessException ex)
        {
            if(!string.IsNullOrEmpty(ex.Message))
            {
                modelState.AddModelError(string.Empty, ex.Message);
            }

            if (ex.ValidationResults != null)
            {
                foreach (var erro in ex.ValidationResults)
                {
                    modelState.AddModelError(erro.Key, erro.Message);
                }
            }
        }

        /// <summary>
        /// Transfere os erros encontrados na camada de negocio (Bll) para o ModelState.
        /// </summary>
        /// <param name="modelState">Estado do modelo.</param>
        /// <param name="ex">Exceção de negócio.</param>
        /// <pparam name="valueProvider">Valores informados pelo usuário.</pparam>
        public static void TransferirErros(this ModelStateDictionary modelState, BusinessException ex, IDictionary<string, ValueProviderResult> valueProvider)
        {
            if (!string.IsNullOrEmpty(ex.Message))
            {
                modelState.AddModelError(string.Empty, ex.Message);
            }

            if (ex.ValidationResults != null)
            {
                foreach (var erro in ex.ValidationResults)
                {
                    modelState.AddModelError(erro.Key, erro.Message);
                    modelState.SetModelValue(erro.Key, valueProvider[erro.Key]);
                }
            }
        }
    }
}

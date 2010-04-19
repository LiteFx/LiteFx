using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Web.Mvc.Models
{
    /// <summary>
    /// View default para telas Adição/Alteração.
    /// </summary>
    /// <typeparam name="T">Entidade.</typeparam>
    /// <typeparam name="S">Tipo do settings.</typeparam>
    public class BaseViewModel<T, S> : ISecurity
    {
        /// <summary>
        /// Modelo
        /// </summary>
        public T EntityModel { get; set; }
        /// <summary>
        /// Identificador do Parent do Modelo
        /// </summary>
        public S Setting { get; set; }
        /// <summary>
        /// Filtro Corrente
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// Coluna para ordenação
        /// </summary>
        public string Column { get; set; }
        /// <summary>
        /// Sentido da Ordenação
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// Action do Form
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// Flag para permissão de alteração na View
        /// </summary>
        public bool AccessDenied { get; set; }
        /// <summary>
        /// Coluna Principal da Listagem
        /// </summary>
        public string PrincipalColumn { get; set; }
        /// <summary>
        /// Mensagem para a View
        /// </summary>
        public string Message { get; set; }
    }
}

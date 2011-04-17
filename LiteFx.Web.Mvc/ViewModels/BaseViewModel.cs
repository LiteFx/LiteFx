
namespace LiteFx.Web.Mvc.ViewModels
{
    /// <summary>
    /// View default para telas Adição/Alteração.
    /// </summary>
    /// <typeparam name="T">Entidade.</typeparam>
    /// <typeparam name="TSetting">Tipo do settings.</typeparam>
    public class BaseViewModel<T, TSetting>
    {
        /// <summary>
        /// Modelo
        /// </summary>
        public T EntityModel { get; set; }
        /// <summary>
        /// Identificador do Parent do Modelo
        /// </summary>
        public TSetting Setting { get; set; }
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
        /// Coluna Principal da Listagem
        /// </summary>
        public string PrincipalColumn { get; set; }
        /// <summary>
        /// Mensagem para a View
        /// </summary>
        public string Message { get; set; }
    }
}

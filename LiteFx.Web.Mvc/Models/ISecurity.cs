
namespace LiteFx.Web.Mvc.Models
{
    /// <summary>
    /// Interface que deve ser implementada por Models que contenham informações sobre segurança.
    /// </summary>
    public interface ISecurity
    {
        /// <summary>
        /// Flag para permissão de alteração na View
        /// </summary>
        bool AccessDenied { get; set; }
    }
}

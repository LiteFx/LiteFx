using System.Collections.Generic;

namespace LiteFx.Web.Mvc.ViewModels
{
    /// <summary>
    /// View default para telas Listagem. 
    /// </summary>
    /// <typeparam name="T">Entidade.</typeparam>
    /// <typeparam name="S">Tipo do settings.</typeparam>
    public class BaseListViewModel<T, S> : BaseViewModel<IEnumerable<T>, S>
    {
    }
}

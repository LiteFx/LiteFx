using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Web.Mvc.Models
{
    /// <summary>
    /// View default para telas Listagem. 
    /// </summary>
    /// <typeparam name="T">Entidade</typeparam>
    public class DefaultListViewModel<T> : BaseListViewModel<T, long?>
    {
    }
}

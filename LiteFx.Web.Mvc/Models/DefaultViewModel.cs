using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Web.Mvc.Models
{
    /// <summary>
    /// View default para telas Adição/Alteração. 
    /// </summary>
    /// <typeparam name="T">Entidade</typeparam>
    public class DefaultViewModel<T> : BaseViewModel<T, long?>
    {
    }
}
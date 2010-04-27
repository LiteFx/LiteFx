using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases
{
    /// <summary>
    /// To be used when a entity needs to be unique identified.
    /// </summary>
    public interface IIdentificator<T>
    {
        /// <summary>
        /// Entity unique identifier.
        /// </summary>
        T Identificator { get; set; }
    }
}

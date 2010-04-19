using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases
{
    /// <summary>
    /// To be used when a object needs to bee unique identified.
    /// </summary>
    public interface IIdentificator
    {
        /// <summary>
        /// Object unique identifier.
        /// </summary>
        long Identificator { get; set; }
    }
}

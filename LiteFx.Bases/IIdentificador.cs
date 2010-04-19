using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases
{
    /// <summary>
    /// To be used when a object needs to bee unique identified.
    /// </summary>
    public interface IIdentificador
    {
        long Identificador { get; set; }
    }
}

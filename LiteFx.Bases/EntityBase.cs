using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    /// <typeparam name="TIdentificator">Type of identificator.</typeparam>
    public class EntityBase<TIdentificator> where TIdentificator : IEquatable<TIdentificator>
    {
        /// <summary>
        /// Entity identificator.
        /// </summary>
        public virtual TIdentificator Identificador { get; set; }
    }
}

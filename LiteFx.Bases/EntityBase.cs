using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LiteFx.Bases
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    /// <typeparam name="TId">Type of id.</typeparam>
    public class EntityBase<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Entity id.
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual TId Id { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LiteFx.Bases.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidatableEntity : IValidatableObject
    {
        void ConfigureValidation();
    }
}

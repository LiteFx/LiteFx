using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiteFx.Bases.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidatableEntity : IValidatableObject
    {
        void ConfigureValidation();
        IEnumerable<ValidationResult> Validate();
    }
}

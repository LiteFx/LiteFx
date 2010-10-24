using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LiteFx.Bases.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidation : IValidatableObject
    {
        void AddValidationResult(string mensagem, string key);

        IEnumerable<ValidationResult> Validate();
    }
}

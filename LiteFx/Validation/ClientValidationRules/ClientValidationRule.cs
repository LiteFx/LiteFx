using System;
using System.Collections.Generic;

namespace LiteFx.Validation.ClientValidationRules
{
    /// <summary>
    /// Class that represent rules for client validation.
    /// </summary>
    public class ClientValidationRule
    {
        private readonly Dictionary<string, object> _validationParameters = new Dictionary<string, object>();
        private string _validationType;

        public string ErrorMessage { get; set; }

        /// <summary>
        /// Validation parameters. Eg: <code>ValidationParameters.Add('max', 50)</code> For the type of validation 'length' you can use the 'max' parameter set to 50 and 
        /// the client validation mechanism will know that this field accepts a maximum of 50 character set.
        /// </summary>
        public IDictionary<string, object> ValidationParameters
        {
            get
            {
                return _validationParameters;
            }
        }

        /// <summary>
        /// Type of the client validation. Eg: length, required.
        /// </summary>
        public string ValidationType
        {
            get
            {
                return _validationType ?? String.Empty;
            }
            set
            {
                _validationType = value;
            }
        }
    }
}

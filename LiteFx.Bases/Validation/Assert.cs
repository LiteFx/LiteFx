using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace LiteFx.Bases.Validation
{
    public class Assert : IValidation
    {
        public bool LastAssertionIsValid { get; internal set; }

        public Assert()
        {
            LastAssertionIsValid = true;
            Results = new ValidationResults();
        }

        #region IValidation Members

        public ValidationResults Results
        {
            get;
            private set;
        }

        public void AddValidationResult(string mensagem, string key)
        {
            Results.AddResult(new ValidationResult(mensagem, null, key, key, null));
        }

        public void Validate()
        {
            if (!Results.IsValid)
                throw new BusinessException(Results);
        }

        public bool IsValid()
        {
            return Results.IsValid;
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                return (from e in Results
                        where e.Key == columnName
                        select e.Message).SingleOrDefault();
            }
        }

        #endregion
    }
}

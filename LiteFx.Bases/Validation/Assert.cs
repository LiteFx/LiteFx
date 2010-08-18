using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace LiteFx.Bases.Validation
{
    public class Assert<T> : IValidation
    {
        private List<Assertion> assertions;
        public List<Assertion> Assertions
        {
            get { return assertions ?? (assertions = new List<Assertion>()); }
        }

        public T InstanceReference { get; set; }

        public bool LastAssertionIsValid { get; internal set; }

        public Assert(T instanceReference)
        {
            InstanceReference = instanceReference;
            LastAssertionIsValid = true;
            Results = new ValidationResults();
            assertionsExecuted = false;
        }

        private bool assertionsExecuted { get; set; }

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

        private void Validate(bool throwsExcepetion) 
        {
            Results = Microsoft.Practices.EnterpriseLibrary.Validation.Validation.Validate(InstanceReference);

            foreach (var item in Assertions)
            {
                item.Evaluate(InstanceReference, Results);
            }

            assertionsExecuted = true;

            if (throwsExcepetion)
                if (!Results.IsValid)
                    throw new BusinessException(Results);
        }

        public void Validate()
        {
            Validate(true);
        }

        public bool IsValid
        {
            get
            {
                Validate(false);
                return Results.IsValid;
            }
        }

        #endregion

        #region IDataErrorInfo Members
        /// <summary>
        /// Implemented just for supply the interface requisites. Always return an empty string.
        /// </summary>
        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                if(columnName == "Error" || columnName == "IsValid") return null;

                if (!assertionsExecuted)
                    Validate(false);

                return (from e in Results
                        where e.Key == columnName
                        select e.Message).FirstOrDefault();
            }
        }

        #endregion
    }
}

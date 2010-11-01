using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace LiteFx.Bases.Validation
{
    public class Assert<T>
    {
        private List<Assertion> assertions;
        public List<Assertion> Assertions
        {
            get { return assertions ?? (assertions = new List<Assertion>()); }
        }

        public bool LastAssertionIsValid { get; internal set; }

        public Assert()
        {
            LastAssertionIsValid = true;
            ValidationResults = new List<ValidationResult>();
            assertionsExecuted = false;
        }

        private bool assertionsExecuted { get; set; }

        #region IValidation Members

        public IList<ValidationResult> ValidationResults
        {
            get;
            private set;
        }

        public void AddValidationResult(string message, string key)
        {
            ValidationResults.Add(new ValidationResult(message, new string[] { key }));
        }

        private IEnumerable<ValidationResult> Validate(T instanceReference, bool throwsExcepetion) 
        {
            foreach (var item in Assertions)
            {
                item.Evaluate(instanceReference, ValidationResults);
            }

            assertionsExecuted = true;

            if (throwsExcepetion)
                if (ValidationResults.Count > 0)
                    throw new BusinessException(ValidationResults);

            return ValidationResults;
        }

        public IEnumerable<ValidationResult> Validate(T instanceReference)
        {
            return Validate(instanceReference, true);
        }

        #endregion
    }
}

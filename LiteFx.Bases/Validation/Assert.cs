using System.Collections.Generic;
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

        public Assert()
        {
            AssertionsExecuted = false;
        }

        internal bool AssertionsExecuted { get; set; }

        private IEnumerable<ValidationResult> Validate(T instanceReference, bool throwsExcepetion, IList<ValidationResult> validationResults) 
        {
            if (!AssertionsExecuted)
            {
                foreach (var item in Assertions)
                {
                    item.Evaluate(instanceReference, validationResults);
                }

                AssertionsExecuted = true;

                if (throwsExcepetion)
                    if (validationResults.Count > 0)
                        throw new BusinessException(validationResults);
            }

            return validationResults;
        }

        public IEnumerable<ValidationResult> Validate(T instanceReference, IList<ValidationResult> validationContext)
        {
            return Validate(instanceReference, false, validationContext);
        }
    }
}

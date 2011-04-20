using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiteFx.Validation
{
    public class Assert : IAssert
    {
        private List<Assertion> assertions;
        public List<Assertion> Assertions
        {
            get { return assertions ?? (assertions = new List<Assertion>()); }
        }

        public IEnumerable<ValidationResult> Validate(object instanceReference, IList<ValidationResult> validationResults)
        {
            foreach (var item in Assertions)
            {
                item.Evaluate(instanceReference, validationResults);
            }
            
            return validationResults;
        }
    }

    public class Assert<T> : Assert, IAssert<T> { }
}

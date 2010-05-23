using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases.Validation
{
    public class Validator<T, TResult>
    {
        public Assertion<T, TResult> Assertion { get; set; }
        public Assert<T> AssertReference { get; set; }

        public Validator(Assertion<T, TResult> assertion, Assert<T> assertReference)
        {
            AssertReference = assertReference;
            Assertion = assertion;
        }

        public void EndValidation()
        {
            AssertReference.Assertions.Add(Assertion);
        }
    }
}

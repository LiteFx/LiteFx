using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;

namespace LiteFx.Bases.Validation
{
    public abstract class Assertion
    {
        public abstract bool Evaluate(object obj, IList<ValidationResult> results);
    }

    public class Assertion<T, TProperty> : Assertion
    {
        public List<Accessor<T, TProperty>> Accessors
        {
            get;
            private set;
        }

        public List<Predicate<TProperty>> Predicates { get; private set; }

        public Assertion WhenAssertion { get; set; }

        public Assertion()
        {
            Accessors = new List<Accessor<T, TProperty>>();
            Predicates = new List<Predicate<TProperty>>();
        }

        public override bool Evaluate(object obj, IList<ValidationResult> results)
        {
            if (WhenAssertion != null && !WhenAssertion.Evaluate(obj, null)) return true;

            bool allPredicatesAreValid = true;

            foreach (var predicate in Predicates)
            {
                foreach (var accessor in Accessors)
                {
                    if (!predicate.EvalPredicate(accessor.CompiledAccessor((T)obj)))
                    {
                        if (results != null)
                            results.Add(
                                new ValidationResult(string.Format(predicate.ValidationMessage, accessor.MemberName),
                                                     new string[] { accessor.MemberName }));
                        allPredicatesAreValid = false;
                    }
                }
            }

            return allPredicatesAreValid;
        }
    }
}

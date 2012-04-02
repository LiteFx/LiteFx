using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LiteFx.Validation
{
    public abstract class Assertion
    {
        public Assertion WhenAssertion { get; set; }

        public abstract IEnumerable<string> AccessorMemberNames { get; }
        public abstract IEnumerable<Predicate> BasePredicates { get; }

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
        public override IEnumerable<Predicate> BasePredicates
        {
            get
            {
                return Predicates.Select(p => p as Predicate);
            }
        }

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
                        {
                            List<string> memberNames = new List<string>() { accessor.MemberName };
                            
                            if(accessor.MemberType.IsSubclassOf(typeof(EntityBase)))
                                memberNames.Add(string.Format("{0}.Id", accessor.MemberName));
                            
                            results.Add(new ValidationResult(string.Format(predicate.ValidationMessage, ValidationHelper.ResourceManager.GetString(accessor.MemberName)), memberNames));
                        }

                        allPredicatesAreValid = false;
                    }
                }
            }

            return allPredicatesAreValid;
        }

        public override IEnumerable<string> AccessorMemberNames
        {
            get { return Accessors.Select(a => a.MemberName); }
        }
    }
}

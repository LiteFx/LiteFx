using System;

namespace LiteFx.Bases.Validation
{
    public class Predicate<TProperty>
    {
        public Func<TProperty, bool> EvalPredicate { get; internal set; }

        public string ValidationMessage
        {
            get;
            private set;
        }

        public Predicate(Func<TProperty, bool> predicate, string validationMessage)
        {
            EvalPredicate = predicate;
            ValidationMessage = validationMessage;
        }
    }
}

using System;
using LiteFx.Validation.ClientValidationRules;

namespace LiteFx.Validation
{
	public class Predicate
	{
		public string ValidationMessage
		{
			get;
			private set;
		}

		public ClientValidationRule ClienteValidationRule { get; set; }

		public Predicate(string validationMessage)
		{
			ValidationMessage = validationMessage;
		}

		public Predicate(string validationMessage, ClientValidationRule clientValidationRule)
			: this(validationMessage)
		{
			ClienteValidationRule = clientValidationRule;
		}
	}

	public class Predicate<TProperty> : Predicate
	{
		public Func<TProperty, bool> EvalPredicate
		{
			get;
			internal set;
		}

		public Predicate(Func<TProperty, bool> predicate, string validationMessage)
			: base(validationMessage)
		{
			EvalPredicate = predicate;
		}

		public Predicate(Func<TProperty, bool> predicate, string validationMessage, ClientValidationRule clientValidationRule)
			: base(validationMessage, clientValidationRule)
		{
			EvalPredicate = predicate;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Linq.Expressions;

namespace LiteFx.Bases.Validation
{
    public class Assert<T> : IValidation
    {
        private List<Assertion> assertions;
        public List<Assertion> Assertions
        {
            get
            {
                if (assertions == null)
                    assertions = new List<Assertion>();
                return assertions;
            }
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
            Results = Microsoft.Practices.EnterpriseLibrary.Validation.Validation.Validate<T>(InstanceReference);

            foreach (var item in Assertions.AsEnumerable())
            {
                if (!item.IsValid(InstanceReference))
                    AddValidationResult(string.Format(item.ValidationMessage, item.MemberName), item.MemberName);
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

        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
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

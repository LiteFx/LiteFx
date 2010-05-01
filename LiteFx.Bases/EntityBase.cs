using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteFx.Bases.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace LiteFx.Bases
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    /// <typeparam name="TIdentificator">Type of identificator.</typeparam>
    public class EntityBase<TIdentificator> : IValidation
        where TIdentificator : IEquatable<TIdentificator>
    {
        /// <summary>
        /// Entity identificator.
        /// </summary>
        public virtual TIdentificator Identificador { get; set; }

        public EntityBase()
        {
            Assert = new Assert();
        }

        protected virtual Assert Assert { get; set; }

        #region IValidation Members

        public virtual ValidationResults Results { get { return Assert.Results; } }

        public virtual void AddValidationResult(string mensagem, string key)
        {
            Assert.AddValidationResult(mensagem, key);
        }

        public virtual void Validate()
        {
            Assert.Validate();
        }

        public virtual bool IsValid() 
        {
            return Assert.IsValid();
        }

        #endregion

        #region IDataErrorInfo Members

        public virtual string Error
        {
            get { return Assert.Error; }
        }

        public virtual string this[string columnName]
        {
            get { return Assert[columnName]; }
        }

        #endregion
    }
}

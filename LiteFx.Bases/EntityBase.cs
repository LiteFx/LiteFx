using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteFx.Bases.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.ComponentModel.DataAnnotations;

namespace LiteFx.Bases
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    /// <typeparam name="TId">Type of id.</typeparam>
    public class EntityBase<TId> : IValidation
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Entity id.
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual TId Id { get; set; }

        /// <summary>
        /// Default constructor of the EntityBase class.
        /// </summary>
        public EntityBase()
        {
            Assert = new Assert();
        }

        /// <summary>
        /// Instance of assertion class to perform validations.
        /// </summary>
        protected virtual Assert Assert { get; set; }

        #region IValidation Members

        [ScaffoldColumn(false)]
        public virtual ValidationResults Results { get { return Assert.Results; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="key"></param>
        public virtual void AddValidationResult(string mensagem, string key)
        {
            Assert.AddValidationResult(mensagem, key);
        }

        /// <summary>
        /// Verify if the entity is valid, if it is not valid throws an <see cref="LiteFx.Bases.BusinessException"/>.
        /// </summary>
        /// <exception cref="LiteFx.Bases.BusinessException">This exception was throw if the Entity is not valid.</exception>
        public virtual void Validate()
        {
            Assert.Validate();
        }

        /// <summary>
        /// Verify if the entity is valid.
        /// </summary>
        /// <returns>True if the entity is valid, false if it is not.</returns>
        public virtual bool IsValid() 
        {
            return Assert.IsValid();
        }

        #endregion

        #region IDataErrorInfo Members
        /// <summary>
        /// Error property implementation for <see cref="System.ComponentModel.IDataErrorInfo"/>.
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual string Error
        {
            get { return Assert.Error; }
        }

        /// <summary>
        /// Error indexer implementation for <see cref="System.ComponentModel.IDataErrorInfo"/>.
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual string this[string columnName]
        {
            get { return Assert[columnName]; }
        }
        #endregion
    }
}

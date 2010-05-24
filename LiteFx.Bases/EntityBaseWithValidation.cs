using System;
using LiteFx.Bases.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace LiteFx.Bases
{
    /// <summary>
    /// EntityBase implementation that supports validation.
    /// </summary>
    /// <typeparam name="T">Type of the entity. It will help the validation engine to discover wich object it will handle.</typeparam>
    /// <typeparam name="TId">Type of id.</typeparam>
    public abstract class EntityBaseWithValidation<T, TId> : EntityBase<TId>, IValidation
        where T : EntityBaseWithValidation<T, TId>
        where TId : IEquatable<TId>
    {

        /// <summary>
        /// Default constructor of the EntityBase class.
        /// </summary>
        public EntityBaseWithValidation()
        {
            Assert = new Assert<T>((T)this);
        }

        /// <summary>
        /// Instance of assertion class to perform validations.
        /// </summary>
        protected virtual Assert<T> Assert { get; set; }

        #region IValidation Members

        [ScaffoldColumn(false)]
        public virtual ValidationResults Results { get { return Assert.Results; } }

        /// <summary>
        /// Adds a validation result to the ValidationResults collection.
        /// </summary>
        /// <param name="mensagem">Validation message.</param>
        /// <param name="key">Validation key.</param>
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
        public virtual bool IsValid { get { return Assert.IsValid; } }

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

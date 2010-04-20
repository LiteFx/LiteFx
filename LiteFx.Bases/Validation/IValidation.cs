using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace LiteFx.Bases.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidation : IDataErrorInfo
    {
        /// <summary>
        /// Resultados das validações realizadas na Bll.
        /// </summary>
        ValidationResults Results { get; }

        void ValidateResults();
    }
}

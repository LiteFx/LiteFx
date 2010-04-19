using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace LiteFx.Web.Mvc
{
    /// <summary>
    /// Representação da cultura no cliente (Browser).
    /// Utilizado pelo Microsoft Ajax Framework para manter a internacionalização configurada na aplicação.
    /// </summary>
    [Serializable]
    public class ClientCultureInfo
    {
        /// <summary>
        /// Nome da cultura.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Variavel com este casing no cliente (JavaScript)")]
        public string name;

        /// <summary>
        /// Formato da data.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Variavel com este casing no cliente (JavaScript)")]
        public DateTimeFormatInfo dateTimeFormat;

        /// <summary>
        /// Formato do númerico.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Variavel com este casing no cliente (JavaScript)")]
        public NumberFormatInfo numberFormat;

        /// <summary>
        /// Construdor da ClientCultureInfo
        /// </summary>
        /// <param name="cultureInfo">Informações da cultura.</param>
        public ClientCultureInfo(CultureInfo cultureInfo)
        {
            this.name = cultureInfo.Name;
            this.numberFormat = cultureInfo.NumberFormat;
            this.dateTimeFormat = cultureInfo.DateTimeFormat;
        }
    }
}

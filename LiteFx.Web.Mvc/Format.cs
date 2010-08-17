using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Web.Mvc
{
    /// <summary>
    /// Enumerador utilizado para verificar o formato requerido pelo usuário.
    /// </summary>
    public enum Format
    {
        /// <summary>
        /// Format Html (HiperText Markup Language).
        /// </summary>
        Html,
        /// <summary>
        /// Format Json (Javascript Object Notation).
        /// </summary>
        Json,
        /// <summary>
        /// Format Xml (eXtensible Markup Language).
        /// </summary>
        Xml,
        /// <summary>
        /// Formato Pdf (Portrait Document Format).
        /// </summary>
        Pdf,
        /// <summary>
        /// Formato Excel.
        /// </summary>
        Xls,
        /// <summary>
        /// Fomato Csv (Comma separated value).
        /// </summary>
        Csv,
        /// <summary>
        /// Portable Network Graphics (PNG) is a bitmapped image format that employs lossless data compression.
        /// </summary>
        Png
    }
}

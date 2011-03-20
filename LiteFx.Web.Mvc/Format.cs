using System;

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
        /// Javascript Object Notation.
        /// </summary>
        Json,
        /// <summary>
        /// eXtensible Markup Language.
        /// </summary>
        Xml,
        /// <summary>
        /// Portrait Document Format.
        /// </summary>
        Pdf,
        /// <summary
        /// Excel Office Format.
        /// </summary>
        Xls,
        /// <summary>
        /// Comma separated value.
        /// </summary>
        Csv,
        /// <summary>
        /// Portable Network Graphics (PNG) is a bitmapped image format that employs lossless data compression.
        /// </summary>
        Png,
        /// <summary>
        /// Joint Photographic Experts Group format.
        /// </summary>
        Jpeg,
        /// <summary>
        /// Graphics Interchange Format.
        /// </summary>
        Gif,
        /// <summary>
        /// Doc Format (Word Office Format).
        /// </summary>
        Doc
    }

    /// <summary>
    /// Constantes com content types usados pelos custom results.
    /// </summary>
    public abstract class ContentType
    {
        public abstract string ContentTypeString { get; }

        public static ContentType Html { get { return new HtmlContentType(); } }
        public static ContentType Pdf { get { return new PdfContentType(); } }
        public static ContentType Xls { get { return new XlsContentType(); } }
        public static ContentType Csv { get { return new CsvContentType(); } }
        public static ContentType Xml { get { return new XmlContentType(); } }
        public static ContentType Json { get { return new JsonContentType(); } }
        public static ContentType Doc { get { return new DocContentType(); } }
        public static ContentType Png { get { return new PngContentType(); } }
        public static ContentType Jpeg { get { return new JpegContentType(); } }
        public static ContentType Gif { get { return new GifContentType(); } }

        public static ContentType FromFormat(Format format)
        {
            switch (format)
            {
                case Format.Json:
                    return Json;
                case Format.Xml:
                    return Xml;
                case Format.Pdf:
                    return Pdf;
                case Format.Xls:
                    return Xls;
                case Format.Csv:
                    return Csv;
                case Format.Png:
                    return Png;
                case Format.Jpeg:
                    return Jpeg;
                case Format.Gif:
                    return Gif;
                case Format.Html:
                    return Html;
                case Format.Doc:
                    return Doc;
            }

            throw new ArgumentOutOfRangeException();
        }

        public override string ToString()
        {
            return ContentTypeString;
        }
    }

    public sealed class PdfContentType : ContentType
    {
        public override string ContentTypeString
        {
            get { return "application/pdf"; }
        }
    }

    public sealed class XlsContentType : ContentType
    {
        public override string ContentTypeString
        {
            get { return "application/ms-excel"; }
        }
    }

    public sealed class CsvContentType : ContentType
    {
        public override string ContentTypeString
        {
            get { return "text/csv"; }
        }
    }

    public sealed class XmlContentType : ContentType
    {
        public override string ContentTypeString
        {
            get { return "text/xml"; }
        }
    }

    public sealed class HtmlContentType : ContentType
    {
        public override string ContentTypeString
        {
            get { return "text/html"; }
        }
    }

    public sealed class JsonContentType : ContentType
    {
        public override string ContentTypeString
        {
            get { return "text/json"; }
        }
    }

    public sealed class DocContentType : ContentType
    {
        public override string ContentTypeString
        {
            get { return "application/msword"; }
        }
    }

    public sealed class PngContentType : ContentType
    {
        public override string ContentTypeString
        {
            get { return "image/png"; }
        }
    }

    public sealed class JpegContentType : ContentType
    {
        public override string ContentTypeString
        {
            get { return "image/jpeg"; }
        }
    }

    public sealed class GifContentType : ContentType
    {
        public override string ContentTypeString
        {
            get { return "image/gif"; }
        }
    }
}

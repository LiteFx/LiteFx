using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using LiteFx.Web.Mvc.CustomActionResults;
using System.IO.Compression;
using System.Collections;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace LiteFx.Web.Mvc
{
    public class BaseController : Controller
    {
        #region [ HttpCompression ]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpResponseBase response = filterContext.HttpContext.Response;

            string acceptEncoding = request.Headers["Accept-Encoding"];

            if (acceptEncoding == null)
                return;

            if (acceptEncoding.ToLower().Contains("gzip"))
            {
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                response.AppendHeader("Content-Encoding", "gzip");
            }
            else if (acceptEncoding.ToLower().Contains("deflate"))
            {
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                response.AppendHeader("Content-Encoding", "deflate");
            }
        }
        #endregion

        #region [ Excel ]
        /// <summary>
        /// Retorna um ExcelResult com os dados contidos no parametro rows e escreve no Response o ExcelResult.
        /// </summary>
        /// <param name="rows">Dados que serão escritos no arquivo.</param>
        /// <returns>Retorna um ExcelResult com os dados contidos no parametro rows e escreve no Response o ExcelResult.</returns>
        protected ExcelResult Excel(IList rows, Type resourceType)
        {
            return new ExcelResult(rows, ControllerContext.RouteData.Values["controller"].ToString(), resourceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected ActionResult Excel(string viewName, object model, string fileName)
        {
            SetExcelHeaderAndContentType(fileName);

            return PartialView(viewName, model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected ActionResult Excel(string viewName, string fileName) 
        {
            SetExcelHeaderAndContentType(fileName);

            return PartialView(viewName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        private void SetExcelHeaderAndContentType(string fileName) 
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("{0}.xls", fileName));
            Response.Charset = "utf-8";
            Response.ContentEncoding = Encoding.GetEncoding("iso-8859-1");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = ContentType.Excel;
        }
        #endregion

        #region [ Pdf ]
        /// <summary>
        /// Retorna um PdfResult com os dados contidos no parametro rows e escreve no Response o PdfResult.
        /// </summary>
        /// <param name="rows">Dados que serão escritos no arquivo.</param>
        /// <param name="tituloRelatorio">Informe o titulo do relatorio. Ele será concatenado com "Relatório de {0}".</param>
        /// <returns>Retorna um PdfResult com os dados contidos no parametro rows e escreve no Response o PdfResult.</returns>
        protected PdfResult Pdf(IList rows, Type resourceType, PdfPageEventHelper pdfPageEventHelper)
        {
            return new PdfResult(rows, ControllerContext.RouteData.Values["controller"].ToString(), resourceType, pdfPageEventHelper);
        }

        /// <summary>
        /// Retorna um PdfResult com os dados contidos no parametro rows e escreve no Response o PdfResult.
        /// </summary>
        /// <param name="rows">Dados que serão escritos no arquivo.</param>
        /// <param name="tituloRelatorio">Informe o titulo do relatorio. Ele será concatenado com "Relatório de {0}".</param>
        /// <param name="columnWidth">Array de Single com a largura das colunas do Relatório</param>
        /// <returns>Retorna um PdfResult com os dados contidos no parametro rows e escreve no Response o PdfResult.</returns>
        protected PdfResult Pdf(IList rows, Type resourceType, PdfPageEventHelper pdfPageEventHelper, Single[] columnWidth)
        {
            return new PdfResult(rows, ControllerContext.RouteData.Values["controller"].ToString(), resourceType, pdfPageEventHelper, columnWidth);
        }

        /// <summary>
        /// Retorna um PdfResult com os dados contidos no parametro rows e escreve no Response o PdfResult.
        /// </summary>
        /// <param name="rows">Dados que serão escritos no arquivo.</param>
        /// <param name="tituloRelatorio">Informe o titulo do relatorio. Ele será concatenado com "Relatório de {0}".</param>
        /// <param name="pageSize">Orientação da página do relatório Pdf.</param>
        /// <returns>Retorna um PdfResult com os dados contidos no parametro rows e escreve no Response o PdfResult.</returns>
        protected PdfResult Pdf(IList rows, Type resourceType, PdfPageEventHelper pdfPageEventHelper, Rectangle pageSize)
        {
            return new PdfResult(rows, ControllerContext.RouteData.Values["controller"].ToString(), resourceType, pdfPageEventHelper, pageSize);
        }

        /// <summary>
        /// Retorna um PdfResult com os dados contidos no parametro rows e escreve no Response o PdfResult.
        /// </summary>
        /// <param name="rows">Dados que serão escritos no arquivo.</param>
        /// <param name="tituloRelatorio">Informe o titulo do relatorio. Ele será concatenado com "Relatório de {0}".</param>
        /// <param name="pageSize">Orientação da página do relatório Pdf.</param>
        /// <param name="ColumnsWdth">Array de Byte com a Largura das Colunas.</param>
        /// <returns>Retorna um PdfResult com os dados contidos no parametro rows e escreve no Response o PdfResult.</returns>
        protected PdfResult Pdf(IList rows, Type resourceType, PdfPageEventHelper pdfPageEventHelper, Rectangle pageSize, Single[] columnsWidth)
        {
            return new PdfResult(rows, ControllerContext.RouteData.Values["controller"].ToString(), resourceType, pdfPageEventHelper, pageSize, columnsWidth);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected PdfResult Pdf(MemoryStream content, string fileName)
        {
            return new PdfResult(content, fileName);
        }
        #endregion

        #region [ Xml ]
        /// <summary>
        /// Retorna o objeto passado por parametro serializado no formato xml.
        /// </summary>
        /// <param name="objectToSerialize">Objeto que será serializado.</param>
        /// <returns>Retorna o objeto passado por parametro serializado no formato xml.</returns>
        protected XmlResult Xml(object objectToSerialize)
        {
            return new XmlResult(objectToSerialize);
        }
        #endregion

        #region [ Csv ]
        /// <summary>
        /// Retorna um CsvResult com os dados contidos no parametro rows e o escreve no Response o CsvResult.
        /// </summary>
        /// <param name="rows">Dados que serão escritos no arquivo.</param>
        /// <returns>Retorna um CsvResult com os dados contidos no parametro rows e o escreve no Response o CsvResult.</returns>
        protected CsvResult Csv(IEnumerable rows, Type resourceType)
        {
            return Csv(rows, resourceType, null);
        }

        /// <summary>
        /// Retorna um CsvResult com os dados contidos no parametro rows e o escreve no Response o CsvResult.
        /// </summary>
        /// <param name="rows">Dados que serão escritos no arquivo.</param>
        /// <param name="headers">Array de String indicando quais colunas devem ser exibidas no Csv</param>
        /// <returns>Retorna um CsvResult com os dados contidos no parametro rows e o escreve no Response o CsvResult.</returns>
        protected CsvResult Csv(IEnumerable rows, Type resourceType, string[] headers)
        {
            return new CsvResult(rows, ControllerContext.RouteData.Values["controller"].ToString(), resourceType, headers);
        }

        /// <summary>
        /// Retorna um Word Result com os dados contidos no Memory Stream e o escreve no Response
        /// </summary>
        /// <param name="memory">Memory Stream com o conteúdo do documento</param>
        /// <param name="fileName">Nome do Arquivo</param>
        /// <returns>Word Result escrito no Response</returns>
        protected WordResult Word(MemoryStream memory, string fileName)
        {
            return new WordResult(memory, fileName);
        }

        /// <summary>
        /// Retorna um Word Result com os dados contidos no Memory Stream e o escreve no Response
        /// </summary>
        /// <param name="memory">Memory Stream com o conteúdo do documento</param>
        /// <param name="fileName">Nome do Arquivo</param>
        /// <returns>Word Result escrito no Response</returns>
        protected WordResult Word(string filePath, string fileName)
        {
            return new WordResult(filePath, fileName);
        }
        #endregion

        #region Futures
        /*protected Format Format { get; set; }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var routeValues = filterContext.RouteData.Values;
            string formatKey = "format";

            if (routeValues.ContainsKey(formatKey))
            {
                string requestedFormat = routeValues[formatKey].ToString();
                try
                {
                    Format = (Format)Enum.Parse(typeof(Format), requestedFormat, true);
                }
                catch
                {
                    //Se não conseguir encontrar o formato ignoro a excecao e coloco no formato html.
                    Format = Format.Html;
                }
            }
            else
                Format = Format.Html;
        }

        protected ActionResult FormatResult(object model) 
        {
            switch (Format)
            {
                case Format.Json:
                    return Json(model);
                    break;
                case Format.Xml:
                    break;
                case Format.Pdf:
                    break;
                case Format.Xls:
                    break;
                case Format.Csv:
                    break;
                default:
                    View(model);
                    break;
            }
        }*/
        #endregion
    }
}

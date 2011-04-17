using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LiteFx.Web.Mvc.ActionResults
{
    /// <summary>
    /// Classe para construção de um WordResult
    /// </summary>
    public class WordResult : ActionResult
    {
        /// <summary>
        /// Memory stream com o conteúdo a ser escrito no response
        /// </summary>
        public MemoryStream Stream { get; internal set; }

        /// <summary>
        /// Nome do Arquivo
        /// </summary>
        public string FileName { get; internal set; }

        /// <summary>
        /// Caminho do Arquivo
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Construtor Default
        /// </summary>
        /// <param name="stream">Memory Stream</param>
        /// <param name="fileName">Nome do Arquivo</param>
        public WordResult(MemoryStream stream, string fileName)
        {
            Stream = stream;
            FileName = fileName;
        }

        /// <summary>
        /// Construtor 
        /// </summary>
        /// <param name="filePath">Caminho do arquivo pra Download</param>
        /// <param name="fileName">Nome do Arquivo</param>
        public WordResult(string filePath, string fileName)
        {
            FilePath = filePath;
            FileName = fileName;
            Stream = null;
        }

        /// <summary>
        /// Construtor Default da Classe
        /// </summary>
        /// <param name="context">Controller</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (Stream == null && !string.IsNullOrEmpty(FilePath))
                WriteFile(FileName, ContentType.Doc.ToString(), FilePath);
            else
                WriteFile(FileName, ContentType.Doc.ToString(), Stream);
        }


        private static void WriteFile(string fileName, string contentType, MemoryStream content)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("{0}.doc", fileName));
            context.Response.ContentType = contentType;
            context.Response.Charset = "utf-8";
            context.Response.OutputStream.Write(content.GetBuffer(), 0, content.GetBuffer().Length);
            content.Close();
            context.Response.OutputStream.Flush();
            context.Response.OutputStream.Close();
            content.Close();
            context.Response.End();
        }

        private static void WriteFile(string fileName, string contentType, string filePath)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("{0}.doc", fileName));
            context.Response.ContentType = contentType;
            context.Response.TransmitFile(filePath);
            context.Response.Charset = "utf-8";
            context.Response.OutputStream.Flush();
            context.Response.OutputStream.Close();

            context.Response.End();
        }
    }
}

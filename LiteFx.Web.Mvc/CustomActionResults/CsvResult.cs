using System;
using System.Collections;
using System.IO;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LiteFx.Web.Mvc.CustomActionResults
{
    public class CsvResult : ActionResult
    {
        private string[] _headers;

        public string FileName { get; internal set; }

        public IEnumerable Rows { get; internal set; }

        public Type ResourceSource { get; internal set; }

        public CsvResult(IEnumerable rows, string fileName, Type resourceSource) : this(rows, fileName, resourceSource, null) { }
            
        public CsvResult(IEnumerable rows, string fileName, Type resourceSource, string[] headers)
        {
            Rows = rows;
            FileName = fileName;
            _headers = headers;
            ResourceSource = resourceSource;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            MemoryStream stream = new MemoryStream();

            StreamWriter writer = new StreamWriter(stream, Encoding.GetEncoding("ISO-8859-1"));

            ResourceManager resm = new ResourceManager(ResourceSource);

            string header;

            var enumerator = Rows.GetEnumerator();
            if (enumerator.MoveNext())
            {
                if (_headers == null)
                    _headers = ActionResultHelper.MontaCabecalho(enumerator.Current);

                for (int i = 0; i < _headers.Length; i++)
                {
                    header = _headers[i].Trim();

                    writer.Write(string.Format("\"{0}\"", resm.GetString(header)));

                    if (i < _headers.Length - 1)
                        writer.Write(';');
                    else
                        writer.WriteLine();
                }

                foreach (Object row in Rows)
                {
                    for (int i = 0; i < _headers.Length; i++)
                    {
                        header = _headers[i];

                        string cell = row.GetType().GetProperty(header).GetValue(row, null) == null ? string.Empty : row.GetType().GetProperty(header).GetValue(row, null).ToString();
                        writer.Write(string.Format("\"{0}\"", cell));

                        if (i < _headers.Length - 1)
                            writer.Write(';');
                        else
                            writer.WriteLine();
                    }
                }
            }
            else
            {
                writer.WriteLine(resm.GetString("NenhumRegistroEncontrado"));
            }

            writer.Close();

            WriteFile(FileName, ContentType.Csv, stream);           

        }

        private static void WriteFile(string fileName, string contentType, MemoryStream content)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("{0}.csv", fileName));
            context.Response.ContentType = contentType;
            context.Response.Charset = "ISO-8859-1";
            context.Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
            context.Response.OutputStream.Write(content.GetBuffer(), 0, content.GetBuffer().Length);
            content.Close();
            context.Response.OutputStream.Flush();
            context.Response.OutputStream.Close();
            content.Close();
            context.Response.End();
        }
    }
}

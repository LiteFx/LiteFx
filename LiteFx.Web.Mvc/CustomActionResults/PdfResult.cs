using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace LiteFx.Web.Mvc.CustomActionResults
{
    public class PdfResult : ActionResult
    {
        private string[] _headers = null;

        public string FileName { get; internal set; }

        public IList Rows { get; internal set; }

        public Type ResourceSource { get; internal set; }

        public PdfPageEventHelper PageEventHelper { get; internal set; }

        public Rectangle DocumentPageSize { get; internal set; }

        public MemoryStream stream { get; internal set; }

        public Single[] ColumnWidth { get; internal set; }

        public PdfResult(IList rows, string fileName, Type resourceSource) : this(rows, fileName, resourceSource, null, null, PageSize.A4, null) { }

        public PdfResult(IList rows, string fileName, Type resourceSource, PdfPageEventHelper eventHelper) : this(rows, fileName, resourceSource, null, eventHelper, PageSize.A4, null) { }

        public PdfResult(IList rows, string fileName, Type resourceSource, PdfPageEventHelper eventHelper, Single[] columnsWidth) : this(rows, fileName, resourceSource, null, eventHelper, PageSize.A4, columnsWidth) { }

        public PdfResult(IList rows, string fileName, Type resourceSource, PdfPageEventHelper eventHelper, Rectangle pageSize) : this(rows, fileName, resourceSource, null, eventHelper, pageSize, null) { }

        public PdfResult(IList rows, string fileName, Type resourceSource, PdfPageEventHelper eventHelper, Rectangle pageSize, Single[] columnsWidth) : this(rows, fileName, resourceSource, null, eventHelper, pageSize, columnsWidth) { }

        public PdfResult(IList rows, string fileName, Type resourceSource, string[] headers, PdfPageEventHelper eventHelper, Rectangle pageSize, Single[] columnsWidth)
        {
            Rows = rows;
            FileName = fileName;
            _headers = headers;
            ResourceSource = resourceSource;
            PageEventHelper = eventHelper;
            DocumentPageSize = pageSize;
            ColumnWidth = columnsWidth;
        }

        public PdfResult(MemoryStream content, string fileName)
        {
            stream = content;
            FileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (stream == null)
            {
                stream = new MemoryStream();

                Document document;

                if (DocumentPageSize != null)
                    document = new Document(DocumentPageSize);
                else
                    document = new Document(PageSize.A4);


                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                writer.CloseStream = false;

                if (PageEventHelper != null)
                    writer.PageEvent = PageEventHelper;

                document.Open();

                if (Rows.Count > 0)
                {
                    // Generate headers from table
                    if (_headers == null)
                    {
                        _headers = ActionResultHelper.MontaCabecalho(Rows[0]);
                    }


                    // Create Header Row
                    PdfPTable table = new PdfPTable(_headers.Length);

                    table.WidthPercentage = 100;

                    ResourceManager resm = new ResourceManager(ResourceSource);

                    foreach (String header in _headers)
                    {
                        Phrase content = new Phrase(new Chunk(resm.GetString(header), new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12)));

                        PdfPCell hcell = new PdfPCell(content);
                        hcell.BackgroundColor = Color.LIGHT_GRAY;
                        hcell.BorderWidth = 0;

                        table.AddCell(hcell);
                    }

                    foreach (Object row in Rows)
                    {
                        foreach (string header in _headers)
                        {
                            string strValue = row.GetType().GetProperty(header).GetValue(row, null).ToString();

                            PdfPCell cell = new PdfPCell(new Phrase(new Chunk(strValue, new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 11))));

                            cell.BorderWidth = 0;

                            table.AddCell(cell);
                        }
                    }

                    table.HeaderRows = 1;

                    if (ColumnWidth != null)
                        table.SetWidths(ColumnWidth);

                    document.Add(table);
                }
                else
                {
                    ResourceManager resm = new ResourceManager(ResourceSource);

                    Chunk chk = new Chunk(resm.GetString("NenhumRegistroEncontrado"));
                    Phrase p = new Phrase(chk);
                    Paragraph ph = new Paragraph(p);
                    ph.SetAlignment("center");
                    document.Add(ph);
                }
                document.Close();
            }

            WriteFile(FileName, ContentType.Pdf, stream);
        }

        private static void WriteFile(string fileName, string contentType, MemoryStream content)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("{0}.pdf", fileName));
            context.Response.ContentType = contentType;
            context.Response.Charset = "utf-8";
            context.Response.OutputStream.Write(content.GetBuffer(), 0, content.GetBuffer().Length);
            content.Close();
            context.Response.OutputStream.Flush();
            context.Response.OutputStream.Close();
            content.Close();
            context.Response.End();
        }
    }
}

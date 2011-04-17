using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiteFx.Web.Mvc.ActionResults
{
    public class ExcelResult : ActionResult
    {
        private string _fileName;
        private string[] _headers;

        private TableStyle _tableStyle;
        private TableItemStyle _headerStyle;
        private TableItemStyle _itemStyle;

        public string FileName
        {
            get { return _fileName; }
        }

        public IList Rows
        {
            get;
            internal set;
        }

        public Type ResourceSource { get; internal set; }


        public ExcelResult(IList rows, string fileName, Type resourceSource) : this(rows, fileName, null, resourceSource, null, null, null) { }

        public ExcelResult(string fileName, IList rows, string[] headers, Type resourceSource) : this(rows, fileName, headers, resourceSource, null, null, null) { }

        public ExcelResult(IList rows, string fileName, string[] headers, Type resourceSource, TableStyle tableStyle, TableItemStyle headerStyle, TableItemStyle itemStyle)
        {
            Rows = rows;
            ResourceSource = resourceSource;
            _fileName = fileName;
            _headers = headers;
            _tableStyle = tableStyle;
            _headerStyle = headerStyle;
            _itemStyle = itemStyle;

            // provide defaults
            if (_tableStyle == null)
            {
                _tableStyle = new TableStyle();
                _tableStyle.BorderStyle = BorderStyle.Solid;
                _tableStyle.BorderColor = Color.Black;
                _tableStyle.BorderWidth = Unit.Parse("2px");
            }
            if (_headerStyle == null)
            {
                _headerStyle = new TableItemStyle();
                _headerStyle.BackColor = Color.LightGray;
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            // Create HtmlTextWriter
            StringWriter sw = new StringWriter();
            HtmlTextWriter tw = new HtmlTextWriter(sw);

            // Build HTML Table from Items
            if (_tableStyle != null)
                _tableStyle.AddAttributesToRender(tw);
            tw.RenderBeginTag(HtmlTextWriterTag.Table);

            // Generate headers from table
            if (_headers == null)
            {
                if (Rows.Count > 0)
                {
                    _headers = ActionResultHelper.MontaCabecalho(Rows[0]);
                }
            }


            // Create Header Row
            tw.RenderBeginTag(HtmlTextWriterTag.Thead);
            if (_headers != null)
                foreach (var header in _headers)
                {
                    var resm = new ResourceManager(ResourceSource);

                    if (_headerStyle != null)
                        _headerStyle.AddAttributesToRender(tw);
                    tw.RenderBeginTag(HtmlTextWriterTag.Th);
                    tw.Write(resm.GetString(header));
                    tw.RenderEndTag();
                }
            tw.RenderEndTag();



            // Create Data Rows
            tw.RenderBeginTag(HtmlTextWriterTag.Tbody);
            foreach (Object row in Rows)
            {
                tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                foreach (string header in _headers)
                {
                    string strValue = row.GetType().GetProperty(header).GetValue(row, null).ToString();
                    strValue = ReplaceSpecialCharacters(strValue);
                    if (_itemStyle != null)
                        _itemStyle.AddAttributesToRender(tw);
                    tw.RenderBeginTag(HtmlTextWriterTag.Td);
                    tw.Write(HttpUtility.HtmlEncode(strValue));
                    tw.RenderEndTag();
                }
                tw.RenderEndTag();
            }
            tw.RenderEndTag(); // tbody

            tw.RenderEndTag(); // table

            WriteFile(_fileName, ContentType.Xls.ContentTypeString, sw.ToString());
        }


        private static string ReplaceSpecialCharacters(string value)
        {
            value = value.Replace("’", "'");
            value = value.Replace("“", "\"");
            value = value.Replace("”", "\"");
            value = value.Replace("–", "-");
            value = value.Replace("…", "...");
            return value;
        }

        private static void WriteFile(string fileName, string contentType, string content)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("{0}.xls", fileName));
            context.Response.Charset = "utf-8";
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = contentType;
            context.Response.Write(content);
            context.Response.End();
        }
    }
}

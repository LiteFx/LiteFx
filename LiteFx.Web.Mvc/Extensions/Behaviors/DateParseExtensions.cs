using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;

namespace LiteFx.Web.Mvc.Extensions.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    public static class DateParseExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="elementId"></param>
        /// <param name="invalidDateMessage"></param>
        /// <param name="helpLabelElementId"></param>
        /// <returns></returns>
        public static string DateParse(this AjaxHelper helper, string elementId, string invalidDateMessage, string helpLabelElementId)
        {
            StringBuilder sb = new StringBuilder();

            // Add jQuery library
            sb.AppendLine(helper.JQueryLibraryInclude());

            // Add script include
            sb.AppendLine(helper.ScriptInclude("Scripts/DateParse/date-" + Thread.CurrentThread.CurrentUICulture.Name + ".js"));

            // Add behavior configuration
            sb.AppendLine(helper.ScriptInclude("dateparse_" + elementId, string.Format(CultureInfo.CurrentCulture, BuildScript(), elementId, Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern, invalidDateMessage, helpLabelElementId), true));

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string BuildScript() 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("$(document).ready(function () {");
            sb.Append("var message = \"{3}\";");
            sb.Append("var input = $(\"#{0}\"), date_string = $(\"#{4}\"), date = null;");
            sb.Append("input.val(\"\");");
            sb.Append("date_string.text(\"\");");
            sb.Append("input.keyup("); 
            sb.Append("function (e) {");
            sb.Append("if (input.val().length > 0) {");
            sb.Append("date = Date.parse(input.val());");
            sb.Append("if (date !== null) {");
            sb.Append("input.removeClass();");
            sb.Append("date_string.text(date.toString(\"{1}\"));");
            sb.Append("} else {");
            sb.Append("input.addClass(\"{2}\");");
            sb.Append("date_string.text(message);");
            sb.Append("}");
            sb.Append("} else {");
            sb.Append("date_string.text(\"\");");
            sb.Append("}");
            sb.Append("});");
            sb.Append("input.blur(");
            sb.Append("function (e) {");
            sb.Append("if (date_string.text() !== \"\") {");
            sb.Append("if(date_string.text() !== message)");
            sb.Append("input.val(date_string.text());");
            sb.Append("date_string.text(\"\");");
            sb.Append("}");
            sb.Append("});");
            sb.Append("});");

            return sb.ToString();
        }
    }
}

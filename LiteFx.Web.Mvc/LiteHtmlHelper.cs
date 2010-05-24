using System;
using System.Text;

using System.Web.Mvc;

namespace LiteFx.Web.Mvc
{
    /// <summary>
    /// Esta classe comportará métodos de auxilio na camada MVC da aplicação.
    /// </summary>
    public static class LiteHtmlHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="ModelName"></param>
        /// <returns></returns>
        public static string AddButtons(this HtmlHelper helper, string ModelName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<ul class=\"UlBtnSaveCancel\"> {0}", Environment.NewLine);
            sb.AppendFormat("    <li> {0}", Environment.NewLine);
            sb.AppendFormat("        <span class=\"btn\"> {0}", Environment.NewLine);
            sb.AppendFormat("		    <input type=\"submit\" value=\"<%= Resources.Salvar %>\"/> {0}", Environment.NewLine);
            sb.AppendFormat("	    </span> {0}", Environment.NewLine);
            sb.AppendFormat("    </li> {0}", Environment.NewLine);
            sb.AppendFormat("    <li> {0}", Environment.NewLine);
            sb.AppendFormat("        <span class=\"btn\"> {0}", Environment.NewLine);
            sb.AppendFormat("		    <input type=\"submit\" value=\"<%= Resources.SalvarEAdicionarNovo %>\" onclick=\"$('form:first input:first').val('<%= bool.TrueString %>'); return true;\"/> {0}", Environment.NewLine);
            sb.AppendFormat("	    </span> {0}", Environment.NewLine);
            sb.AppendFormat("    </li> {0}", Environment.NewLine);
            sb.AppendFormat("    <li> {0}", Environment.NewLine);
            sb.AppendFormat("        <span class=\"btn\"> {0}", Environment.NewLine);
            sb.AppendFormat("            <a href=\"{0}\" onClick=\"Sys.Mvc.AsyncHyperlink.handleClick(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.Replace, updateTargetId: '{0}' });\"><%= Resources.Salvar %></a> {0}",ModelName, Environment.NewLine);
            sb.AppendFormat("	    </span> {0}", Environment.NewLine);
            sb.AppendFormat("    </li> {0}", Environment.NewLine);
            sb.AppendFormat("</ul> {0}", Environment.NewLine);

            return sb.ToString();
        }
    }
}

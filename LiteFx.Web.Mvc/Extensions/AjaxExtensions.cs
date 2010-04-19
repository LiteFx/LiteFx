using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Threading;
using System.Globalization;
using System.Web.UI;

namespace LiteFx.Web.Mvc.Extensions
{
    /// <summary>
    /// Extensões para auxiliar o uso de bibliotecas Ajax no MVC.
    /// </summary>
    public static class AjaxExtensions
    {
        /// <summary>
        /// Caminho da biblioteca Ajax da Microsoft.
        /// </summary>
        private static string microsoftAjaxLibraryUrl = "~/Scripts/MicrosoftAjax.js";

        /// <summary>
        /// Caminho da biblioteca Ajax da Microsoft para MVC.
        /// </summary>
        private static string microsoftAjaxMvcLibraryUrl = "~/Scripts/MicrosoftMvcAjax.js";

        /// <summary>
        /// Caminho da biblioteca do AjaxControlToolkit.
        /// </summary>
        private static string ajaxControlToolkitFolderUrl = "~/Scripts/AjaxControlToolkit/";

        /// <summary>
        /// Caminho da biblioteca do jQuery.
        /// </summary>
        private static string jQueryLibraryUrl = "~/Scripts/jquery-1.3.2.min.js";

        /// <summary>
        /// Este método pode ser utilizado para mudar o caminho padrão da biblioteca Ajax da Microsoft.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="url">Novo caminho.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper", Justification = "Extension Method")]
        public static void SetMicrosoftAjaxLibraryUrl(this AjaxHelper helper, string url)
        {
            microsoftAjaxLibraryUrl = url;
        }

        /// <summary>
        /// Retorna o caminho da biblioteca Ajax da Microsoft.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <returns>Caminho do script.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper", Justification = "Extension Method")]
        public static string GetMicrosoftAjaxLibraryUrl(this AjaxHelper helper)
        {
            return microsoftAjaxLibraryUrl;
        }

        /// <summary>
        /// Este método pode ser utilizado para mudar o caminho padrão da biblioteca Ajax da Microsoft.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="url">Novo caminho.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper", Justification = "Extension Method")]
        public static void SetMicrosoftAjaxMvcLibraryUrl(this AjaxHelper helper, string url)
        {
            microsoftAjaxMvcLibraryUrl = url;
        }

        /// <summary>
        /// Retorna o caminho da biblioteca Ajax da Microsoft.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <returns>Caminho do script.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper", Justification = "Extension Method")]
        public static string GetMicrosoftAjaxMvcLibraryUrl(this AjaxHelper helper)
        {
            return microsoftAjaxMvcLibraryUrl;
        }

        /// <summary>
        /// Este método pode ser utilizado para mudar o caminho padrão da biblioteca do jQuery.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="url">Novo caminho.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper", Justification = "Extension Method")]
        public static void SetJQueryLibraryUrl(this AjaxHelper helper, string url)
        {
            jQueryLibraryUrl = url;
        }

        /// <summary>
        /// Retorna o caminho da biblioteca jQuery.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <returns>Caminho do script.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper", Justification = "Extension Method")]
        public static string GetJQueryLibraryUrl(this AjaxHelper helper)
        {
            return jQueryLibraryUrl;
        }

        /// <summary>
        /// Este método pode ser utilizado para mudar o caminho padrão da biblioteca AjaxControlToolkit.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="url">Novo caminho.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper", Justification = "Extension Method")]
        public static void SetAjaxControlToolkitFolderUrl(this AjaxHelper helper, string url)
        {
            ajaxControlToolkitFolderUrl = url;
        }

        /// <summary>
        /// Retorna o caminho da biblioteca AjaxControlToolkit.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <returns>Caminho do script.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper", Justification = "Extension Method")]
        public static string GetAjaxControlToolkitFolderUrl(this AjaxHelper helper)
        {
            return ajaxControlToolkitFolderUrl;
        }

        /// <summary>
        /// Inclui uma referência da biblioteca Ajax da Microsoft.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <returns>Referência a biblioteca.</returns>
        public static string MicrosoftAjaxLibraryInclude(this AjaxHelper helper)
        {
            return SetScriptCulture(helper) + ScriptExtensions.ScriptInclude(helper, microsoftAjaxLibraryUrl);
        }

        /// <summary>
        /// Inclui uma referência da biblioteca Ajax da Microsoft.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <returns>Referência a biblioteca.</returns>
        public static string MicrosoftAjaxMvcLibraryInclude(this AjaxHelper helper)
        {
            return SetScriptCulture(helper) + ScriptExtensions.ScriptInclude(helper, microsoftAjaxMvcLibraryUrl);
        }

        /// <summary>
        /// Inclui uma referência da biblioteca jQuery.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <returns>Referência a biblioteca.</returns>
        public static string JQueryLibraryInclude(this AjaxHelper helper)
        {
            return ScriptExtensions.ScriptInclude(helper, jQueryLibraryUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ToolkitInclude(this AjaxHelper helper, params string[] fileName)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in fileName)
            {
                string fullUrl = ajaxControlToolkitFolderUrl + item;
                sb.AppendLine(ScriptExtensions.ScriptInclude(helper, fullUrl));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string DynamicToolkitCssInclude(this AjaxHelper helper, string fileName)
        {
            string fullUrl = ajaxControlToolkitFolderUrl + fileName;
            return helper.DynamicCssInclude(fullUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string DynamicCssInclude(this AjaxHelper helper, string url)
        {
            ResourceTracker tracker = new ResourceTracker(helper.ViewContext.HttpContext);
            if (tracker.Contains(url))
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var link=document.createElement('link')");
            sb.AppendLine("link.setAttribute('rel', 'stylesheet');");
            sb.AppendLine("link.setAttribute('type', 'text/css');");
            sb.AppendFormat("link.setAttribute('href', '{0}');", url);
            sb.AppendLine();
            sb.AppendLine("var head = document.getElementsByTagName('head')[0];");
            sb.AppendLine("head.appendChild(link);");
            return string.Format(CultureInfo.CurrentCulture, Constants.ScriptFormat, sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="clientType"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public static string Create(this AjaxHelper helper, string clientType, string elementId)
        {
            //TODO: Corrigir erro de parametro na linha abaixo.
            return Create(helper, clientType, new { }, elementId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="clientType"></param>
        /// <param name="props"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper" , Justification = "Extension Method")]
        public static string Create(this AjaxHelper helper, string clientType, object props, string elementId)
        {
            string strProps = ObjectToString(props);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sys.Application.add_init(function(){");
            sb.AppendFormat("$create({0},{1},null,null,$get('{2}'))", clientType, strProps, elementId);
            sb.AppendLine("});");
            return string.Format(CultureInfo.CurrentCulture, Constants.ScriptFormat, sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thing"></param>
        /// <returns></returns>
        private static string ObjectToString(object thing)
        {
            List<string> colProps = new List<string>();
            PropertyInfo[] props = thing.GetType().GetProperties();
            foreach (var prop in props)
            {
                var val = prop.GetValue(thing, null);
                colProps.Add(String.Format(CultureInfo.CurrentCulture, "{0}:{1}", prop.Name, prop.GetValue(thing, null)));
            }
            return "{" + String.Join(",", colProps.ToArray()) + "}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <returns></returns>
        public static string SetScriptCulture(this AjaxHelper helper) 
        {
            string cultureInfoScriptFormat = string.Format(CultureInfo.CurrentCulture, Constants.ScriptFormat, "var __cultureInfo = '{0}';");

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string cultureInfoJSON = jss.Serialize(new ClientCultureInfo(Thread.CurrentThread.CurrentUICulture));

            return ScriptExtensions.ScriptInclude(helper, "__cultureInfo", string.Format(CultureInfo.CurrentCulture, cultureInfoScriptFormat, cultureInfoJSON), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static string GetLocalizedExtenderBaseScript()
        {
            return "AjaxControlToolkit.ExtenderBase.BaseScripts." + Thread.CurrentThread.CurrentUICulture.Name + ".js";
        }
    }
}

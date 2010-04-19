using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;
using System.Web.UI;

namespace LiteFx.Web.Mvc.Extensions
{
    /// <summary>
    /// Extensões de scripts para facilitar o uso no MVC.
    /// </summary>
    public static class ScriptExtensions
    {
        /// <summary>
        /// Inclui referências a arquivos de script na página.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="url">Caminho do script.</param>
        /// <returns>String com as tags de referência a scripts.</returns>
        public static string ScriptInclude(this AjaxHelper helper, params string[] url)
        {
            ResourceTracker tracker = new ResourceTracker(helper.ViewContext.HttpContext);

            StringBuilder sb = new StringBuilder();
            foreach (string item in url)
            {
                if (!tracker.Contains(item))
                {
                    tracker.Add(item);
                    sb.AppendFormat("<script type='text/javascript' src='{0}'></script>", helper.ResolveUrl(item));
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Inclui um código de script na página.
        /// </summary>
        /// <param name="helper">Classe extendida.</param>
        /// <param name="key">Chave para controlar se o script já foi registrado na página.</param>
        /// <param name="script">Bloco de script que será inserido na página.</param>
        /// <param name="includeScriptTags">True para incluir as tags de script.</param>
        /// <returns>Script formatado</returns>
        public static string ScriptInclude(this AjaxHelper helper, string key, string script, bool includeScriptTags) 
        {
            ResourceTracker tracker = new ResourceTracker(helper.ViewContext.HttpContext);

            string scriptToWrite = string.Empty;

            if (!tracker.Contains(key)) 
            {
                tracker.Add(key);
                scriptToWrite = includeScriptTags ? string.Format(CultureInfo.CurrentCulture, Constants.ScriptFormat, script) : script;
            }

            return scriptToWrite;
        }

        private static string ResolveUrl(this AjaxHelper helper, string relativeUrl) 
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return null;

            if (!relativeUrl.StartsWith("~"))
                return relativeUrl;

            string basePath = helper.ViewContext.HttpContext.Request.ApplicationPath;

            string url = basePath + relativeUrl.Substring(1);
            return url.Replace("//", "/");
        }

    }
}

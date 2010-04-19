using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace LiteFx.Web.UI
{
    /// <summary>
    /// Página base do LiteFx
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {        
        #region Constantes
        const string Basic = "Basic";
        const string Rtl = "Rtl";
        const string Rel = "rel";
        const string ShortcutIcon = "shortcut icon";
        const string FavIco = "~/fav.ico";
        const string Lite = "Lite";
        #endregion

        #region Construtores e inicializadores
        /// <summary>
        /// Construtor da página base.
        /// </summary>
        public BasePage()
        {
            this.PreInit += new EventHandler(Page_PreInit);
            this.Load += new EventHandler(Page_Load);
        }

        /// <summary>
        /// Antes da página inicializar verificamos a direção do texto da cultura atual.
        /// </summary>
        /// <param name="sender">Página que acionou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = (System.Globalization.CultureInfo.CurrentCulture.TextInfo.IsRightToLeft ? Page.Theme + Rtl : Page.Theme);
        }

        /// <summary>
        /// No carregamento da página setamos um título padrão e o fav icon.
        /// </summary>
        /// <param name="sender">Página que acionou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            this.Header.Title = Lite;

            HtmlLink favIcon = new HtmlLink();
            favIcon.Attributes.Add(Rel, ShortcutIcon);
            favIcon.Href = FavIco;
            this.Header.Controls.Add(favIcon);
        }
        #endregion
 
        #region Javascript Helpers
        /// <summary>
        /// Formato para chamada da função alert do javascript.
        /// </summary>
        private const string alertFormat = "alert(\"{0}\");";

        /// <summary>
        /// Exibe uma menssagem no browser.
        /// </summary>
        /// <param name="key">Chave unica que identifica a mensagem.</param>
        /// <param name="message">Mensagem que será exibida no browser.</param>
        /// <example>
        /// <code lang="cs" title="Como utilizar o Alert.">
        /// <![CDATA[
        /// //No code-behind.
        /// this.Alert("alert", Properties.Resources.MyMessage);
        /// //Com isto um alert é exibido no browser.
        /// ]]>
        /// </code>
        /// </example>
        protected void Alert(string key, string message) 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), key, string.Format(alertFormat, message), true);
        }
        #endregion

        #region Query string Helpers
        /// <summary>
        /// Trata um valor do tipo <see cref="string"/> recebido via query string no <see cref="HttpRequest"/>.
        /// </summary>
        /// <param name="key">Chave da query string.</param>
        /// <param name="throwOnNull">Se o valor da query string for nulo uma exceção é disparada.</param>
        /// <returns>Valor resgatado pelo query string.</returns>
        protected string GetString(string key, bool throwOnNull)
        {
            string result = this.Request.QueryString[key];

            if (result == null)
            {
                if (throwOnNull)
                {
                    throw new Exception(string.Format(Properties.Resources.QueryStringNaoPodeSerNula, key));
                }
                else
                {
                    result = string.Empty;
                }
            }

            return this.Server.UrlDecode(result);
        }

        /// <summary>
        /// Trata um valor do tipo <see cref="int"/> recebido via query string no <see cref="HttpRequest"/>.
        /// </summary>
        /// <param name="key">Chave da query string.</param>
        /// <param name="throwOnNull">Se o valor da query string for nulo uma exceção é disparada.</param>
        /// <returns>Valor resgatado pelo query string.</returns>
        protected int? GetInt(string key, bool throwOnNull)
        {
            int? result = null;

            string value = GetString(key, throwOnNull);

            if (!string.IsNullOrEmpty(value))
            {
                result = int.Parse(value);
            }

            return result;
        }

        /// <summary>
        /// Trata um valor do tipo <see cref="double"/> recebido via query string no <see cref="HttpRequest"/>.
        /// </summary>
        /// <param name="key">Chave da query string.</param>
        /// <param name="throwOnNull">Se o valor da query string for nulo uma exceção é disparada.</param>
        /// <returns>Valor resgatado pelo query string.</returns>
        protected double? GetDouble(string key, bool throwOnNull)
        {
            double? result = null;

            string value = GetString(key, throwOnNull);

            if (!string.IsNullOrEmpty(value))
            {
                result = double.Parse(value);
            }

            return result;
        }

        /// <summary>
        /// Trata um valor do tipo <see cref="decimal"/> recebido via query string no <see cref="HttpRequest"/>.
        /// </summary>
        /// <param name="key">Chave da query string.</param>
        /// <param name="throwOnNull">Se o valor da query string for nulo uma exceção é disparada.</param>
        /// <returns>Valor resgatado pelo query string.</returns>
        protected decimal? GetDecimal(string key, bool throwOnNull)
        {
            decimal? result = null;

            string value = GetString(key, throwOnNull);

            if (!string.IsNullOrEmpty(value))
            {
                result = decimal.Parse(value);
            }

            return result;
        }
        #endregion

        #region CreatePageLink Pattern
        /// <summary>
        /// A WebPage que herdar da página base do LiteFx deve implementar este método.
        /// </summary>
        /// <returns>Link para a página.</returns>
        /// <example>
        /// <code lang="cs" title="Como implementar o método CreatePageLink adequadamente.">
        /// <![CDATA[
        /// //Digamos que este método está sendo criado na pagina de Clientes.
        /// public static string CreatePageLink(int ideCli)
        /// {
        ///     return string.Format("~/Clientes.aspx?IdeCli={0}", ideCli);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static string CreatePageLink() 
        {
            throw new NotImplementedException(Properties.Resources.OMetodoCreatePageLinkDeveSerImplementado);
        }
        #endregion
    }
}
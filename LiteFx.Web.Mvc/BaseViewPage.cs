using System;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI;

namespace LiteFx.Web.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseViewPage : ViewPage
    {
        #region Constantes
        const string Rtl = "RTL";
        const string Rel = "rel";
        const string ShortcutIcon = "shortcut icon";
        const string FavIco = "~/fav.ico";
        const string IeCSS = "~/Content/IE.css";
        const string Lite = "Lite";
        #endregion

        #region Construtores e inicializadores
        /// <summary>
        /// Construtor da página base.
        /// </summary>
        public BaseViewPage()
        {
            PreInit += Page_PreInit;
            Load += Page_Load;
        }

        /// <summary>
        /// Antes da página inicializar verificamos a direção do texto da cultura atual.
        /// </summary>
        /// <param name="sender">Página que acionou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void Page_PreInit(object sender, EventArgs e)
        {
            SetarTema(Page);
        }

        /// <summary>
        /// No carregamento da página setamos um título padrão e o fav icon.
        /// </summary>
        /// <param name="sender">Página que acionou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            SetarCabecalho(Header, Request);
        }
        #endregion

        internal static void SetarCabecalho(HtmlHead htmlHead, HttpRequest request) 
        {
            htmlHead.Title = Lite;

            HtmlLink favIcon = new HtmlLink();
            favIcon.Attributes.Add(Rel, ShortcutIcon);
            favIcon.Href = FavIco;
            htmlHead.Controls.Add(favIcon);

            if (request.Browser.Browser == "IE" && (request.Browser.Version == "6.0" || request.Browser.Version == "7.0"))
            {
                HtmlLink link = new HtmlLink();
                link.Href = IeCSS;
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("rel", "stylesheet");
                htmlHead.Controls.Add(link);
            }
        }

        internal static void SetarTema(Page page)
        {
            page.Theme = (System.Globalization.CultureInfo.CurrentCulture.TextInfo.IsRightToLeft ? page.Theme + Rtl : page.Theme);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class BaseViewPage<TModel> : ViewPage<TModel> where TModel : class
    {     
        #region Construtores e inicializadores
        /// <summary>
        /// Construtor da página base.
        /// </summary>
        public BaseViewPage()
        {
            PreInit += Page_PreInit;
            Load += Page_Load;
        }

        /// <summary>
        /// Antes da página inicializar verificamos a direção do texto da cultura atual.
        /// </summary>
        /// <param name="sender">Página que acionou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void Page_PreInit(object sender, EventArgs e)
        {
            BaseViewPage.SetarTema(Page);
        }

        /// <summary>
        /// No carregamento da página setamos um título padrão e o fav icon.
        /// </summary>
        /// <param name="sender">Página que acionou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            BaseViewPage.SetarCabecalho(Header, Request);
        }
        #endregion
    }
}

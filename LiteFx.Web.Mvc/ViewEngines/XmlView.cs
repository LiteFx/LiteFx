using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Compilation;
using System.Globalization;
using System.Web.Mvc;
using LiteFx.Web.Mvc.CustomActionResults;
using System.IO;

namespace LiteFx.Web.Mvc.ViewEngines
{
    public class XmlView : IView
    {

        public XmlView(string viewPath) : this(viewPath, null) { }

        public XmlView(string viewPath, string masterPath)
        {
            if (String.IsNullOrEmpty(viewPath))
            {
                throw new ArgumentException("Argument null or empty.", "viewPath");
            }

            ViewPath = viewPath;
            MasterPath = masterPath ?? String.Empty;
        }

        public string MasterPath
        {
            get;
            private set;
        }

        public string ViewPath
        {
            get;
            private set;
        }

        public virtual void Render(ViewContext viewContext, TextWriter writer)
        {
            viewContext.HttpContext.Response.Clear();
            viewContext.HttpContext.Response.ContentType = ContentType.Xml;

            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            object viewInstance = BuildManager.CreateInstanceFromVirtualPath(ViewPath, typeof(object));
            if (viewInstance == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentUICulture,
                        "Xml View Could Not Be Created",
                        ViewPath));
            }

            ViewPage viewPage = viewInstance as ViewPage;
            if (viewPage != null)
            {
                RenderViewPage(viewContext, viewPage);
                return;
            }

            ViewUserControl viewUserControl = viewInstance as ViewUserControl;
            if (viewUserControl != null)
            {
                RenderViewUserControl(viewContext, viewUserControl);
                return;
            }
            throw new InvalidOperationException(
                String.Format(
                    CultureInfo.CurrentUICulture,
                    "Wrong View Base",
                    ViewPath));
        }

        private void RenderViewPage(ViewContext context, ViewPage page)
        {
            if (!String.IsNullOrEmpty(MasterPath))
            {
                page.MasterLocation = MasterPath;
            }

            page.ViewData = context.ViewData;
            page.RenderView(context);
        }

        private void RenderViewUserControl(ViewContext context, ViewUserControl control)
        {
            if (!String.IsNullOrEmpty(MasterPath))
            {
                throw new InvalidOperationException("User Control Cannot Have Master");
            }

            control.ViewData = context.ViewData;
            control.RenderView(context);
        }
    }
}

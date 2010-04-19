using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Web.Compilation;
using System.Web;
using System.Net;

namespace LiteFx.Web.Mvc.ViewEngines
{
    public class XmlViewEngine : VirtualPathProviderViewEngine
    {
        public XmlViewEngine()
        {
            MasterLocationFormats = new[] {
                "~/Views/{1}/{0}.xml.master",
                "~/Views/Shared/{0}.xml.master"
            };

            ViewLocationFormats = new[] {
                "~/Views/{1}/{0}.xml",
                "~/Views/Shared/{0}.xml"
            };

            PartialViewLocationFormats = ViewLocationFormats;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new WebFormView(partialPath, null);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new WebFormView(viewPath, masterPath);
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions are interpreted as indicating that the file does not exist.")]
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            try
            {
                object viewInstance = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(object));

                return viewInstance != null;
            }
            catch (HttpException he)
            {
                if (he.GetHttpCode() == (int)HttpStatusCode.NotFound)
                {
                    // If BuildManager returns a 404 (Not Found) that means the file did not exist
                    return false;
                }
                else
                {
                    // All other error codes imply other errors such as compilation or parsing errors
                    throw;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

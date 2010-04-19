using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;

namespace LiteFx.Web.Mvc.Extensions.Behaviors
{
    public static class CalendarExtensions
    {
        public static string Calendar(this AjaxHelper helper, string elementId)
        {
            StringBuilder sb = new StringBuilder();

            // Add Microsoft Ajax library
            sb.AppendLine(helper.MicrosoftAjaxLibraryInclude());

            // Add toolkit scripts
            sb.AppendLine(helper.ToolkitInclude
                (
                    AjaxExtensions.GetLocalizedExtenderBaseScript(),
                    "AjaxControlToolkit.Common.Common.js",
                    "AjaxControlToolkit.Common.DateTime.js",
                    "AjaxControlToolkit.Animation.Animations.js",
                    "AjaxControlToolkit.PopupExtender.PopupBehavior.js",
                    "AjaxControlToolkit.Animation.AnimationBehavior.js",
                    "AjaxControlToolkit.Common.Threading.js",
                    "AjaxControlToolkit.Compat.Timer.Timer.js",
                    "AjaxControlToolkit.Calendar.CalendarBehavior.js"
                ));

            // Add Calendar CSS file
            sb.AppendLine(helper.DynamicToolkitCssInclude("AjaxControlToolkit.Calendar.Calendar.css"));

            // Perform $create
            sb.AppendLine(helper.Create("AjaxControlToolkit.CalendarBehavior", elementId));

            return sb.ToString();
        }

        public static string Calendar(this AjaxHelper helper, string elementId, string popupElementId) 
        {
            StringBuilder sb = new StringBuilder();

            // Add Microsoft Ajax library
            sb.AppendLine(helper.MicrosoftAjaxLibraryInclude());

            // Add toolkit scripts
            sb.AppendLine(helper.ToolkitInclude
                (
                    AjaxExtensions.GetLocalizedExtenderBaseScript(),
                    "AjaxControlToolkit.Common.Common.js",
                    "AjaxControlToolkit.Common.DateTime.js",
                    "AjaxControlToolkit.Animation.Animations.js",
                    "AjaxControlToolkit.PopupExtender.PopupBehavior.js",
                    "AjaxControlToolkit.Animation.AnimationBehavior.js",
                    "AjaxControlToolkit.Common.Threading.js",
                    "AjaxControlToolkit.Compat.Timer.Timer.js",
                    "AjaxControlToolkit.Calendar.CalendarBehavior.js"
                ));

            var props = new
            {
                button = string.Format(CultureInfo.CurrentCulture, "$get(\"{0}\")", popupElementId)
            }; 
            // Add Calendar CSS file
            sb.AppendLine(helper.DynamicToolkitCssInclude("AjaxControlToolkit.Calendar.Calendar.css"));

            // Perform $create
            sb.AppendLine(helper.Create("AjaxControlToolkit.CalendarBehavior", props, elementId));

            return sb.ToString();
        }
    }
}

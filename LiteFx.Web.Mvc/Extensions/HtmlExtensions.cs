using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Globalization;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using LiteFx.Web.Mvc.Models;

namespace LiteFx.Web.Mvc.Extensions
{
    /// <summary>
    /// Extensões para o Html do MVC.
    /// </summary>
    public static class HtmlExtensions
    {
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
        /// //Na View.
        /// Html.Alert(Resources.MyMessage);
        /// //Com isto um alert é exibido no browser.
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper", Justification = "Extension Method")]
        public static string Alert(this HtmlHelper htmlHelper, string message)
        {
            return string.Format(CultureInfo.CurrentCulture, Constants.ScriptFormat, string.Format(CultureInfo.CurrentCulture, alertFormat, message));
        }
        #endregion

        public static string HttpMethodOverride(this HtmlHelper helper, HttpVerbs httpVerb)
        {
            return string.Format("<input name=\"X-HTTP-Method-Override\" type=\"hidden\" value=\"{0}\" />", httpVerb.ToString().ToUpper());
        }

        private static string GetTextArea(HtmlHelper helper, string name, object value, Dictionary<string, object> dicHtmlAttributes)
        {
            string sValue = value == null ? string.Empty : value.ToString();

            try
            {
                string className = name.Split('.')[0];
                string propertyName = name.Split('.')[1].Split('_')[0];

                int maxlengthValue;
                bool isRequired = false;

                Type type = Type.GetType(string.Format("PortalSim.Model.{0}, PortalSim.Dal", className));
                PropertyInfo property = type.GetProperty(propertyName);

                if (property != null)
                {
                    object[] attributes;
                    attributes = property.GetCustomAttributes(typeof(StringLengthAttribute), true);
                    if (attributes.Length > 0)
                    {
                        StringLengthAttribute maxlength = (StringLengthAttribute)attributes[0];
                        maxlengthValue = maxlength.MaximumLength;

                        if (dicHtmlAttributes.ContainsKey("maxlength"))
                            dicHtmlAttributes["maxlength"] = maxlengthValue;
                        else
                            dicHtmlAttributes.Add("maxlength", maxlengthValue);

                        if (dicHtmlAttributes.ContainsKey("size"))
                            dicHtmlAttributes["size"] = (int)(maxlengthValue + (maxlengthValue * 0.1));
                        else
                            dicHtmlAttributes.Add("size", (int)(maxlengthValue + (maxlengthValue * 0.1)));
                    }

                    attributes = property.GetCustomAttributes(typeof(RequiredAttribute), true);

                    isRequired = attributes.Length > 0;
                }

                return helper.TextArea(name, sValue, dicHtmlAttributes) +
                       ("{0}") +
                       (isRequired ? "<SPAN class=\"field-validation-error\">*</SPAN>" : string.Empty);
            }
            catch (Exception ex)
            {
                return helper.TextArea(name, sValue, dicHtmlAttributes) + "{0}";
            }
        }

        private static string GetTextBox(HtmlHelper helper, string name, object value, Dictionary<string, object> dicHtmlAttributes)
        {
            try
            {
                string className = name.Split('.')[0];
                string propertyName = name.Split('.')[1].Split('_')[0];

                int maxlengthValue;
                bool isRequired = false;

                Type type = Type.GetType(string.Format("PortalSim.Model.{0}, PortalSim.Dal", className));
                PropertyInfo property = type.GetProperty(propertyName);

                if (property != null)
                {
                    object[] attributes;
                    attributes = property.GetCustomAttributes(typeof(StringLengthAttribute), true);
                    if (attributes.Length > 0)
                    {
                        StringLengthAttribute maxlength = (StringLengthAttribute)attributes[0];
                        maxlengthValue = maxlength.MaximumLength;

                        if (maxlengthValue > 100)
                            GetTextArea(helper, name, value, dicHtmlAttributes);

                        if (dicHtmlAttributes.ContainsKey("maxlength"))
                            dicHtmlAttributes["maxlength"] = maxlengthValue;
                        else
                            dicHtmlAttributes.Add("maxlength", maxlengthValue);

                        if (dicHtmlAttributes.ContainsKey("size"))
                            dicHtmlAttributes["size"] = (int)(maxlengthValue + (maxlengthValue * 0.1));
                        else
                            dicHtmlAttributes.Add("size", (int)(maxlengthValue + (maxlengthValue * 0.1)));
                    }

                    attributes = property.GetCustomAttributes(typeof(RequiredAttribute), true);

                    isRequired = attributes.Length > 0;
                }

                return helper.TextBox(name, value, dicHtmlAttributes) +
                       ("{0}") +
                       (isRequired ? "<SPAN class=\"field-validation-error\">*</SPAN>" : string.Empty);
            }
            catch (Exception ex)
            {
                return helper.TextBox(name, value, dicHtmlAttributes) + "{0}";
            }
        }

        public static string Combo(this HtmlHelper helper, string name, object value, string imgTitle)
        {
            return helper.Combo(name, value, null, imgTitle);
        }
        public static string Combo(this HtmlHelper helper, string name, object value, object htmlAttributes, string imgTitle)
        {
            return helper.Combo(name, value, htmlAttributes, imgTitle, null);
        }
        public static string Combo(this HtmlHelper helper, string name, object value, object htmlAttributes, string imgTitle, bool? accessDenied)
        {
            Dictionary<string, object> dicHtmlAttributes = new Dictionary<string, object>();
            if (htmlAttributes != null)
            {
                Type htmlAttributesType = htmlAttributes.GetType();
                foreach (PropertyInfo prop in htmlAttributesType.GetProperties())
                    dicHtmlAttributes.Add(prop.Name, prop.GetValue(htmlAttributes, null));
            }

            if (!accessDenied.HasValue && helper.ViewData.Model is ISecurity)
                accessDenied = ((ISecurity)helper.ViewData.Model).AccessDenied;

            if (accessDenied.HasValue && accessDenied.Value)
            {
                dicHtmlAttributes.Add("readonly", true);
                if (dicHtmlAttributes.ContainsKey("class"))
                    dicHtmlAttributes["class"] += " readonly";
                else
                    dicHtmlAttributes.Add("class", "readonly");
            }

            return string.Format(GetTextBox(helper, name, value, dicHtmlAttributes), string.Format("<div class=\"autocomplete\" id=\"{0}Btn\" title=\"{1}\"></div>", name.Replace('.', '_'), imgTitle));
        }

        public static string Date(this HtmlHelper helper, string name, object value, string imgTitle)
        {
            return helper.Date(name, value, null, imgTitle);
        }
        public static string Date(this HtmlHelper helper, string name, object value, object htmlAttributes, string imgTitle)
        {
            return helper.Date(name, value, htmlAttributes, imgTitle, null);
        }
        public static string Date(this HtmlHelper helper, string name, object value, object htmlAttributes, string imgTitle, bool? accessDenied)
        {
            Dictionary<string, object> dicHtmlAttributes = new Dictionary<string, object>();
            if (htmlAttributes != null)
            {
                Type htmlAttributesType = htmlAttributes.GetType();
                foreach (PropertyInfo prop in htmlAttributesType.GetProperties())
                    dicHtmlAttributes.Add(prop.Name, prop.GetValue(htmlAttributes, null));
            }

            if (!accessDenied.HasValue && helper.ViewData.Model is ISecurity)
                accessDenied = ((ISecurity)helper.ViewData.Model).AccessDenied;

            if (accessDenied.HasValue && accessDenied.Value)
            {
                dicHtmlAttributes.Add("readonly", true);
                if (dicHtmlAttributes.ContainsKey("class"))
                    dicHtmlAttributes["class"] += " readonly";
                else
                    dicHtmlAttributes.Add("class", "readonly");
            }

            return string.Format(GetTextBox(helper, name, value, dicHtmlAttributes), string.Format("<div class=\"autocompleteCalendar\" id=\"{0}Btn\" title=\"{1}\"></div>", name.Replace('.', '_'), imgTitle));
        }

        public static string TextAreaFor(this HtmlHelper helper, string name, object value)
        {
            return helper.TextAreaFor(name, value, null);
        }
        public static string TextAreaFor(this HtmlHelper helper, string name, object value, object htmlAttributes)
        {
            return helper.TextAreaFor(name, value, htmlAttributes, null);
        }
        public static string TextAreaFor(this HtmlHelper helper, string name, object value, object htmlAttributes, bool? accessDenied)
        {
            Dictionary<string, object> dicHtmlAttributes = new Dictionary<string, object>();
            if (htmlAttributes != null)
            {
                Type htmlAttributesType = htmlAttributes.GetType();
                foreach (PropertyInfo prop in htmlAttributesType.GetProperties())
                    dicHtmlAttributes.Add(prop.Name, prop.GetValue(htmlAttributes, null));
            }

            if (!accessDenied.HasValue && helper.ViewData.Model is ISecurity)
                accessDenied = ((ISecurity)helper.ViewData.Model).AccessDenied;

            if (accessDenied.HasValue && accessDenied.Value)
            {
                dicHtmlAttributes.Add("readonly", true);
                if (dicHtmlAttributes.ContainsKey("class"))
                    dicHtmlAttributes["class"] += " readonly";
                else
                    dicHtmlAttributes.Add("class", "readonly");
            }

            return string.Format(GetTextArea(helper, name, value, dicHtmlAttributes), string.Empty);
        }

        public static string TextBoxFor(this HtmlHelper helper, string name, object value)
        {
            return helper.TextBoxFor(name, value, null);
        }
        public static string TextBoxFor(this HtmlHelper helper, string name, object value, object htmlAttributes)
        {
            return helper.TextBoxFor(name, value, htmlAttributes, null);
        }
        public static string TextBoxFor(this HtmlHelper helper, string name, object value, object htmlAttributes, bool? accessDenied)
        {
            Dictionary<string, object> dicHtmlAttributes = new Dictionary<string, object>();
            if (htmlAttributes != null)
            {
                Type htmlAttributesType = htmlAttributes.GetType();
                foreach (PropertyInfo prop in htmlAttributesType.GetProperties())
                    dicHtmlAttributes.Add(prop.Name, prop.GetValue(htmlAttributes, null));
            }

            if (!accessDenied.HasValue && helper.ViewData.Model is ISecurity)
                accessDenied = ((ISecurity)helper.ViewData.Model).AccessDenied;

            if (accessDenied.HasValue && accessDenied.Value)
            {
                dicHtmlAttributes.Add("readonly", true);
                if (dicHtmlAttributes.ContainsKey("class"))
                    dicHtmlAttributes["class"] += " readonly";
                else
                    dicHtmlAttributes.Add("class", "readonly");
            }

            return string.Format(GetTextBox(helper, name, value, dicHtmlAttributes), string.Empty);
        }

        public static string EditableDiv(this HtmlHelper helper, string name, object value, string label)
        {
            return helper.EditableDiv(name, value, label, null);
        }
        public static string EditableDiv(this HtmlHelper helper, string name, object value, string label, bool? accessDenied)
        {
            if (!accessDenied.HasValue && helper.ViewData.Model is ISecurity)
                accessDenied = ((ISecurity)helper.ViewData.Model).AccessDenied;

            TagBuilder tbInput = new TagBuilder("input");
            tbInput.Attributes.Add("name", name);
            tbInput.Attributes.Add("id", name.Replace(".", "_"));
            tbInput.Attributes.Add("type", "hidden");
            tbInput.Attributes.Add("value", (string)value);

            TagBuilder tbDiv = new TagBuilder("div");
            tbDiv.AddCssClass("FakeTextArea");

            if (accessDenied.HasValue && !accessDenied.Value)
                tbDiv.Attributes.Add("contentEditable", "true");
            else
                tbDiv.AddCssClass("readonly");

            tbDiv.Attributes.Add("onBlur", string.Format(" $(\"#{0}\").val($(this).text())", name.Replace(".", "_")));
            tbDiv.InnerHtml = (string)value;

            ModelState modelState;
            if (helper.ViewData.ModelState.TryGetValue(name, out modelState))
                if (modelState.Errors.Count > 0)
                    tbDiv.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            string className = name.Split('.')[0];
            string propertyName = name.Split('.')[1].Split('_')[0];
            Type type = Type.GetType(string.Format("PortalSim.Model.{0}, PortalSim.Dal", className));

            bool isRequired;
            isRequired = type.GetProperty(propertyName).GetCustomAttributes(typeof(RequiredAttribute), true).Length > 0;

            TagBuilder tbLabel = new TagBuilder("label")
            {
                InnerHtml = string.Format("{0} {1} {2} {3}", label, isRequired ? "<SPAN class=\"field-validation-error\">*</SPAN>" : string.Empty, tbDiv, tbInput)
            };

            return tbLabel.ToString();
        }

        public static string CheckBoxFor(this HtmlHelper helper, string name, bool isChecked)
        {
            return helper.CheckBoxFor(name, isChecked, null);
        }
        public static string CheckBoxFor(this HtmlHelper helper, string name, bool isChecked, object htmlAttributes)
        {
            return helper.CheckBoxFor(name, isChecked, htmlAttributes, null);
        }
        public static string CheckBoxFor(this HtmlHelper helper, string name, bool isChecked, object htmlAttributes, bool? accessDenied)
        {
            Dictionary<string, object> dicHtmlAttributes = new Dictionary<string, object>();
            if (htmlAttributes != null)
            {
                Type htmlAttributesType = htmlAttributes.GetType();
                foreach (PropertyInfo prop in htmlAttributesType.GetProperties())
                    dicHtmlAttributes.Add(prop.Name, prop.GetValue(htmlAttributes, null));
            }

            if (!accessDenied.HasValue && helper.ViewData.Model is ISecurity)
                accessDenied = ((ISecurity)helper.ViewData.Model).AccessDenied;

            if (accessDenied.HasValue && accessDenied.Value)
                dicHtmlAttributes.Add("disabled", true);

            return helper.CheckBox(name, isChecked, dicHtmlAttributes);
        }
    }
}
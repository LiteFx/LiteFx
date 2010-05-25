using System;
using System.Linq;
using System.Reflection;

namespace LiteFx.Web.Mvc.CustomActionResults
{
    /// <summary>
    /// Métodos de auxilio dos ActionResults Customizados.
    /// </summary>
    public static class ActionResultHelper
    {
        public static string[] MontaCabecalho(object firstRow)
        {
            string[] _headers;
            Type type = firstRow.GetType();

            /*if (firstRow is System.Data.Objects.DataClasses.EntityObject)
                _headers = type.GetProperties().Where(p => VerificaAtributo(p)).Select(p => p.Name).ToArray();
            else*/
            //Não voltaremos a usar o entity da forma acima

            _headers = type.GetProperties().Where(p => VerificaAtributoPoco(p)).Select(p => p.Name).ToArray();

            return _headers;
        }

        public static bool VerificaAtributo(PropertyInfo pf)
        {
            if (pf.Name == "UPDTME")
                return false;

            object[] attributes = pf.GetCustomAttributes(typeof(System.Data.Objects.DataClasses.EdmScalarPropertyAttribute), true);

            if (attributes.Length == 0)
                return false;

            return !((System.Data.Objects.DataClasses.EdmScalarPropertyAttribute)attributes[0]).EntityKeyProperty;
        }

        public static bool VerificaAtributoPoco(PropertyInfo pf)
        {
            return !(pf.IsDefined(typeof(HideFromViewAttribute), true));
        }

    }

    /// <summary>
    /// Constantes com content types usados pelos custom results.
    /// </summary>
    public static class ContentType
    {
        public const string Pdf = "application/pdf";
        public const string Excel = "application/ms-excel";
        public const string Csv = "text/csv";
        public const string Xml = "text/xml";
        public const string Doc = "application/msword";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace LiteFx.Web.Mvc.ActionResults
{
    public static class ActionResultHelper
    {
        public static string[] MontaCabecalho(object firstRow)
        {
            string[] _headers;
            Type type = firstRow.GetType();
            _headers = type.GetProperties().Where(p => IsScaffoldColumn(p)).Select(p => p.Name).ToArray();

            return _headers;
        }

        public static bool IsScaffoldColumn(PropertyInfo pf)
        {
            if (pf.IsDefined(typeof(ScaffoldColumnAttribute), true))
            {
                ScaffoldColumnAttribute scaffold = (ScaffoldColumnAttribute)pf.GetCustomAttributes(typeof(ScaffoldColumnAttribute), true)[0];
                return scaffold.Scaffold;
            }

            return false;
        }

    }

}

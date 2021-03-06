﻿using System.Web.Mvc;
using System.Xml.Serialization;

namespace LiteFx.Web.Mvc.ActionResults
{
    /// <summary>
    /// Retorna um objeto serializado no formato xml.
    /// </summary>
    public class XmlResult : ActionResult
    {
        /// <summary>
        /// Objeto que será serializado.
        /// </summary>
        private object _objectToSerialize;

        /// <summary>
        /// Cria uma nova instancia do XmlResult.
        /// </summary>
        /// <param name="objectToSerialize">Objeto que será serializado.</param>
        public XmlResult(object objectToSerialize)
        {
            _objectToSerialize = objectToSerialize;
        }

        /// <summary>
        /// Serialises the object that was passed into the constructor 
        /// to XML and writes the corresponding XML to the result stream.
        /// </summary>
        public override void ExecuteResult(ControllerContext context)
        {
            if (_objectToSerialize != null)
            {
                var xs = new XmlSerializer(_objectToSerialize.GetType());
                context.HttpContext.Response.ContentType = ContentType.Xml.ToString();
                xs.Serialize(context.HttpContext.Response.Output, _objectToSerialize);
            }
        }
    }
}

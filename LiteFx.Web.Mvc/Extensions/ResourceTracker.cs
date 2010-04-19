using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;

namespace LiteFx.Web.Mvc.Extensions
{
    /// <summary>
    /// Rastreador de recursos utilizados.
    /// </summary>
    public class ResourceTracker
    {
        /// <summary>
        /// Chave da classe que será utilizada no contexto da aplicação.
        /// </summary>
        const string resourceKey = "__resources";

        /// <summary>
        /// Lista com os recursos utilizados
        /// </summary>
        private List<string> resources;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="context">Contexto HTTP para salvar a lista.</param>
        public ResourceTracker(HttpContextBase context)
        {
            resources = (List<string>)context.Items[resourceKey];
            if (resources == null)
            {
                resources = new List<string>();
                context.Items[resourceKey] = resources;
            }
        }

        /// <summary>
        /// Adiciona uma referência na lista.
        /// </summary>
        /// <param name="key">Chave a ser adicionada.</param>
        public void Add(string key)
        {
            key = key.ToLower(CultureInfo.CurrentCulture);
            resources.Add(key);
        }

        
        /// <summary>
        /// Verifica se uma chave existe na lista atual.
        /// </summary>
        /// <param name="key">Chave que será verificada.</param>
        /// <returns>Retorna true se a chave foi encontrada e false para caso não.</returns>
        public bool Contains(string key)
        {
            key = key.ToLower(CultureInfo.CurrentCulture);
            return resources.Contains(key);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;

namespace LiteFx.Util
{
    public static class EnumUtil
    {
        /// <summary>
        /// Transforma uma enum em um Dictionary&lt;int, string&gt;. O texto da Enum é recuperado através de um resource.
        /// </summary>
        /// <param name="enumerator">Enumerador que será transformado.</param>
        /// <param name="resourceSource">Recurso onde os textos serão recuperados.</param>
        /// <returns>Retorna um Dictionary&lt;int, string&gt; construido baseado em uma enum.</returns>
        public static Dictionary<int, string> EnumToDictionary(Enum enumerator, Type resourceSource)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();

            string[] nomes = Enum.GetNames(enumerator.GetType());

            int[] valores = (int[])(Enum.GetValues(enumerator.GetType()));

            ResourceManager resm = new ResourceManager(resourceSource);

            for (int i = 0; i < valores.Length; i++) dic.Add(valores[i], resm.GetString(nomes[i]));

            return dic.OrderBy(d => d.Value).ToDictionary(d => d.Key, d => d.Value);
        }

        /// <summary>
        /// Transforma uma enum em um Dictionary&lt;int, string&gt;. O texto da Enum é recuperado através de um resource.
        /// </summary>
        /// <param name="enumerator">Enumerador que será transformado.</param>
        /// <param name="resourceSource">Recurso onde os textos serão recuperados.</param>
        /// <returns>Retorna um Dictionary&lt;int, string&gt; construido baseado em uma enum.</returns>
        public static Dictionary<int, string> ToDictionary(this Enum enumerator, Type resourceSource) 
        {
            return EnumUtil.EnumToDictionary(enumerator, resourceSource);
        }

        /// <summary>
        /// Retorna o texto da Enum recuperado através de um resource.
        /// </summary>
        /// <param name="enumerator">Enumerador que será transformado.</param>
        /// <param name="resourceSource">Recurso onde os textos serão recuperados.</param>
        /// <returns>String do Enum baseado no Resource</returns>
        public static string ToString(this Enum enumerator, Type resourceSource)
        {
            string nome = Enum.GetName(enumerator.GetType(), enumerator);
            ResourceManager resm = new ResourceManager(resourceSource);

            return resm.GetString(nome);
        }
    }
}
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace LiteFx.Cryptography.Util
{
    /// <summary>
    /// Utilitarios para criptografia.
    /// </summary>
    public static class CryptographyUtil
    {
        
        /// <summary>
        /// Cria um "sal" para reforçar valores criptografados com Hash.
        /// </summary>
        /// <param name="length">Quantidade de bytes contidos no sal.</param>
        /// <returns>Sal gerado.</returns>
        public static string CreateSalt(int length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[length];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
    }
}

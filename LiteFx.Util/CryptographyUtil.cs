using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Password.
        /// </summary>
        private const string password = "l41TfExRul3z";

        /// <summary>
        /// Salt.
        /// </summary>
        private const string stringForSalt = "S0m3ti3xtf0r1t";

        /// <summary>
        /// Encripta um texto utilizando o algoritimo Rijndael.
        /// </summary>
        /// <param name="textToEncrypt">Texto a ser criptografado.</param>
        /// <returns>Texto criptografado.</returns>
        public static string Encrypt(string textToEncrypt) 
        {
            string encryptedText = string.Empty;

            RijndaelManaged rijn = new RijndaelManaged();

            byte[] salt = Encoding.ASCII.GetBytes(stringForSalt);
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);

            rijn.Key = key.GetBytes(rijn.KeySize / 8);
            rijn.IV = key.GetBytes(rijn.BlockSize / 8);

            ICryptoTransform encryptor = rijn.CreateEncryptor();

            MemoryStream outMemoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(outMemoryStream, encryptor, CryptoStreamMode.Write);

            byte[] buffer = Encoding.ASCII.GetBytes(textToEncrypt);

            cryptoStream.Write(buffer, 0, buffer.Length);

            cryptoStream.FlushFinalBlock();

            encryptedText = Convert.ToBase64String(outMemoryStream.ToArray());

            cryptoStream.Close();
            outMemoryStream.Close();

            return encryptedText;
        }

        /// <summary>
        /// Descriptografa um texto criptografado por esta classe.
        /// </summary>
        /// <param name="textToDecrypt">Texto a ser descriptografado.</param>
        /// <returns>Texto descriptografado.</returns>
        public static string Decrypt(string textToDecrypt)
        {
            string decryptedText = string.Empty;

            RijndaelManaged rijn = new RijndaelManaged();

            byte[] salt = Encoding.ASCII.GetBytes(stringForSalt);
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);

            rijn.Key = key.GetBytes(rijn.KeySize / 8);
            rijn.IV = key.GetBytes(rijn.BlockSize / 8);

            ICryptoTransform decryptor = rijn.CreateDecryptor(rijn.Key,rijn.IV);

            byte[] bytesToDecrypt =  Convert.FromBase64String(textToDecrypt);
            MemoryStream inMemoryStream = new MemoryStream(bytesToDecrypt);

            CryptoStream cryptoStream = new CryptoStream(inMemoryStream, decryptor, CryptoStreamMode.Read);

            StreamReader streamReader = new StreamReader(cryptoStream);

            decryptedText = streamReader.ReadToEnd();

            streamReader.Close();
            cryptoStream.Close();
            inMemoryStream.Close();

            return decryptedText;
        }
    }
}

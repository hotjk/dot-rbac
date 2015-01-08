using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Utility.Security
{
    public class RSAManager
    {
        public RSAManager(string key)
        {
            _key = key;
        }
        private string _key;

        public static void GenerateKeyAndIV(out string publicKey, out string privateKey)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                publicKey = RSA.ToXmlString(false);
                privateKey = RSA.ToXmlString(true);
            }
        }

        public byte[] Encrypt(byte[] bytesToBeEncrypted)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(_key);
                var encryptedBytes = RSA.Encrypt(bytesToBeEncrypted, false);
                return encryptedBytes;
            }
        }

        public byte[] Decrypt(byte[] bytesToBeDecrypted)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(_key);
                var decryptedBytes = RSA.Decrypt(bytesToBeDecrypted, false);
                return decryptedBytes;
            }
        }

        public string Encrypt(string text)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(text);
            byte[] encryptedBytes = Encrypt(bytesToBeEncrypted);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string text)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(text);
            byte[] decryptedBytes = Decrypt(bytesToBeDecrypted);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

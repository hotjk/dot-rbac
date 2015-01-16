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

        public byte[] Decrypt(string text)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(text);
            byte[] decryptedBytes = Decrypt(bytesToBeDecrypted);
            return decryptedBytes;
            //return Encoding.UTF8.GetString(decryptedBytes);
        }

        public string PrivateEncrypt(byte[] bytesToBeEncrypted)
        {
            //byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(text);
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(_key);
                byte[] encryptedBytes = RSA.PrivareEncryption(bytesToBeEncrypted);
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public byte[] PublicDecrypt(string text)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(text);
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(_key);
                byte[] decryptedBytes = RSA.PublicDecryption(bytesToBeDecrypted);
                return decryptedBytes;
                //return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Utility.Security
{
    public static class EnvelopeService
    {
        public static Envelope Encrypt(string id, string source, string publicKey)
        {
            Envelope envelope = new Envelope { Id = id };
            byte[] key, iv;
            RijndaelManager.GenerateKeyAndIV(out key, out iv);
            RijndaelManager aes = new RijndaelManager(key, iv);
            envelope.Data = aes.Encrypt(source);

            RSAManager rsa = new RSAManager(publicKey);
            envelope.Key = Convert.ToBase64String(rsa.Encrypt(key));
            envelope.IV = Convert.ToBase64String(rsa.Encrypt(iv));
            return envelope;
        }

        public static string Decrypt(Envelope envelope, string privateKey)
        {
            RSAManager rsa = new RSAManager(privateKey);
            byte[] key = rsa.Decrypt(envelope.Key);
            byte[] iv = rsa.Decrypt(envelope.IV);
            RijndaelManager aes = new RijndaelManager(key, iv);
            return aes.Decrypt(envelope.Data);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class SecurityKey
    {
        public SecurityKey(string privateKey, string publicKey)
        {
            this.PrivateKey = privateKey;
            this.PublicKey = publicKey;
        }

        public string PrivateKey { get; private set; }
        public string PublicKey { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class Envelope
    {
        public int Client { get; set; }
        public string EncryptedKey { get; set; }
        public string EncryptedIV { get; set; }
        public string Data { get; set; }
    }
}

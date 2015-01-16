using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Client
{
    public class ClientSettingsRequest
    {
        public ClientSettingsRequest(string client, string path)
        {
            this.Client = client;
            this.Path = path;
        }
        public string Client { get; set; }
        public string Path { get; set; }
    }
}

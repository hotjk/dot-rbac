using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class ClientSettings
    {
        public class Entry
        {
            public string Path { get; set; }
            public string Value { get; set; }
        }

        public ClientSettings(string name)
        {
            this.Client = name;
            this.Entries = new List<Entry>(10);
        }
        public string Client { get; set; }
        public List<Entry> Entries { get; set; }
    }
}

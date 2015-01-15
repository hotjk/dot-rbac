using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class SettingsEntry
    {
        public string Path { get; set; }
        public string Value { get; set; }
    }

    public class SettingsResponse
    {
        public string Client { get; set; }
        public List<SettingsEntry> Entries { get; set; }
    }
}

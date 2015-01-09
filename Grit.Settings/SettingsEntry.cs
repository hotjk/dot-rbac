using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Settings
{
    public class SettingsEntry
    {
        public string Key { get; private set; }
        public Version Version { get; private set; }
        public SettingsType Type { get; private set; }
        public string Comments { get; set; }

        public object _value;
    }
}

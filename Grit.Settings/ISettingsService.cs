using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Settings
{
    public interface ISettingsService
    {
        public ICollection<SettingsEntry> Find(string[] keys, Version version);
        public ICollection<SettingsEntry> FindAll(Version version);
        public bool Update(SettingsEntry entry);
        public bool Delete(string[] keys, Version version);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Settings
{
    public interface ISettingsService
    {
        ICollection<SettingsEntry> Find(string[] keys, Version version);
        ICollection<SettingsEntry> FindAll(Version version);
        bool Update(SettingsEntry entry);
        bool Delete(string[] keys, Version version);
    }
}

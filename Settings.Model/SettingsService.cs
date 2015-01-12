using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class SettingsService
    {
        public bool UpdateNode(int id, string name, IList<Entry> entries)
        {
            return true;
        }

        public bool DeleteNode(int id)
        {
            return true;
        }

        public IList<Node> GetNodes()
        {
            return null;
        }

        public IList<Node> GetNodes(int client)
        {
            return null;
        }
    }
}

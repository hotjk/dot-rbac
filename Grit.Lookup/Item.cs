using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Lookup
{
    public class Item
    {
        public Item(int lookup, int id, string value, string comments = null)
        {
            this.Lookup = lookup;
            this.Id = id;
            this.Value = value;
            this.Comments = comments;
        }

        public int Lookup { get; private set; }
        public int Id { get; private set; }
        public string Value { get; private set; }
        public string Comments { get; private set; }
        public IList<Item> Children { get; private set; }
    }
}

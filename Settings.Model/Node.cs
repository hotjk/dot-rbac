using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public IList<Entry> Entries { get; set; }
    }
}

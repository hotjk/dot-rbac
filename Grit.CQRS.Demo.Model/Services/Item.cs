using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Services
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; internal set; }
        public string Description { get; internal set; }
    }
}

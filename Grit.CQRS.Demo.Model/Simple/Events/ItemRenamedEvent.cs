using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Events
{
    public class ItemRenamedEvent : Event
    {
        public string Title { get; internal set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Events
{
    public class ItemCreatedEvent : Event
    {
        public string Title { get; internal set; }
        public string Description { get; internal set; }

        public ItemCreatedEvent(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}

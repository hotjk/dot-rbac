using Grit.CQRS.Demo.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.EventHandlers
{
    public class ItemCreatedEventHandler : IEventHandler<ItemCreatedEvent>
    {
        public void Handle(ItemCreatedEvent handle)
        {
            Console.WriteLine(this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}

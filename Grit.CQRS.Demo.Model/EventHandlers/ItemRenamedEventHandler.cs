using Grit.CQRS.Demo.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.EventHandlers
{
    public class ItemRenamedEventHandler : IEventHandler<ItemRenamedEvent>
    {
        public void Handle(ItemRenamedEvent handle)
        {
            Console.WriteLine(this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}

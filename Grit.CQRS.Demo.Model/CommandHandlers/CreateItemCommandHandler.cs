using Grit.CQRS.Demo.Model.Commands;
using Grit.CQRS.Demo.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.CommandHandlers
{
    public class CreateItemCommandHandler : ICommandHandler<CreateItemCommand>
    {
        public CreateItemCommandHandler()
        {
            Console.WriteLine("CreateItemCommandHandler");
        }
        public void Execute(CreateItemCommand command)
        {
            Console.WriteLine(this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            ServiceLocator.EventBus.Publish(
                new ItemCreatedEvent
                {
                    Title = "Title",
                    Description = "Description"
                });
        }
    }
}

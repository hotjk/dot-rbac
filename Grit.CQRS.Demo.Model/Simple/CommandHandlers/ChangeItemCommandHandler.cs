using Grit.CQRS.Demo.Model.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.CommandHandlers
{
    public class ChangeItemCommandHandler : ICommandHandler<ChangeItemCommand>
    {
        public void Execute(ChangeItemCommand command)
        {
            Console.WriteLine(this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}

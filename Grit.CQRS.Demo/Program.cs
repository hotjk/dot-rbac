using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Grit.CQRS.Demo.Model.Commands;
using Grit.CQRS.Demo.Model.Events;
using Grit.CQRS.Demo.Model;

namespace Grit.CQRS.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            BootStrapper.BootStrap();
            ServiceLocator.CommandBus.Send(
                new CreateItemCommand
                {
                    Title = "Title",
                    Description = "Description"
                });
            ServiceLocator.CommandBus.Send(
                new CreateItemCommand
                {
                    Title = "Title",
                    Description = "Description"
                });
        }
    }
}

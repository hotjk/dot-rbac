using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Grit.CQRS.Demo.Model.Commands;
using Grit.CQRS.Demo.Model.Events;
using Grit.CQRS.Demo.Model;
using Grit.Sequence;
using Grit.Configuration;
using Grit.CQRS.Demo.Model.Investments.Events;

namespace Grit.CQRS.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            BootStrapper.BootStrap();
            //BasicTest();
            InvestTest();
        }

        private static void BasicTest()
        {
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

        private static void InvestTest()
        {
            int accountId = 1;
            int projectId = 1;
            ISequenceService sequenceService = BootStrapper.Kernel.Get<ISequenceService>();

            InvestmentCreateCommand command = new InvestmentCreateCommand
            {
                AccountId = accountId,
                ProjectId = projectId,
                InvestmentId = sequenceService.Next((int)SequenceID.CQRS_Investment, 1),
                Amount = 10000
            };
            ServiceLocator.CommandBus.Send(command);
        }
    }
}

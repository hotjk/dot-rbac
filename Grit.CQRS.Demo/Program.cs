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
using Grit.CQRS.Demo.Contracts.Commands;
using Grit.CQRS.Demo.Model.Accounts;
using Grit.CQRS.Demo.Model.Projects;
using Grit.CQRS.Demo.Model.Investments;

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
            IAccountService accountService = BootStrapper.Kernel.Get<IAccountService>();
            IProjectService projectService = BootStrapper.Kernel.Get<IProjectService>();
            IInvestmentService investmentService = BootStrapper.Kernel.Get<IInvestmentService>();

            if (accountService.Get(accountId) == null)
            {
                ServiceLocator.CommandBus.Send(new InitAccountCommand
                {
                    AccountId = accountId
                });
            }

            if(projectService.Get(projectId) == null)
            {
                ServiceLocator.CommandBus.Send(new InitProjectCommand
                {
                    ProjectId = projectId,
                    Name = "Test Project"
                });
            }


            ServiceLocator.CommandBus.Send(new InvestmentCreateCommand
            {
                AccountId = accountId,
                ProjectId = projectId,
                InvestmentId = sequenceService.Next((int)SequenceID.CQRS_Investment, 1),
                Amount = 100.00m
            });
        }
    }
}

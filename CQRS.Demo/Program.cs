using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using CQRS.Demo.Model;
using Grit.Sequence;
using CQRS.Demo.Contracts.Commands;
using CQRS.Demo.Model.Accounts;
using CQRS.Demo.Model.Projects;
using CQRS.Demo.Model.Investments;
using CQRS.Demo.Contracts;

namespace Grit.CQRS.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //EnsoureAssemblyLoaded.Pike();
            BootStrapper.BootStrap();
            //BasicTest();
            InvestTest();
        }
        public enum SequenceID
        {
            CQRS_Account = 100,
            CQRS_Project = 101,
            CQRS_Investment = 102,
        }

        private static void InvestTest()
        {
            int accountId = 1;
            int projectId = 1;
            ISequenceService sequenceService = BootStrapper.Kernel.Get<ISequenceService>();
            IAccountService accountService = BootStrapper.Kernel.Get<IAccountService>();
            IProjectService projectService = BootStrapper.Kernel.Get<IProjectService>();
            IInvestmentService investmentService = BootStrapper.Kernel.Get<IInvestmentService>();

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

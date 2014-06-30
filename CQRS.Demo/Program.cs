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
using System.Transactions;
using CQRS.Demo.Contracts.Events;
using Grit.CQRS.Exceptions;

namespace Grit.CQRS.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            // Pike a dummy method to ensoure Command/Event assembly been loaded
            EnsoureAssemblyLoaded.Pike();

            BootStrapper.BootStrap();
            
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
            ISequenceService sequenceService = BootStrapper.Kernel.Get<ISequenceService>();
            IAccountService accountService = BootStrapper.Kernel.Get<IAccountService>();
            IProjectService projectService = BootStrapper.Kernel.Get<IProjectService>();

            int accountId = 1;
            int projectId = 1;
            decimal amount = 100.00m;
            int investmentId = sequenceService.Next((int)SequenceID.CQRS_Investment, 1);

            //var account = accountService.Get(accountId);
            //if (account.Amount < amount)
            //{
            //    Console.WriteLine("No money.");
            //}

            //var project = projectService.Get(projectId);
            //if(project.Amount < amount)
            //{
            //    Console.WriteLine("No space.");
            //}

            try
            {
                using (TransactionScope scope = new TransactionScope(
                    TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead }))
                {
                    ServiceLocator.CommandBus.Send(new CreateInvestmentCommand
                    {
                        AccountId = accountId,
                        ProjectId = projectId,
                        InvestmentId = investmentId,
                        Amount = amount
                    });
                    scope.Complete();
                }
            }
            catch(BusinessException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                using (TransactionScope scope = new TransactionScope(
                    TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead }))
                {
                    ServiceLocator.CommandBus.Send(new CompleteInvestmentCommand
                    {
                        InvestmentId = investmentId
                    });
                    scope.Complete();
                }
            }
            catch (BusinessException ex)
            {
                Console.WriteLine(ex.Message);
            }

            BootStrapper.Dispose();
        }
    }
}

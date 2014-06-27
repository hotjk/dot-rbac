using CQRS.Demo.Model;
using CQRS.Demo.Model.Accounts;
using CQRS.Demo.Model.Investments;
using CQRS.Demo.Model.Projects;
using CQRS.Demo.Repositories;
using CQRS.Demo.Repositories.Write;
using Grit.Sequence;
using Grit.Sequence.Repository.MySql;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo
{
    public static class BootStrapper
    {
        public static IKernel Kernel { get; private set; }

        public static void BootStrap()
        {
            AddIocBindings();
            InitHandlerFactory();
            InitServiceLocator();
        }

        private static void AddIocBindings()
        {
            Kernel = new StandardKernel();

            Kernel.Bind<ISequenceRepository>().To<SequenceRepository>().InSingletonScope();
            Kernel.Bind<ISequenceService>().To<SequenceService>().InSingletonScope();

            Kernel.Bind<ICommandHandlerFactory>().To<CommandHandlerFactory>().InSingletonScope();
            Kernel.Bind<ICommandBus>().To<CommandBus>().InSingletonScope();
            Kernel.Bind<IEventHandlerFactory>().To<EventHandlerFactory>().InSingletonScope();
            Kernel.Bind<IEventBus>().To<EventBus>().InSingletonScope();

            Kernel.Bind<IInvestmentRepository>().To<InvestmentRepository>().InSingletonScope();
            Kernel.Bind<IInvestmentWriteRepository>().To<InvestmentWriteRepository>().InSingletonScope();
            Kernel.Bind<IInvestmentService>().To<InvestmentService>().InSingletonScope();
            Kernel.Bind<IProjectRepository>().To<ProjectRepository>().InSingletonScope();
            Kernel.Bind<IProjectWriteRepository>().To<ProjectWriteRepository>().InSingletonScope();
            Kernel.Bind<IProjectService>().To<ProjectService>().InSingletonScope();
            Kernel.Bind<IAccountRepository>().To<AccountRepository>().InSingletonScope();
            Kernel.Bind<IAccountWriteRepository>().To<AccountWriteRepository>().InSingletonScope();
            Kernel.Bind<IAccountService>().To<AccountService>().InSingletonScope();
        }

        private static void InitHandlerFactory()
        {
            CommandHandlerFactory.Init(Kernel, new string[] { "CQRS.Demo.Contracts" },
                new string[] { "CQRS.Demo.Model.Write" });
            EventHandlerFactory.Init(Kernel, new string[] { "CQRS.Demo.Contracts" },
                new string[] { "CQRS.Demo.Model.Write", "CQRS.Demo" });
        }

        private static void InitServiceLocator()
        {
            ServiceLocator.Init(Kernel);
        }
    }
}

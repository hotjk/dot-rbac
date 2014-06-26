using Grit.CQRS.Demo.Model;
using Grit.CQRS.Demo.Model.Accounts;
using Grit.CQRS.Demo.Model.CommandHandlers;
using Grit.CQRS.Demo.Model.EventHandlers;
using Grit.CQRS.Demo.Model.Investments;
using Grit.CQRS.Demo.Model.Projects;
using Grit.CQRS.Demo.Repository.Read;
using Grit.CQRS.Demo.Repository.Write;
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

            Kernel.Bind<CreateItemCommandHandler>().ToSelf().InSingletonScope();
            Kernel.Bind<ChangeItemCommandHandler>().ToSelf().InSingletonScope();
            Kernel.Bind<ItemCreatedEventHandler>().ToSelf().InSingletonScope();
            Kernel.Bind<ItemRenamedEventHandler>().ToSelf().InSingletonScope();

            Kernel.Bind<IInvestmentRepository>().To<InvestmentRepository>().InSingletonScope();
            Kernel.Bind<IInvestmentWriteRepository>().To<InvestmentWriteRepository>().InSingletonScope();
            Kernel.Bind<IProjectRepository>().To<ProjectRepository>().InSingletonScope();
            Kernel.Bind<IProjectWriteRepository>().To<ProjectWriteRepository>().InSingletonScope();
            Kernel.Bind<IAccountRepository>().To<AccountRepository>().InSingletonScope();
            Kernel.Bind<IAccountWriteRepository>().To<AccountWriteRepository>().InSingletonScope();
        }

        private static void InitHandlerFactory()
        {
            CommandHandlerFactory.Init(Kernel, new string[] { "Grit.CQRS.Demo.Model" });
            EventHandlerFactory.Init(Kernel, new string[] { "Grit.CQRS.Demo.Model" });
        }

        private static void InitServiceLocator()
        {
            ServiceLocator.Init(Kernel);
        }
    }
}

using Grit.CQRS.Demo.Model;
using Grit.CQRS.Demo.Model.CommandHandlers;
using Grit.CQRS.Demo.Model.EventHandlers;
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

            Kernel.Bind<ICommandHandlerFactory>().To<CommandHandlerFactory>().InSingletonScope();
            Kernel.Bind<ICommandBus>().To<CommandBus>().InSingletonScope();
            Kernel.Bind<IEventHandlerFactory>().To<EventHandlerFactory>().InSingletonScope();
            Kernel.Bind<IEventBus>().To<EventBus>().InSingletonScope();

            Kernel.Bind<CreateItemCommandHandler>().ToSelf().InSingletonScope();
            Kernel.Bind<ChangeItemCommandHandler>().ToSelf().InSingletonScope();
            Kernel.Bind<ItemCreatedEventHandler>().ToSelf().InSingletonScope();
            Kernel.Bind<ItemRenamedEventHandler>().ToSelf().InSingletonScope();
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

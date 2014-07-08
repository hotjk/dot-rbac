using Grit.CQRS;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public sealed class ServiceLocator
    {
        public static IKernel Kernel { get; private set; }
        public static ICommandBus CommandBus { get; private set; }
        public static IEventBus EventBus
        {
            get
            {
                return Kernel.GetService(typeof(IEventBus)) as IEventBus;
            }
        }

        public static IActionBus ActionBus
        {
            get
            {
                return Kernel.GetService(typeof(IActionBus)) as IActionBus;
            }
        }

        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        public static void Init(IKernel kernel)
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    Kernel = kernel;

                    Kernel.Bind<ICommandHandlerFactory>().To<CommandHandlerFactory>().InSingletonScope();
                    Kernel.Bind<ICommandBus>().To<CommandBus>().InSingletonScope();
                    Kernel.Bind<IEventHandlerFactory>().To<EventHandlerFactory>().InSingletonScope();
                    // EventBus must be thread scope, published events will be saved in thread EventBus._events, until FlushAll/Clear.
                    Kernel.Bind<IEventBus>().To<EventBus>().InThreadScope();
                    Kernel.Bind<IActionHandlerFactory>().To<ActionHandlerFactory>().InSingletonScope();
                    // ActionBus must be thread scope, single thread bind to use single anonymous RabbitMQ queue for reply.
                    Kernel.Bind<IActionBus>().To<ActionBus>().InThreadScope(); 

                    CommandBus = kernel.Get<ICommandBus>();
                    _isInitialized = true;
                }
            }
        }
    }
}

using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model
{
    public sealed class ServiceLocator
    {
        public static IKernel Kernel { get; private set; }
        public static ICommandBus CommandBus { get; private set; }
        public static IEventBus EventBus { get; private set; }

        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        public static void Init(IKernel kernel)
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    Kernel = kernel;
                    CommandBus = kernel.Get<ICommandBus>();
                    EventBus = kernel.Get<IEventBus>();
                    _isInitialized = true;
                }
            }
        }
    }
}

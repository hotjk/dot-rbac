using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.ServiceBus.Demo
{
    public static class BootStrapper
    {
        public static IKernel Kernel { get; private set; }
        public static Bus Bus { get; private set; }

        public static void BootStrap()
        {
            Bus = new Bus();
            AddIocBindings();
        }

        private static void AddIocBindings()
        {
            Kernel = new StandardKernel();

            // 1. Service instance must be singletion;
            // 2. Service instance must existed before send message or publish event to it.
            Kernel.Bind<ITestService1>().To<TestService1>().InSingletonScope().OnActivation(
                (ctx, instance) =>
                {
                    Bus.RegisterHandler<TestCommand>(instance.Handle);
                    Bus.RegisterHandler<TestEvent2>(instance.Handle);
                });
            BootStrapper.Kernel.Get<ITestService1>();

            Kernel.Bind<ITestService2>().To<TestService2>().InSingletonScope().OnActivation(
                (ctx, instance) =>
                {
                    Bus.RegisterHandler<TestEvent1>(instance.Handle);
                });
            BootStrapper.Kernel.Get<ITestService2>();
        }
    }
}

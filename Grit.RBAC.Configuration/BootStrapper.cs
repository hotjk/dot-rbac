using Grit.RBAC.Repository.MySql;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC.Configuration
{
    public static class BootStrapper
    {
        public static IKernel Kernel { get; private set; }

        public static void BootStrap(IKernel kernel)
        {
            Kernel = kernel;
            AddIocBindings();
        }

        private static void AddIocBindings()
        {
            Kernel.Bind<IRBACRepository>().To<RBACRepository>().InSingletonScope();
            Kernel.Bind<IRBACService>().To<RBACService>().InSingletonScope();

            Kernel.Bind<IRBACWriteRepository>().To<RBACWriteRepository>().InSingletonScope();
            Kernel.Bind<IRBACWriteService>().To<RBACWriteService>().InSingletonScope();
        }
    }
}

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

        public static void BootStrap()
        {
            AddIocBindings();
        }

        private static void AddIocBindings()
        {
            Kernel = new StandardKernel();
            Kernel.Bind<IRBACRepository>().To<RBACRepository>().InSingletonScope();
            Kernel.Bind<IRBACService>().To<RBACService>().InSingletonScope();
        }
    }
}

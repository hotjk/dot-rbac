using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Grit.Sequence;
using Grit.Sequence.Repository.MySql;
using Grit.Tree;
using Grit.Tree.Repository.MySql;
using Grit.Core.Data;
using System.Configuration;

namespace Grit.RBAC.Demo.Web.App_Start
{
    public static class BootStrapper
    {
        public static Ninject.IKernel NinjectContainer { get; private set; }
        public static void BootStrap()
        {
            NinjectContainer = new StandardKernel();
            AddIoCBindings();
            Grit.RBAC.Configuration.BootStrapper.BootStrap(NinjectContainer);
        }

        private static void AddIoCBindings()
        {
            var treeConnectionStringProvider = new ConnectionStringProvider(ConfigurationManager.ConnectionStrings["Tree.MySql"].ConnectionString);
            var sequenceConnectionStringProvider = new ConnectionStringProvider(ConfigurationManager.ConnectionStrings["Sequence.MySql"].ConnectionString);

            NinjectContainer.Bind<ISequenceRepository>().To<SequenceRepository>().InSingletonScope()
                .WithConstructorArgument<IConnectionStringProvider>(sequenceConnectionStringProvider);
            NinjectContainer.Bind<ISequenceService>().To<SequenceService>().InSingletonScope();

            NinjectContainer.Bind<ITreeRepository>().To<TreeRepository>().InSingletonScope()
                .WithConstructorArgument<IConnectionStringProvider>(treeConnectionStringProvider);
            NinjectContainer.Bind<ITreeService>().To<TreeService>()
                .InSingletonScope()
                .Named("Tree")
                .WithConstructorArgument("table", "tree");
        }
    }
}
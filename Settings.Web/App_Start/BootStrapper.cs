using Grit.Core.Data;
using Grit.Sequence;
using Grit.Sequence.Repository.MySql;
using Grit.Tree;
using Grit.Tree.Repository.MySql;
using Ninject;
using Settings.Model;
using Settings.Repository.MySql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Settings.Web
{
    public class BootStrapper
    {
        public static Ninject.IKernel NinjectContainer { get; private set; }
        public static void BootStrap()
        {
            NinjectContainer = new StandardKernel();
            AddIoCBindings();
        }

        private static void AddIoCBindings()
        {
            var settingsConnectionStringProvider = new ConnectionStringProvider(ConfigurationManager.ConnectionStrings["Settings.MySql"].ConnectionString);
            var sequenceConnectionStringProvider = new ConnectionStringProvider(ConfigurationManager.ConnectionStrings["Sequence.MySql"].ConnectionString);

            NinjectContainer.Bind<ISequenceRepository>().To<SequenceRepository>().InSingletonScope()
                .WithConstructorArgument<IConnectionStringProvider>(sequenceConnectionStringProvider);
            NinjectContainer.Bind<ISequenceService>().To<SequenceService>().InSingletonScope();

            NinjectContainer.Bind<ITreeRepository>().To<TreeRepository>().InSingletonScope()
                .WithConstructorArgument<IConnectionStringProvider>(settingsConnectionStringProvider);
            NinjectContainer.Bind<ITreeService>().To<TreeService>().InSingletonScope()
                .WithConstructorArgument("table", "settings_tree");

            NinjectContainer.Bind<INodeRepository>().To<NodeRepository>().InSingletonScope()
                .WithConstructorArgument<IConnectionStringProvider>(settingsConnectionStringProvider);
            NinjectContainer.Bind<INodeService>().To<NodeService>().InSingletonScope();

            NinjectContainer.Bind<IClientRepository>().To<ClientRepository>().InSingletonScope()
                .WithConstructorArgument<IConnectionStringProvider>(settingsConnectionStringProvider);
            NinjectContainer.Bind<IClientService>().To<ClientService>().InSingletonScope();

            NinjectContainer.Bind<IUserRepository>().To<UserRepository>().InSingletonScope()
                .WithConstructorArgument<IConnectionStringProvider>(settingsConnectionStringProvider);
            NinjectContainer.Bind<IUserService>().To<UserService>().InSingletonScope();

            NinjectContainer.Bind<ISqlRepository>().To<SqlRepository>().InSingletonScope()
                .WithConstructorArgument<IConnectionStringProvider>(settingsConnectionStringProvider);
            NinjectContainer.Bind<ISqlService>().To<SqlService>().InSingletonScope();
        }
    }
}
using Grit.Sequence;
using Grit.Sequence.Repository.MySql;
using Grit.Tree;
using Grit.Tree.Repository.MySql;
using Ninject;
using Settings.Model;
using Settings.Repository.MySql;
using System;
using System.Collections.Generic;
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
            NinjectContainer.Bind<ISequenceRepository>().To<SequenceRepository>().InSingletonScope();
            NinjectContainer.Bind<ISequenceService>().To<SequenceService>().InSingletonScope();
            NinjectContainer.Bind<ITreeRepository>().To<TreeRepository>().InSingletonScope();
            NinjectContainer.Bind<ITreeService>().To<TreeService>().InSingletonScope()
                .WithConstructorArgument("table", "settings_tree");

            NinjectContainer.Bind<INodeRepository>().To<NodeRepository>().InSingletonScope();
            NinjectContainer.Bind<INodeService>().To<NodeService>().InSingletonScope();
            NinjectContainer.Bind<IClientRepository>().To<ClientRepository>().InSingletonScope();
            NinjectContainer.Bind<IClientService>().To<ClientService>().InSingletonScope();
            NinjectContainer.Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
            NinjectContainer.Bind<IUserService>().To<UserService>().InSingletonScope();
        }
    }
}
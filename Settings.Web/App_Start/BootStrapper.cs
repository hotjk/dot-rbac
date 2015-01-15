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

namespace Settings.Web.App_Start
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

            NinjectContainer.Bind<ISettingsRepository>().To<SettingsRepository>().InSingletonScope();
            NinjectContainer.Bind<ISettingsService>().To<SettingsService>().InSingletonScope();
        }
    }
}
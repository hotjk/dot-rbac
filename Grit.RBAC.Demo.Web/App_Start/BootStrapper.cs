using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Grit.Tree;
using Grit.Tree.Repository.MySql;
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
            var treeSqlOption = new Grit.Tree.Repository.MySql.SqlOption
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["Tree.MySql"].ConnectionString,
                Table = "tree"
            };

            NinjectContainer.Bind<ITreeRepository>().To<TreeRepository>().InSingletonScope()
                .WithConstructorArgument<Grit.Tree.Repository.MySql.SqlOption>(treeSqlOption);
            NinjectContainer.Bind<ITreeService>().To<TreeService>().InSingletonScope();
        }
    }
}
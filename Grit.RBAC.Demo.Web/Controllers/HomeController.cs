using Grit.Tree;
using Grit.Tree.ViewModel;
using Grit.Utility.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grit.RBAC.Demo.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRBACService RBACService { get; set; }
        private ITreeService TreeService { get; set; }
        public HomeController(IRBACService rbacService,
            ITreeService treeService)
        {
            RBACService = rbacService;
            TreeService = treeService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Group()
        {
            var permissions = RBACService.GetPermissions();
            var root = TreeService.GetTree(1);
            var builder = new JsTreeConverter<Permission>(x => x.Name, x => x.PermissionId);
            ViewBag.Tree = builder.Build(root, permissions).children;
            return View();
        }

        [HttpPost]
        public ActionResult Group([ModelBinder(typeof(JsonNetModelBinder))] IList<JsTreeNode> nodes)
        {
            return new JsonNetResult(nodes);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
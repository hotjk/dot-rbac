using Grit.Tree;
using Grit.Tree.JsTree;
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
            var root = TreeService.GetTree(8);
            ViewBag.Tree = new JsTreeBuilder<Permission>(x => x.Name, x => x.PermissionId)
                .Build(root, permissions)
                .children;
            return View();
        }

        [HttpPost]
        public ActionResult Group([ModelBinder(typeof(JsonNetModelBinder))] IList<JsTreeNode> nodes)
        {
            var root = new JsTreeParser().Parse(8, nodes);
            TreeService.SaveTree(root);
            return new JsonNetResult(nodes);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
using Grit.Tree;
using Grit.Tree.JsTree;
using Grit.Tree.UITree;
using Grit.Utility.Web.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grit.RBAC.Demo.Web.Controllers
{
    public class OtherController : Controller
    {
        private IRBACService RBACService { get; set; }
        private IRBACWriteService RBACWriteService { get; set; }
        private ITreeService TreeService { get; set; }

        public OtherController(IRBACService rbacService,
            IRBACWriteService rbacWriteService,
            ITreeService treeService)
        {
            RBACService = rbacService;
            RBACWriteService = rbacWriteService;
            TreeService = treeService;
        }

        [HttpGet]
        public ActionResult Lookup()
        {
            var permissions = RBACService.GetPermissions();
            var root = TreeService.GetTree(Constants.PERMISSION_TREE_ID);
            ViewBag.Tree = new UITreeBuilder<Permission>(x => x.Name, x => x.PermissionId)
                .Build(root, permissions);

            ViewBag.jsTree = new JsTreeBuilder<Permission>(x => x.Name, x => x.PermissionId)
                .Build(root, permissions);

            return View();
        }

        [HttpGet]
        public ActionResult LookupLite()
        {
            var permissions = RBACService.GetPermissions();
            var root = TreeService.GetTree(Constants.PERMISSION_TREE_ID);
            ViewBag.Tree = new UITreeBuilder<Permission>(x => x.Name, x => x.PermissionId)
                .Build(root, permissions);
            return View();
        }
    }
}
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
    public class HomeController : Controller
    {
        private IRBACService RBACService { get; set; }
        private ITreeService TreeService { get; set; }
        public HomeController(IRBACService rbacService,
            [Ninject.Named("Tree")] ITreeService treeService)
        {
            RBACService = rbacService;
            TreeService = treeService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Lookup()
        {
            var permissions = RBACService.GetPermissions();
            var root = TreeService.GetTree(8);
            ViewBag.Tree = new UITreeBuilder<Permission>(x => x.Name, x => x.PermissionId)
                .Build(root, permissions);

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

        [HttpGet]
        public ActionResult Map()
        {
            var roles = RBACService.GetRolesWithPermission();
            var leftTree = TreeService.GetTree(7);
            ViewBag.LeftTree = new JsTreeBuilder<Role>(x => x.Name, x => x.RoleId, x=>x.Permissions.Select(n=>n.PermissionId))
                .Build(leftTree, roles)
                .children;

            var permissions = RBACService.GetPermissions();
            var rightTree = TreeService.GetTree(8);
            ViewBag.RightTree = new JsTreeBuilder<Permission>(x => x.Name, x => x.PermissionId)
                .Build(rightTree, permissions)
                .children;

            return View();
        }

        [HttpPost]
        public ActionResult Map([ModelBinder(typeof(JsonNetModelBinder))] IList<JsTreeNode> nodes)
        {
            var root = new JsTreeParser().Parse(7, nodes);
            var roles = RBACService.GetRoles();
            var permissions = RBACService.GetPermissions();
            root.Each(x =>
            {
                if(x.Elements != null)
                {
                    var role = roles.SingleOrDefault(n => n.RoleId == x.Id);
                    if (role != null)
                    {
                        var rolePermissions = permissions.Where(n => x.Elements.Any(m => m == n.PermissionId));
                        role.Add(rolePermissions);
                    }
                }
            });
            RBACService.SaveRolePermissions(roles);
            return new JsonNetResult(nodes);
        }
    }
}
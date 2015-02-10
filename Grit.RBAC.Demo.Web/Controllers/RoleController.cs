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
    public class RoleController : Controller
    {
        private IRBACService RBACService { get; set; }
        private IRBACWriteService RBACWriteService { get; set; }
        private ITreeService TreeService { get; set; }
        private const int PERMISSION_TREE_ID = 8;

        public RoleController(IRBACService rbacService,
            IRBACWriteService rbacWriteService,
            ITreeService treeService)
        {
            RBACService = rbacService;
            RBACWriteService = rbacWriteService;
            TreeService = treeService;
        }

        [HttpGet]
        public ActionResult Group()
        {
            var roles = RBACService.GetRoles();
            var root = TreeService.GetTree(Constants.ROLE_TREE_ID);
            ViewBag.Tree = new JsTreeBuilder<Role>(x => x.Name, x => x.RoleId)
                .Build(root, roles)
                .children;
            return View();
        }

        [HttpPost]
        public ActionResult Group([ModelBinder(typeof(JsonNetModelBinder))] IList<JsTreeNode> nodes)
        {
            var root = new JsTreeParser().Parse(Constants.ROLE_TREE_ID, nodes);
            TreeService.SaveTree(root);
            return new JsonNetResult(nodes);
        }

        [HttpGet]
        public ActionResult Map()
        {
            var roles = RBACService.GetRoles(true);
            var leftTree = TreeService.GetTree(Constants.ROLE_TREE_ID);
            ViewBag.LeftTree = new JsTreeBuilder<Role>(x => x.Name, x => x.RoleId, x=>x.Permissions.Select(n=>n.PermissionId))
                .Build(leftTree, roles)
                .children;

            var permissions = RBACService.GetPermissions();
            var rightTree = TreeService.GetTree(Constants.PERMISSION_TREE_ID);
            ViewBag.RightTree = new JsTreeBuilder<Permission>(x => x.Name, x => x.PermissionId)
                .Build(rightTree, permissions)
                .children;

            return View();
        }

        [HttpPost]
        public ActionResult Map([ModelBinder(typeof(JsonNetModelBinder))] IList<JsTreeNode> nodes)
        {
            var root = new JsTreeParser().Parse(Constants.ROLE_TREE_ID, nodes);
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
            RBACWriteService.SaveRolePermissions(roles);
            return new JsonNetResult(nodes);
        }
    }
}
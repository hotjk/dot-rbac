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
    public class SubjectController : Controller
    {
        private IRBACService RBACService { get; set; }
        private IRBACWriteService RBACWriteService { get; set; }
        private ITreeService TreeService { get; set; }

        public SubjectController(IRBACService rbacService,
            IRBACWriteService rbacWriteService,
            ITreeService treeService)
        {
            RBACService = rbacService;
            RBACWriteService = rbacWriteService;
            TreeService = treeService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var subjects = RBACService.GetSubjects();
            var root = TreeService.GetTree(Constants.SUBJECT_TREE_ID);
            var jsTree = new JsTreeBuilder<Subject>(x => x.Name, x => x.SubjectId)
                .Build(root, subjects);
            ViewBag.Tree = jsTree.children;
            return View();
        }

        [HttpGet]
        public ActionResult Role(int id)
        {
            var subject = RBACService.GetSubject(id, true, false, false);
            var roles = RBACService.GetRoles();

            var treeRole = TreeService.GetTree(Constants.ROLE_TREE_ID);
            var jsTreeRole = new JsTreeBuilder<Role>(x => x.Name, x => x.RoleId)
                .Build(treeRole, roles);
            jsTreeRole.Each(x =>
            {
                if (x == jsTreeRole) return;
                x.state.disabled = true;
                if (subject.HaveRole(x.data.content))
                {
                    x.state.selected = true;
                }
            });

            return new JsonNetResult(jsTreeRole.children);
        }

        [HttpGet]
        public ActionResult Permission(int id)
        {
            var subject = RBACService.GetSubject(id, true, true, true);
            var permissions = RBACService.GetPermissions();

            var treePermission = TreeService.GetTree(Constants.PERMISSION_TREE_ID);
            var jsTreePermission = new JsTreeBuilder<Permission>(x => x.Name, x => x.PermissionId)
                .Build(treePermission, permissions);
            jsTreePermission.Each(x =>
            {
                if (x == jsTreePermission) return;
                x.state.disabled = true;
                if (subject.HavePermission(x.data.content))
                {
                    x.state.selected = true;
                }
            });

            return new JsonNetResult(jsTreePermission.children);
        }

        [HttpGet]
        public ActionResult Group()
        {
            var subjects = RBACService.GetSubjects();
            var root = TreeService.GetTree(Constants.SUBJECT_TREE_ID);
            ViewBag.Tree = new JsTreeBuilder<Subject>(x => x.Name, x => x.SubjectId)
                .Build(root, subjects)
                .children;
            return View();
        }

        [HttpPost]
        public ActionResult Group([ModelBinder(typeof(JsonNetModelBinder))] IList<JsTreeNode> nodes)
        {
            var root = new JsTreeParser().Parse(Constants.SUBJECT_TREE_ID, nodes);
            TreeService.SaveTree(root);
            return new JsonNetResult(nodes);
        }

        [HttpGet]
        public ActionResult RolePermission(int id)
        {
            var subject = RBACService.GetSubject(id, true, false, true);
            ViewBag.SubjectId = subject.SubjectId;

            var roles = RBACService.GetRoles();
            var treeRole = TreeService.GetTree(Constants.ROLE_TREE_ID);
            var jsTreeRole = new JsTreeBuilder<Role>(x => x.Name, x => x.RoleId)
                .Build(treeRole, roles);
            jsTreeRole.Each(x =>
            {
                if (x == jsTreeRole) return;
                if (subject.HaveRole(x.data.content))
                {
                    x.state.selected = true;
                }
            });
            ViewBag.RoleTree = jsTreeRole.children;

            var permissions = RBACService.GetPermissions();
            var treePermission = TreeService.GetTree(Constants.PERMISSION_TREE_ID);
            var jsTreePermission = new JsTreeBuilder<Permission>(x => x.Name, x => x.PermissionId)
                .Build(treePermission, permissions);
            jsTreePermission.Each(x =>
            {
                if (x == jsTreePermission) return;
                if (subject.HaveDirectPermission(x.data.content))
                {
                    x.state.selected = true;
                }
            });
            ViewBag.PermissionTree = jsTreePermission.children;

            return View();
        }

        [HttpPost]
        public ActionResult RolePermission(int id, int[][] array)
        {
            var subject = RBACService.GetSubject(id, false, false, false);
            var roles = RBACService.GetRoles();
            foreach(var roleId in array[0])
            {
                var found = roles.First(n=>n.RoleId == roleId);
                if(found != null)
                {
                    subject.Add(found);
                }
            }
            var permissions = RBACService.GetPermissions();
            foreach(var permissionId in array[1])
            {
                var found = permissions.First(n=>n.PermissionId == permissionId);
                if(found != null)
                {
                    subject.Add(found);
                }
            }
            RBACWriteService.SaveSubjectRoles(subject);
            RBACWriteService.SaveSubjectPermissions(subject);

            return new JsonNetResult(new object());
        }
    }
}
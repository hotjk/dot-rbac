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
    }
}
﻿using Grit.Tree;
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
    public class PermssionController : Controller
    {
        private IRBACService RBACService { get; set; }
        private IRBACWriteService RBACWriteService { get; set; }
        private ITreeService TreeService { get; set; }

        public PermssionController(IRBACService rbacService,
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
            var permissions = RBACService.GetPermissions();
            var root = TreeService.GetTree(Constants.PERMISSION_TREE_ID);
            ViewBag.Tree = new JsTreeBuilder<Permission>(x => x.Name, x => x.PermissionId)
                .Build(root, permissions)
                .children;
            return View();
        }

        [HttpPost]
        public ActionResult Group([ModelBinder(typeof(JsonNetModelBinder))] IList<JsTreeNode> nodes)
        {
            var root = new JsTreeParser().Parse(Constants.PERMISSION_TREE_ID, nodes);
            TreeService.SaveTree(root);
            return new JsonNetResult(nodes);
        }
    }
}
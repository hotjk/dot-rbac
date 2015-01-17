using Grit.Utility.Security;
using Settings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settings.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(Grit.Tree.ITreeService treeService,
            ISettingsService settingsService)
        {
            this.SettingsService = settingsService;
            this.TreeService = treeService;
        }

        private Grit.Tree.ITreeService TreeService { get; set; }
        private ISettingsService SettingsService { get; set; }

        public ActionResult Index()
        {
            var nodes = SettingsService.GetNodes();
            var root = TreeService.GetTree(Constants.TREE_NODE);
            ViewBag.Tree = new Grit.Tree.JsTree.JsTreeBuilder<Node>(
                x => x.Name,
                x => x.NodeId)
                .Build(root, nodes)
                .children;

            return View();
        }
    }
}
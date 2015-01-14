using Grit.Sequence;
using Grit.Tree.JsTree;
using Grit.Utility.Web.Json;
using Settings.Model;
using Settings.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settings.Web.Controllers
{
    public class NodeController : ControllerBase
    {
        public NodeController(ISequenceService sequenceService,
            Grit.Tree.ITreeService treeService,
            INodeService nodeService)
        {
            this.SequenceService = sequenceService;
            this.NodeService = nodeService;
            this.TreeService = treeService;
        }

        private ISequenceService SequenceService { get; set; }
        private Grit.Tree.ITreeService TreeService { get; set; }
        private INodeService NodeService { get; set; }

        

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id.HasValue)
            {
                Node node = NodeService.GetNode(id.Value);
                NodeVM vm = NodeVM.FromModel(node);
                return View(vm);
            }
            return View(NodeVM.FromModel());
        }

        [HttpPost]
        public ActionResult Edit(NodeVM vm)
        {
            if (!ModelState.IsValid || !vm.IsValid(ModelState))
            {
                return View(vm);
            }

            var node = NodeVM.ToModel(vm);
            node.UpdateAt = DateTime.Now;
            if (node.NodeId == 0)
            {
                node.NodeId = SequenceService.Next(Constants.SEQUENCE_SETTINGS, 1);
                node.CreateAt = node.UpdateAt;
            }

            if(!NodeService.UpdateNode(node))
            {
                ModelState.AddModelError(string.Empty, 
                    "保存失败，编辑过程中可能其他用户已经编辑了节点的数据");
                return View(vm);
            }

            Info = "保存成功";
            return RedirectToAction("Edit", new { id = node.NodeId });
        }

        [HttpGet]
        public ActionResult Group()
        {
            var nodes = NodeService.GetNodes();
            var root = TreeService.GetTree(Constants.TREE_NODE);

            ViewBag.Tree = new Grit.Tree.JsTree.JsTreeBuilder<Node>(x => x.Name, x => x.NodeId)
                .Build(root, nodes)
                .children;

            return View();
        }

        [HttpPost]
        public ActionResult Group([ModelBinder(typeof(JsonNetModelBinder))] IList<JsTreeNode> nodes)
        {
            var root = new JsTreeParser().Parse(Constants.TREE_NODE, nodes);
            TreeService.SaveTree(root);
            return new JsonNetResult(nodes);
        }
    }
}
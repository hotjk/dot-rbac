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
    public class ClientController : ControllerBase
    {
        public ClientController(ISequenceService sequenceService,
            Grit.Tree.ITreeService treeService,
            ISettingsService settingsService)
        {
            this.SequenceService = sequenceService;
            this.SettingsService = settingsService;
            this.TreeService = treeService;
        }

        private ISequenceService SequenceService { get; set; }
        private Grit.Tree.ITreeService TreeService { get; set; }
        private ISettingsService SettingsService { get; set; }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Settings.Model.Client client = SettingsService.GetClient(id.Value);
                if (client == null)
                {
                    return new HttpNotFoundResult("客户不存在");
                }
                ClientVM vm = ClientVM.FromModel(client);
                return View(vm);
            }
            return View(ClientVM.FromModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ClientVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var client = ClientVM.ToModel(vm);
            client.UpdateAt = DateTime.Now;
            if (client.ClientId == 0)
            {
                client.ClientId = SequenceService.Next(Constants.SEQUENCE_SETTINGS_CLIENT, 1);
                client.CreateAt = client.UpdateAt;
            }

            if(!SettingsService.UpdateClient(client))
            {
                ModelState.AddModelError(string.Empty, 
                    "保存失败，编辑过程中可能其他用户已经编辑了客户的数据");
                return View(vm);
            }

            Info = "保存成功";
            return RedirectToAction("Edit", new { id = client.ClientId });
        }

        [HttpGet]
        public ActionResult Map()
        {
            var clients = SettingsService.GetClients();
            var leftTree = new Grit.Tree.Node(1);

            ViewBag.LeftTree = new Grit.Tree.JsTree.JsTreeBuilder<Settings.Model.Client>(x => x.Name, x => x.ClientId, x => x.Nodes)
                .Build(leftTree, clients)
                .children;

            var nodes = SettingsService.GetNodes();
            var rightTree = TreeService.GetTree(Constants.TREE_NODE);
            ViewBag.RightTree = new Grit.Tree.JsTree.JsTreeBuilder<Node>(x => x.Name, x => x.NodeId)
                .Build(rightTree, nodes)
                .children;

            return View();
        }

        [HttpPost]
        public ActionResult Map([ModelBinder(typeof(JsonNetModelBinder))] IList<Grit.Tree.JsTree.JsTreeNode> tree)
        {
            var root = new JsTreeParser().Parse(Constants.TREE_NODE, tree);
            var clients = SettingsService.GetClients();
            var nodes = SettingsService.GetNodes();
            root.Each(x =>
            {
                if (x.Elements != null)
                {
                    var client = clients.SingleOrDefault(n => n.ClientId == x.Data);
                    if (client != null)
                    {
                        client.Nodes = nodes.Where(n => x.Elements.Any(m => m == n.NodeId)).Select(n => n.NodeId).ToArray();
                    }
                }
            });
            SettingsService.SaveClientNodes(clients);
            return new JsonNetResult(clients);
        }
    }
}
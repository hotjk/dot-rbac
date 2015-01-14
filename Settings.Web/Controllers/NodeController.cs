using Grit.Sequence;
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
            ISettingsService settingsService)
        {
            this.SequenceService = sequenceService;
            this.SettingsService = settingsService;
        }

        private ISequenceService SequenceService { get; set; }
        private ISettingsService SettingsService { get; set; }

        private const int SEQUENCE_SETTINGS = 1000;

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id.HasValue)
            {
                Node node = SettingsService.GetNode(id.Value);
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
                node.NodeId = SequenceService.Next(SEQUENCE_SETTINGS, 1);
                node.CreateAt = node.UpdateAt;
            }

            if(!SettingsService.UpdateNode(node))
            {
                ModelState.AddModelError(string.Empty, 
                    "保存失败，编辑过程中可能其他用户已经编辑了节点的数据");
                return View(vm);
            }

            Info = "保存成功";
            return RedirectToAction("Edit", new { id = node.NodeId });
        }
    }
}
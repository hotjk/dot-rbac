using Settings.Model;
using Settings.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settings.Web.Controllers
{
    public class NodeController : Controller
    {
        [HttpGet]
        public ActionResult Edit()
        {
            Node node = new Node() { Name="test" };
            var vm = NodeVM.FromModel(node);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(NodeVM vm)
        {
            if (!ModelState.IsValid || !vm.IsValid(ModelState))
            {
                return View(vm);
            }

            var node = NodeVM.ToModel(vm);
            vm = NodeVM.FromModel(node);
            return View(vm);
        }
    }
}
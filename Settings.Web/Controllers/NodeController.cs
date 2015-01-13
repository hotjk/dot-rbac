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
            NodeVM vm = new NodeVM();
            return View(vm);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            TempData["demo"] = "Hello world, 中文字符，很长很长的中文字符。";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = TestRepository1.SelectColumns + TestRepository2.SelectColumns;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = TempData["demo"];

            return View();
        }
    }
}
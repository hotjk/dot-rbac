using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = TempData["demo"];

            return View();
        }

        public void Captcha(int length=4)
        {
            var image = new Grit.Utility.Captcha.CaptchaImage(80, 36, true, true, true).Generate(Grit.Utility.Security.RandomText.Generate(length));
            Response.ContentType = "image/gif";
            image.Save(Response.OutputStream, ImageFormat.Gif);
        }
    }
}
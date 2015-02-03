using Grit.Core.Caching;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Serialization;
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

        public void Captcha(int l = 4, uint w = 80, uint h = 36, bool wrap = true, bool deform = true, bool noise = true)
        {
            var image = new Grit.Utility.Captcha.CaptchaImage(w, h, wrap, deform, noise).Generate(
                Grit.Utility.Security.RandomText.Generate(l));
            Response.ContentType = "image/gif";
            image.Save(Response.OutputStream, ImageFormat.Gif);
        }

        [DataContract]
        public class Person
        {
            [DataMember(Order = 2)]
            public string FirstName { get; set; }
            [DataMember(Order = 1)]
            public string LastName { get; set; }
            [DataMember(Order = 3)]
            public int Age { get; set; }
            [DataMember(Order = 4)]
            public bool OK { get; set; }
        }

        public string Cache()
        {
            Person person = new Person { FirstName = "Tester", LastName = "中文", Age = 12, OK = true };
            RedisCacheManagerOptions options = new RedisCacheManagerOptions { Configuration = "localhost", DBIndex = 1 };
            RedisCacheManager manager = new RedisCacheManager(options);
            manager.Set("p1", person, 10);
            Person found = manager.Get<Person>("p1");
            return found.LastName;
        }
    }
}
using Grit.Utility.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settings.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string test()
        {
            string privateKey;
            string publicKey;
            RSAManager.GenerateKeyAndIV(out publicKey, out privateKey);
            RSAManager rsa = new RSAManager(privateKey);
            string encrypted = rsa.PrivareEncrypt("hello world中文");
            rsa = new RSAManager(publicKey);
            string decrypted = rsa.PublicDecrypt(encrypted);
            return decrypted;
        }
    }
}
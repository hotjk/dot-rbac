using Grit.Sequence;
using Grit.Tree.JsTree;
using Grit.Utility.Security;
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
    public class EncryptKeyController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            string publicKey, privateKey;
            RSAManager.GenerateKeyAndIV(out publicKey, out privateKey);
            return View(new EncryptKeyVM { PublicKey = publicKey, PrivateKey = privateKey });
        }
   }
}
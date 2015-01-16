using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Settings.Client.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string api = ConfigurationManager.AppSettings["SettingsAPI"];
            string key = ConfigurationManager.AppSettings["SettingsPrivateKey"];
            SettingsService service = new SettingsService();
            var settings = service.GetClientSettings(api, key);
            return View(settings);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Optimization;

namespace Grit.Utility.Web.Mvc
{
    public static class AppScriptsBandles
    {
        public class Item
        {
            public string Key { get; set; }
            public string Bundle { get; set; }
            public string Include { get; set; }
        }

        static AppScriptsBandles()
        {
            Items = new List<Item>();
        }

        public static IEnumerable<Item> Items { get; private set; }

        public static void AddFolder(string scriptsPath, BundleCollection bundles)
        {
            Items = GetScriptsInFolder("~/Scripts/app/");
            foreach (var item in Items)
            {
                bundles.Add(new ScriptBundle(item.Bundle).Include(item.Include));
            }
        }

        private static IEnumerable<Item> GetScriptsInFolder(string scriptsPath)
        {
            if (!scriptsPath.StartsWith("~"))
            {
                throw new ArgumentException("Scripts path MUST start with ~");
            }
            IList<Item> list = new List<Item>();
            scriptsPath = scriptsPath.TrimEnd('/') + "/";
            string scriptsAbstractPath = HttpContext.Current.Server.MapPath(scriptsPath);
            foreach (string path in Directory.GetFiles(scriptsAbstractPath, "*.*", SearchOption.AllDirectories))
            {
                string relativePath = path.Substring(scriptsAbstractPath.Length).Replace('\\', '/');
                Item item = new Item
                {
                    Key = relativePath.Replace('.', '-'),
                    Bundle = Path.Combine("~/bundles/", relativePath.Replace('.', '-')),
                    Include = Path.Combine(scriptsPath, relativePath)
                };
                list.Add(item);
            }
            return list;
        }

        public static IHtmlString GetRequireJsPathScripts()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in Items) 
            {
                sb.AppendFormat("'{0}': '{1}',", item.Key, System.Web.Optimization.Scripts.Url(item.Bundle));
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return new System.Web.Mvc.MvcHtmlString(sb.ToString());
        }

        public static string GetRequireJsBundle(string key)
        {
            return Items.First(n=>n.Key == key).Bundle;
        }
    }
}

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
    public static class BundleHelper
    {
        public static int AddFilesInFolderToBundle(string scriptsPath, System.Web.Optimization.BundleCollection bundles)
        {
            if (!scriptsPath.StartsWith("~"))
            {
                throw new ArgumentException("Scripts path MUST start with ~");
            }
            scriptsPath = scriptsPath.TrimEnd('/') + "/";
            string scriptsAbstractPath = HttpContext.Current.Server.MapPath(scriptsPath);
            int n = 0;
            foreach (string path in Directory.GetFiles(scriptsAbstractPath, "*.*", SearchOption.AllDirectories))
            {
                string relativePath = Path.Combine(scriptsPath, path.Substring(scriptsAbstractPath.Length).Replace('\\', '/'));
                bundles.Add(new ScriptBundle(relativePath).Include(relativePath));
                n++;
            }
            return n;
        }
    }
}

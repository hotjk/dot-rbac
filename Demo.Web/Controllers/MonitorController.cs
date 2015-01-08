using Grit.Utility.Basic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Web.Http;

namespace Demo.Web.Controllers
{
    public class MonitorController : ApiController
    {
        [HttpGet]
        public string
            Index()
        {
            var version = Grit.Utility.Basic.Assembly.GetComplieVersion();
            DateTime compileAt = new DateTime(2000, 1, 1).AddDays(version[2]).AddSeconds(version[3] * 2);
            return compileAt.ToString() + Grit.Utility.Basic.Assembly.RetrieveLinkerTimestamp().ToString();
        }
    }
}

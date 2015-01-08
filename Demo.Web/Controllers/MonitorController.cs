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
            return AssemblyManager.GetVersionTimestamp(AssemblyManager.GetComplieVersion(Assembly.GetExecutingAssembly())).ToString() 
                + AssemblyManager.RetrieveLinkerTimestamp().ToString();
        }
    }
}

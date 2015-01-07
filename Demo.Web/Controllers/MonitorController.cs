using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace Demo.Web.Controllers
{
    public class MonitorController : ApiController
    {
        [HttpGet]
        public string Version()
        {
            return Grit.Utility.Basic.AssemblyHelper.RetrieveLinkerTimestamp().ToString();
            //return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //return System.IO.File.GetCreationTime(this.GetType().Assembly.Location).ToString();
        }
    }
}

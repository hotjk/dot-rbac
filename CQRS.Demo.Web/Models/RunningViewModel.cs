using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQRS.Demo.Web.Models
{
    public class RunningViewModel
    {
        /// <summary>
        /// Controller
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// Action
        /// </summary>
        public string A { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        public Guid E { get; set; }

        public HtmlString Url()
        {
            // todo: Parameters
            return new HtmlString(string.Format("{0}/{1}?id={2}&e={3}", C, A, Id, E));
        }
    }
}
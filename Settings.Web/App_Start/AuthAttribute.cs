using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Settings.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthAttribute : FilterAttribute, IAuthorizationFilter 
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                FormsIdentity formIdentity = HttpContext.Current.User.Identity as FormsIdentity;
                FormsAuthenticationTicket ticket = formIdentity.Ticket as FormsAuthenticationTicket;
                string userData = ticket.UserData;
                return;
            }
            string url = filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri;
            FormsAuthentication.RedirectToLoginPage("returnurl=" + url);
        }
    }
}
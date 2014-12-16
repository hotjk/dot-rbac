using Grit.Core;
using Grit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Web.Controllers
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
    }
    public class UserRepository : Repository<User>
    {

    }
    public class SQLDataController : Controller
    {
        public string Index()
        {
            UserRepository repo = new UserRepository();
            return null;
        }
    }
}
using Grit.Core;
using Grit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Web.Controllers
{
    public class TestModel1 : BaseEntity
    {
        public string Username {get;set;}
    }
    public class TestModel2 : BaseEntity
    {
        public string Nickname { get; set; }
    }
    public class TestRepository1 : Repository<TestModel1>
    {
    }

    public class TestRepository2 : Repository<TestModel2>
    {
    }
}
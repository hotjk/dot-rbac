using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Configuration
{
    public static class MySql
    {
        public static readonly string MySqlSequence = "Server=localhost;Port=3306;Database=grit;Uid=root;Pwd=flowers;";
        public static readonly string MySqlRBAC = "Server=localhost;Port=3306;Database=grit;Uid=root;Pwd=flowers;";

        public static readonly string MySqlCQRSRead = "Server=localhost;Port=3306;Database=grit;Uid=root;Pwd=flowers;";
        public static readonly string MySqlCQRSWrite = "Server=localhost;Port=3306;Database=grit;Uid=root;Pwd=flowers;";
    }
}

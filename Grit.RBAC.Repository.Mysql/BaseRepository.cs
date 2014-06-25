﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC.Repository.MySql
{
    public class BaseRepository
    {
        protected static IDbConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(Grit.Configuration.MySql.MySqlRBAC);
            return connection;
        }
    }
}

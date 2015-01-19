using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC.Repository.MySql
{
    public class BaseRepository
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["RBAC.MySql"].ConnectionString;
        protected static IDbConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}

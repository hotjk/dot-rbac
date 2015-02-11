using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Tree.Repository.MySql
{
    public class BaseRepository
    {
        public BaseRepository(SqlOption option)
        {
            Option = option;
        }
        protected SqlOption Option { get; private set; }

        public IDbConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(Option.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Sequence.Repository.MySql
{
    public class BaseRepository
    {
        protected static IDbConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(Grit.Configuration.MySql.MySqlSequence);
            connection.Open();
            return connection;
        }
    }
}

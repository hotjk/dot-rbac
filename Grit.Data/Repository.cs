using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grit.Core.Data;
using Grit.Core;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;

namespace Grit.Data
{
    public abstract class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        protected virtual string Table { get; private set; }

        public Repository()
        {
            this.Table = typeof(T).Name;
        }

        protected abstract IDbConnection OpenConnection();

        public virtual T GetById(object id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<T>(
                    "SELECT * FROM @Table WHERE Id=@Id;",
                    new { Table = Table, Id = (int)id }).SingleOrDefault();
            }
        }
    }
}

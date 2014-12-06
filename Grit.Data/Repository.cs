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
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using DapperExtensions;

namespace Grit.Data
{
    public abstract class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        protected static IClassMapper<T> Mapper { get; private set; }

        public static string SelectColumns { get; private set; }

        static Repository()
        {
            Mapper = new AutoClassMapper<T>();
            MySqlDialect SqlDialect = new MySqlDialect();
            SelectColumns = Mapper.Properties
                .Where(p => !p.Ignored)
                .Select(p => SqlDialect.GetColumnName(SqlDialect.GetTableName(Mapper.SchemaName, Mapper.TableName, null), p.ColumnName, null))
                .AppendStrings();
        }
    }
}

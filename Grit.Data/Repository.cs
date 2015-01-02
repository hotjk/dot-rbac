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
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        static Repository()
        {
            var Mapper = new AutoClassMapper<T>();
            MySqlDialect SqlDialect = new MySqlDialect();
            var tableName = SqlDialect.GetTableName(Mapper.SchemaName, Mapper.TableName, null);
            var keyColumns = Mapper.Properties.Where(p => p.KeyType == KeyType.Identity);

            var columns = Mapper.Properties.Where(p => !p.Ignored);
            var selectColumns = columns.Select(p => SqlDialect.GetColumnName(tableName, p.ColumnName, null));
            var selectSql = string.Format("SELECT {0} FROM {1}",
                selectColumns.AppendStrings(),tableName);

            columns = Mapper.Properties.Where(p => !(p.Ignored || p.IsReadOnly));
            var insertColumns = columns.Select(p => SqlDialect.GetColumnName(tableName, p.ColumnName, null));
            var insertParameters = columns.Select(p => SqlDialect.ParameterPrefix + p.Name);
            var insertSql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                tableName, insertColumns.AppendStrings(), insertParameters.AppendStrings());

            columns = Mapper.Properties.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity));
            var updateSql = string.Format("UPDATE {0} SET {1} WHERE {2}",
                tableName, 
                columns.Select(p => string.Format("{0} = {1}{2}", p.ColumnName, SqlDialect.ParameterPrefix, p.Name)).AppendStrings(),
                keyColumns.Select(p => string.Format("{0} = {1}{2}", p.ColumnName, SqlDialect.ParameterPrefix, p.Name)).AppendStrings("AND "));
            return;
        }
    }
}

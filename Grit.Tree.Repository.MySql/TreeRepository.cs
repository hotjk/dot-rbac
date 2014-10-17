using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Grit.Tree.Repository.MySql
{
    public class TreeRepository : BaseRepository, ITreeRepository
    {
        public IEnumerable<Node> GetTreeNodes(int root)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Node>(
                    "SELECT `Id`, `Root`, `Parent`, `Order`, `Data` FROM `tree` WHERE `Root` = @Root ORDER BY `Parent`;",
                    new { Root = root });
            }
        }
     }
}

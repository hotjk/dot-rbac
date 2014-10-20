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
        public IEnumerable<Node> GetTreeNodes(int tree)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Node>(
                    "SELECT `Tree`, `Id`, `Parent`, `Data` FROM `tree` WHERE `Tree` = @Tree ORDER BY `Parent`, `Id`;",
                    new { Tree = tree });
            }
        }

        public void SaveTreeNodes(IList<Node> nodes)
        {
            if (nodes == null || nodes.Count == 0)
            {
                return;
            }

            using (IDbConnection connection = OpenConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute("DELETE FROM `Tree` WHERE `Tree` = @Tree;",
                        new { Tree = nodes.First().Tree });
                    connection.Execute("INSERT INTO `tree`(`Tree`, `Id`, `Parent`, `Data`) VALUES (@Tree, @Id, @Parent, @Data);",
                        nodes);
                    transaction.Commit();
                }
            }
        }
     }
}

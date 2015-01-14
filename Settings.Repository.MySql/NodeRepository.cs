using Settings.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Settings.Repository.MySql
{
    public class NodeRepository : BaseRepository, INodesRepository
    {
        public IEnumerable<Node> GetNodes()
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    return connection.Query<Node>(@"SELECT `NodeId`, `Name`, `Version`, `CreateAt`, `UpdateAt` FROM `settings_node`;");
                }
            }
        }

        public Node GetNode(int nodeId)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    using (var multi = connection.QueryMultiple(@"SELECT `NodeId`, `Name`, `Version`, `CreateAt`, `UpdateAt` FROM `settings_node` WHERE `NodeId` = @NodeId;
SELECT `NodeId`, `Key`, `Value` FROM `Settings_Entry` WHERE `NodeId` = @NodeId;", 
                        new { NodeId = nodeId })) {
                        var node = multi.Read<Node>().SingleOrDefault();
                        if(node == null) return null;
                        node.Entries = multi.Read<Entry>().ToList();
                        return node;
                    }
                }
            }
        }

        public bool SaveNode(Node node)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    Node found = connection.Query<Node>("SELECT `NodeId`, `Name`, `Version`, `CreateAt`, `UpdateAt` FROM `settings_node` WHERE `NodeId` = @NodeId FOR UPDATE;",
                        new { NodeId = node.NodeId }).SingleOrDefault();
                    if (found == null)
                    {
                        if (1 != connection.Execute(@"INSERT INTO `settings_node` 
(`NodeId`, `Name`, `Version`, `CreateAt`, `UpdateAt`)
VALUES (@NodeId, @Name, @Version, @CreateAt, @UpdateAt);", node))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (found.Version != node.Version)
                        {
                            return false;
                        }
                        node.Version++;
                        int n = connection.Execute(@"UPDATE `settings_node` 
SET `Name` = @Name, `Version` = @Version, `UpdateAt` = @UpdateAt 
WHERE NodeId = @NodeId;", node);
                    }
                    connection.Execute("DELETE FROM `settings_entry` WHERE `NodeId` = @NodeId;", new { NodeId = node.NodeId });
                    connection.Execute(@"INSERT INTO `settings_entry` (`NodeId`, `Key`, `Value`) VALUES (@NodeId, @Key, @Value);",
                        node.Entries);
                    transaction.Commit();
                    return true;
                }
            }
        }

        public bool DeleteNode(int nodeId, int version)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    Node found = connection.Query<Node>("SELECT `NodeId`, `Name`, `Version`, `CreateAt`, `UpdateAt` FROM `settings_node` WHERE `NodeId` = @NodeId FOR UPDATE;",
                        new { NodeId = nodeId }).SingleOrDefault();
                    if (found == null)
                    {
                        return false;
                    }
                    if (found.Version != version)
                    {
                        return false;
                    }
                    connection.Execute(@"DELETE FROM `settings_node` WHERE `NodeId` = @NodeId; DELETE FROM `settings_entry` WHERE `NodeId` = @NodeId;", new { NodeId = nodeId });
                    return true;
                }
            }
            
        }
    }
}

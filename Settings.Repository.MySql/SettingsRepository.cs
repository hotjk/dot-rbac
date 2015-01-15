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
    public class SettingsRepository : BaseRepository, ISettingsRepository
    {
        public IEnumerable<Node> GetNodes()
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Node>(@"SELECT `NodeId`, `Name`, `Version`, `CreateAt`, `UpdateAt` FROM `settings_node`;");
            }
        }

        public IEnumerable<Node> GetNodes(int[] ids)
        {
            if (ids == null || !ids.Any())
            {
                return new List<Node>();
            }
            using (IDbConnection connection = OpenConnection())
            {
                using (var multi = connection.QueryMultiple(@"SELECT `NodeId`, `Name`, `Version`, `CreateAt`, `UpdateAt` FROM `settings_node` WHERE `NodeId` IN @Ids;
SELECT `NodeId`, `Key`, `Value` FROM `Settings_Entry` WHERE `NodeId` IN @Ids;",
                    new { Ids = ids }))
                {
                    var nodes = multi.Read<Node>();
                    var entries = multi.Read<Entry>();
                    foreach (var node in nodes)
                    {
                        node.Entries = entries.Where(n => n.NodeId == node.NodeId).ToList();
                    }
                    return nodes;
                }
            }
        }

        public Node GetNode(int nodeId)
        {
            using (IDbConnection connection = OpenConnection())
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

        public bool SaveNode(Node node)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    Node found = connection.Query<Node>("SELECT `NodeId`, `Version` FROM `settings_node` WHERE `NodeId` = @NodeId FOR UPDATE;",
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


        public Client GetClient(int clientId)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (var multi = connection.QueryMultiple(@"SELECT `ClientId`, `Name`, `PublicKey`, `Version`, `CreateAt`, `UpdateAt` FROM `settings_client` WHERE `ClientId` = @ClientId;
SELECT `NodeId` FROM `settings_client_node` WHERE `ClientId` = @ClientId;", 
                    new {  ClientId = clientId }))
                {
                    var client = multi.Read<Client>().SingleOrDefault();
                    if (client != null)
                    {
                        client.Nodes = multi.Read<int>().ToArray();
                    }
                    return client;
                }
            }
        }

        public Client GetClient(string name)
        {
            using (IDbConnection connection = OpenConnection())
            {
                var client = connection.Query<Client>(@"SELECT `ClientId`, `Name`, `PublicKey`, `Version`, `CreateAt`, `UpdateAt` FROM `settings_client` WHERE `Name` = @Name;", 
                    new { Name = name }).SingleOrDefault();
                if(client == null)
                {
                    return null;
                }
                client.Nodes = connection.Query<int>(@"SELECT `NodeId` FROM `settings_client_node` WHERE `ClientId` = @ClientId;", new { ClientId = client.ClientId }).ToArray();
                return client;
            }
        }

        private class ClientNode
        {
            public int ClientId { get; set; }
            public int NodeId { get; set; }
        }

        public IEnumerable<Client> GetClients()
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (var multi = connection.QueryMultiple(@"SELECT `ClientId`, `Name`, `Version`, `CreateAt`, `UpdateAt` FROM `settings_client`;
SELECT `ClientId`, `NodeId` FROM `settings_client_node`;"))
                {
                    var clients = multi.Read<Client>();
                    var nodes = multi.Read<ClientNode>(); 
                    foreach(var client in clients)
                    {
                        client.Nodes = nodes.Where(n => n.ClientId == client.ClientId).Select(n => n.NodeId).ToArray();
                    }
                    return clients;
                }
            }
        }

        public bool SaveClient(Client client)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    Client found = connection.Query<Client>("SELECT `ClientId`, `Version` FROM `settings_client` WHERE `ClientId` = @ClientId FOR UPDATE;",
                        new { ClientId = client.ClientId }).SingleOrDefault();
                    if (found == null)
                    {
                        if (1 != connection.Execute(@"INSERT INTO `settings_client` 
(`ClientId`, `Name`, `PublicKey`, `Version`, `CreateAt`, `UpdateAt`)
VALUES (@ClientId, @Name, @PublicKey, @Version, @CreateAt, @UpdateAt);", client))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (found.Version != client.Version)
                        {
                            return false;
                        }
                        client.Version++;
                        int n = connection.Execute(@"UPDATE `settings_client` 
SET `Name` = @Name, `PublicKey` = @PublicKey, `Version` = @Version, `UpdateAt` = @UpdateAt 
WHERE ClientId = @ClientId;", client);
                    }
                    transaction.Commit();
                    return true;
                }
            }
        }

        public bool SaveClientNodes(IEnumerable<Client> clients)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    foreach (var client in clients)
                    {
                        connection.Execute("DELETE FROM `settings_client_node` WHERE `ClientId` = @ClientId;", client);
                        connection.Execute("INSERT INTO `settings_client_node` (ClientId, NodeId) VALUES (@ClientId, @NodeId);",
                            client.Nodes.Select(n => new { ClientId = client.ClientId, NodeId = n }));
                    }
                    transaction.Commit();
                }
            }
            return true;
        }
    }
}

using Grit.Utility.Security;
using Settings.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class SettingsService : ISettingsService
    {
        private static readonly byte[] KEY = Convert.FromBase64String(ConfigurationManager.AppSettings["PersistenceKey"]);
        private static readonly byte[] IV = Convert.FromBase64String(ConfigurationManager.AppSettings["PersistenceIV"]);

        public SettingsService(ISettingsRepository settingsRepository)
        {
            this.SettingsRepository = settingsRepository;
        }
        private ISettingsRepository SettingsRepository { get; set; }

        public bool UpdateNode(Node node)
        {
            RijndaelManager rsa = new RijndaelManager(KEY, IV);
            node.Entries.ForEach(x => {
                x.NodeId = node.NodeId;
                x.Value = rsa.Encrypt(x.Value);
            });
            return SettingsRepository.SaveNode(node);
        }

        public bool DeleteNode(Node node)
        {
            return SettingsRepository.DeleteNode(node);
        }

        public Node GetNode(int nodeId)
        {
            var node = SettingsRepository.GetNode(nodeId);
            RijndaelManager rsa = new RijndaelManager(KEY, IV);
            node.Entries.ForEach(x => {
                x.Value = rsa.Decrypt(x.Value);
            });
            return node;
        }

        public IEnumerable<Node> GetNodes()
        {
            return SettingsRepository.GetNodes();
        }

        public IEnumerable<Node> GetNodes(int[] nodes)
        {
            return SettingsRepository.GetNodes(nodes);
        }

        public Client GetClient(int clientId)
        {
            return SettingsRepository.GetClient(clientId);
        }

        public Client GetClient(string name)
        {
            return SettingsRepository.GetClient(name);
        }

        public IEnumerable<Client> GetClients()
        {
            return SettingsRepository.GetClients();
        }

        public bool UpdateClient(Client client)
        {
            return SettingsRepository.SaveClient(client);
        }

        public bool SaveClientNodes(IEnumerable<Client> clients)
        {
            return SettingsRepository.SaveClientNodes(clients);
        }

        public bool DeleteClient(Client client)
        {
            return SettingsRepository.DeleteClient(client);
        }

        public SettingsResponse GetClientSettings(Client client, Grit.Tree.Node tree)
        {
            var clientNodes = GetNodes(client.Nodes);
            var allNodes = GetNodes();

            SettingsResponse resp = new SettingsResponse(client.Name);
            var path = new List<Grit.Tree.Node>(5);
            foreach (var node in clientNodes)
            {
                if (node.Entries == null || !node.Entries.Any()) continue;
                path.Clear();
                tree.FindByData(node.NodeId, path);

                string strPath = string.Join("/",
                    path.Select(n => allNodes.FirstOrDefault(x => x.NodeId == n.Data)).Select(n => n.Name).Reverse())
                    + "/";

                resp.Entries.AddRange(node.Entries.Select(n => new SettingsResponse.Entry { Path = strPath + n.Key, Value = n.Value }));
            }
            return resp;
        }
    }
}

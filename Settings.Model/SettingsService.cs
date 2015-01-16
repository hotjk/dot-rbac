using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class SettingsService : ISettingsService
    {
        public SettingsService(ISettingsRepository settingsRepository)
        {
            this.SettingsRepository = settingsRepository;
        }
        private ISettingsRepository SettingsRepository { get; set; }

        public bool UpdateNode(Node node)
        {
            node.Entries.ForEach(x => x.NodeId = node.NodeId);
            return SettingsRepository.SaveNode(node);
        }

        public bool DeleteNode(int nodeId, int version)
        {
            return SettingsRepository.DeleteNode(nodeId, version);
        }

        public Node GetNode(int nodeId)
        {
            return SettingsRepository.GetNode(nodeId);
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

        public ClientSettings GetClientSettings(Client client, Grit.Tree.Node tree)
        {
            var clientNodes = GetNodes(client.Nodes);
            var allNodes = GetNodes();

            ClientSettings resp = new ClientSettings(client.Name);
            var path = new List<Grit.Tree.Node>(5);
            foreach (var node in clientNodes)
            {
                if (node.Entries == null || !node.Entries.Any()) continue;
                path.Clear();
                tree.FindByData(node.NodeId, path);

                string strPath = string.Join("/",
                    path.Select(n => allNodes.FirstOrDefault(x => x.NodeId == n.Data)).Select(n => n.Name).Reverse())
                    + "/";

                resp.Entries.AddRange(node.Entries.Select(n => new ClientSettings.Entry { Path = strPath + n.Key, Value = n.Value }));
            }
            return resp;
        }
    }
}

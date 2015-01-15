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

        //public SettingsResponse GetSettings(string client)
        //{
        //    var aClient = GetClient(client);
        //    if (aClient == null)
        //    {
        //        return null;
        //    }

        //    var nodes = GetNodes(aClient.Nodes);
        //    var allNodes = GetNodes();

        //    SettingsResponse resp = new SettingsResponse { Client = client, Entries = new List<SettingsEntry>() };
        //    foreach (var node in nodes)
        //    {
        //        if (node.Entries == null || !node.Entries.Any()) continue;

        //        List<Node> path = new List<Node>(5);
        //        var tNode = treeNodes.FirstOrDefault(n => n.Data == node.NodeId);
        //        path.Add(allNodes.FirstOrDefault(n => n.NodeId == tNode.Data));
        //        while (tNode.Parent != null)
        //        {
        //            tNode = tNodes.SingleOrDefault(n => n.Id == tNode.Parent.Value);
        //            if (tNode.Data == null) break;
        //            path.Add(allNodes.FirstOrDefault(n => n.NodeId == tNode.Data));
        //        }
        //        string strPath = string.Join("/", path.Select(n => n.Name).Reverse()) + "/";

        //        foreach (var data in node.Entries)
        //        {
        //            resp.Entries.Add(new SettingsEntry { Path = strPath + data.Key, Value = data.Value });
        //        }
        //    }
        //}
    }
}

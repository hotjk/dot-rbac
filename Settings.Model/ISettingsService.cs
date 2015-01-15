using System;
using System.Collections.Generic;
namespace Settings.Model
{
    public interface ISettingsService
    {
        IEnumerable<Node> GetNodes();
        IEnumerable<Node> GetNodes(int client);
        Node GetNode(int nodeId);
        bool UpdateNode(Node node);
        bool DeleteNode(int nodeId, int version);

        Client GetClient(int clientId);
        IEnumerable<Client> GetClients();
        bool UpdateClient(Client client);
        bool SaveClientNodes(IEnumerable<Client> clients);
    }
}

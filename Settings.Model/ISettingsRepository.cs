using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public interface ISettingsRepository
    {
        IEnumerable<Node> GetNodes();
        Node GetNode(int nodeId);
        bool SaveNode(Node node);
        bool DeleteNode(int nodeId, int version);

        Client GetClient(int clientId);
        IEnumerable<Client> GetClients();
        bool SaveClient(Client client);
        bool SaveClientNodes(IEnumerable<Client> clients);
    }
}

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
        IEnumerable<Node> GetNodes(int[] nodes);
        bool SaveNode(Node node);
        bool DeleteNode(Node node);

        Client GetClient(int clientId);
        Client GetClient(string name);
        IEnumerable<Client> GetClients();
        bool SaveClient(Client client);
        bool SaveClientNodes(IEnumerable<Client> clients);
        bool DeleteClient(Client client);

        User GetUser(int userId);
        User GetUser(string username);
        bool SaveUser(User user);
        bool DeleteUser(User user);
    }
}

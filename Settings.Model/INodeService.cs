using System;
using System.Collections.Generic;
namespace Settings.Model
{
    public interface INodeService
    {
        bool DeleteNode(int nodeId, int version);
        IEnumerable<Node> GetNodes();
        IList<Node> GetNodes(int client);
        bool UpdateNode(Node node);
        Node GetNode(int nodeId);
    }
}

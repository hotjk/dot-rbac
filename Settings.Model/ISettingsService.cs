using System;
using System.Collections.Generic;
namespace Settings.Model
{
    public interface ISettingsService
    {
        bool DeleteNode(int nodeId, int version);
        IList<Node> GetNodes();
        IList<Node> GetNodes(int client);
        bool UpdateNode(Node node);
        Node GetNode(int nodeId);
    }
}

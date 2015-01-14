using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class NodeService : INodeService
    {
        public NodeService(INodesRepository nodeRepository)
        {
            this.NodeRepository = nodeRepository;
        }
        private INodesRepository NodeRepository { get; set; }

        public bool UpdateNode(Node node)
        {
            node.Entries.ForEach(x => x.NodeId = node.NodeId);
            return NodeRepository.SaveNode(node);
        }

        public bool DeleteNode(int nodeId, int version)
        {
            return NodeRepository.DeleteNode(nodeId, version);
        }

        public Node GetNode(int nodeId)
        {
            return NodeRepository.GetNode(nodeId);
        }

        public IEnumerable<Node> GetNodes()
        {
            return NodeRepository.GetNodes();
        }

        public IList<Node> GetNodes(int client)
        {
            return null;
        }
    }
}

using Grit.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Tree
{
    public class TreeService : ITreeService
    {
        public TreeService(ISequenceRepository sequenceRepository,
            ITreeRepository treeRepository)
        {
            this.SequenceRepository = sequenceRepository;
            this.TreeRepository = treeRepository;
        }

        private ISequenceRepository SequenceRepository { get; set; }
        private ITreeRepository TreeRepository { get; set; }

        public Node GetTree(int treeId)
        {
            var nodes = TreeRepository.GetTreeNodes(treeId);
            Node root = nodes.FirstOrDefault();
            if(root == null)
            {
                root = new Node { Root = treeId };
            }
            foreach(var node in nodes.Skip(1))
            {
                root.AddChild(node);
            }
            return root;
        }
    }
}

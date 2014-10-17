using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Tree
{
    public class Node
    {
        public int Id { get; private set; }
        public int Root { get; set; }
        public int? Parent { get; private set; }
        public int Order { get; private set; }
        public IList<Node> Children { get; private set; }
        public int? Data { get; private set; }

        public bool AddChild(Node node)
        {
            if (node.Parent == null || node.Parent == this.Id)
            {
                node.Parent = node.Id;
                if (Children == null)
                {
                    Children = new List<Node>();
                }
                Children.Add(node);
                return true;
            }
            else
            {
                if (Children != null)
                {
                    foreach (Node child in Children)
                    {
                        if (child.AddChild(node))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void SummarizeData(ISet<int> set)
        {
            if (Data.HasValue)
            {
                set.Add(Data.Value);
            }
            if (Children != null && Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    child.SummarizeData(set);
                }
            }
        }
    }
}

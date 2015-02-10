using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Tree.JsTree
{
    public class JsTreeNode
    {
        public const string ICON_GLYPH = "glyphicon glyphicon-leaf";
        public string text { get; set; }
        public string icon { get; set; }
        public JsTreeNodeData data { get; set; }
        public IList<JsTreeNode> children { get; set; }
        public JsTreeNodeState state { get; set; }

        public void AddChild(JsTreeNode node)
        {
            if (children == null)
            {
                children = new List<JsTreeNode>();
            }
            children.Add(node);
        }

        public void Each(Action<JsTreeNode> action)
        {
            action(this);
            if (children != null && children.Count > 0)
            {
                foreach (var child in children)
                {
                    child.Each(action);
                }
            }
        }
    }
}

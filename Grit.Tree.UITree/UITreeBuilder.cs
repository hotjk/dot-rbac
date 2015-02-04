using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Tree.UITree
{
    public class UITreeBuilder<T>
    {
        private Func<T, string> GetText { get; set; }
        private Func<T, int> GetContent { get; set; }
        public UITreeBuilder(
            Func<T, string> getText,
            Func<T, int> getContent)
        {
            this.GetText = getText;
            this.GetContent = getContent;
        }

        public UITreeNode Build(Node node, IEnumerable<T> entities)
        {
            UITreeNode tree = new UITreeNode
            {
                root = GetUITreeNode(node, entities)
            };
            tree.root[0] = node.Tree;

            int max = 0;
            GetMaxDeepth(node, 0, ref max);
            tree.deepth = max;

            return tree;
        }

        public void GetMaxDeepth(Node node, int deepth, ref int max)
        {
            if(deepth > max)
            {
                max = deepth;
            }
            if(node.Children != null)
            {
                foreach(var child in node.Children)
                {
                    GetMaxDeepth(child, deepth+1, ref max);
                }
            }
        }

        public Object[] GetUITreeNode(Node node, IEnumerable<T> entities)
        {
            var entity = entities.SingleOrDefault(n => GetContent(n) == node.Data);

            if (node.Children != null && node.Children.Count > 0)
            {
                Object[] obj = new Object[4];
                if (entity != null)
                {
                    obj[0] = GetContent(entity);
                    obj[1] = GetText(entity);
                }
                obj[2] = 0;
                Object[] children = new Object[node.Children.Count];
                for (int i = 0; i < node.Children.Count; i++)
                {
                    children[i] = GetUITreeNode(node.Children[i], entities);
                }
                obj[3] = children;
                return obj;
            }
            else
            {
                Object[] obj = new Object[3];
                obj[0] = GetContent(entity);
                obj[1] = GetText(entity);
                obj[2] = 0;
                return obj;
            }
        }
    }
}

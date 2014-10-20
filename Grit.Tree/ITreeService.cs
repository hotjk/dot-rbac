using System;
namespace Grit.Tree
{
    public interface ITreeService
    {
        Node GetTree(int treeId);
        void SaveTree(Node root);
    }
}

using System;
namespace Grit.Tree
{
    public interface ITreeService
    {
        string Table { get; }
        Node GetTree(int treeId);
        void SaveTree(Node root);
    }
}

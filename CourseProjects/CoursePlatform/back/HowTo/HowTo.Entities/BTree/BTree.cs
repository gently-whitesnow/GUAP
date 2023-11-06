using System.Collections.Generic;

namespace HowTo.Entities.BTree;

public class BTree<TValue> where TValue : class, IBTreeValue
{
    public TreeNode<TValue> Root { get; }

    public BTree(IList<TValue> sortedData)
    {
        Root = SortedArrayToBSTRecursive(sortedData, 0, sortedData.Count - 1);
    }
    
    TreeNode<TValue> SortedArrayToBSTRecursive(IList<TValue> sortedData, int start, int end)
    {
        if (end - start <= 1)
            return new ()
            {
                Value = sortedData[end],
                Left = end == start ? null : SortedArrayToBSTRecursive(sortedData, start, start)
            };
            
        var median = CalculateBSTMedian(start, end);

        return new ()
        {
            Value = sortedData[median],
            Left = SortedArrayToBSTRecursive(sortedData, start, median - 1),
            Right = SortedArrayToBSTRecursive(sortedData, median + 1, end)
        };
    }

    int CalculateBSTMedian(int start, int end)
    {
        if ((end - start) % 2 != 0)
            return (start + end) / 2 + 1;
        return (start + end) / 2;
    }
    public TValue? GetValue(int id)
    {
        if (Root == null)
            return null;

        if (Root.Value.Id > id)
            return Get(Root.Left, id);
        if (Root.Value.Id < id)
            return Get(Root.Right, id);

        return Root.Value;
    }

    private TValue? Get(TreeNode<TValue> treeNode, int id)
    {
        if (treeNode == null)
            return null;

        if (treeNode.Value.Id > id)
            return Get(treeNode.Left, id);
        if (treeNode.Value.Id < id)
            return Get(treeNode.Right, id);

        return treeNode.Value;
    }
}
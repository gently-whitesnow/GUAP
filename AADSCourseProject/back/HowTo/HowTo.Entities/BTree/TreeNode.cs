using System.Diagnostics.CodeAnalysis;

namespace HowTo.Entities.BTree;

public class TreeNode<TValue>
{
    public TreeNode<TValue>? Left { get; set; }
    public TreeNode<TValue>? Right { get; set; }
    [NotNull] public TValue Value { get; set; }
}
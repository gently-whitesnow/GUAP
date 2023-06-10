using HowTo.Entities.BTree;
using Xunit.Abstractions;

namespace HowTo.Tests;

public class BTreeTests
{
    private readonly ITestOutputHelper _output;
    public BTreeTests(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void FillingBTree()
    {
        var btree = new BTree<BTreeData>(new List<BTreeData>
        {
            new (1),
            new (2),
            new (3),
            new (4),
            new (5),
            new (6),
            new (7),
            new (8),
            new (9),
            new (10),
        });
        
        Assert.Equal("1", btree.GetValue(1)?.NeededValue);
        Assert.Equal("2", btree.GetValue(2)?.NeededValue);
        Assert.Equal("3", btree.GetValue(3)?.NeededValue);
        Assert.Equal("4", btree.GetValue(4)?.NeededValue);
        Assert.Equal("5", btree.GetValue(5)?.NeededValue);
        Assert.Equal("6", btree.GetValue(6)?.NeededValue);
        Assert.Equal("7", btree.GetValue(7)?.NeededValue);
        Assert.Equal("8", btree.GetValue(8)?.NeededValue);
        Assert.Equal("9", btree.GetValue(9)?.NeededValue);
        Assert.Equal("10", btree.GetValue(10)?.NeededValue);
        PrintTree(btree.Root, "center");
        /*
                    6
                3       9 
             2    5   8   10
           1     4   7
          
        */
    }
    
    private void PrintTree(TreeNode<BTreeData> root, string position)
    {
        if (root == null)
        {
            _output.WriteLine("null");
            return;
        }
        _output.WriteLine($"{position}-{root.Value?.NeededValue}");
        PrintTree(root.Left, "left");
        PrintTree(root.Right, "right");
    }
}

class BTreeData : IBTreeValue
{
    public BTreeData(int id)
    {
        Id = id;
        NeededValue = id.ToString();
    }
    public string NeededValue { get; set; }
    public int Id { get; }
}
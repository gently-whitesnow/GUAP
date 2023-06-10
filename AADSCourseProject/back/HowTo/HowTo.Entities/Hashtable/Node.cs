namespace AlgorithmsAndDataStructures._2_DataHash;

public class Node
{
    public bool Deleted { get; set; }
    public int Value { get; set; }

    public Node(int val)
    {
        Value = val;
    }
}
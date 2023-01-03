namespace AlgorithmsAndDataStructures._2_DataHash;

public class Node
{
    public bool Deleted { get; set; }
    public string Value { get; set; }

    public Node(string val)
    {
        Value = val;
    }
}
namespace AlgorithmsAndDataStructures._1_LinearAndCyclicLists;

public class Node<TValue>
{
    public TValue Value;
    public Node<TValue>? Next;
    
    public Node(TValue value)
    {
        Value = value;
    }
}
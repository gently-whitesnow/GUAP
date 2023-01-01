using System;

namespace AlgorithmsAndDataStructures._1_LinearAndCyclicLists;

public class SolutionFirst
{
    public void Run()
    {
        var list = new LinearList<string>();

        var quantity = Input.GetUInt("Enter positive N:");

        for (int i = 0; i < quantity; i++)
        {
            list.Add(Input.GetString($"Enter {i} value:"));
        }
        
        Console.WriteLine("Print all");
        Console.WriteLine(list.ToString());

        var distinctList = list.Distinct();
        
        Console.WriteLine("Print distinct");
        Console.WriteLine(distinctList.ToString());
        
        
    }
    
}
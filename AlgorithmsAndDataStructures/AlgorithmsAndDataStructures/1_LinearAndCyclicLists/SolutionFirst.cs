using System;

namespace AlgorithmsAndDataStructures._1_LinearAndCyclicLists;

public class SolutionFirst
{
    public void Run()
    {
        var demonstrationList = new LinearList<string>();

        var quantity = Input.GetUInt("Введите положительное количество значений N:");

        for (int i = 0; i < quantity; i++)
        {
            demonstrationList.Add(Input.GetString($"Введите {i} значение:"));
        }
        
        Console.WriteLine("Содержимое списка");
        Console.WriteLine(demonstrationList.ToString());

        var distinctList = demonstrationList.Distinct();
        
        Console.WriteLine("Содержимое списка с уникальными значениями");
        Console.WriteLine(distinctList.ToString());
        
        var removalList = new LinearList<string>();

        quantity = Input.GetUInt("Введите положительное количество значений N на удаление:");

        for (int i = 0; i < quantity; i++)
        {
            removalList.Add(Input.GetString($"Введите {i} значение, которое хотите удалить:"));
        }
        
        while (removalList.First!=null)
        {
            distinctList.RemoveOrDefault(removalList.First.Value);
            removalList.RemoveFirst();
        }
        Console.WriteLine("Содержимое списка после удаления");
        Console.WriteLine(distinctList.ToString());

    }
    
}
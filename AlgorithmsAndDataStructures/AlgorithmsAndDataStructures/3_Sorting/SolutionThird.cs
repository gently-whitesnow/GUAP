using System;
using AlgorithmsAndDataStructures._2_DataHash;

namespace AlgorithmsAndDataStructures._3_Sorting;

public class SolutionThird
{
    public void Run()
    {
        var array = new SortedArray();

        Console.WriteLine();
        Console.WriteLine("Добавление с последующей пересортировкой");
        array.Add(1);
        array.Add(5);
        array.Add(3);
        array.Add(3);
        array.Add(4);
        array.Add(1);
        array.Print();
        
        Console.WriteLine();
        Console.WriteLine("Удаление первого элемента и элемента со значением 5 с последующей пересортировкой");
        array.Remove(5);
        array.RemoveIndex(0);
        array.Print();
        
        Console.WriteLine();
        Console.WriteLine($"Получение элемента по значению 4 - {array.Find(v=>v==4)}");
        Console.WriteLine($"Получение элемента по индексу 0 - {array[0]}");
        Console.WriteLine($"Получение индекса первого элемента со значением 3 - {array.FindIndex(3)}");

    }
}
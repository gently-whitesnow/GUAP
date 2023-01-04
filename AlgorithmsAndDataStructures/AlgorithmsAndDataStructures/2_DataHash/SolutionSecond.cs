using System;
using System.Collections.Generic;

namespace AlgorithmsAndDataStructures._2_DataHash;

public class SolutionSecond
{
    private Random rand = new Random();
    public void Run()
    {

        var hashTable = new HashTable(10);

        var listToRemove = new List<string>();
        for (int i = 0; i < 20; i++)
        {
            var h = GetRandomHash();
            hashTable.Add(h);
            if(i%3==0)
                listToRemove.Add(h);
        }
        Console.WriteLine("Исходная таблица");
        hashTable.Print();

        foreach (var hash in listToRemove)
        {
            hashTable.Remove(hash);
        }

        Console.WriteLine($"Таблица после удаления {listToRemove.Count} элементов");
        hashTable.Print();

        Console.WriteLine($"Количество элементов в таблице после удаления - {hashTable.Count}");
        hashTable.Rehash();
        Console.WriteLine($"Количество элементов в таблице после рехеша - {hashTable.Count}");
    }
    
    string GetRandomHash()
    {
        return
            $"{rand.Next(0, 10)}" +
            $"{rand.Next(0, 10)}" +
            $"{(char)(rand.Next(0, 26)+'A')}" +
            $"{(char)(rand.Next(0, 26)+'A')}" +
            $"{rand.Next(0, 10)}" +
            $"{rand.Next(0, 10)}";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsAndDataStructures._2_DataHash;

public class SolutionSecond
{
    private Random rand = new Random();
    public void Run()
    {
        var hashTable = new HashTable(10);
        // Добавляем хеш
        var removalHash = GetRandomHash();
        hashTable.Add(removalHash);
        // Удаляем этот же хеш (проставляем флаг deleted=true)
        hashTable.Remove(removalHash);
        
        // Добавляем 100 элементов в таблицу
        foreach (var _ in Enumerable.Range(0,100))
        {
            var h = GetRandomHash();
            hashTable.Add(h);
        }
        
        Console.WriteLine(hashTable.Count);
        
        
        
        // var hashTable = new HashTable(10);
        //
        // var listToRemove = new List<string>();
        // for (int i = 0; i < 20; i++)
        // {
        //     var h = GetRandomHash();
        //     hashTable.Add(h);
        //     if(i%3==0)
        //         listToRemove.Add(h);
        // }
        // Console.WriteLine("Исходная таблица");
        // hashTable.Print();
        //
        // foreach (var hash in listToRemove)
        // {
        //     hashTable.Remove(hash);
        // }
        //
        // Console.WriteLine($"Таблица после удаления {listToRemove.Count} элементов");
        // hashTable.Print();
        //
        // Console.WriteLine($"Количество элементов в таблице после удаления - {hashTable.Count}");
        // hashTable.Rehash();
        // Console.WriteLine($"Количество элементов в таблице после рехеша - {hashTable.Count}");
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
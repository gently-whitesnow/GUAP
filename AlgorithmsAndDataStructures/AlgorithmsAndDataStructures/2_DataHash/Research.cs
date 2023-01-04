using System;
using System.Collections.Generic;

namespace AlgorithmsAndDataStructures._2_DataHash;

public static class Research
{
    public static void Run()
    {
        var hashTableInCombinations = new HashTable(2500);
        var hashTableByFibo = new HashTable(2500);
        
        var listCombinations = new List<long>();
        var listFibo = new List<long>();
        
        var rand = new Random();
        
        for (int i = 0; i < 7500; i++)
        {
            var hash = GetRandomHash();
            listCombinations.Add(hashTableInCombinations.GetIndexInCombinations(hash));
            listFibo.Add(hashTableByFibo.GetIndexByFibo(hash));
        }
        
        Helpers.SaveToCsv(listCombinations, "research_combinations.csv");
        Helpers.SaveToCsv(listFibo, "research_fibo.csv");
        
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
}
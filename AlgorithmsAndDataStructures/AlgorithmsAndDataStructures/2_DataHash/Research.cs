using System;
using System.Collections.Generic;

namespace AlgorithmsAndDataStructures._2_DataHash;

public static class Research
{
    public static void Run()
    {
        var hashTableByDivision = new HashTable(1500);
        var hashTableByFibo = new HashTable(1500);
        
        var listDivision = new List<long>();
        var listFibo = new List<long>();
        
        var rand = new Random();
        
        for (int i = 0; i < 4500; i++)
        {
            var hash = GetRandomHash();
            listDivision.Add(hashTableByDivision.GetIndexByDivision(hash));
            listFibo.Add(hashTableByFibo.GetIndexByFibo(hash));
        }
        
        Helpers.SaveToCsv(listDivision, "research_division.csv");
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
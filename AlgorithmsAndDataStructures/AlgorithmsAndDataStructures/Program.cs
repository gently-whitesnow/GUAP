
using System;
using AlgorithmsAndDataStructures.LinearAndCyclicLists;

var list = new LinearList<int?>();
list.Add(1);
list.Add(2);
list.Add(null);
list.Add(4);
list.Add(5);

Console.WriteLine("Print all");
Console.WriteLine(list.ToString());

Console.WriteLine("Remove first");
list.RemoveFirst();
Console.WriteLine(list.ToString());

Console.WriteLine("Remove last");
list.RemoveLast();
Console.WriteLine(list.ToString());

Console.WriteLine("Remove first condition value");
list.RemoveOrDefault(null);
Console.WriteLine(list.ToString());

Console.WriteLine("Find first condition value");
Console.WriteLine(list.FindFirstOrDefault(4)?.Value);

Console.WriteLine("Find first condition null value");
Console.WriteLine(list.FindFirstOrDefault(-1)?.Value);


using System;
using AlgorithmsAndDataStructures._1_LinearAndCyclicLists;
using NUnit.Framework;

namespace Tests;

public class LinearListTest
{
    private LinearList<int?> testList;
    [SetUp]
    public void Setup()
    {
        // Инициализация
        testList = new LinearList<int?>();
        testList.Add(1);
        testList.Add(2);
        testList.Add(null);
        testList.Add(null);
        testList.Add(4);
        testList.Add(4);
        testList.Add(5);
        testList.Add(5);
        testList.Add(5);
        testList.Add(6);
    }

    [Test]
    public void Init()
    {
        Assert.Pass();
    }

    [Test]
    public void AddOneElement()
    {
        var list = new LinearList<int>();
        list.Add(1);

        if (list.First is not null && list.Last is not null)
            Assert.AreEqual(list.First.Value, list.Last.Value);
        else
            Assert.Fail("Last or First is null");
        Assert.Pass();
    }

    [Test]
    public void RemoveFirst()
    {
        var list = new LinearList<int?>(testList);
        list.RemoveFirst();
        Assert.AreEqual("2 null null 4 4 5 5 5 6", list.ToString());
    }
    
    [Test]
    public void RemoveLast()
    {
        var list = new LinearList<int?>(testList);
        list.RemoveLast();
        Assert.AreEqual("1 2 null null 4 4 5 5 5", list.ToString());
    }
    
    [Test]
    public void RemoveFirstConditionValue()
    {
        var list = new LinearList<int?>(testList);
        var val = 4;
        list.RemoveOrDefault(val);
        Assert.AreEqual("1 2 null null 4 5 5 5 6", list.ToString());
    }
    
    [Test]
    public void FindFirstConditionValue()
    {
        var list = new LinearList<int?>(testList);
        var val = 4;
        var ans = list.FindFirstOrDefault(val);
        Assert.AreEqual(ans?.Value, val);
    }
    
    [Test]
    public void FindNotExistConditionValue()
    {
        var list = new LinearList<int?>(testList);
        var val = -Int32.MaxValue;
        var ans = list.FindFirstOrDefault(val);
        Assert.AreEqual(ans?.Value, null);
    }
    
    [Test]
    public void Contains()
    {
        var list = new LinearList<int?>(testList);
        var val1 = -Int32.MaxValue;
        var val2 = 1;
        Assert.AreEqual(false, list.Contains(val1));
        Assert.AreEqual(true, list.Contains(val2));
        Assert.AreEqual(true, list.Contains(null));
    }
}
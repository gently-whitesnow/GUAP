using System;
using AlgorithmsAndDataStructures._1_LinearAndCyclicLists;
using AlgorithmsAndDataStructures._2_DataHash;
using NUnit.Framework;

namespace Tests;

public class LinearListTest
{
    private LinearList<int?> testList;
    [SetUp]
    public void Setup()
    {
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

        testList.RemoveFirst();
        Assert.AreEqual("2 null null 4 4 5 5 5 6", testList.ToString());
    }
    
    [Test]
    public void RemoveLast()
    {
        testList.RemoveLast();
        Assert.AreEqual("1 2 null null 4 4 5 5 5", testList.ToString());
    }
    
    [Test]
    public void RemoveFirstConditionValue()
    {
 
        var val = 4;
        testList.RemoveOrDefault(val);
        Assert.AreEqual("1 2 null null 4 5 5 5 6", testList.ToString());
    }
    
    [Test]
    public void FindFirstConditionValue()
    {
        var val = 4;
        var ans = testList.FindFirstOrDefault(val);
        Assert.AreEqual(ans?.Value, val);
    }
    
    [Test]
    public void FindNotExistConditionValue()
    {
        var val = -Int32.MaxValue;
        var ans = testList.FindFirstOrDefault(val);
        Assert.AreEqual(ans?.Value, null);
    }
    
    [Test]
    public void Contains()
    {

        var val1 = -Int32.MaxValue;
        var val2 = 1;
        Assert.AreEqual(false, testList.Contains(val1));
        Assert.AreEqual(true, testList.Contains(val2));
        Assert.AreEqual(true, testList.Contains(null));
    }
}
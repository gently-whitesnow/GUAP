using System.Linq;
using AlgorithmsAndDataStructures._3_Sorting;
using NUnit.Framework;

namespace Tests;

public class SortedArrayTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Init()
    {
        Assert.Pass();
    }
    
    [Test]
    public void Add()
    {
        var array = new SortedArray();
        var count = 10;
        foreach (var value in Enumerable.Range(0,count))
        {
            array.Add(value);
        }
        Assert.AreEqual(count, array.Length);
        Assert.AreEqual(9, array[9]);
    }
    
    [Test]
    public void Remove()
    {
        var array = new SortedArray();
        var count = 10;
        foreach (var value in Enumerable.Range(0,count))
        {
            array.Add(value);
        }
        
        Assert.AreEqual(true, array.Remove(8));
        Assert.AreEqual(count-1, array.Length);
        Assert.AreEqual(9, array[8]);
    }
    
    [Test]
    public void RemoveIndex()
    {
        var array = new SortedArray();
        var count = 10;
        foreach (var value in Enumerable.Range(0,count))
        {
            array.Add(value);
        }

        array.RemoveIndex(0);
        Assert.AreEqual(count-1, array.Length);
        Assert.AreEqual(1, array[0]);
    }
    
    [Test]
    public void Find()
    {
        var array = new SortedArray();
        var count = 10;
        foreach (var value in Enumerable.Range(0,count))
        {
            array.Add(value);
        }
        
        Assert.AreEqual(8, array.Find(v=>v>7));
        Assert.AreEqual(0, array.Find(v=>v>100));
    }
    
    [Test]
    public void FindIndex()
    {
        var array = new SortedArray();
        var count = 10;
        foreach (var value in Enumerable.Range(0,count))
        {
            array.Add(value*2);
        }
        
        Assert.AreEqual(5, array.FindIndex(10));
    }
    
    [Test]
    public void CountingSort()
    {
        var array = new SortedArray();
        var count = 10;
        for (int i = count - 1; i >= 0; i--)
        {
            array.Add(i);
        }
        
        Assert.Greater(array[count-1], array[0]);
    }
    
    [Test]
    public void CountingSortDesc()
    {
        var array = new SortedArray(true);
        var count = 10;
        for (int j = 0; j < count; j++)
        {
            array.Add(j);
        }
        
        Assert.Greater(array[0], array[count-1]);
    }
}
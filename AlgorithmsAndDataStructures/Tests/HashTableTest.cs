using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmsAndDataStructures._2_DataHash;
using NUnit.Framework;

namespace Tests;

public class HashTableTest
{
    Random rand = new();
    private HashTable _hashTable;
    
    [SetUp]
    public void Setup()
    {
        _hashTable = new HashTable(10);
        foreach (var _ in Enumerable.Range(0,5))
        {
            _hashTable.Add(GetRandomHash());
        }
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

    [Test]
    public void Init()
    {
        Assert.Pass();
    }
    
    [Test]
    public void Add()
    {
        var hash = GetRandomHash();
        _hashTable.Add(hash);
        
        Assert.AreEqual(hash,_hashTable.Find(hash)?.Value);
    }
    
    [Test]
    public void Remove()
    {
        var hash = GetRandomHash();
        _hashTable.Add(hash);
        
        Assert.AreEqual(hash,_hashTable.Find(hash)?.Value);
        _hashTable.Remove(hash);
        Assert.AreEqual(null,_hashTable.Find(hash)?.Value);
    }
    
    [Test]
    public void Overflow()
    {
        var hashTable = new HashTable(10);
        var hashesList = new List<string>();
        foreach (var _ in Enumerable.Range(0,100))
        {
            var h = GetRandomHash();
            hashesList.Add(h);
            hashTable.Add(h);
        }

        var counter = 0;
        foreach (var hash in hashesList)
        {
            if (hashTable.Contains(hash))
                counter++;
        }
        
        Assert.AreEqual(hashesList.Count, counter);
    }
    
    [Test]
    public void OverflowAsGarbageCollector()
    {
        var hashTable = new HashTable(10);
        var hashesList = new List<string>();
        var removalHash = GetRandomHash();
        hashTable.Add(removalHash);
        hashesList.Remove(removalHash);
        foreach (var _ in Enumerable.Range(0,100))
        {
            var h = GetRandomHash();
            hashesList.Add(h);
            hashTable.Add(h);
        }
        

        var counter = 0;
        foreach (var hash in hashesList)
        {
            if (hashTable.Contains(hash))
                counter++;
        }
        
        Assert.AreEqual(hashesList.Count, counter);
    }
    
    [Test]
    public void Find()
    {
        var hash = GetRandomHash();
        var notExistHash = GetRandomHash();
        _hashTable.Add(hash);
        
        Assert.AreEqual(hash,_hashTable.Find(hash)?.Value);
        Assert.AreEqual(null,_hashTable.Find(notExistHash)?.Value);
    }
    
    [Test]
    public void FindByIndex()
    {
        var hashTable = new HashTable(10);
        hashTable.Add("00AA00");
        var founded = false;
        foreach (var index in Enumerable.Range(0,10))
        {
            if ("00AA00" == hashTable.FindByIndex(index)?.Value)
                founded = true;
        }
        Assert.AreEqual(true,founded);
    }
    
    [Test]
    public void Contains()
    {
        var hash = GetRandomHash();
        var notExistHash = GetRandomHash();
        _hashTable.Add(hash);
        
        Assert.AreEqual(true,_hashTable.Contains(hash));
        Assert.AreEqual(false,_hashTable.Contains(notExistHash));
    }
    
    [Test]
    public void Collisions()
    {
        var hashTable = new HashTable(10);
        hashTable.Add("00AA00");
        hashTable.Add("00AA01");
        hashTable.Add("00AA02");
        hashTable.Add("00AA03");

        hashTable.Remove("00AA00");
        hashTable.Remove("00AA02");
        
        Assert.AreEqual(true,hashTable.Contains("00AA01"));
        Assert.AreEqual(true,hashTable.Contains("00AA03"));
        Assert.AreEqual(4,hashTable.Count);
    }
}
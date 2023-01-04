using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmsAndDataStructures._2_DataHash;
using NUnit.Framework;

namespace Tests;

public class HashTableTest
{
    
    
    private HashTable _hashTable;
    
    [SetUp]
    // Для некоторых тестов, инициализируем таблицу размером 10 и добавляем 5 элементов
    public void Setup()
    {
        _hashTable = new HashTable(10);
        foreach (var _ in Enumerable.Range(0,5))
        {
            _hashTable.Add(GetRandomHash());
        }
    }
    
    Random rand = new();
    // Функция получения случайных хеш-ключей
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
    // Проверка добавления в таблицу
    public void Add()
    {
        // Добавить рандомный хеш
        var hash = GetRandomHash();
        _hashTable.Add(hash);
        
        // Найти его в таблице
        Assert.AreEqual(hash,_hashTable.Find(hash)?.Value);
    }
    
    [Test]
    // Проверка удаления ключа из таблицы
    public void Remove()
    {
        // Добавить рандомный хеш
        var hash = GetRandomHash();
        _hashTable.Add(hash);
        // Проверить что он добавился в таблицу
        Assert.AreEqual(hash,_hashTable.Find(hash)?.Value);
        
        // Удалить и проверить что хеша в таблице нет
        _hashTable.Remove(hash);
        Assert.AreEqual(null,_hashTable.Find(hash)?.Value);
    }
    
    [Test]
    // Проверка переполнения таблицы
    public void Overflow()
    {
        // Создание таблицы размером 10
        var hashTable = new HashTable(10);
        var hashesList = new List<string>();
        
        //Добавление в таблицу 100 элементов
        foreach (var _ in Enumerable.Range(0,100))
        {
            var h = GetRandomHash();
            hashesList.Add(h);
            hashTable.Add(h);
        }

        var counter = 0;
        // Подсчет находящихся в таблице элементов
        foreach (var hash in hashesList)
        {
            if (hashTable.Contains(hash))
                counter++;
        }
        // Проверка, что количество сгенерированных хешей, равно
        // количеству хешей в таблице
        Assert.AreEqual(hashesList.Count, counter);
    }
    
    [Test]
    // Проверка, что при переполнении создается таблица, не включающая удаленные записи
    public void OverflowAsGarbageCollector()
    {
        // Создаем таблицу размером 10 
        var hashTable = new HashTable(10);
        var hashesList = new List<string>();
        // Добавляем хеш
        var removalHash = GetRandomHash();
        hashTable.Add(removalHash);
        // Удаляем этот же хеш (проставляем флаг deleted=true)
        hashesList.Remove(removalHash);
        
        // Добавляем 100 элементов в таблицу
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
        // На выходе ожидаем что будет 100 элементов, а не 101,
        // так как 1 удаленный элемент очистится
        Assert.AreEqual(hashesList.Count, counter);
    }
    
    [Test]
    // Поиск хеша
    public void Find()
    {
        // создаем рандомных хеш и добавляем его
        var hash = GetRandomHash();
        _hashTable.Add(hash);
        // создаем хеш который не будем добавлять в таблицу
        var notExistHash = GetRandomHash();
        
        // Добавленный должен быть в таблице
        Assert.AreEqual(hash,_hashTable.Find(hash)?.Value);
        // Другой же должен отсутствовать
        Assert.AreEqual(null,_hashTable.Find(notExistHash)?.Value);
    }
    
    [Test]
    // Поиск по индексу таблицы
    public void FindByIndex()
    {
        // Создаем таблицу размером 10
        var hashTable = new HashTable(10);
        // создаем рандомных хеш и добавляем его
        var hash = GetRandomHash();
        hashTable.Add(hash);
        var founded = false;
        // Перебираем все индексы в надежде найти наш хеш
        foreach (var index in Enumerable.Range(0,10))
        {
            if (hash == hashTable.FindByIndex(index)?.Value)
                founded = true;
        }
        // Проверяем нашли ли мы его
        Assert.AreEqual(true,founded);
    }
    
    [Test]
    // Содержит ли таблица заданный ключ или нет
    public void Contains()
    {
        // Генерируем рандомный хеш и добавляем его
        var hash = GetRandomHash();
        _hashTable.Add(hash);
        // Генерируем хеш, который добавлять не будем
        var notExistHash = GetRandomHash();
        
        // Первый хеш должен быть в таблице
        Assert.AreEqual(true,_hashTable.Contains(hash));
        // Второй должен отсутствовать
        Assert.AreEqual(false,_hashTable.Contains(notExistHash));
    }
    
    [Test]
    // Создание коллизий и удаление первых элементов создавших коллизию
    public void Collisions()
    {
        var hashTable = new HashTable(10);
        hashTable.Add("00AA00");
        hashTable.Add("00AA01");
        hashTable.Add("00AA02");
        hashTable.Add("00AA03");

        hashTable.Remove("00AA00");
        hashTable.Remove("00AA02");
        
        // После удаления элементов создавших коллизию,
        // остальные ключи должны находиться в таблице
        Assert.AreEqual(true,hashTable.Contains("00AA01"));
        Assert.AreEqual(true,hashTable.Contains("00AA03"));
        
        // Размер таблице не должен изменяться, так как мы проставляем флаг deleted=true
        Assert.AreEqual(4,hashTable.Count);
    }
    
    
    [Test]
    // Функция создающая новую таблицу такого же размера,
    // но без удаленных узлов
    public void Reheash()
    {
        // Создаем таблицу размера 10
        var hashTable = new HashTable(10);

        var removeList = new List<string>();
        // Добавляем 10_000 элементов
        for (int i = 0; i < 10000; i++)
        {
            var h = GetRandomHash();
            hashTable.Add(h);
            // Отдельно запоминаем каждый второй
            if (i % 2 == 0)
                removeList.Add(h);
        }
        
        var count = hashTable.Count;
        hashTable.Rehash();
        // Количество элементов до reheash и после должно быть одинаковым
        Assert.AreEqual(count,hashTable.Count);
        
        // Теперь удаляем каждый второй хеш, который мы запомнили ранее
        count = hashTable.Count;
        foreach (var data in removeList)
        {
            hashTable.Remove(data);
        }
        // Количество элементов в таблице не должно измениться
        Assert.AreEqual(count,hashTable.Count);
        hashTable.Rehash();
        // После reheash таблица должна очиститься от удаленных ранее узлов
        Assert.AreEqual(count-removeList.Count,hashTable.Count);
    }
}
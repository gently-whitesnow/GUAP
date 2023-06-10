using HowTo.Entities.Hashtable;

namespace HowTo.Tests;

public class HashtableTests
{

    [Fact]
    // Проверка добавления в таблицу
    public void Add()
    {
        var hashTable = new HashTable(10);
        foreach (var a in Enumerable.Range(0,5))
        {
            hashTable.Add(a);
        }
        // Добавить рандомный хеш
        var hash = 7;
        hashTable.Add(hash);
        
        // Найти его в таблице
        Assert.Equal(hash,hashTable.Find(hash)?.Value);
    }
    
    [Fact]
    // Проверка переполнения таблицы
    public void Overflow()
    {
        // Создание таблицы размером 10
        var hashTable = new HashTable(10);
        var hashesList = new List<int>();
        
        //Добавление в таблицу 100 элементов
        foreach (var a in Enumerable.Range(0,100))
        {
            hashesList.Add(a);
            hashTable.Add(a);
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
        Assert.Equal(hashesList.Count, counter);
    }
    
    [Fact]
    // Проверка, что при переполнении создается таблица, не включающая удаленные записи
    public void OverflowAsGarbageCollector()
    {
        // Создаем таблицу размером 10 
        var hashTable = new HashTable(10);
        var hashesList = new List<int>();
        // Добавляем хеш
        var removalHash = 7;
        hashTable.Add(removalHash);
        // Удаляем этот же хеш (проставляем флаг deleted=true)
        hashesList.Remove(removalHash);
        
        // Добавляем 100 элементов в таблицу
        foreach (var a in Enumerable.Range(0,100))
        {
            hashesList.Add(a);
            hashTable.Add(a);
        }
        
        var counter = 0;
        foreach (var hash in hashesList)
        {
            if (hashTable.Contains(hash))
                counter++;
        }
        // На выходе ожидаем что будет 100 элементов, а не 101,
        // так как 1 удаленный элемент очистится
        Assert.Equal(hashesList.Count, counter);
    }
    
    [Fact]
    // Поиск хеша
    public void Find()
    {
        var hashTable = new HashTable(10);
        foreach (var a in Enumerable.Range(0,5))
        {
            hashTable.Add(a);
        }
        // создаем рандомных хеш и добавляем его
        var hash = 7;
        hashTable.Add(hash);
        // создаем хеш который не будем добавлять в таблицу
        var notExistHash = 8;
        
        // Добавленный должен быть в таблице
        Assert.Equal(hash,hashTable.Find(hash)?.Value);
        // Другой же должен отсутствовать
        Assert.Equal(null,hashTable.Find(notExistHash)?.Value);
    }
    
    [Fact]
    // Поиск по индексу таблицы
    public void FindByIndex()
    {
        // Создаем таблицу размером 10
        var hashTable = new HashTable(10);
        // создаем рандомных хеш и добавляем его
        var hash = 1;
        hashTable.Add(hash);
        var founded = false;
        // Перебираем все индексы в надежде найти наш хеш
        foreach (var index in Enumerable.Range(0,10))
        {
            if (hash == hashTable.FindByIndex(index)?.Value)
                founded = true;
        }
        // Проверяем нашли ли мы его
        Assert.Equal(true,founded);
    }
    
    [Fact]
    // Проверка удаления ключа из таблицы
    public void Remove()
    {
        var hashTable = new HashTable(10);
        foreach (var a in Enumerable.Range(0,5))
        {
            hashTable.Add(a);
        }
        // Добавить рандомный хеш
        var hash = 7;
        hashTable.Add(hash);
        // Проверить что он добавился в таблицу
        Assert.Equal(hash,hashTable.Find(hash)?.Value);
        
        // Удалить и проверить что хеша в таблице нет
        hashTable.Remove(hash);
        Assert.Equal(null,hashTable.Find(hash)?.Value);
    }
    
    [Fact]
    // Содержит ли таблица заданный ключ или нет
    public void Contains()
    {
        var hashTable = new HashTable(10);
        foreach (var a in Enumerable.Range(0,5))
        {
            hashTable.Add(a);
        }
        // Генерируем рандомный хеш и добавляем его
        var hash = 7;
        hashTable.Add(hash);
        // Генерируем хеш, который добавлять не будем
        var notExistHash = 8;
        
        // Первый хеш должен быть в таблице
        Assert.Equal(true,hashTable.Contains(hash));
        // Второй должен отсутствовать
        Assert.Equal(false,hashTable.Contains(notExistHash));
    }
    
    [Fact]
    // Создание коллизий и удаление первых элементов создавших коллизию
    public void Collisions()
    {
        var hashTable = new HashTable(10);
        hashTable.Add(10);
        hashTable.Add(11);
        hashTable.Add(1);
        hashTable.Add(2);

        hashTable.Remove(10);
        hashTable.Remove(2);
        
        // После удаления элементов создавших коллизию,
        // остальные ключи должны находиться в таблице
        Assert.Equal(true,hashTable.Contains(11));
        Assert.Equal(true,hashTable.Contains(1));
        
        // Размер таблице не должен изменяться, так как мы проставляем флаг deleted=true
        Assert.Equal(4,hashTable.Count);
    }
}
using System;
using AlgorithmsAndDataStructures._2_DataHash;

namespace HowTo.Entities.Hashtable;

public class HashTable
{
    // Дефолтный размер таблицы
    private readonly long _defaultTableSize = 8;

    // n - размер таблицы
    private long _tableSize;

    // количество заполненных ячеек в таблице
    public long Count;

    // коэффициент заполнения, после которого таблица расширяется
    private const double FilledCoefficient = 0.7;

    private Node?[] _table;

    public HashTable(long tableSize)
    {
        _tableSize = tableSize;
        _table = new Node[tableSize];
    }

    public HashTable()
    {
        _tableSize = _defaultTableSize;
        _table = new Node[_defaultTableSize];
    }

    private void Resize()
    {
        var oldTable = (Node?[]) _table.Clone();
        _tableSize *= 2;
        Count = 0;
        _table = new Node[_tableSize];
        foreach (var n in oldTable)
        {
            if (n == null || n.Deleted)
                continue;
            Add(n.Value);
        }
    }

    public Node? Find(int key)
    {

        var i1 = GetIndex(key);
        for (int i = 0; i < _tableSize; i++)
        {
            if (_table[i1]?.Value == key && !_table[i1].Deleted)
            {
                return _table[i1];
            }

            i1 = (i1+i+1) % _tableSize;
        }

        return null;
    }

    public Node? FindByIndex(long index)
    {
        if (index >= _tableSize || index < 0)
            throw new ArgumentOutOfRangeException();

        return _table[index];
    }

    public void Add(int key)
    {
        if (Count > _tableSize * FilledCoefficient)
            Resize();

        var i1 = GetIndex(key);
        for (int i = 0; i < _tableSize; i++)
        {
            if (_table[i1] == null)
            {
                _table[i1] = new Node(key);
                Count++;
                return;
            }
            
            i1 = (i1+i+1) % _tableSize;
        }

        // Если не получилось добавить
        Resize();
        Add(key);
    }

    public long GetIndex(int key)
    {
        // нормализация числа согласно размеру таблицы
        var normalizeNumber = key % _tableSize;

        return normalizeNumber;
    }
    
    public bool Contains(int hashKey)
    {
        if (Find(hashKey) == null)
            return false;
        return true;
    }
    
    public bool Remove(int hashKey)
    {
        var node = Find(hashKey);
        if (node == null)
            return false;
        node.Deleted = true;
        return true;
    }
}
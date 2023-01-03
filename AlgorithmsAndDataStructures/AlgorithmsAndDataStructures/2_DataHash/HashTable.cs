using System;

namespace AlgorithmsAndDataStructures._2_DataHash;

public class HashTable
{
    // Заданный формат хеша
    private readonly string _format = "99ZZ99";

    // 00AA00 - 10*10*26*26*10*10 = 6_760_000 комбинаций
    private const long Combinations = 6_760_000;

    private readonly long _defaultTableSize = 3;

    // n - размер таблицы
    private long TableSize;

    // количество заполненных индексов
    public long Count;

    // коэффициент заполнения
    private readonly double FilledCoefficient = 0.7;


    private Node?[] _table;

    public HashTable(long tableSize)
    {
        TableSize = tableSize;
        _table = new Node[tableSize];
    }

    public HashTable()
    {
        TableSize = _defaultTableSize;
        _table = new Node[_defaultTableSize];
    }

    private void Resize()
    {
        var oldTable = (Node?[]) _table.Clone();
        TableSize *= 2;
        Count = 0;
        _table = new Node[TableSize];
        foreach (var n in oldTable)
        {
            if (n == null || n.Deleted)
                continue;
            Add(n.Value);
        }
    }

    public bool Remove(string hashKey)
    {
        var node = Find(hashKey);
        if (node == null)
            return false;
        node.Deleted = true;
        return true;
    }

    public bool Contains(string hashKey)
    {
        if (Find(hashKey) == null)
            return false;
        return true;
    }

    public Node? Find(string hashKey)
    {
        Validate(hashKey);

        var i1 = GetIndexByDivision(hashKey);
        var i2 = GetIndexByFibo(hashKey);
        for (int i = 0; i < TableSize; i++)
        {
            if (_table[i1]?.Value == hashKey && !_table[i1].Deleted)
            {
                return _table[i1];
            }

            i1 = (i*i1 + i2) % TableSize;
        }

        return null;
    }
    
    public Node? FindByIndex(long index)
    {
        if (index >= TableSize || index < 0)
            throw new ArgumentOutOfRangeException();
        
        return _table[index];
    }

    public void Add(string hashKey)
    {
        Validate(hashKey);

        if (Count > TableSize * FilledCoefficient)
            Resize();

        var i1 = GetIndexByDivision(hashKey);
        var i2 = GetIndexByFibo(hashKey);
        for (int i = 0; i < TableSize; i++)
        {
            if (_table[i1] == null)
            {
                _table[i1] = new Node(hashKey);
                Count++;
                return;
            }

            i1 = (i*i1 + i2) % TableSize;
        }
        // Если не получилось добавить
        Resize();
        Add(hashKey);
    }

    public long GetIndexByDivision(string key)
    {
        Validate(key);

        // преобразование ключа в число
        var number = ConvertToNumber(key);

        // нормализация числа согласно размеру таблицы
        var normalizeNumber = (long) (number * ((double) TableSize / Combinations));

        return normalizeNumber;

        long ConvertToNumber(string hash)
        {
            var num = 0;
            num += int.Parse(hash[5].ToString());
            num += int.Parse(hash[4].ToString()) * 10;
            num += (hash[3] - 'A') * 10 * 10;
            num += (hash[2] - 'A') * 26 * 10 * 10;
            num += int.Parse(hash[1].ToString()) * 26 * 26 * 10 * 10;
            num += int.Parse(hash[0].ToString()) * 26 * 26 * 10 * 10 * 10;

            return num;
        }
    }

    //https://kvodo.ru/hesh-funktsii.html
    public long GetIndexByFibo(string key)
    {
        Validate(key);

        // преобразование ключа в число
        var number = ConvertToNumber(key);

        // золотое сечение
        // мультипликативная хеш-функция
        // коэффициент
        var A = (Math.Sqrt(5) - 1) / 2; // 0,6180339887

        // дробная часть при умножении на коэффициент
        var part = A * number - (int) (A * number);

        // количество комбинаций на полученную дробную часть
        var index = (long) (Combinations * part);

        // нормализация числа согласно размеру таблицы
        var normalizeNumber = (long) (index * ((double) TableSize / Combinations));

        return normalizeNumber;

        long ConvertToNumber(string hash)
        {
            var num = 0;
            num += int.Parse(hash[5].ToString());
            num += int.Parse(hash[4].ToString()) * 10;
            num += (hash[3] - 'A') * 10 * 10;
            num += (hash[2] - 'A') * 26 * 10 * 10;
            num += int.Parse(hash[1].ToString()) * 26 * 26 * 10 * 10;
            num += int.Parse(hash[0].ToString()) * 26 * 26 * 10 * 10 * 10;

            return num;
        }
    }

    void Validate(string inputKey)
    {
        if (inputKey.Length != _format.Length)
            throw new FormatException("Неверный размер хеш ключа");

        if (!char.IsDigit(inputKey[0]))
            throw new FormatException($"Неверный формат {0} символа {inputKey}");
        if (!char.IsDigit(inputKey[1]))
            throw new FormatException($"Неверный формат {1} символа {inputKey}");
        if (!char.IsUpper(inputKey[2]))
            throw new FormatException($"Неверный формат {2} символа {inputKey}");
        if (!char.IsUpper(inputKey[3]))
            throw new FormatException($"Неверный формат {3} символа {inputKey}");
        if (!char.IsDigit(inputKey[4]))
            throw new FormatException($"Неверный формат {4} символа {inputKey}");
        if (!char.IsDigit(inputKey[5]))
            throw new FormatException($"Неверный формат {5} символа {inputKey}");
    }
}
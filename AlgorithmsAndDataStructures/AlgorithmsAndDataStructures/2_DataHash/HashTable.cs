using System;

namespace AlgorithmsAndDataStructures._2_DataHash;

public class HashTable
{
    // Заданный формат хеша
    private readonly string _format = "99ZZ99";

    // 00AA00 - 10*10*26*26*10*10 = 6_760_000 комбинаций
    private const long Combinations = 6_760_000;

    private readonly long _defaultTableSize = 8;

    // n - размер таблицы
    private long _tableSize;

    // количество заполненных индексов
    public long Count;

    // коэффициент заполнения
    private const double FilledCoefficient = 0.7;

    // Количество коллизий (экспериментальный параметр)
    public long Collisions = 0;


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
        Collisions = 0;
        _table = new Node[_tableSize];
        foreach (var n in oldTable)
        {
            if (n == null || n.Deleted)
                continue;
            Add(n.Value);
        }
    }

    // Используется в качестве демонтрации, так как rehash происходит при resize
    public void Rehash()
    {
        var oldTable = (Node?[]) _table.Clone();
        Count = 0;
        Collisions = 0;
        _table = new Node[_tableSize];
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
        if (IsNotValid(hashKey))
            return null;

        var i1 = GetIndexByDivision(hashKey);
        var i2 = GetIndexByFibo(hashKey);
        for (int i = 0; i < _tableSize; i++)
        {
            if (_table[i1]?.Value == hashKey && !_table[i1].Deleted)
            {
                return _table[i1];
            }

            i1 = (i * i1 + i2) % _tableSize;
        }

        return null;
    }

    public Node? FindByIndex(long index)
    {
        if (index >= _tableSize || index < 0)
            throw new ArgumentOutOfRangeException();

        return _table[index];
    }

    public void Add(string hashKey)
    {
        if (IsNotValid(hashKey))
            return;

        if (Count > _tableSize * FilledCoefficient)
            Resize();

        var i1 = GetIndexByDivision(hashKey);
        var i2 = GetIndexByFibo(hashKey);
        for (int i = 0; i < _tableSize; i++)
        {
            if (_table[i1] == null)
            {
                _table[i1] = new Node(hashKey);
                Count++;
                return;
            }

            Collisions++;
            i1 = (i * i1 + i2) % _tableSize;
        }

        // Если не получилось добавить
        Resize();
        Add(hashKey);
    }

    public long GetIndexByDivision(string key)
    {
        if (IsNotValid(key))
            return -1;

        // преобразование ключа в число
        var number = ConvertToNumber(key);

        // нормализация числа согласно размеру таблицы
        var normalizeNumber = (long) (number * ((double) _tableSize / Combinations));

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
        if (IsNotValid(key))
            return 01;

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
        var normalizeNumber = (long) (index * ((double) _tableSize / Combinations));

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

    bool IsNotValid(string inputKey)
    {
        if (inputKey.Length != _format.Length)
        {
            Console.WriteLine("Неверный размер хеш ключа");
            return true;
        }

        if (!char.IsDigit(inputKey[0]))
        {
            Console.WriteLine($"Неверный формат {0} символа {inputKey}");
            return true;
        }
        if (!char.IsDigit(inputKey[1]))
        {
            Console.WriteLine($"Неверный формат {1} символа {inputKey}");
            return true;
        }
        if (!char.IsUpper(inputKey[2]))
        {
            Console.WriteLine($"Неверный формат {2} символа {inputKey}");
            return true;
        }
        if (!char.IsUpper(inputKey[3]))
        {
            Console.WriteLine($"Неверный формат {3} символа {inputKey}");
            return true;
        }
        if (!char.IsDigit(inputKey[4]))
        {
            Console.WriteLine($"Неверный формат {4} символа {inputKey}");
            return true;
        }
        if (!char.IsDigit(inputKey[5]))
        {
            Console.WriteLine($"Неверный формат {5} символа {inputKey}");
            return true;
        }

        return false;
    }

    public void Print()
    {
        for (int i = 0; i < _tableSize; i++)
        {
            var node = _table[i];
            var value = node != null && !node.Deleted ? node.Value : "";
            Console.WriteLine($"{i} - {value}");
        }
    }
}
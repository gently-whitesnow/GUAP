using System;

namespace AlgorithmsAndDataStructures._3_Sorting;

public class SortedArray
{
    private const int DefaultCapacity = 8;
    private int Capacity { get; set; } = DefaultCapacity;
    public int Length { get; set; } = 0;

    private int[] _array;

    // Направление сортировки
    private readonly bool _descending = false;

    public SortedArray(int capacity, bool descending = false)
    {
        Capacity = capacity < 0 ? 0 : capacity;
        _array = new int[Capacity];
        _descending = descending;
    }

    public SortedArray(bool descending = false)
    {
        _array = new int[Capacity];
        _descending = descending;
    }

    public void Add(int value)
    {
        if (Length + 1 >= Capacity)
            Resize();
        _array[Length] = value;
        Length++;
        CountingSort();
    }


    public bool Remove(int value)
    {
        var index = FindIndex(value);
        if (index == -1)
            return false;
        RemoveIndex(index);
        return true;
    }

    public void RemoveIndex(int index)
    {
        var oldArray = (int[]) _array.Clone();
        var di = 0;
        for (int i = 0; i < Length; i++)
        {
            if (i == index)
            {
                di++;
                continue;
            }

            _array[i - di] = oldArray[i];
        }

        Length--;
    }

    public int FindIndex(int value)
    {
        for (int i = 0; i < Length; i++)
        {
            if (_array[i].Equals(value))
                return i;
        }

        return -1;
    }

    public int Find(Predicate<int> condition)
    {
        for (int i = 0; i < Length; i++)
        {
            if (condition(_array[i]))
                return _array[i];
        }

        return default;
    }

    // descending = true по нисходящей, от большего к меньшему
    private void CountingSort()
    {
        if (Length < 1)
            return;

        var newArray = new int[Capacity];
        var equals = 0;

        for (int i = 0; i < Length; i++)
        {
            var k = 0;
            for (int j = 0; j < Length; j++)
            {
                if (_array[i] > _array[j] || _array[i] == _array[j] && i < j)
                    k++;
                equals++;
            }

            var index = _descending ? Length - k - 1 : k;
            newArray[index] = _array[i];
        }
        Console.WriteLine($"Сортировка - перестановок {Length}, cравнений {equals}");

        _array = newArray;
    }

    // Индексатор
    public int this[int index]
    {
        get
        {
            if (index >= Length || index < 0)
                throw new ArgumentOutOfRangeException();
            return _array[index];
        }
        set
        {
            if (index >= Length || index < 0)
                throw new ArgumentOutOfRangeException();
            _array[index] = value;
        }
    }

    public void Print()
    {
        for (int i = 0; i < Length; i++)
        {
            Console.Write(_array[i] + " ");
        }

        Console.WriteLine();
    }

    private void Resize()
    {
        var newcapacity = Capacity == 0 ? DefaultCapacity : 2 * Capacity;

        if ((uint) newcapacity > int.MaxValue) newcapacity = int.MaxValue;

        if (newcapacity < Capacity) newcapacity = Capacity;

        Capacity = newcapacity;

        var newArray = new int[Capacity];
        for (int i = 0; i < Length; i++)
        {
            newArray[i] = _array[i];
        }

        _array = newArray;
    }
}
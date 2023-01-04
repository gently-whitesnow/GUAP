using System;

namespace AlgorithmsAndDataStructures._3_Sorting;

public class SortedArray
{
    // Дефолтный размер массива
    private const int DefaultCapacity = 8;
    
    // Размер массива
    private int Capacity { get; set; } = DefaultCapacity;
    // Количество элементов в массиве
    public int Length { get; set; } = 0;
    // Массив с данными
    private int[] _array;

    // Направление сортировки (по умолчанию - сортировка по возрастанию)
    private readonly bool _descending = false;
    
    // Конструктор при задании размера массива
    public SortedArray(int capacity, bool descending = false)
    {
        Capacity = capacity < 0 ? 0 : capacity;
        _array = new int[Capacity];
        _descending = descending;
    }

    // Дефолтный конструктор
    public SortedArray(bool descending = false)
    {
        _array = new int[Capacity];
        _descending = descending;
    }

    // Добавление значения
    public void Add(int value)
    {
        if (Length + 1 >= Capacity)
            Resize();
        _array[Length] = value;
        Length++;
        // Сортировка после добавления
        CountingSort();
    }

    // Удаление значения
    public bool Remove(int value)
    {
        var index = FindIndex(value);
        if (index == -1)
            return false;
        RemoveIndex(index);
        return true;
    }
    
    // Удаление значение по его индексу
    // При удалении значения, все элементы после текущего индекса
    // смещаются на один индекс к началу
    public void RemoveIndex(int index)
    {
        var di = 0;
        for (int i = 0; i < Length; i++)
        {
            if (i == index)
            {
                di++;
                continue;
            }

            _array[i - di] = _array[i];
        }

        Length--;
    }

    // Получение элемента по индексу
    public int FindIndex(int value)
    {
        for (int i = 0; i < Length; i++)
        {
            if (_array[i].Equals(value))
                return i;
        }

        return -1;
    }

    // Получение первого элемента соответствующего условию
    public int Find(Predicate<int> condition)
    {
        for (int i = 0; i < Length; i++)
        {
            if (condition(_array[i]))
                return _array[i];
        }

        return default;
    }
    
    // Сортировка подсчетом
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
            // Базово - сортировка по возрастанию
            var index = _descending ? Length - k - 1 : k;
            newArray[index] = _array[i];
        }
        Console.WriteLine($"Сортировка - перестановок {Length}, cравнений {equals}");

        _array = newArray;
    }

    // Индексатор, для обращения к массиву через текущий класс
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

    // Вывод элементов находящихся в массиве
    public void Print()
    {
        for (int i = 0; i < Length; i++)
        {
            Console.Write(_array[i] + " ");
        }

        Console.WriteLine();
    }

    // Увеличение размера массива при достижении его максимального размера
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
using System;

namespace AlgorithmsAndDataStructures._3_Sorting;

public class SortedArray<TValue> where TValue : struct
{
    private int _capacity { get; set; } = 8;
    public int Length { get; set; } = 0;

    private TValue[] array;

    public SortedArray(int capacity)
    {
        _capacity = capacity;
        array = new TValue[_capacity];
    }

    public SortedArray()
    {
        array = new TValue[_capacity];
    }

    public void Add(TValue value)
    {
        array[Length] = value;
        Length++;
    }

    public void Remove(TValue value)
    {
        for (int i = 0; i < Length; i++)
        {
            // array[Length] = null;
        }
    }

    public int FindIndex(TValue value)
    {
        for (int i = 0; i < Length; i++)
        {
            if (array[i].Equals(value))
                return i;
        }

        return -1;
    }

    public TValue Find(Predicate<TValue> condition)
    {
        for (int i = 0; i < Length; i++)
        {
            if (condition(array[i]))
                return array[i];
        }

        return default;
    }
    
    // Индексатор
    public TValue this[int index]
    {
        get => array[index];
        set => array[index] = value;
    }
    
    
}
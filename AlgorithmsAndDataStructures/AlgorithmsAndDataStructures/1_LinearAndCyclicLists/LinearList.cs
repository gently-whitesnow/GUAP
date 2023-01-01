namespace AlgorithmsAndDataStructures._1_LinearAndCyclicLists;

public class LinearList<TValue>
{
    public Node<TValue>? First;
    public Node<TValue>? Last;

    public LinearList()
    {
        
    }
    public LinearList(LinearList<TValue> list)
    {
        First = list.First;
        Last = list.Last;
    }

    /// <summary>
    /// Проверка наличия узлов в списке
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return First == null;
    }

    public void Add(TValue value)
    {
        var node = new Node<TValue>(value);

        if (IsEmpty())
        {
            First = node;
            Last = node;
            return;
        }

		// предыдущий элемент
        Last.Next = node;
		// последний элемент
        Last = node;
    }

    public override string ToString()
    {
        var str = "";
        if (IsEmpty())
            return str;
        var cursor = First;
        while (cursor != null)
        {
            str += $"{cursor.Value?.ToString() ?? "null"} ";
            cursor = cursor.Next;
        }

        return str.TrimEnd();
    }

	// Находит первый элемент соответствующий значению или default при его отсутствии
    public Node<TValue>? FindFirstOrDefault(TValue value)
    {
        if (IsEmpty())
            return null;
        var cursor = First;
        while (cursor != null)
        {
            if (cursor.Value != null && cursor.Value.Equals(value) || cursor.Value == null && value == null)
                return cursor;
            cursor = cursor.Next;
        }

        return default;
    }
    
    public bool Contains(TValue value)
    {
        if (IsEmpty())
            return false;
        var cursor = First;
        while (cursor != null)
        {
            if (cursor.Value != null && cursor.Value.Equals(value) || cursor.Value == null && value == null)
                return true;
            cursor = cursor.Next;
        }

        return false;
    }

    public void RemoveFirst()
    {
        if(IsEmpty())
            return;
        First = First.Next;
    }
    
    public void RemoveLast()
    {
        if(IsEmpty())
            return;
        if (Last == First)
        {
            RemoveFirst();
            return;
        }
        
        var cursor = First;
        while (cursor != null)
        {
            if (cursor.Next == Last)
            {
                cursor.Next = null;
                break;
            }
            cursor = cursor.Next;
        }
    }

	// Удаляет первый элемент соответствующий значению или ничего не делает
    public void RemoveOrDefault(TValue value)
    {
        if(IsEmpty())
            return;
        
        if (First.Value != null && First.Value.Equals(value) || First.Value == null && value == null)
        {
            RemoveFirst();
            return;
        }

        if (Last.Value != null && Last.Value.Equals(value) || Last.Value == null && value == null)
        {
            RemoveLast();
            return;
        }

        var cursorSlow = First;
        var cursorFast = First.Next;

        while (cursorFast != null)
        {
            if (cursorFast.Value != null && cursorFast.Value.Equals(value) || cursorFast.Value == null && value == null)
            {
                cursorSlow.Next = cursorFast.Next;
                break;
            }
            cursorFast = cursorFast.Next;
            cursorSlow = cursorSlow.Next;
        }
    }

    public LinearList<TValue> Distinct()
    {
        var distinctedList = new LinearList<TValue>();
        if(IsEmpty())
            return distinctedList;
        
        var cursorParent = First;
        while (cursorParent!=null)
        {
            if(!distinctedList.Contains(cursorParent.Value))
                distinctedList.Add(cursorParent.Value);
            cursorParent = cursorParent.Next;
        }
        return distinctedList;
    }
}
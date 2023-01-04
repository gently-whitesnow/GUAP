namespace AlgorithmsAndDataStructures._1_LinearAndCyclicLists;

public class LinearList<TValue>
{
    // Ссылка на первый элемент списка
    public Node<TValue>? First;
    // Ссылка на последний элемент списка
    public Node<TValue>? Last;
    
    /// Проверка наличия узлов в списке
    public bool IsEmpty()
    {
        return First == null;
    }

    // Добавление элемента в список
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
    
    // Проверяет находится ли элемент в списке
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

    // Удаляет первый элемент
    public void RemoveFirst()
    {
        if(IsEmpty())
            return;
        First = First.Next;
    }
    
    // Удаляет последний элемент
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

    // Оставляет только уникальные значения
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
    
    // Метод для вывода на экран содержимого списка
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
}
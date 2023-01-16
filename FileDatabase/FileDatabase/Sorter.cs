using System;
using System.Collections.Generic;
using System.Linq;

namespace FileDatabase;

public static class Sorter
{
    public static bool Descending;
    public static string SortingInfo;
    public static bool IsSorting;

    public static Func<IEnumerable<Train>, IEnumerable<Train>> OrderCondition = list => list;

    public static void SortBy<TField>(string sortByInfo, Func<Train, TField> fieldSorted)
    {
        if (SortingInfo == $"сортировка по {sortByInfo}")
        {
            OrderCondition = list => list;
            SortingInfo = "";
            return;
        }

        OrderCondition = list => Descending ? list.OrderByDescending(fieldSorted) : list.OrderBy(fieldSorted);
        SortingInfo = $"сортировка по {sortByInfo}";
    }

    public static void Reset()
    {
        Descending = false;
        SortingInfo = "";
        IsSorting = false;
        OrderCondition = list => list;
    }
}
using EBooks.Core.Entities;

namespace EBooks.Core.Extensions;

public static class ListExtensions
{
    public static int BinarySearch<TDbModel>(this List<TDbModel> list, DbModel item)
        where TDbModel : DbModel
    {
        return BinarySearch(list, item.Id);
    }
    
    public static int BinarySearch<TDbModel>(this List<TDbModel> list, uint searchableId)
        where TDbModel : DbModel
    {
        var left = 0;
        var right = list.Count - 1;

        while (left <= right)
        {
            var middle = (left + right) / 2;

            var comparisonResult = list[middle].Id.CompareTo(searchableId);

            if (comparisonResult == 0)
            {
                return middle;
            }

            if (comparisonResult < 0)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        // возврат ожидаемого места вставки значения
        return ~left;
    }
}
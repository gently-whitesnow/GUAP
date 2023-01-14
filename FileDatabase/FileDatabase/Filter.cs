using System;
using System.Collections.Generic;
using System.Linq;

namespace FileDatabase;

public static class Filter
{
    // 5 15 20 20 
    public static int? IdFilter = null;
    public static string? TrainNumberFilter = null;
    public static string? PointNameFilter = null;
    public static DateTime? DepartureTimeFilter = null;

    public static bool IsFiltering;

    public static Func<IEnumerable<Train>, IEnumerable<Train>> FilterCondition = (list) => list.Where(v =>
        (IdFilter == null || v.Id == IdFilter.Value) &&
        (TrainNumberFilter == null || v.TrainNumber == TrainNumberFilter) &&
        (PointNameFilter == null || v.PointName == PointNameFilter) &&
        (DepartureTimeFilter == null || v.DepartureTime.Equals(DepartureTimeFilter)));

    public static void Clear()
    {
        IdFilter = null;
        TrainNumberFilter = null;
        PointNameFilter = null;
        DepartureTimeFilter = null;
    }

    public static bool IsNotEmpty()
    {
        return IdFilter != null || TrainNumberFilter != null || PointNameFilter != null || DepartureTimeFilter != null;
    }

    public static string GetActiveFilters()
    {
        var id = IdFilter == null ? "" : $"NumberFilter = {IdFilter.Value}";
        var trainNumber = TrainNumberFilter == null ? "" : $"TrainNumber = {TrainNumberFilter}";
        var pointName = PointNameFilter == null ? "" : $"PointName = {PointNameFilter}";
        var departureTime = DepartureTimeFilter == null ? "" : $"DepartureTime = {DepartureTimeFilter}";
        return $"{id} {trainNumber} {pointName} {departureTime}";
    }
}
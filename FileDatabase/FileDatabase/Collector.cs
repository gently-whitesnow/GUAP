using System;
using System.Globalization;

namespace FileDatabase;

public static class Collector
{
    public static bool IsChanging;
    
    public static void AddTrain(BdContext _bdContext)
    {
        var valueTrainNumber =  GetTrainNumber("value");
        if(string.IsNullOrEmpty(valueTrainNumber))
            return;
        var valuePointName =  GetPointName("value");
        if(string.IsNullOrEmpty(valuePointName))
            return;
        var valueDepartureTime =  GetDepartureTime("value");
        if(valueDepartureTime ==null)
            return;
        if (GetApproval($"add {valueTrainNumber} {valuePointName} {valueDepartureTime.Value.ToString("g",CultureInfo.GetCultureInfo("de-DE"))}"))
            _bdContext.Insert(new Train(valueTrainNumber,valuePointName,valueDepartureTime.Value));
    }

    public static int? GetId(string whatDo)
    {
        return Cage<int?>($"Type {whatDo} for id", "X:int and X > 0 and X < 100000", (s) =>
        {
            if (int.TryParse(s, out var value) && value < 100_000)
                return value;
            return null;
        });
    }

    public static string? GetTrainNumber(string whatDo)
    {
        return Cage<string?>($"Type {whatDo} for TrainNumber", "X:string and X.Length < 16", (s) =>
        {
            if (s.Length <= 15)
                return s;
            return null;
        });
    }

    public static string? GetPointName(string whatDo)
    {
        return Cage<string?>($"Type {whatDo} for PointName", "X:string and X.Length < 21", (s) =>
        {
            if (s.Length <= 20)
                return s;
            return null;
        });
    }

    public static DateTime? GetDepartureTime(string whatDo)
    {
        return Cage<DateTime?>($"Type {whatDo} for DepartureTime",
            "X:datetime and X.Length < 20 and format: dd.MM.yyyy hh:mm:ss", (s) =>
            {
                if (s.Length <= 19 && DateTime.TryParseExact(s, "g", CultureInfo.GetCultureInfo("de-DE"), DateTimeStyles.None, out var value))
                    return value;
                return null;
            });
    }
    
    public static bool GetApproval(string whatDo)
    {
        return Cage<bool?>($"Enter 'Y' if agree {whatDo}", "", (s) =>
        {
            if (s.Trim().ToLower() is "y")
                return true;
            return false;
        })??false;
    }

    private static T? Cage<T>(string requestMessage, string humanReadableCondition, Func<string, T?> condition)
    {
        while (true)
        {
            Console.WriteLine();
            Printer.Step($"{requestMessage}, nothing for exit :");
            var val = Console.ReadLine();
            if (string.IsNullOrEmpty(val))
                return default;
            var res = condition(val);
            if (res != null)
                return res;
            Printer.Error($"Incorrect value, required {humanReadableCondition}");
        }
    }
}
using System;

namespace AlgorithmsAndDataStructures;

public static class Input
{
    public static int? GetUInt(string humanReadableString)
    {
        return BaseGet<int?>(humanReadableString, val =>
        {
            if (int.TryParse(val, out var num) && num>=0)
                return num;
            return null;
        });
    }

    public static string GetString(string humanReadableString)
    {
        return BaseGet<string>(humanReadableString, val => val);
    }

    private static TValue BaseGet<TValue>(string humanReadableString, Func<string, TValue?> condition)
    {
        Console.WriteLine(humanReadableString);
        while (true)
        {
            var val = Console.ReadLine();
            if (!string.IsNullOrEmpty(val))
            {
                var result = condition(val);
                if (result != null)
                    return result;
            }
            Console.WriteLine("Ошибка ввода");
        }
    }
}
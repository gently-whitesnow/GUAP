using System;
using System.Collections.Generic;
using System.Globalization;

namespace FileDatabase;

public static class Printer
{
    
    public static void Error(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
        
    public static void Warning(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
        
    public static void Success(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
        
    public static void Prompt(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
        
    public static void Speech(string humanReadableString)
    {
        Console.ResetColor();
        Console.WriteLine(humanReadableString);
    }
        
    public static void Step(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
    
    public static void Data(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
    
    public static void PrintHeader()
    {
        var formattedId = "| " + "Id".PadRight(5);
        var formattedTrainNumber = " | " + "TrainNumber".PadRight(15);
        var formattedPointName = " | " + "PointName".PadRight(20);
        var formattedDepartureTime = " | " + "DepartureTime".PadRight(20) + " |";
        var additionalInfo = " 🔼 - Up, 🔽 - Down";

        Speech($"{formattedId}{formattedTrainNumber}{formattedPointName}{formattedDepartureTime}{additionalInfo}");
        if (Cursor.ViewRange.Min != 0)
            Prompt(".........................................................................");
    }

    public static  void PrintData(List<Train> data)
    {
        for (int i = Cursor.ViewRange.Min; i <= Cursor.ViewRange.Max; i++)
        {
            var formattedId = "| " + data[i].Id.ToString().PadRight(5);
            var formattedTrainNumber = " | " + data[i].TrainNumber.PadRight(15);
            var formattedPointName = " | " + data[i].PointName.PadRight(20);
            var formattedDepartureTime = " | " + data[i].DepartureTime.ToString("g",CultureInfo.GetCultureInfo("de-DE")).PadRight(20) + " |";

            if (i == Cursor.Position)
                Step($"{formattedId}{formattedTrainNumber}{formattedPointName}{formattedDepartureTime}");
            else
                Data($"{formattedId}{formattedTrainNumber}{formattedPointName}{formattedDepartureTime}");
        }
    }

    public static  void PrintFooter()
    {
        if (Cursor.ViewRange.Max != Cursor.MaxBorder)
            Prompt(".........................................................................");
        Console.WriteLine();

        Step("For SORT press: 'O'");
        if (Sorter.IsSorting)
        {
            Step("'1' - Id\t '2' - TrainNumber\t '3' - PointName\t '4' - DepartureTime \t 'D' - Descending");
        }
        else
        {
            Step("For DELETE press: 'D'");
        }

        if (!string.IsNullOrEmpty(Sorter.SortingInfo))
        {
            var orderingInfo = Sorter.Descending ? "Descending" : "Ascending";
            Warning(orderingInfo + $" {Sorter.SortingInfo}");
        }

        Step("For FILTER press: 'F'");
        if (Filter.IsFiltering)
        {
            Step("'1' - Id\t '2' - TrainNumber\t '3' - PointName\t '4' - DepartureTime");
        }

        if (Filter.IsNotEmpty())
            Warning($"Active filter {Filter.GetActiveFilters()}");
        
        Step("For CHANGE press: 'C'");
        if (Collector.IsChanging)
        {
            Step("'2' - TrainNumber\t '3' - PointName\t '4' - DepartureTime");
        }
        Step("For ADD press: 'A'");
        Step("For SAVE press: 'S'");
        
        Console.WriteLine();
        Console.WriteLine("Press 'R' to reset");
        Console.WriteLine("Press 'Q' to leave");
    }
}
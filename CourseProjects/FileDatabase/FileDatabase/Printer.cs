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
        var formattedId = "| " + "Ид".PadRight(5);
        var formattedTrainNumber = " | " + "Номер поезда".PadRight(15);
        var formattedPointName = " | " + "Пункт назначения".PadRight(20);
        var formattedDepartureTime = " | " + "Время отправления".PadRight(20) + " |";
        var additionalInfo = " 🔼 - Вверх, 🔽 - Вниз";

        Speech($"{formattedId}{formattedTrainNumber}{formattedPointName}{formattedDepartureTime}{additionalInfo}");
        if (Cursor.ViewRange.Min != 0)
            Prompt(".........................................................................");
    }

    public static void PrintData(List<Train> data)
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

        Step("Для СОРТИРОВКИ нажмите 'O'");
        if (Sorter.IsSorting)
        {
            Step("'1' - Ид\t '2' - Номер поезда\t '3' - Пункт Назначения\t '4' - Время отправления \t 'D' - Направление сортировки");
        }
        else
        {
            Step("Для УДАЛЕНИЯ нажмите 'D'");
        }

        if (!string.IsNullOrEmpty(Sorter.SortingInfo))
        {
            var orderingInfo = Sorter.Descending ? "Убывающая" : "Возрастающая";
            Warning(orderingInfo + $" {Sorter.SortingInfo}");
        }

        Step("Для ПОИСКА нажмите 'F'");
        if (Filter.IsFiltering)
        {
            Step("'1' - Ид\t '2' - Номер поезда\t '3' - Пункт Назначения\t '4' - Время отправления");
        }

        if (Filter.IsNotEmpty())
            Warning($"Активные фильтры: {Filter.GetActiveFilters()}");
        
        Step("Для ИЗМЕНЕНИЯ нажмите 'C'");
        if (Collector.IsChanging)
        {
            Step("'2' - Номер поезда\t '3' - Пункт Назначения\t '4' - Время отправления");
        }
        Step("Для ДОБАВЛЕНИЯ нажмите 'A'");
        Step("Для СОХРАНЕНИЯ нажмите 'S'");
        
        Console.WriteLine();
        Console.WriteLine("Нажмите 'R' для сброса");
        Console.WriteLine("Нажмите 'Q' для выхода");
    }
}
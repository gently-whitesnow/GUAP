using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace FileDatabase;

public class App
{
    private readonly BdContext _bdContext;
    private Func<IEnumerable<Train>, IEnumerable<Train>> filter = list => list;

    private bool IsFiltering;

    private bool IsChanging;

    private List<Train> tempData;

    public App(BdContext bdContext)
    {
        _bdContext = bdContext;
    }


    private bool _running = true;

    public void Run()
    {
        while (_running)
        {
            filter = Collector.GetFilter();
            tempData = _bdContext.Select(filter, Sorter.OrderCondition);
            Cursor.Init(tempData.Count - 1);

            Display();
        }
    }

    private void Display()
    {
        Console.Clear();

        PrintHeader();
        PrintData();
        PrintFooter();

        Listen();
    }

    private void Listen()
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.Q:
            {
                _running = false;
                return;
            }
            case ConsoleKey.S:
            {
                if (Collector.GetApproval($"save"))
                    _bdContext.Save();
                return;
            }
            case ConsoleKey.A:
            {
                IsChanging = false;
                IsFiltering = false;
                Sorter.IsSorting = false;
                Collector.AddTrain(_bdContext);
                return;
            }
            case ConsoleKey.D:
            {
                if (Sorter.IsSorting)
                {
                    Sorter.Descending = !Sorter.Descending;
                    return;
                }
                if(Collector.GetApproval("to delete"))
                    _bdContext.Delete(tempData.ElementAt(Cursor.Position));

                return;
            }
            case ConsoleKey.D1:
            {
                if (Sorter.IsSorting)
                {
                    Sorter.SortBy("Id", train => train.Id);
                }

                if (IsFiltering)
                {
                    Collector.IdFilter = Collector.GetId("filter");
                }

                return;
            }
            case ConsoleKey.D2:
            {
                if (Sorter.IsSorting)
                {
                    Sorter.SortBy("TrainNumber", train => train.TrainNumber);
                }

                if (IsFiltering)
                {
                    Collector.TrainNumberFilter = Collector.GetTrainNumber("filter");
                }
                
                if (IsChanging)
                {
                    var value =  Collector.GetTrainNumber("change");
                    if(string.IsNullOrEmpty(value))
                        return;
                    if (Collector.GetApproval($"to change on {value}"))
                        _bdContext.Update(tempData.ElementAt(Cursor.Position).Id, (train) => train.TrainNumber = value);
                }

                return;
            }
            case ConsoleKey.D3:
            {
                if (Sorter.IsSorting)
                {
                    Sorter.SortBy("PointName", train => train.PointName);
                }

                if (IsFiltering)
                {
                    Collector.PointNameFilter = Collector.GetPointName("filter");
                }
                
                if (IsChanging)
                {
                    var value =  Collector.GetPointName("change");
                    if(string.IsNullOrEmpty(value))
                        return;
                    if (Collector.GetApproval($"change on {value}"))
                        _bdContext.Update(tempData.ElementAt(Cursor.Position).Id, (train) => train.PointName = value);
                }

                return;
            }
            case ConsoleKey.D4:
            {
                if (Sorter.IsSorting)
                {
                    Sorter.SortBy("DepartureTime", train => train.DepartureTime);
                }

                if (IsFiltering)
                {
                    Collector.DepartureTimeFilter = Collector.GetDepartureTime("filter");
                }
                
                if (IsChanging)
                {
                    var value =  Collector.GetDepartureTime("change");
                    if(value == null)
                        return;
                    if (Collector.GetApproval($"change on {value}"))
                        _bdContext.Update(tempData.ElementAt(Cursor.Position).Id, (train) => train.DepartureTime = value.Value);
                }

                return;
            }
            case ConsoleKey.R:
            {
                filter = list => list;
                Sorter.Reset();
                IsFiltering = false;
                IsChanging = false;
                Collector.Clear();
                return;
            }
            case ConsoleKey.O:
            {
                Sorter.IsSorting = !Sorter.IsSorting;
                IsFiltering = false;
                IsChanging = false;
                break;
            }
            case ConsoleKey.F:
            {
                IsFiltering = !IsFiltering;
                IsChanging = false;
                Sorter.IsSorting = false;
                break;
            }
            case ConsoleKey.C:
            {
                IsChanging = !IsChanging;
                Sorter.IsSorting = false;
                IsFiltering = false;
                break;
            }
            case ConsoleKey.UpArrow:
            {
                Cursor.Up();
                break;
            }
            case ConsoleKey.DownArrow:
            {
                Cursor.Down();
                break;
            }
            default:
            {
                Printer.Error("Неизвестная команда!");
                Thread.Sleep(1000);
                break;
            }
        }

        Display();
    }

    private void PrintHeader()
    {
        var formattedId = "| " + "Id".PadRight(5);
        var formattedTrainNumber = " | " + "TrainNumber".PadRight(15);
        var formattedPointName = " | " + "PointName".PadRight(20);
        var formattedDepartureTime = " | " + "DepartureTime".PadRight(20) + " |";
        var additionalInfo = " 🔼 - Up, 🔽 - Down";

        Printer.Speech(
            $"{formattedId}{formattedTrainNumber}{formattedPointName}{formattedDepartureTime}{additionalInfo}");
        if (Cursor.ViewRange.Min != 0)
            Printer.Prompt(".........................................................................");
    }

    private void PrintData()
    {
        for (int i = Cursor.ViewRange.Min; i <= Cursor.ViewRange.Max; i++)
        {
            var formattedId = "| " + tempData[i].Id.ToString().PadRight(5);
            var formattedTrainNumber = " | " + tempData[i].TrainNumber.PadRight(15);
            var formattedPointName = " | " + tempData[i].PointName.PadRight(20);
            var formattedDepartureTime = " | " + tempData[i].DepartureTime.ToString("g",CultureInfo.GetCultureInfo("de-DE")).PadRight(20) + " |";

            if (i == Cursor.Position)
                Printer.Step($"{formattedId}{formattedTrainNumber}{formattedPointName}{formattedDepartureTime}");
            else
                Printer.Data($"{formattedId}{formattedTrainNumber}{formattedPointName}{formattedDepartureTime}");
        }
    }

    private void PrintFooter()
    {
        if (Cursor.ViewRange.Max != Cursor.MaxBorder)
            Printer.Prompt(".........................................................................");
        Console.WriteLine();

        Printer.Step("For SORT press: 'O'");
        if (Sorter.IsSorting)
        {
            Printer.Step("'1' - Id\t '2' - TrainNumber\t '3' - PointName\t '4' - DepartureTime \t 'D' - Descending");
        }
        else
        {
            Printer.Step("For DELETE press: 'D'");
        }

        if (!string.IsNullOrEmpty(Sorter.SortingInfo))
        {
            var orderingInfo = Sorter.Descending ? "Descending" : "Ascending";
            Printer.Warning(orderingInfo + $" {Sorter.SortingInfo}");
        }

        Printer.Step("For FILTER press: 'F'");
        if (IsFiltering)
        {
            Printer.Step("'1' - Id\t '2' - TrainNumber\t '3' - PointName\t '4' - DepartureTime");
        }

        if (Collector.NotEmpty())
            Printer.Warning($"Active filter {Collector.ActiveFilters()}");
        
        Printer.Step("For CHANGE press: 'C'");
        if (IsChanging)
        {
            Printer.Step("'2' - TrainNumber\t '3' - PointName\t '4' - DepartureTime");
        }
        Printer.Step("For ADD press: 'A'");
        Printer.Step("For SAVE press: 'S'");
        
        Console.WriteLine();
        Console.WriteLine("Press 'R' to reset");
        Console.WriteLine("Press 'Q' to leave");
    }
}
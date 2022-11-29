using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FileDatabase;

public class App
{
    private readonly BdContext _bdContext;
    private Func<IEnumerable<Train>, IEnumerable<Train>> filter = list => list;
    Func<IEnumerable<Train>, IEnumerable<Train>> order = list => list;
    
    private bool Descending;
    private string OrderingInfo;
    private bool IsOrdering;
    
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
            tempData = _bdContext.Select(filter, order);

            Cursor.Init(tempData.Count - 1);
            filter = Collector.GetFilter();

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
            // Actions
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
                IsOrdering = false;
                var valueTrainNumber =  Collector.GetTrainNumber("value");
                if(string.IsNullOrEmpty(valueTrainNumber))
                    return;
                var valuePointName =  Collector.GetPointName("value");
                if(string.IsNullOrEmpty(valuePointName))
                    return;
                var valueDepartureTime =  Collector.GetDepartureTime("value");
                if(valueDepartureTime ==null)
                    return;
                if (Collector.GetApproval($"add {valueTrainNumber} {valuePointName} {valueDepartureTime}"))
                    _bdContext.Insert(new Train(valueTrainNumber,valuePointName,valueDepartureTime.Value));
                return;
            }
            // Ordering
            case ConsoleKey.D:
            {
                if (IsOrdering)
                {
                    Descending = !Descending;
                    return;
                }
                if(Collector.GetApproval("to delete"))
                    _bdContext.Delete(tempData.ElementAt(Cursor.Position));

                return;
            }
            case ConsoleKey.T:
            {
                if (IsOrdering)
                {
                    if (OrderingInfo == "ordering by Id")
                    {
                        order = list => list;
                        OrderingInfo = "";
                        return;
                    }
                    order = list => Descending ? list.OrderByDescending(d => d.Id) : list.OrderBy(a => a.Id);
                    OrderingInfo = "ordering by Id";
                }

                if (IsFiltering)
                {
                    Collector.IdFilter = Collector.GetId("filter");
                }

                return;
            }
            case ConsoleKey.Y:
            {
                if (IsOrdering)
                {
                    if (OrderingInfo == "ordering by TrainNumber")
                    {
                        order = list => list;
                        OrderingInfo = "";
                        return;
                    }
                    order = list =>
                        Descending ? list.OrderByDescending(d => d.TrainNumber) : list.OrderBy(a => a.TrainNumber);
                    OrderingInfo = "ordering by TrainNumber";
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
            case ConsoleKey.U:
            {
                if (IsOrdering)
                {
                    if (OrderingInfo == "ordering by PointName")
                    {
                        order = list => list;
                        OrderingInfo = "";
                        return;
                    }
                    order = list =>
                        Descending ? list.OrderByDescending(d => d.PointName) : list.OrderBy(a => a.PointName);
                    OrderingInfo = "ordering by PointName";
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
            case ConsoleKey.I:
            {
                if (IsOrdering)
                {
                    if (OrderingInfo == "ordering by DepartureTime")
                    {
                        order = list => list;
                        OrderingInfo = "";
                        return;
                    }

                    order = list =>
                        Descending ? list.OrderByDescending(d => d.DepartureTime) : list.OrderBy(a => a.DepartureTime);
                    OrderingInfo = "ordering by DepartureTime";
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
            // Filtering
            case ConsoleKey.R:
            {
                order = list => list;
                filter = list => list;
                Descending = false;
                OrderingInfo = "";
                IsOrdering = false;
                IsFiltering = false;
                IsChanging = false;
                Collector.Clear();
                return;
            }
            // Static
            case ConsoleKey.O:
            {
                IsOrdering = !IsOrdering;
                IsFiltering = false;
                IsChanging = false;
                break;
            }
            case ConsoleKey.F:
            {
                IsFiltering = !IsFiltering;
                IsChanging = false;
                IsOrdering = false;
                break;
            }
            case ConsoleKey.C:
            {
                IsChanging = !IsChanging;
                IsOrdering = false;
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
            var formattedDepartureTime = " | " + tempData[i].DepartureTime.ToString().PadRight(20) + " |";

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

        Printer.Step("For sorting press: 'O'");
        if (IsOrdering)
        {
            Printer.Step("'T' - Id\t 'Y' - TrainNumber\t 'U' - PointName\t 'I' - DepartureTime \t 'D' - Descending");
        }
        else
        {
            Printer.Step("For delete press: 'D'");
        }

        if (!string.IsNullOrEmpty(OrderingInfo))
        {
            var orderingInfo = Descending ? "Descending" : "Ascending";
            Printer.Warning(orderingInfo + $" {OrderingInfo}");
        }

        Printer.Step("For filtering press: 'F'");
        if (IsFiltering)
        {
            Printer.Step("'T' - Id\t 'Y' - TrainNumber\t 'U' - PointName\t 'I' - DepartureTime");
        }
        

        if (Collector.NotEmpty())
            Printer.Warning($"Active filter {Collector.ActiveFilters()}");
        
        Printer.Step("For changing press: 'C'");
        if (IsChanging)
        {
            Printer.Step("'Y' - TrainNumber\t 'U' - PointName\t 'I' - DepartureTime");
        }
        Printer.Step("For add press: 'A'");
        Printer.Step("For save press: 'S'");
        


        Console.WriteLine();
        Console.WriteLine("Press 'R' to reset");
        Console.WriteLine("Press 'Q' to leave");
    }
}
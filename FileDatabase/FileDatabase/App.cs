using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FileDatabase;

public class App
{
    
    private readonly BdContext _bdContext;
    private Func<IEnumerable<Train>,IEnumerable<Train>> filter  = list => list;
    Func<IEnumerable<Train>,IEnumerable<Train>> order = list => list;
    private bool Descending;
    private string OrderingInfo;

    private bool IsOrdering;
    private bool IsFiltering;
    
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

            Cursor.Init(tempData.Count-1);
    
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
            // Ordering
            case ConsoleKey.D:
            {
                if (IsOrdering)
                {
                    Descending = !Descending;
                }

                return;
            }
            case ConsoleKey.T:
            {
                if (IsOrdering)
                {
                    order = list => Descending?list.OrderByDescending(d=>d.Id):list.OrderBy(a=>a.Id);
                    OrderingInfo = "ordering by Id";
                }
                return;
            }
            case ConsoleKey.Y:
            {
                if (IsOrdering)
                {
                    order = list =>
                        Descending ? list.OrderByDescending(d => d.TrainNumber) : list.OrderBy(a => a.TrainNumber);
                    OrderingInfo = "ordering by TrainNumber";
                }

                return;
                
            }
            case ConsoleKey.U:
            {
                if (IsOrdering)
                {
                    order = list =>
                        Descending ? list.OrderByDescending(d => d.PointName) : list.OrderBy(a => a.PointName);
                    OrderingInfo = "ordering by PointName";
                }

                return;
            }
            case ConsoleKey.I:
            {
                if (IsOrdering)
                {
                    order = list =>
                        Descending ? list.OrderByDescending(d => d.DepartureTime) : list.OrderBy(a => a.DepartureTime);
                    OrderingInfo = "ordering by DepartureTime";
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
                return;
            }
            // Static
            case ConsoleKey.O:
            {
                IsOrdering = !IsOrdering;
                break;
            }
            case ConsoleKey.F:
            {
                IsFiltering = !IsFiltering;
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
        var formattedId = "| "+"Id".PadRight(5);
        var formattedTrainNumber = " | "+"TrainNumber".PadRight(15);
        var formattedPointName = " | "+"PointName".PadRight(20);
        var formattedDepartureTime = " | "+"DepartureTime".PadRight(20)+" |";
        var additionalInfo = " 🔼 - Up, 🔽 - Down";

        Printer.Speech($"{formattedId}{formattedTrainNumber}{formattedPointName}{formattedDepartureTime}{additionalInfo}");
        if(Cursor.ViewRange.Min!=0)
            Printer.Prompt(".........................................................................");
    }
    private void PrintData()
    {
        for (int i = Cursor.ViewRange.Min; i <= Cursor.ViewRange.Max; i++)
        {
            var formattedId = "| "+tempData[i].Id.ToString().PadRight(5);
            var formattedTrainNumber = " | "+tempData[i].TrainNumber.PadRight(15);
            var formattedPointName = " | "+tempData[i].PointName.PadRight(20);
            var formattedDepartureTime = " | "+tempData[i].DepartureTime.ToString().PadRight(20)+" |";

            if(i == Cursor.Position)
                Printer.Step($"{formattedId}{formattedTrainNumber}{formattedPointName}{formattedDepartureTime}");
            else
                Printer.Data($"{formattedId}{formattedTrainNumber}{formattedPointName}{formattedDepartureTime}");
        }
    }
    
    private void PrintFooter()
    {
        if(Cursor.ViewRange.Max!=Cursor.MaxBorder)
            Printer.Prompt(".........................................................................");
        Console.WriteLine();
        
        Printer.Step("For ordering press: 'O'");
        if (IsOrdering || string.IsNullOrEmpty(OrderingInfo))
        {
            Printer.Step("'T' - Id\t 'Y' - TrainNumber\t 'U' - PointName\t 'I' - DepartureTime \t 'D' - Descending");
            var orderingInfo = Descending ? "Descending" : "Ascending";
            Printer.Warning(orderingInfo + $" {OrderingInfo}");
        }
        Console.WriteLine();
        Console.WriteLine("Press 'R' to reset");
        Console.WriteLine("Press 'Q' to leave");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FileDatabase;

public class App
{
    // контекст для работы с базой данных 
    private readonly BdContext _bdContext;

    private List<Train> _tempData;

    public App(BdContext bdContext)
    {
        _bdContext = bdContext;
    }


    private bool _running = true;

    public void Run()
    {
        while (_running)
        {
            _tempData = _bdContext.Select(Filter.FilterCondition, Sorter.OrderCondition);
            Cursor.Init(_tempData.Count - 1);

            Display();
        }
    }

    private void Display()
    {
        Console.Clear();

        Printer.PrintHeader();
        Printer.PrintData(_tempData);
        Printer.PrintFooter();

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
                Collector.IsChanging = false;
                Filter.IsFiltering = false;
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
                    _bdContext.Delete(_tempData.ElementAt(Cursor.Position));

                return;
            }
            case ConsoleKey.D1:
            {
                if (Sorter.IsSorting)
                {
                    Sorter.SortBy("Id", train => train.Id);
                }

                if (Filter.IsFiltering)
                {
                    Filter.IdFilter = Collector.GetId("filter");
                }

                return;
            }
            case ConsoleKey.D2:
            {
                if (Sorter.IsSorting)
                {
                    Sorter.SortBy("TrainNumber", train => train.TrainNumber);
                }

                if (Filter.IsFiltering)
                {
                    Filter.TrainNumberFilter = Collector.GetTrainNumber("filter");
                }
                
                if (Collector.IsChanging)
                {
                    var value =  Collector.GetTrainNumber("change");
                    if(string.IsNullOrEmpty(value))
                        return;
                    if (Collector.GetApproval($"to change on {value}"))
                        _bdContext.Update(_tempData.ElementAt(Cursor.Position).Id, (train) => train.TrainNumber = value);
                }

                return;
            }
            case ConsoleKey.D3:
            {
                if (Sorter.IsSorting)
                {
                    Sorter.SortBy("PointName", train => train.PointName);
                }

                if (Filter.IsFiltering)
                {
                    Filter.PointNameFilter = Collector.GetPointName("filter");
                }
                
                if (Collector.IsChanging)
                {
                    var value =  Collector.GetPointName("change");
                    if(string.IsNullOrEmpty(value))
                        return;
                    if (Collector.GetApproval($"change on {value}"))
                        _bdContext.Update(_tempData.ElementAt(Cursor.Position).Id, (train) => train.PointName = value);
                }

                return;
            }
            case ConsoleKey.D4:
            {
                if (Sorter.IsSorting)
                {
                    Sorter.SortBy("DepartureTime", train => train.DepartureTime);
                }

                if (Filter.IsFiltering)
                {
                    Filter.DepartureTimeFilter = Collector.GetDepartureTime("filter");
                }
                
                if (Collector.IsChanging)
                {
                    var value =  Collector.GetDepartureTime("change");
                    if(value == null)
                        return;
                    if (Collector.GetApproval($"change on {value}"))
                        _bdContext.Update(_tempData.ElementAt(Cursor.Position).Id, (train) => train.DepartureTime = value.Value);
                }

                return;
            }
            case ConsoleKey.R:
            {
                Sorter.Reset();
                Filter.IsFiltering = false;
                Collector.IsChanging = false;
                Filter.Clear();
                return;
            }
            case ConsoleKey.O:
            {
                Sorter.IsSorting = !Sorter.IsSorting;
                Filter.IsFiltering = false;
                Collector.IsChanging = false;
                break;
            }
            case ConsoleKey.F:
            {
                Filter.IsFiltering = !Filter.IsFiltering;
                Collector.IsChanging = false;
                Sorter.IsSorting = false;
                break;
            }
            case ConsoleKey.C:
            {
                Collector.IsChanging = !Collector.IsChanging;
                Sorter.IsSorting = false;
                Filter.IsFiltering = false;
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
}
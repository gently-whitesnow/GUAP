using System;

namespace FileDatabase;

public class Train
{ 
    public Train(string trainNumber, string pointName, DateTime departureTime)
    {
        TrainNumber = trainNumber;
        PointName = pointName;
        DepartureTime = departureTime;
    }

    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер поезда
    /// </summary>
    public string TrainNumber { get; set; }

    /// <summary>
    /// Пункт назначения
    /// </summary>
    public string PointName { get; set; }

    /// <summary>
    /// Время отправления
    /// </summary>
    public DateTime DepartureTime { get; set; }
}
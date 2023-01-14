using System;

namespace FileDatabase;

public struct Train
{
    public Train(string trainNumber, string pointName, DateTime departureTime)
    {
        TrainNumber = trainNumber;
        PointName = pointName;
        DepartureTime = departureTime;
        Id = 0;
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
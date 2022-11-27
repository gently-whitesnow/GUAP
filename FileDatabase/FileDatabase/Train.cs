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

    public int Id { get; set; }
    public string TrainNumber { get; set; }
    public string PointName { get; set; }
    public DateTime DepartureTime { get; set; }
}
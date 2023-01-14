using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace FileDatabase;

public class BdContext
{
    string path = "trains.json";

    private int _id = 1;
    private List<Train> staticData = new();

    public BdContext()
    {
        var loadedData = Load();
        if (loadedData == null || loadedData.Count == 0)
            FillMockValues();
        else
        {
            staticData = loadedData;
            _id = staticData.Count;
        }
    }

    public List<Train> Select(Func<IEnumerable<Train>, IEnumerable<Train>> filter,
        Func<IEnumerable<Train>, IEnumerable<Train>> order)
    {
        return order(filter(staticData)).ToList();
    }

    public void Insert(Train train)
    {
        train.Id = _id++;
        staticData.Add(train);
    }

    public void Update(int id, Action<Train> updateFunc)
    {
        updateFunc(staticData.First(d => d.Id == id));
    }

    public void Delete(Train train)
    {
        staticData.Remove(train);
    }

    public void Save()
    {
        File.WriteAllText(path, string.Empty);
        var bytes = JsonSerializer.SerializeToUtf8Bytes(staticData);
        using var writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
        writer.Write(bytes);
        Printer.Success("Success");
        Thread.Sleep(1000);
    }


    public List<Train>? Load()
    {
        try
        {
            if (!File.Exists(path))
                return null;
            using BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));
            const int bufferSize = 4096;
            using var ms = new MemoryStream();
            var buffer = new byte[bufferSize];
            int count;
            while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                ms.Write(buffer, 0, count);
            return JsonSerializer.Deserialize<List<Train>>(ms.ToArray());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public void FillMockValues()
    {
        Insert(new Train("N322", "Moscow", DateTime.ParseExact("14.12.1999", "dd.MM.yyyy", null)));
        Insert(new Train("N321", "Peter", DateTime.ParseExact("14.12.2000", "dd.MM.yyyy", null)));
        Insert(new Train("N323", "Kaluga", DateTime.ParseExact("15.12.1999", "dd.MM.yyyy", null)));
        Insert(new Train("N324", "Tula", DateTime.ParseExact("14.11.1999", "dd.MM.yyyy", null)));
        Insert(new Train("N325", "Koselsk", DateTime.ParseExact("14.12.1998", "dd.MM.yyyy", null)));
        Insert(new Train("N326", "Krim", DateTime.ParseExact("21.12.2000", "dd.MM.yyyy", null)));
        Insert(new Train("N322", "Moscow", DateTime.ParseExact("14.12.1999", "dd.MM.yyyy", null)));
        Insert(new Train("N321", "Peter", DateTime.ParseExact("14.12.2000", "dd.MM.yyyy", null)));
        Insert(new Train("N323", "Kaluga", DateTime.ParseExact("15.12.1999", "dd.MM.yyyy", null)));
        Insert(new Train("N324", "Tula", DateTime.ParseExact("14.11.1999", "dd.MM.yyyy", null)));
        Insert(new Train("N325", "Koselsk", DateTime.ParseExact("14.12.1998", "dd.MM.yyyy", null)));
        Insert(new Train("N326", "Krim", DateTime.ParseExact("21.12.2000", "dd.MM.yyyy", null)));
        Insert(new Train("N322", "Moscow", DateTime.ParseExact("14.12.1999", "dd.MM.yyyy", null)));
        Insert(new Train("N321", "Peter", DateTime.ParseExact("14.12.2000", "dd.MM.yyyy", null)));
        Insert(new Train("N323", "Kaluga", DateTime.ParseExact("15.12.1999", "dd.MM.yyyy", null)));
        Insert(new Train("N324", "Tula", DateTime.ParseExact("14.11.1999", "dd.MM.yyyy", null)));
        Insert(new Train("N325", "Koselsk", DateTime.ParseExact("14.12.1998", "dd.MM.yyyy", null)));
        Insert(new Train("N326", "Krim", DateTime.ParseExact("21.12.2000", "dd.MM.yyyy", null)));
    }
}
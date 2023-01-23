using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace FileDatabase;

public class BdContext
{
    private const string Path = "trains.json";

    private int _id = 0;
    private readonly List<Train> _staticData = new();

    public BdContext()
    {
        var loadedData = Load();
        if (loadedData == null || loadedData.Count == 0)
            throw new Exception("Не удалось считать файл");
        else
        {
            _staticData = loadedData;
            _id = _staticData.Max(d => d.Id);
        }
    }

    /// <summary>
    /// Получение данных с применением фильтра и сортировки
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    public List<Train> Select(Func<IEnumerable<Train>, IEnumerable<Train>> filter,
        Func<IEnumerable<Train>, IEnumerable<Train>> order)
    {
        return order(filter(_staticData)).ToList();
    }

    /// <summary>
    /// Добавить новый объект
    /// </summary>
    /// <param name="train"></param>
    public void Insert(Train train)
    {
        train.Id = ++_id;
        _staticData.Add(train);
    }

    /// <summary>
    /// Изменить объект
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateFunc"></param>
    public void Update(int id, Action<Train> updateFunc)
    {
        updateFunc(_staticData.First(d => d.Id == id));
    }

    /// <summary>
    /// Удалить объект
    /// </summary>
    /// <param name="train"></param>
    public void Delete(Train train)
    {
        _staticData.Remove(train);
    }

    /// <summary>
    /// Сохранить данные в файл
    /// </summary>
    public void Save()
    {
        File.WriteAllText(Path, string.Empty);
        var bytes = JsonSerializer.SerializeToUtf8Bytes(_staticData);
        using var writer = new BinaryWriter(File.Open(Path, FileMode.OpenOrCreate));
        writer.Write(bytes);
        Printer.Success("Success");
        Thread.Sleep(1000);
    }

    /// <summary>
    /// Загрузить данные из файла
    /// </summary>
    /// <returns></returns>
    public List<Train>? Load()
    {
        try
        {
            if (!File.Exists(Path))
                return null;
            using BinaryReader reader = new BinaryReader(File.Open(Path, FileMode.Open));
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

    /// <summary>
    /// В случае отсутствия файла, загрузить тестовые данные
    /// </summary>
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
using System;
using System.Globalization;

namespace FileDatabase;

public static class Collector
{
    /// <summary>
    /// Флаг ожидания выбоора значения, которое мы хотим изменить
    /// </summary>
    public static bool IsChanging;
    
    /// <summary>
    /// Последовательно запрашивает данные по модели и добавляет их в базу данных
    /// </summary>
    /// <param name="_bdContext"></param>
    public static void AddTrain(BdContext _bdContext)
    {
        var valueTrainNumber =  GetTrainNumber("значение");
        if(string.IsNullOrEmpty(valueTrainNumber))
            return;
        var valuePointName =  GetPointName("значение");
        if(string.IsNullOrEmpty(valuePointName))
            return;
        var valueDepartureTime =  GetDepartureTime("значение");
        if(valueDepartureTime ==null)
            return;
        if (GetApproval($"добавления {valueTrainNumber} {valuePointName} {valueDepartureTime.Value.ToString("g",CultureInfo.GetCultureInfo("de-DE"))}"))
            _bdContext.Insert(new Train(valueTrainNumber,valuePointName,valueDepartureTime.Value));
    }

    /// <summary>
    /// Запросить валдиный id записи
    /// </summary>
    /// <param name="whatDo"></param>
    /// <returns></returns>
    public static int? GetId(string whatDo)
    {
        return Cage<int?>($"Введите {whatDo} для ид", "Длина данных > 0 и < 100000", (s) =>
        {
            if (int.TryParse(s, out var value) && value < 100_000)
                return value;
            return null;
        });
    }

    /// <summary>
    /// Запросить валидный номер поезда
    /// </summary>
    /// <param name="whatDo"></param>
    /// <returns></returns>
    public static string? GetTrainNumber(string whatDo)  
    {
        return Cage<string?>($"Введите {whatDo} для номера поезда", "Длина данных < 16 и > 3", (s) =>
        {
            if (s.Length <= 15 && s.Length > 3)
                return s;
            return null;
        });
    }

    /// <summary>
    /// Запросить валидное название станции
    /// </summary>
    /// <param name="whatDo"></param>
    /// <returns></returns>
    public static string? GetPointName(string whatDo)
    {
        return Cage<string?>($"Введите {whatDo} для пункта назначения", "Длина данных < 21", (s) =>
        {
            if (s.Length <= 20)
                return s;
            return null;
        });
    }

    /// <summary>
    /// Запросить валидное время отправления
    /// </summary>
    /// <param name="whatDo"></param>
    /// <returns></returns>
    public static DateTime? GetDepartureTime(string whatDo)
    {
        return Cage<DateTime?>($"Введите {whatDo} для времени отправления",
            "Длина данных < 20 и формат: dd.MM.yyyy hh:mm", (s) =>
            {
                if (s.Length <= 19 && DateTime.TryParseExact(s, "g", CultureInfo.GetCultureInfo("de-DE"), DateTimeStyles.None, out var value))
                    return value;
                return null;
            });
    }
    
    /// <summary>
    /// Заправшивает у пользователя подтверждение
    /// </summary>
    /// <param name="whatDo"></param>
    /// <returns></returns>
    public static bool GetApproval(string whatDo)
    {
        return Cage<bool?>($"Введите 'Y' для {whatDo}", "", (s) =>
        {
            if (s.Trim().ToLower() is "y")
                return true;
            return false;
        })??false;
    }

    /// <summary>
    /// Запрашивает у пользователя данные и проверяет их на валидность
    /// </summary>
    /// <param name="requestMessage">название запрашиваемого параметра</param>
    /// <param name="humanReadableCondition">условие </param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static T? Cage<T>(string requestMessage, string humanReadableCondition, Func<string, T?> condition)
    {
        while (true)
        {
            Console.WriteLine();
            Printer.Step($"{requestMessage}, enter для пропуска :");
            var val = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(val))
                return default;
            var res = condition(val);
            if (res != null)
                return res;
            Printer.Error($"Некоректное значение, требуется {humanReadableCondition}");
        }
    }
}
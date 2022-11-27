using System;

namespace FileDatabase;

public static class Printer
{
    
    public static void Error(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
        
    public static void Warning(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
        
    public static void Success(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
        
    public static void Prompt(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
        
    public static void Speech(string humanReadableString)
    {
        Console.ResetColor();
        Console.WriteLine(humanReadableString);
    }
        
    public static void Step(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
    
    public static void Data(string humanReadableString)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(humanReadableString);
        Console.ResetColor();
    }
}
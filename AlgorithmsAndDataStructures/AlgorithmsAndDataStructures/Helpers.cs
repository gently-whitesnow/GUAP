using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmsAndDataStructures;

public static class Helpers
{
    public static string GetAsciiString(long counter)
    {
        var str = "";
        while (counter > 0)
        {
            if (counter > 127)
            {
                str = (char) 127 + str;
            }
            else
            {
                str = (char) counter + str;
                break;
            }

            counter -= 127;
        }

        return str;
    }


    public static void SaveToCsv(List<long> result, string path)
    {
        var str = "";
        var dict = result.GroupBy(d => d)
            .ToDictionary(v=>v.Key,l=>l.Count()).OrderBy(d=>d.Key).ToArray();
        foreach (var (key,value) in dict)
        {
            str += $"{key};{value}\n";
        }
        File.WriteAllText(path, string.Empty);
        File.WriteAllText(path, str);
        
    }
}
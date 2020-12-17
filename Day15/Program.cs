using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

var input = File.ReadAllText("input.txt")
    .Trim()
    .Split(",")
    .Select(int.Parse)
    .ToList();

WriteLine(GetLastNumber(2020));
WriteLine(GetLastNumber(30000000));

int GetLastNumber(int n)
{
    var last = input.Last();
    var dictionary = new Dictionary<int, int>();
    for (var k = 0; k < input.Count - 1; k++)
        dictionary.Add(input[k], k + 1);

    for (var k = input.Count; k < n; k++)
    {
        var value = 0;
        if (dictionary.ContainsKey(last))
        {
            value = k - dictionary[last];
            dictionary[last] = k;
        }
        else
            dictionary.Add(last, k);

        last = value;
    }

    return last;
}
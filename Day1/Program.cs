using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using static System.Console;

var stopwatch = Stopwatch.StartNew();
var input = ReadInput();
PartOne();
PartTwo();
WriteLine(stopwatch.ElapsedMilliseconds);

List<int> ReadInput()
{
    var list = new List<int>();
    var s = ReadLine();
    while (!string.IsNullOrEmpty(s))
    {
        list.Add(int.Parse(s));
        s = ReadLine();
    }

    return list;
}

void PartOne()
{   
    var answer = input.Where(i => input.Any(x => x == 2020 - i))
                .Select(i => i * (2020 - i))
                .First();
    WriteLine(answer);
}

void PartTwo()
{
    for (var k = 0; k < input.Count; k++)
    {
        for (var j = k + 1; j < input.Count; j++)
        {
            if (input[k] + input[j] >= 2020)
                continue;

            if (input.Any(i => i == 2020 - input[k] - input[j]))
            {
                 WriteLine((2020 - input[k] - input[j]) * input[k] * input[j]);
                 return;
            }
        }
    }
}
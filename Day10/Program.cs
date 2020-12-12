using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static System.Console;

var input = ReadInput();

var stopwatch = Stopwatch.StartNew();
PartOne(input);
PartTwo(input);
WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

List<long> ReadInput()
{
    List<long> input = new();
    
    var s = ReadLine();
    while (!string.IsNullOrWhiteSpace(s))
    {
        input.Add(long.Parse(s));
        s = ReadLine();
    }

    return input;
}

void PartOne(List<long> input)
{
    input.Sort();
    input.Insert(0, 0);
    input.Add(input.Last() + 3);

    var ones = 0;
    var threes = 0;
    for (var k = 1; k < input.Count; k++)
    {
        if (input[k] - input[k-1] == 1)
            ones++;
        else
            threes++;
    }

    WriteLine(ones * threes);
}

void PartTwo(List<long> input)
{
    input = input.OrderBy(x => x).ToList();

    var str = new StringBuilder();
    for (int k = 1; k < input.Count; k++)
    {
        var val = input[k] - input[k - 1];
        if (str.Length == 0 || !(str[str.Length - 1] == '3' && val == 3))
            str.Append(val);
    }

    var j = str.ToString()
        .Split("3")
        .Select(x => x.Length - 1)
        .Select(x => Math.Pow(2, x) - (x >= 3 ? 1 : 0))
        .Where(x => x > 1)
        .Aggregate(1d, (acc, i) => acc * i);

    WriteLine(j);
}
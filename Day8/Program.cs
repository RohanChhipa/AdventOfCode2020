using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;

var input = ReadInput();

var stopwatch = Stopwatch.StartNew();
PartOne(input);
PartTwo(input);
WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

List<(string, int)> ReadInput()
{
    List<(string, int)> input = new();
    
    var s = ReadLine();
    while (!string.IsNullOrWhiteSpace(s))
    {
        var tmp = s.Split(" ");
        input.Add((tmp[0], int.Parse(tmp[1])));
        s = ReadLine();
    }

    return input;
}

void PartOne(List<(string, int)> lines)
{
    var acc = GetRoute(lines)
        .Select(x => lines[x])
        .Where(x => x.Item1 == "acc")
        .Sum(x => x.Item2);

    WriteLine(acc);
}

void PartTwo(List<(string, int)> lines)
{
    var route = GetRoute(lines)
        .OrderByDescending(x => x)
        .Where(x => lines[x].Item1 != "acc");

    foreach (var i in route)
    {
        var originalOperation = lines[i].Item1;
        lines[i] = (lines[i].Item1 == "jmp" ? "nop" : "jmp", lines[i].Item2);

        var newRoute = GetRoute(lines);
        if (newRoute.Contains(lines.Count))
        {
            WriteLine(newRoute
                .SkipLast(1)
                .Where(x => lines[x].Item1 == "acc")
                .Sum(x => lines[x].Item2)
            );
            return;
        }

        lines[i] = (originalOperation, lines[i].Item2);
    }
}

List<int> GetRoute(List<(string, int)> input)
{
    var i = 0;
    var route = new List<int>();
    while (!route.Contains(i) && i < input.Count)
    {
        route.Add(i);
        var line = input[i];
        if (line.Item1 == "jmp")
            i += line.Item2 - 1;

        i++;
    }

    if (i == input.Count)
        route.Add(i);

    return route;
}
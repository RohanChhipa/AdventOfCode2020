using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;

var input = ReadInput();

var stopwatch = Stopwatch.StartNew();
PartOne();
PartTwo();
WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

List<string> ReadInput()
{
    List<string> input = new();

    var s = ReadLine();
    while (!string.IsNullOrWhiteSpace(s))
    {
        input.Add(s);
        s = ReadLine();
    }

    return input;
}

void PartOne()
{
    WriteLine(CountTrees(3, 1));
}

void PartTwo()
{
    var answer = new List<(int, int)> {
        (1, 1),
        (3, 1),
        (5, 1),
        (7, 1),
        (1, 2),
    }
    .Select(tuple => CountTrees(tuple.Item1, tuple.Item2))
    .Aggregate(1L, (total, next) => total * next);

    WriteLine(answer);
}

long CountTrees(int x, int y)
{
    var rowLength = input.First().Length;
    return Enumerable.Range(1, (input.Count - 1) / y)
        .Select(i => input[i * y][(i * x) % rowLength])
        .Count(c => c == '#');
}
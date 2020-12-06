using System.Diagnostics;
using System.IO;
using System.Linq;
using static System.Console;

var input = ReadInput();

var stopwatch = Stopwatch.StartNew();
PartOne(input);
PartTwo(input);
WriteLine($"Time: {stopwatch.ElapsedMilliseconds}ms");

string ReadInput()
{
    string[] lines = File.ReadAllLines("./input.txt");
    return string.Join("\n", lines);
}

void PartOne(string input)
{
    var count = input.Split("\n\n")
        .SelectMany(s => s.Replace("\n", "")
            .ToCharArray()
            .Distinct())
        .Count();

    WriteLine(count);
}

void PartTwo(string input)
{
    var groups = input.Split("\n\n");
    var groupSize = groups
        .Select(s => s.Count(x => x == '\n') + 1)
        .ToArray();

    var count = groups
        .SelectMany((s, i) => s.Replace("\n", "")
            .ToCharArray()
            .GroupBy(c => c)
            .Where(g => g.Count() == groupSize[i]))
        .Count();

    WriteLine(count);
}
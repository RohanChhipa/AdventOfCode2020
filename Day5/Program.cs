using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;

var input = ReadInput();

var stopwatch = Stopwatch.StartNew();
PartOne(input);
PartTwo(input);
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

void PartOne(List<string> input)
{
    WriteLine(GetSeatIds(input).Max());
}

void PartTwo(List<string> input)
{
    var seatIds = GetSeatIds(input);
    seatIds.Sort();
    var seatMap = seatIds.ToHashSet();

    var seat = seatIds.First(i => !seatMap.Contains(i + 1));
    WriteLine(seat + 1);
}

List<int> GetSeatIds(List<string> input)
{
     return input
        .Select(s => (s.Substring(0, 7), s.Substring(7)))
        .Select(tuple => (CalculatePosition(tuple.Item1, 'B', 'F', 127), CalculatePosition(tuple.Item2, 'R', 'L', 7)))
        .Select(tuple => tuple.Item1 * 8 + tuple.Item2)
        .ToList();
}

int CalculatePosition(string input, char upperSymbol, char lowerSymbol, int upperBound)
{
    var lower = 0;
    var upper = upperBound;
    foreach (char c in input)
    {
        var step = (int) Math.Ceiling((upper - lower) / 2.0);
        if (c == lowerSymbol)
            upper -= step;

        if (c == upperSymbol)
            lower += step;
    }

    return lower;
}
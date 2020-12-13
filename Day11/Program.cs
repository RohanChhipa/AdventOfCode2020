using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;

var stopwatch = Stopwatch.StartNew();
var input = ReadInput();

// WriteLine(string.Join(", ", input[(1, 9)].distant));

PartOne(input);
PartTwo(input);
WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

Dictionary<(int, int), Seat> ReadInput()
{
    Write("Parsing input...");
    List<string> input = new();

    var s = ReadLine();
    while (!string.IsNullOrWhiteSpace(s))
    {
        input.Add(s);
        s = ReadLine();
    }

    var dictionary = new Dictionary<(int, int), Seat>();
    for (var k = 0; k < input.Count; k++)
    {
        for (var j = 0; j < input[k].Length; j++)
        {
            if (input[k][j] == '.')
                continue;

            dictionary.Add((k, j), new Seat(false, new HashSet<(int, int)>(), new HashSet<(int, int)>()));
        }
    }

    foreach (var key in dictionary.Keys)
    {
        var seat = dictionary[key];
        for (var k = -1; k <= 1; k++)
        {
            for (var j = -1; j <= 1; j++)
            {
                var adjacentKey = (key.Item1 + k, key.Item2 + j);
                if (adjacentKey == key)
                    continue;

                if (dictionary.ContainsKey(adjacentKey))
                {
                    seat.adjacent.Add(adjacentKey);

                    var adjacentSeat = dictionary[adjacentKey];
                    adjacentSeat.adjacent.Add(key);
                }
            }
        }

        for (var k = key.Item2-1; k >= 0; k--)
        {
            if (input[key.Item1][k] != '.')
            {
                seat.distant.Add((key.Item1, k));
                break;
            }
        }

        for (var k = key.Item2+1; k < input.Count; k++)
        {
            if (input[key.Item1][k] != '.')
            {
                seat.distant.Add((key.Item1, k));
                break;
            }
        }
        
        for (var k = key.Item1-1; k >= 0; k--)
        {
            if (input[k][key.Item2] != '.')
            {
                seat.distant.Add((k, key.Item2));
                break;
            }
        }
        
        for (var k = key.Item1+1; k < input.Count; k++)
        {
            if (input[k][key.Item2] != '.')
            {
                seat.distant.Add((k, key.Item2));
                break;
            }
        }
        
        for (var k = 1;; k++)
        {
            if (key.Item1 - k < 0 || key.Item2 - k < 0)
                break;
            
            if (input[key.Item1 - k][key.Item2 - k] != '.')
            {
                seat.distant.Add((key.Item1 - k, key.Item2 - k));
                break;
            }
        }
        
        for (var k = 1;; k++)
        {
            if (key.Item1 + k >= input.Count || key.Item2 - k < 0)
                break;
            
            if (input[key.Item1 + k][key.Item2 - k] != '.')
            {
                seat.distant.Add((key.Item1 + k, key.Item2 - k));
                break;
            }
        }
        
        for (var k = 1;; k++)
        {
            if (key.Item1 - k < 0 || key.Item2 + k >= input.Count)
                break;
            
            if (input[key.Item1 - k][key.Item2 + k] != '.')
            {
                seat.distant.Add((key.Item1 - k, key.Item2 + k));
                break;
            }
        }
        
        for (var k = 1;; k++)
        {
            if (key.Item1 + k >= input.Count || key.Item2 + k >= input.Count)
                break;
            
            if (input[key.Item1 + k][key.Item2 + k] != '.')
            {
                seat.distant.Add((key.Item1 + k, key.Item2 + k));
                break;
            }
        }
    }

    WriteLine("Done");

    return dictionary;
}

void PartOne(Dictionary<(int, int), Seat> input)
{
    var seen = new HashSet<string>();
    var seating = new SortedDictionary<(int, int), bool>();
    foreach(var key in input.Keys)
        seating.Add(key, false);

    var s = "";
    while (!seen.Contains(s))
    {
        seen.Add(s);

        var clone = new SortedDictionary<(int, int), bool>(seating);
        foreach (var key in clone.Keys.Where(x => !clone[x]))
            if (input[key].adjacent.All(x => !clone[x]))
                seating[key] = true;

        foreach (var key in clone.Keys.Where(x => clone[x]))
            if (input[key].adjacent.Count(x => clone[x]) >= 4)
                seating[key] = false;
        
        s = string.Join("", seating.Keys.Select(x => seating[x]));
    }

    WriteLine(seating.Values.Count(x => x));
}

void PartTwo(Dictionary<(int, int), Seat> input)
{
    var seen = new HashSet<string>();
    var seating = new SortedDictionary<(int, int), bool>();
    foreach(var key in input.Keys)
        seating.Add(key, false);

    var s = "";
    while (!seen.Contains(s))
    {
        seen.Add(s);

        var clone = new SortedDictionary<(int, int), bool>(seating);
        foreach (var key in clone.Keys.Where(x => !clone[x]))
            if (input[key].distant.All(x => !clone[x]))
                seating[key] = true;

        foreach (var key in clone.Keys.Where(x => clone[x]))
            if (input[key].distant.Count(x => clone[x]) >= 5)
                seating[key] = false;
        
        s = string.Join("", seating.Keys.Select(x => seating[x]));
    }

    WriteLine(seating.Values.Count(x => x));
}

void PrintMatrix(SortedDictionary<(int, int), bool> dictionary)
{
    var rows = dictionary.Keys.Max(x => x.Item1) + 1;
    var cols = dictionary.Keys.Max(x => x.Item2) + 1;

    var matrix = new char[rows][];
    for (var k = 0; k < rows; k++)
        matrix[k] = new string('.', cols).ToCharArray();

    foreach (var key in dictionary.Keys)
        matrix[key.Item1][key.Item2] = dictionary[key] ? '#' : 'L';        

    foreach (var row in matrix)
        WriteLine(string.Join("", row));

    WriteLine();
}

record Seat(bool isOccupied, HashSet<(int, int)> adjacent, HashSet<(int, int)> distant);
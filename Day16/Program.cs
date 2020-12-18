using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

var rawInput = File.ReadAllText("input.txt", Encoding.UTF8).Split("\r\n\r\n");
PartOne();
PartTwo();

void PartOne()
{
    var ranges = GetRanges();
    var values = rawInput[2]
        .Replace("nearby tickets:\r\n", "")
        .Replace("\r\n", ",")
        .Split(",")
        .Select(int.Parse)
        .ToList();

    var validValues = values
        .Where(x => ranges.Any(y => x >= y.Item1 && x <= y.Item2))
        .ToList();

    WriteLine(values.Where(i => !validValues.Contains(i)).Sum());
}

void PartTwo()
{
    var departureRanges = GetRanges("departure");
    var ranges = GetRanges();
    var otherRanges = rawInput[0]
        .Split("\r\n")
        .Select(x => x.Substring(x.IndexOf(": ") + 2).Replace(" ", ""))
        .Select(x => x.Split("or"))
        .Select(x => x.Select(y => y.Split("-")))
        .Select(x => x.Select(y => (int.Parse(y[0]), int.Parse(y[1]))))
        .ToList();

    var yourTicket = rawInput[1]
        .Replace("your ticket:\r\n", "")
        .Split(",")
        .Select((x, i) => (value: int.Parse(x), i))
        .ToList();

    var nearbyTickets = rawInput[2]
        .Replace("nearby tickets:\r\n", "")
        .Split("\r\n")
        .Select(x => x.Split(",")
            .Select((s, i) => (value: int.Parse(s), i)))
        .Select(x => x.ToList())
        .Where(list => list.All(x => ranges.Any(tuple => x.value < tuple.Item1 || x.value > tuple.Item2)))
        .ToList();

    var sets = Enumerable.Range(0, yourTicket.Count)
        .Select(i => new HashSet<int>())
        .ToArray();
    foreach (var nearbyTicket in nearbyTickets)
    {
        for (var k = 0; k < nearbyTicket.Count; k++)
        {
            for (var j = 0; j < otherRanges.Count; j++)
            {
                if (otherRanges[j]
                    .Any(tuple => nearbyTicket[k].value >= tuple.Item1 && nearbyTicket[k].value <= tuple.Item2))
                {
                    sets[k].Add(j);
                }
            }
        }
    }
    
    foreach (var nearbyTicket in nearbyTickets)
    {
        for (var k = 0; k < nearbyTicket.Count; k++)
        {
            for (var j = 0; j < otherRanges.Count; j++)
            {
                if (!otherRanges[j]
                    .Any(tuple => nearbyTicket[k].value >= tuple.Item1 && nearbyTicket[k].value <= tuple.Item2))
                {
                    sets[k].Remove(j);
                }
            }
        }
    }
}

List<(int, int)> GetRanges(string prefix = "")
{
    return rawInput[0]
        .Split("\r\n")
        .Where(s => s.StartsWith(prefix))
        .Select(x => x.Substring(x.IndexOf(": ") + 2).Replace(" ", ""))
        .SelectMany(x => x.Split("or"))
        .Select(x => x.Split("-"))
        .Select(x => (int.Parse(x[0]), int.Parse(x[1])))
        .ToList();
}
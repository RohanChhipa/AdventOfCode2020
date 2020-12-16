using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

var input = File.ReadAllText("./input.txt")
    .Replace(" ", "")
    .Replace("[", "-")
    .Replace("]", "")
    .Replace("=", "-")
    .Split("\n")
    .Select(s => s.Split("-"))
    .ToList();

PartOne();
PartTwo();

void PartOne()
{
    var mask = "";
    var memory = new Dictionary<int, double>();
    foreach (var inputLine in input)
    {
        if (inputLine[0] == "mask")
            mask = inputLine[1];

        if (inputLine[0] == "mem")
        {
            var location = int.Parse(inputLine[1]);
            if (!memory.ContainsKey(location))
                memory.Add(location, 0);

            var bitstring = Convert
                .ToString(int.Parse(inputLine[2]), 2)
                .PadLeft(36, '0');

            var value = Enumerable.Range(0, bitstring.Length)
                .Sum(i => Math.Pow(2, bitstring.Length - 1 - i) * ((mask[i] == 'X' ? bitstring[i] : mask[i]) - '0'));

            memory[location] = value;
        }
    }

    WriteLine(memory.Values.Sum());
}

void PartTwo()
{
    var mask = "";
    var memory = new Dictionary<string, double>();
    foreach (var inputLine in input)
    {
        if (inputLine[0] == "mask")
            mask = inputLine[1].Trim();

        if (inputLine[0] == "mem")
        {
            var location = Convert.ToString(int.Parse(inputLine[1]), 2).PadLeft(mask.Length, '0');
            var maskedLocations = GenerateMasks(mask, inputLine[1]);

            var value = double.Parse(inputLine[2].Trim());
            foreach (var maskedLocation in maskedLocations)
            {
                if (!memory.ContainsKey(maskedLocation))
                    memory.Add(maskedLocation, 0);

                memory[maskedLocation] = value;
            }
        }
    }

    WriteLine(memory.Values.Sum());
}

List<string> GenerateMasks(string mask, string location)
{
    var masks = new List<string>();

    location = Convert.ToString(int.Parse(location), 2).PadLeft(mask.Length, '0');
    var maskedLocation = location.Select((x, i) => mask[i] == '0' ? x : mask[i]).ToList();
    var builder = new StringBuilder(string.Join("", maskedLocation));

    var maskLocations = maskedLocation.Select((c, i) => (c, i))
        .Where(x => x.c == 'X')
        .Select(x => x.i)
        .ToList();

    for (var k = 0; k < Math.Pow(2, maskLocations.Count); k++)
    {
        var s = Convert.ToString(k, 2).PadLeft(maskLocations.Count, '0');
        for (var j = 0; j < maskLocations.Count; j++)
            builder[maskLocations[j]] = s[j];

        masks.Add(builder.ToString());
    }

    return masks;
}
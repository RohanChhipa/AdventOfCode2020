using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

var directions = new[] { -1, 1, 1, -1 };
var directionsLookup = new Dictionary<char, int>()
{
    {'N', -1},
    {'E', 1},
    {'S', 1},
    {'W', -1},
};

var input = File.ReadAllLines("./input.txt");
PartOne();
PartTwo();

void PartOne()
{
    var currentDir = 1;
    var pos = new[] { 0, 0 };

    foreach (var line in input)
    {
        var command = line[0];
        var value = int.Parse(line.Substring(1));

        if (command == 'L' || command == 'R')
        {
            if (command == 'L')
                value = 360 - value;

            currentDir += value / 90;
            currentDir %= directions.Length;
        }

        if (command == 'F')
            command = directionsLookup.Keys.ElementAt(currentDir);

        if (directionsLookup.ContainsKey(command))
        {
            var idx = command == 'N' || command == 'S' ? 0 : 1;
            pos[idx] += directionsLookup[command] * value;
        }
    }

    WriteLine(pos.Sum());
}

void PartTwo()
{
    var shipPos = new[] { 0, 0 };
    var waypoint = new[] { 10, 1 };
    foreach (var line in input)
    {
        var command = line[0];
        var value = int.Parse(line.Substring(1));

        if (command == 'F')
            shipPos = Enumerable.Range(0, shipPos.Length)
                .Select(i => shipPos[i] + (value * waypoint[i]))
                .ToArray();

        if (command == 'L' || command == 'R')
        {
            if (command == 'L')
                value = 360 - value;

            for (var k = 0; k < value / 90; k++)
                waypoint = waypoint.Append(waypoint.First() * -1).Skip(1).ToArray();
        }

        if (directionsLookup.ContainsKey(command))
        {
            var idx = command == 'N' || command == 'S' ? 1 : 0;
            waypoint[idx] += directionsLookup[command] * value;
        }
    }

    WriteLine(string.Join(", ", shipPos));
    WriteLine(shipPos.Sum(x => Math.Abs(x)));
}
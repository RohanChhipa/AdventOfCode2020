using System;
using System.IO;
using System.Linq;
using static System.Console;

var input = File.ReadAllLines("./input.txt");

var time = double.Parse(input[0]);
var busses = input[1].Split(",")
    .Select((x, i) => (Bus: x, Position: i))
    .Where(x => x.Bus != "x")
    .Select(x => (Bus: int.Parse(x.Bus), Position: x.Position))
    .ToList();

PartOne();
PartTwo();

void PartOne()
{
    var min = busses
        .Select(x => (Time: Math.Ceiling(time / x.Bus) * x.Bus, BusId: x.Bus))
        .Where(x => x.Time >= time)
        .Min();

    WriteLine((min.Time - time) * min.BusId);
}

void PartTwo()
{
    var i = 1;
    long t = 0;
    long stepSize = busses.First().Bus;

    while (i < busses.Count)
    {
        t += stepSize;
        if ((t + busses[i].Position) % busses[i].Bus == 0)
        {
            stepSize *= busses[i].Bus;
            i++;
        }
    }

    WriteLine(t);
}
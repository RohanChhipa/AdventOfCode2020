using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;

var inputs = ReadInput();

var stopwatch = Stopwatch.StartNew();
PartOne();
PartTwo();
WriteLine($"Ellapsed time: {stopwatch.ElapsedMilliseconds}");

void PartOne()
{
    var count = inputs.Select(x => x.password.Count(y => y == x.character))
        .Zip(inputs)
        .Where(x => x.Second.min <= x.First && x.Second.max >= x.First)
        .Count();

    WriteLine(count);
}

void PartTwo()
{
    var count = inputs.Where(x => x.password[x.min - 1] != x.password[x.max - 1])
        .Where(x => x.password[x.min - 1] == x.character || x.password[x.max - 1] == x.character)
        .Count();

    WriteLine(count);
}

List<Input> ReadInput()
{
    var inputs = new List<Input>();

    var s = ReadLine();
    while (!string.IsNullOrEmpty(s))
    {
        string[] arr = s.Replace("-", " ").Replace(":", "").Split(" ");
        inputs.Add(new Input(int.Parse(arr[0]), int.Parse(arr[1]), arr[2][0], arr[3]));

        s = ReadLine();
    }

    return inputs;
}

record Input(int min, int max, char character, string password);
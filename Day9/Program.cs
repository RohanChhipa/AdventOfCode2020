using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;

var input = ReadInput();

var stopwatch = Stopwatch.StartNew();
PartOne(input);
PartTwo(input);
WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

List<long> ReadInput()
{
    List<long> input = new();
    
    var s = ReadLine();
    while (!string.IsNullOrWhiteSpace(s))
    {
        input.Add(long.Parse(s));
        s = ReadLine();
    }

    return input;
}

void PartOne(List<long> input)
{
    WriteLine(GetInvalidValue(input, 25));
}

void PartTwo(List<long> input)
{
    var value = GetInvalidValue(input, 25);
    
    var i = 0;
    var j = 1;
    var sum = input[i] + input[j];
    
    while (true)
    {
        if (sum == value)
        {
            var section = input.Skip(i).Take(j - i + 1);
            WriteLine(section.Max() + section.Min());
            return;
        }

        if (sum < value)
        {
            j++;
            sum += input[j];
        }
        else
        {
            sum -= input[i];
            i++;
        }
    }
}

long GetInvalidValue(List<long> input, int groupSize)
{
    for (var k = groupSize; k < input.Count; k++)
    {
        var start = k - groupSize;
        var group = input.Skip(start).Take(groupSize);

        bool exists = group.SelectMany(x => group, (x, y) => new {x, y})
            .Where(x => x.x != x.y)
            .Any(x => (x.x + x.y) == input[k]);

        if (!exists)
            return input[k];
    }

    return 0;
}
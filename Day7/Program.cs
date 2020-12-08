using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static System.Console;

var input = ReadInput();

var stopwatch = Stopwatch.StartNew();
PartOne(input);
PartTwo(input);
WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

string ReadInput()
{
    var builder = new StringBuilder();
    var s = ReadLine();
    while (!string.IsNullOrEmpty(s))
    {
        builder.AppendLine(s);
        s = ReadLine();
    }

    return builder.ToString();
}

void PartOne(string input)
{
    var dictionary = ParseInput(input);
    var queue = new List<string>{"shiny gold"};
    var set = new HashSet<string>();

    while(queue.Count > 0)
    {
        var bag = queue.First();
        queue.RemoveAt(0);
        set.Add(bag);

        var tmp = dictionary.Keys
            .Where(x => dictionary[x].Any(y => y.bagColour.Contains(bag)))
            .ToList();

        queue.AddRange(tmp);
    }
    
    WriteLine(set.Count - 1);
}

void PartTwo(string input)
{
    var dictionary = ParseInput(input);
    Func<string, int> traverse = null; 
    traverse = (key) => {
        if (dictionary[key].Count == 0)
            return 1;

        return dictionary[key].Sum(x => x.count * traverse(x.bagColour)) + 1;
    };

    WriteLine(traverse("shiny gold") - 1);
}

Dictionary<string, List<Bag>> ParseInput(string input)
{
    var dictionary = new Dictionary<string, List<Bag>>();
    var s = input.Replace(" bags.", "")
        .Replace(" bag.", "")
        .Replace(" bags, ", ", ")
        .Replace(" bags", "")
        .Replace(" bag", "")
        .Replace(" contain ", "-")
        .Split("\n")
        .SkipLast(1);

    foreach(var line in s)
    {
        var vs = line.Split("-");
        vs[0] = vs[0].Trim();

        if (!dictionary.ContainsKey(vs[0]))
            dictionary.Add(vs[0], new List<Bag>());

        if (!vs[1].Contains("no other"))
        {
            dictionary[vs[0]] = vs[1].Split(", ")
                .Select(x => {
                    var space = x.IndexOf(" ");
                    return new Bag(x.Substring(space + 1).Trim(), int.Parse(x.Substring(0, space).Trim()));
                })
                .ToList();
        }
    }

    return dictionary;
}

record Bag(string bagColour, int count);
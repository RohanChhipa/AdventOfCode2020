using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Globalization;
using static System.Console;

var input = ReadInput();

var stopwatch = Stopwatch.StartNew();
PartOne();
PartTwo();
WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

string ReadInput()
{
    string[] lines = File.ReadAllLines("./input.txt");
    return string.Join("\n", lines);
}

IEnumerable<string[]> GetValidPassports()
{
     var validPassports = input.Split("\n\n")
        .Select(s => string.Join(" ", s.Split("\n")))
        .Select(s => s.Split(" "))
        .Where(s => s.Count() >= 7)
        .Where(s => {
            if (s.Count() == 7)
                return !s.Any(str => str.Contains("cid:"));

            return true;
        });
    
    return validPassports;
}

void PartOne()
{
    var a = GetValidPassports();
    WriteLine(a.Count());
}

void PartTwo()
{
    var a = GetValidPassports().Where(s => ValidateFields(s));
    WriteLine(a.Count());
}

bool ValidateFields(string[] passport)
{
    var colours = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

    return passport
        .SelectMany(s => s.Split(" "))
        .All(pair => {
            var s = pair.Split(":");
            // WriteLine(string.Join("-", s));
            switch (s[0])
            {
                case "byr": 
                    return int.Parse(s[1]) >= 1920 && int.Parse(s[1]) <= 2002;
                case "iyr": 
                    return int.Parse(s[1]) >= 2010 && int.Parse(s[1]) <= 2020;
                case "eyr": 
                    return int.Parse(s[1]) >= 2020 && int.Parse(s[1]) <= 2030;
                case "hgt": {
                    if (s[1].EndsWith("cm"))
                    {
                        var height = s[1].Replace("cm", "");
                        return int.Parse(height) >= 150 && int.Parse(height) <= 193;
                    }

                    if (s[1].EndsWith("in"))
                    {
                        var height = s[1].Replace("in", "");
                        return int.Parse(height) >= 59 && int.Parse(height) <= 76;
                    }

                    return false;
                }
                case "hcl": 
                    return s[1].StartsWith('#') 
                        && int.TryParse(s[1].Substring(1), NumberStyles.HexNumber, null, out _);
                case "ecl": 
                    return colours.Contains(s[1]);
                case "pid": 
                    return s[1].Length == 9 && s[1].All(c => char.IsNumber(c));
                case "cid": 
                    return true;
                default:
                    return false;
            }
        });
}
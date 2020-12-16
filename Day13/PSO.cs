using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

public class PSO
{
    public void Run()
    {
        var input = File.ReadAllLines("./input.txt");

        var time = double.Parse(input[0]);
        var busses = input[1].Split(",")
            .Select((x, i) => (Bus: x, Position: i))
            .Where(x => x.Bus != "x")
            .Select(x => (Bus: int.Parse(x.Bus), Position: x.Position))
            .ToList();

        var swarmSize = 100;
        var iterations = 1000;

        var w = 0.729844;
        var c = 1.49618;

        var maxValue = 3162341;

        var random = new Random();

        var fitness = new double[swarmSize];
        var position = Enumerable.Range(0, swarmSize)
                .Select(x => Enumerable.Range(0, busses.Count)
                .Select(i => random.NextDouble() * maxValue)
                .ToArray())
            .ToArray();

        var velocity = Enumerable.Range(0, swarmSize)
            .Select(x => new double[busses.Count()])
            .ToArray();

        var localBestFitness = Enumerable.Range(0, swarmSize)
            .Select(i => double.MaxValue)
            .ToArray();
        var localBest = Enumerable.Range(0, swarmSize)
            .Select(x => new double[busses.Count()])
            .ToArray();

        var globalBestFitness = double.MaxValue;
        var globalBest = new double[busses.Count];

        for (var t = 0; t < iterations; t++)
        {
            for (var k = 0; k < swarmSize; k++)
            {
                fitness[k] = CalculateFitness(position[k], busses);
                if (fitness[k] < localBestFitness[k])
                {
                    localBestFitness[k] = fitness[k];
                    Array.Copy(position[k], localBest[k], busses.Count);
                }

                if (fitness[k] < globalBestFitness)
                {
                    globalBestFitness = fitness[k];
                    Array.Copy(position[k], globalBest, busses.Count);
                }
            }

            for (var k = 0; k < swarmSize; k++)
            {
                velocity[k] = velocity[k]
                    .Select((x, i) => w * x + (c * random.NextDouble() * (localBest[k][i] - position[k][i]))
                        + (c * random.NextDouble() * (globalBest[i] - position[k][i])))
                    .ToArray();

                position[k] = position[k].Zip(velocity[k], (a, b) => a + b).ToArray();
            }

            WriteLine($"{globalBestFitness} - {string.Join(", ", globalBest)}");
        }
    }

    double CalculateFitness(double[] position, List<(int Bus, int Position)> busses)
    {
        var values = position.Select((x, i) => (int)x * busses[i].Bus - busses[i].Position);
        var min = values.Min();

        return values.Sum(i => i - min);
    }
}


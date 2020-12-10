using System.Security;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Linq;

var parser = new Regex("(?<l>\\d+)x(?<w>\\d+)x(?<h>\\d+)");
var boxes = File
    .ReadAllLines("data.txt")
    .Select(line => ParseLine(line));

Part1();
Part2();

void Part2()
{
    var ribbonNeeded = boxes
        .Select(dimensions => RibbonNeeded(dimensions))
        .Sum();

    Console.WriteLine($"We need {ribbonNeeded} feet of ribbon");
}

void Part1()
{
    var paperNeed = boxes
        .Select(dimensions => PaperNeeded(dimensions))
        .Sum();

    Console.WriteLine($"We need {paperNeed} square feet of paper");
}

int RibbonNeeded((int, int, int) dimensions)
{
    var sides = new [] { dimensions.Item1, dimensions.Item2, dimensions.Item3, }.OrderBy(i => i).ToArray();
    var small1 = sides[0];
    var small2 = sides[1];
    return 2 * (small1 + small2) + sides.Aggregate(1, (curr, next) => curr *= next);
}

(int, int, int) ParseLine(string line)
{
    var matches = parser.Matches(line);
    return (
        Convert.ToInt32(matches[0].Groups[1].Value),
        Convert.ToInt32(matches[0].Groups[2].Value),
        Convert.ToInt32(matches[0].Groups[3].Value)
    );
}

int PaperNeeded((int, int, int) dimensions)
{
    var (l, w, h) = dimensions;
    var parts = new[] { l * w, w * h, h * l };
    return 2 * parts.Sum() + parts.Min();
}
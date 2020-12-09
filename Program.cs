using System.Security;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Linq;

var parser = new Regex("(?<l>\\d+)x(?<w>\\d+)x(?<h>\\d+)");
Part1();

void Part1()
{
    var paperNeed = File
        .ReadAllLines("data.txt")
        .Select(line => ParseLine(line))
        .Select(dimensions => PaperNeeded(dimensions))
        .Sum();

    Console.WriteLine($"We need {paperNeed} square feet of paper");
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
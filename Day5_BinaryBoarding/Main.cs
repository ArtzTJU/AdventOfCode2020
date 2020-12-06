using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day5
{
    public static class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day5_BinaryBoarding\input.txt").ToList();   

            var result = lines
                .Select(line => line.Aggregate(0, (last, current) => 2 * last + (current == 'B' || current == 'R' ? 1 : 0)))
                .Max();

            return result.ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day5_BinaryBoarding\input.txt").ToList();   

            var ids = lines
                .Select(line => line.Aggregate(0, (last, current) => 2 * last + (current == 'B' || current == 'R' ? 1 : 0)))
                .ToList();

            var result = ids.First(id => !ids.Contains(id + 1) && ids.Contains(id + 2)) + 1;

            return result.ToString();
        }

    }
}
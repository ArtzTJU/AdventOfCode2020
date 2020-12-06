using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day6
{
    public static class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadAllText(@"Day6_CustomCustoms\input.txt");

            var result = lines.Split("\r\n\r\n")
                .Sum(x => x
                    .Replace("\r\n", string.Empty)
                    .Distinct()
                    .Count());

            return result.ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadAllText(@"Day6_CustomCustoms\input.txt");  

            var result = lines.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                    .Select(y => y.ToArray())
                    .Aggregate((prev, next) => prev.Intersect(next).ToArray()))
                .Sum(x => x.Length);

            return result.ToString();
        }

    }
}
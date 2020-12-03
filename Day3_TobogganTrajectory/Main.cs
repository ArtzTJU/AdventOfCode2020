using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day3
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day3_TobogganTrajectory\input.txt").ToList();
            var result = TraverseLines(lines, 3, 1);

            return result.ToString();

        }
        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day3_TobogganTrajectory\input.txt").ToList();
            var result = TraverseLines(lines, 1, 1) 
                * TraverseLines(lines, 3, 1) 
                * TraverseLines(lines, 5, 1) 
                * TraverseLines(lines, 7, 1) 
                * TraverseLines(lines, 1, 2);

            return result.ToString();
        }

        private static long TraverseLines(List<string> lines, int xStep, int yStep)
        {
            var result = 0L;
            var index = 0;

            for (int i = 0; i < lines.Count; i += yStep)
            {
                var line = lines[i];
                var isTree = line[index] == '#';
                if (isTree)
                    result++;

                index += xStep;
                if (index >= line.Length)
                    index = index - line.Length;
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day8
{
    public static class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadAllLines(@"Day8_HandheldHalting\input.txt").ToList();
            return CalculateLast(lines).Accumulator.ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadAllLines(@"Day8_HandheldHalting\input.txt").ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var instruction = line.Split()[0];

                if (instruction == "acc")
                    continue;
                else if (instruction == "nop")
                    lines[i] = line.Replace("nop", "jmp");
                else
                    lines[i] = line.Replace("jmp", "nop");

                var lastCalculation = CalculateLast(lines);

                if (lastCalculation.LastIndex == lines.Count - 1)
                    return lastCalculation.Accumulator.ToString();

                lines[i] = line;
            }

            return "-1";
        }

        private static (int Accumulator, int LastIndex) CalculateLast(List<string> lines)
        {
            var linesHashset = new HashSet<int>();
            var accumulator = 0;
            var currentLine = 0;

            while (true)
            {
                if(currentLine >= lines.Count)
                    break;

                var line = lines[currentLine];

                if (linesHashset.Contains(currentLine))
                    break;
                else
                    linesHashset.Add(currentLine);

                var lineSplit = line.Split();
                var instruction = lineSplit[0];
                var argument = int.Parse(lineSplit[1]);

                if (instruction == "nop")
                {
                    currentLine++;
                    continue;
                }
                else if (instruction == "acc")
                {
                    accumulator += argument;
                    currentLine++;
                    continue;
                }
                else if (instruction == "jmp")
                {
                    currentLine += argument;
                    continue;
                }
            }


            return (accumulator, linesHashset.Max());
        }
    }
}
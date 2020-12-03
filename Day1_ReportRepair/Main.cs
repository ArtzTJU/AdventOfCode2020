using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day1
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day1_ReportRepair\input.txt")
                .Select(x => int.Parse(x))
                .ToHashSet();

            var firstNumber = lines.First(x => lines.Any(y => y + x == 2020));
            var secondNumber = 2020 - firstNumber;

            return (firstNumber * secondNumber).ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day1_ReportRepair\input.txt")
                .Select(x => int.Parse(x))
                .ToHashSet();

            var firstNumber = 0;
            var secondNumber = 0;
            var thirdNumber = 0;

            lines.Any(x => lines.Any(y => lines.Any(z =>
            {
                if ((x + y + z) == 2020)
                {
                    firstNumber = x;
                    secondNumber = y;
                    thirdNumber = z;
                    return true;
                }

                return false;
            })));

            return (firstNumber * secondNumber * thirdNumber).ToString();
        }
    }
}

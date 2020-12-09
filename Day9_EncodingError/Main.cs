using System;
using System.Linq;
using System.IO;

namespace AdventOfCode.Day9
{
    public class Main
    {
        public static string PartOne()
        {
            var numbers = File
                .ReadAllLines(@"Day9_EncodingError\input.txt")
                .Select(long.Parse)
                .ToList();

            for (var i = 25; i < numbers.Count; i++)
            {
                var preamble = numbers.Skip(i - 25).Take(25).ToList();
                var number = numbers[i];
                var found = false;

                for (var j = 0; j < 24; j++)
                    for (var k = j + 1; k < 25; k++)
                        if (preamble[j] + preamble[k] == number)
                            found = true;

                if (found == false)
                    return number.ToString();
            }

            return "-1";
        }

        public static string PartTwo()
        {
            var numbers = File
                .ReadAllLines(@"Day9_EncodingError\input.txt")
                .Select(long.Parse)
                .ToList();

            for (var i = 0; i < numbers.Count; i++)
            {
                var smallest = numbers[i];
                var largest = numbers[i];
                var sum = numbers[i];
                for (var y = i + 1; y < numbers.Count; y++)
                {
                    smallest = smallest > numbers[y] ? numbers[y] : smallest;
                    largest = largest < numbers[y] ? numbers[y] : largest;
                    sum += numbers[y];

                    if (sum > 217430975)
                        break;
                    if (sum == 217430975)
                        return (smallest + largest).ToString();
                }
            }

            return "-1";

        }
    }
}
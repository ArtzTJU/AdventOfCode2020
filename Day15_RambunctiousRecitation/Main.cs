using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day15
{
    public class Main
    {
        public static string PartOne()
        {
            var numbers = File.ReadLines(@"Day15_RambunctiousRecitation\input.txt")
                             .First()
                             .Split(',')
                             .Select(int.Parse)
                             .ToList();

            return Calculate(numbers, 2020).ToString();
        }

        public static string PartTwo()
        {
            var numbers = File.ReadLines(@"Day15_RambunctiousRecitation\input.txt")
                            .First()
                            .Split(',')
                            .Select(int.Parse)
                            .ToList();

            return Calculate(numbers, 30000000).ToString();
        }

        private static int Calculate(List<int> numbers, int count)
        {
            var memory = numbers
                .Select((character, index) => (character, index))
                .ToDictionary(x => x.character, x => (-1, x.index));

            var lastNumber = memory.Last().Key;

            for (var i = numbers.Count; i < count; i++)
            {
                if (memory.TryGetValue(lastNumber, out var lastIndex))
                {
                    if (lastIndex.Item1 == -1)
                        lastNumber = 0;
                    else
                        lastNumber = lastIndex.index - lastIndex.Item1;
                }

                if (memory.ContainsKey(lastNumber))
                    memory[lastNumber] = (memory[lastNumber].index, i);
                else
                    memory[lastNumber] = (-1, i);
            }

            return lastNumber;
        }
    }
}

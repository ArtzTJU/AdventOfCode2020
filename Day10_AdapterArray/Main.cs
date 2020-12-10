using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode.Day10
{
    public static class Main
    {
        public static string PartOne()
        {
            var adapters = File.ReadAllLines(@"Day10_AdapterArray\input.txt")
                .Select(int.Parse)
                .OrderBy(x => x)
                .ToList();

            adapters.Insert(0, 0);

            var difference1 = 0;
            var difference3 = 1;

            for (var i = 0; i < adapters.Count - 1; i++)
            {
                var currentAdapter = adapters[i];
                var nextAdapter = adapters[i + 1];
                var difference = nextAdapter - currentAdapter;

                if (difference == 3)
                    difference3++;
                if (difference == 1)
                    difference1++;
            }

            return (difference1 * difference3).ToString();

        }

        public static string PartTwo()
        {
            var adapters = File.ReadAllLines(@"Day10_AdapterArray\input.txt")
                .Select(int.Parse)
                .OrderByDescending(x => x)
                .ToList();

            adapters.Add(0);

            var arrangments = new Dictionary<int, long>
            {
                { adapters.First() + 3, 1 }
            };

            foreach (var adapter in adapters)
            {
                arrangments.TryGetValue(adapter + 1, out long arrangement1);
                arrangments.TryGetValue(adapter + 2, out long arangement2);
                arrangments.TryGetValue(adapter + 3, out long arangement3);

                arrangments[adapter] = arrangement1 + arangement2 + arangement3;
            }

            return arrangments[0].ToString();
        }
    }
}
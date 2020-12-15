using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Day13
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day13_ShuttleSearch\input.txt").ToList(); ;

            var departTime = int.Parse(lines[0]);
            var buses = lines[1].Split(',').Where(x => int.TryParse(x, out var _)).Select(int.Parse).ToList();

            var times = new Dictionary<int, int>();

            foreach (var bus in buses)
            {
                var time = bus;

                while (time < departTime)
                    time += bus;

                times[bus] = time;
            }

            var firstPossibleBus = times.OrderBy(x => x.Value).First();

            return ((firstPossibleBus.Value - departTime) * firstPossibleBus.Key).ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day13_ShuttleSearch\input.txt").ToList(); 

            return "";
        }
    }
}

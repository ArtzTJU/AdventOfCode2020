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
            var lines = File.ReadLines(@"Day13\input.txt").ToList(); ;

            var departTime = int.Parse(lines[0]);
            var buses = lines[1].Split(',').Where(x => int.TryParse(x, out var _)).Select(int.Parse).ToList();

            var times = new Dictionary<int, int>();

            foreach (var bus in buses)
            {
                var time = bus;

                while (time < departTime)
                {
                    time += bus;
                }

                times[bus] = time;
            }

            var res = times.OrderBy(x => x.Value).First();

            return ((res.Value - departTime) * res.Key).ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day13\input.txt").ToList(); ;

            var buses = lines[1].Split(',')
                .Select((x,i) => new { x, i})
                .Where(x => int.TryParse(x.x, out var _))
                .ToList();

            var times = new Dictionary<int, int>();

            foreach (var bus in buses)
            {
                Console.Write(bus.i +1 ); Console.Write("-"+bus.x);
                Console.WriteLine()
;
            }


            return "";
        }
    }
}

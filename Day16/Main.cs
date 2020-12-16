using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day16
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day16\input.txt");

            var fields = lines
                .Take(20)
                .Select(line =>
                {
                    var name = line.Split(":")[0];
                    var groups = Regex.Match(line, @".*: ([0-9]*)-([0-9]*) or ([0-9]*)-([0-9]*)").Groups;
                    return new
                    {
                        name,
                        i1 = int.Parse(groups[1].Value),
                        i2 = int.Parse(groups[2].Value),
                        i3 = int.Parse(groups[3].Value),
                        i4 = int.Parse(groups[4].Value)
                    };
                })
                .ToList();
            var myTicket = lines.Skip(22).First();
            var nearbyTickets = lines
                .Skip(25)
                .Select(line => line.Split(',').Select(int.Parse).ToList())
                .ToList();

            var result = 0;

            foreach (var ticket in nearbyTickets)
                foreach (var number in ticket)
                    if (!fields.Any(field => (field.i1 <= number && number <= field.i2) || (field.i3 <= number && number <= field.i4)))
                        result += number;

            return result.ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day16\input.txt");

            var fields = lines
                .Take(20)
                .Select(line =>
                {
                    var name = line.Split(":")[0];
                    var groups = Regex.Match(line, @".*: ([0-9]*)-([0-9]*) or ([0-9]*)-([0-9]*)").Groups;
                    return new
                    {
                        name,
                        i1 = int.Parse(groups[1].Value),
                        i2 = int.Parse(groups[2].Value),
                        i3 = int.Parse(groups[3].Value),
                        i4 = int.Parse(groups[4].Value)
                    };
                })
                .ToList();
            var myTicket = lines.Skip(22).First().Split(',').Select(long.Parse).ToList();
            var tickets = lines
                .Skip(25)
                .Select(line => line.Split(',').Select(int.Parse).ToList())
                .ToList();


            for (var i = 0; i < tickets.Count; i++)
            {
                var deleted = false;
                var ticket = tickets[i];
                foreach (var number in ticket)
                {
                    if (!fields.Any(field => (field.i1 <= number && number <= field.i2) || (field.i3 <= number && number <= field.i4)))
                    {
                        tickets.RemoveAt(i);
                        i--;
                        deleted = true;
                    }

                    if (deleted)
                        break;
                }

                if (deleted)
                    continue;
            }

            var possibilities = fields.ToDictionary(fields => fields.name, _ => new List<int>());
            for (int i = 0; i < fields.Count; i++)
            {
                var values = tickets.Select(ticket => ticket[i]).ToList();
                foreach (var field in fields)
                    if (values.All(value => (field.i1 <= value && value <= field.i2) || (field.i3 <= value && value <= field.i4)))
                        possibilities[field.name].Add(i);
            }

            var defined = new Dictionary<string, int>();
            while (possibilities.Any())
            {
                var exacts = possibilities.Where(possibility => possibility.Value.Count == 1);
                foreach (var exact in exacts)
                {
                    var value = exact.Value[0];
                    foreach (var possibility in possibilities.Except(exacts))
                    {
                        possibility.Value.Remove(value);
                    }
                    defined[exact.Key] = value;
                    possibilities.Remove(exact.Key);
                }
            }

            var indexes = defined.Where(x => x.Key.StartsWith("departure")).Select(x => x.Value).ToList();

            return indexes.Aggregate(1L, (a,b) => a * myTicket[b]).ToString();
        }
    }
}

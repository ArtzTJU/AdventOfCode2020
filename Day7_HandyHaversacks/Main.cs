using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode.Day7
{
    public static class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day7_HandyHaversacks\input.txt").ToList();

            var bags = ParseBags(lines);
            var goldBag = bags["shiny gold"];

            return goldBag.GetParentColors().Distinct().Count().ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day7_HandyHaversacks\input.txt").ToList();

            var bags = ParseBags(lines);
            var goldBag = bags["shiny gold"];

            return goldBag.GetChildrenAmount().ToString();
        }

        private static Dictionary<string, Bag> ParseBags(IEnumerable<string> lines)
        {
            var bags = new Dictionary<string, Bag>();

            foreach (var line in lines)
            {
                var split = line.Split("bags contain");

                var parent = split[0].Trim();
                var children = split[1].Split(",").Select(x => x.Trim());

                if (bags.TryGetValue(parent, out var parentBag) == false)
                {
                    parentBag = new Bag { Color = parent };
                    bags[parent] = parentBag;
                };

                foreach (var child in children)
                {
                    if (child == "no other bags.")
                        continue;

                    var amount = int.Parse(child.Split()[0]);
                    var color = child
                        .TrimStart('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ')
                        .Split("bag")[0].Trim();

                    if (bags.TryGetValue(color, out var childBag) == false)
                    {
                        childBag = new Bag { Color = color };
                        bags[color] = childBag;
                    };

                    childBag.Parents.Add(parentBag);
                    parentBag.Children.Add((childBag, amount));
                }
            }

            return bags;
        }

        public class Bag
        {
            public string Color { get; set; }
            public List<(Bag, int)> Children { get; set; } = new List<(Bag, int)>();
            public List<Bag> Parents { get; set; } = new List<Bag>();

            public List<string> GetParentColors()
            {
                return Parents
                    .SelectMany(p => p
                        .GetParentColors()
                        .Concat(new[] { p.Color }))
                    .ToList();
            }

            public long GetChildrenAmount()
            {
                var children = Children.Sum(c => c.Item2);
                var nestedChildren = Children.Sum(c => c.Item1.GetChildrenAmount() * c.Item2);

                return children + nestedChildren;
            }
        }
    }
}
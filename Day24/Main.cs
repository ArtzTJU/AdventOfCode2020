using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day24
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadAllLines(@"Day24\input.txt");
            var grid = new Dictionary<(int, int), bool>();

            foreach (var line in lines)
            {
                var x = 0;
                var y = 0;

                for (var i = 0; i < line.Length; i++)
                {
                    var firstChar = line[i];

                    var secondChar = i == line.Length - 1 ? ' ' : line[i + 1];

                    switch (firstChar)
                    {
                        case 'e':
                            x++;
                            y++;
                            break;
                        case 's':
                            switch (secondChar)
                            {
                                case 'e':
                                    x++;
                                    break;
                                case 'w':
                                    y--;
                                    break;
                            }
                            i++;
                            break;
                        case 'w':
                            x--;
                            y--;
                            break;
                        case 'n':
                            switch (secondChar)
                            {
                                case 'e':
                                    y++;
                                    break;
                                case 'w':
                                    x--;
                                    break;
                            }
                            i++;
                            break;
                    }


                }

                grid.TryGetValue((x, y), out var value);
                grid[(x, y)] = !value;
            }

            return grid.Count(x => x.Value).ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadAllLines(@"Day24\input.txt");
            var grid = new Dictionary<(int, int), bool>();

            foreach (var line in lines)
            {
                var x = 0;
                var y = 0;

                for (var i = 0; i < line.Length; i++)
                {
                    var firstChar = line[i];

                    var secondChar = i == line.Length - 1 ? ' ' : line[i + 1];

                    switch (firstChar)
                    {
                        case 'e':
                            x++;
                            y++;
                            break;
                        case 's':
                            switch (secondChar)
                            {
                                case 'e':
                                    x++;
                                    break;
                                case 'w':
                                    y--;
                                    break;
                            }
                            i++;
                            break;
                        case 'w':
                            x--;
                            y--;
                            break;
                        case 'n':
                            switch (secondChar)
                            {
                                case 'e':
                                    y++;
                                    break;
                                case 'w':
                                    x--;
                                    break;
                            }
                            i++;
                            break;
                    }


                }

                grid.TryGetValue((x, y), out var value);
                grid[(x, y)] = !value;
            }

            for (var i = 0; i < 100; i++)
            {
                var nextGrid = new Dictionary<(int, int), bool>();

                foreach (var tile in grid)
                {
                    nextGrid[tile.Key] = tile.Value;

                    var neighbors = GetNeighbors(tile.Key, grid);

                    foreach (var neighbor in neighbors)
                    {
                        if (grid.ContainsKey(neighbor.Key))
                            continue;

                        var outerNeighbors = GetNeighbors(neighbor.Key, grid)
                            .Where(x => grid.ContainsKey(x.Key));

                        if (outerNeighbors.Count(x => x.Value) == 2)
                            nextGrid[neighbor.Key] = true;
                    }

                    if (tile.Value == true && (neighbors.Count(x => x.Value) > 2 || neighbors.Count(x => x.Value) == 0))
                        nextGrid[tile.Key] = false;
                    else if (tile.Value == false && neighbors.Count(x => x.Value) == 2)
                        nextGrid[tile.Key] = true;
                }

                grid = nextGrid.ToDictionary(x => x.Key, x => x.Value);
            }

            return grid.Count(x => x.Value).ToString();
        }

        public static Dictionary<(int, int), bool> GetNeighbors((int, int) target, Dictionary<(int, int), bool> grid)
        {
            var possibleNeighbors = new List<(int, int)>
            {
                (target.Item1 + 1, target.Item2 + 1),
                (target.Item1 + 1, target.Item2),
                (target.Item1, target.Item2 - 1),
                (target.Item1 - 1, target.Item2 - 1),
                (target.Item1, target.Item2 + 1),
                (target.Item1 - 1, target.Item2),
            };

            var neighbors = new Dictionary<(int, int), bool>();
            foreach (var neighbor in possibleNeighbors)
            {
                grid.TryGetValue(neighbor, out var state);
                neighbors[neighbor] = state;
            }
            return neighbors;
        }

    }
}

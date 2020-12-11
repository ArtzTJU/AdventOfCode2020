using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode.Day11
{
    public static class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadAllLines(@"Day11_SeatingSystem\input.txt");
            var seats = new Dictionary<(int x, int y), char>();

            for (var y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (var x = 0; x < line.Length; x++)
                {
                    var character = line[x];
                    seats[(x, y)] = character;
                }
            }

            while (true)
            {
                seats = Tick(seats, out var changed);

                if (changed == false)
                    return seats.Count(x => x.Value == '#').ToString();
            }

            return "-1";
        }

        private static Dictionary<(int x, int y), char> Tick(Dictionary<(int x, int y), char> input, out bool changed)
        {
            var result = new Dictionary<(int x, int y), char>();
            changed = false;

            foreach (var seat in input)
            {
                result[seat.Key] = seat.Value;

                if (seat.Value == '.')
                    continue;

                var neighbors = new List<char>(8);
                var possibleNeighbors = new[] {
                    (seat.Key.x -1, seat.Key.y - 1),
                    (seat.Key.x, seat.Key.y - 1),
                    (seat.Key.x + 1, seat.Key.y - 1),
                    (seat.Key.x - 1, seat.Key.y),
                    (seat.Key.x + 1, seat.Key.y),
                    (seat.Key.x - 1, seat.Key.y + 1),
                    (seat.Key.x, seat.Key.y + 1),
                    (seat.Key.x + 1, seat.Key.y + 1)
                };

                foreach (var possibleNeighbor in possibleNeighbors)
                    if (input.TryGetValue(possibleNeighbor, out var value))
                        neighbors.Add(value);

                if (seat.Value == 'L' && neighbors.Count(x => x == '#') == 0)
                {
                    result[seat.Key] = '#';
                    changed = true;
                }

                else if (seat.Value == '#' && neighbors.Count(x => x == '#') >= 4)
                {
                    result[seat.Key] = 'L';
                    changed = true;
                }

            }

            return result;
        }
        
        public static string PartTwo()
        {
            var lines = File.ReadAllLines(@"Day11_SeatingSystem\input.txt");
            var seats = new Dictionary<(int x, int y), char>();

            for (var y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (var x = 0; x < line.Length; x++)
                {
                    var character = line[x];
                    seats[(x, y)] = character;
                }
            }

            while (true)
            {
                seats = TickTwo(seats, out var changed);

                if (changed == false)
                    return seats.Count(x => x.Value == '#').ToString();
            }

            return "-1";
        }

        private static Dictionary<(int x, int y), char> TickTwo(Dictionary<(int x, int y), char> input, out bool changed)
        {
            var result = new Dictionary<(int x, int y), char>();
            changed = false;

            foreach (var seat in input)
            {
                result[seat.Key] = seat.Value;

                if (seat.Value == '.')
                    continue;

                var occupied = 0;
                var possibleDirections = new Func<int, int, (int, int)>[] {
                    (x, y) => (x - 1, y - 1),
                    (x, y) => (x, y - 1),
                    (x, y) => (x + 1, y - 1),
                    (x, y) => (x - 1, y),
                    (x, y) => (x + 1, y),
                    (x, y) => (x - 1, y + 1),
                    (x, y) => (x, y + 1),
                    (x, y) => (x + 1, y + 1)
                };

                foreach(var direction in possibleDirections){
                    var next = direction(seat.Key.x, seat.Key.y);

                    while(input.TryGetValue(next, out var value))
                    {
                        if(value == '#')
                        {
                            occupied++;
                            break;
                        }
                        else if(value == 'L'){
                            break;
                        }

                        next = direction(next.Item1, next.Item2);
                    }
                }

                if (seat.Value == 'L' && occupied == 0)
                {
                    result[seat.Key] = '#';
                    changed = true;
                }

                else if (seat.Value == '#' && occupied >= 5)
                {
                    result[seat.Key] = 'L';
                    changed = true;
                }

            }

            return result;
        }
    }
}
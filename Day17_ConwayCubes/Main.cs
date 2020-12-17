using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day17
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day17_ConwayCubes\input.txt").ToList();
            var cubes = new Dictionary<(int x, int y, int z), bool>();

            for (var y = 0; y < lines.Count; y++)
                for (var x = 0; x < lines[y].Length; x++)
                    cubes[(x, y, 0)] = lines[y][x] == '#' ? true : false;

            var cycles = 0;

            while (cycles < 6)
            {
                var nextCubes = new Dictionary<(int x, int y, int z), bool>();

                foreach (var cube in cubes)
                {
                    var neighbors = GetNeighbors(cube.Key, cubes);

                    foreach (var neighbor in neighbors)
                    {
                        if (cubes.ContainsKey(neighbor.Key))
                            continue;

                        var outerNeighbors = GetNeighbors(neighbor.Key, cubes)
                            .Where(x => cubes.ContainsKey(x.Key));

                        if (outerNeighbors.Count(x => x.Value) == 3)
                            nextCubes[neighbor.Key] = true;

                    }

                    if (cube.Value == true && (neighbors.Count(x => x.Value) == 2 || neighbors.Count(x => x.Value) == 3))
                        nextCubes[cube.Key] = cube.Value;
                    else if (cube.Value == false && neighbors.Count(x => x.Value) == 3)
                        nextCubes[cube.Key] = true;
                    else
                        nextCubes[cube.Key] = false;
                }

                cycles++;
                cubes = nextCubes.ToDictionary(x => x.Key, x => x.Value);
            }

            return cubes.Count(x => x.Value).ToString();
        }

        public static Dictionary<(int x, int y, int z), bool> GetNeighbors((int x, int y, int z) target, Dictionary<(int x, int y, int z), bool> cubes)
        {
            var possibleNeighbors = new List<(int x, int y, int z)>();

            for (int x = -1 + target.x; x <= 1 + target.x; x++)
                for (int y = -1 + target.y; y <= 1 + target.y; y++)
                    for (int z = -1 + target.z; z <= 1 + target.z; z++)
                            if (x == target.x && y == target.y && z == target.z)
                                continue;
                            else
                                possibleNeighbors.Add((x, y, z));


            var neighbors = new Dictionary<(int x, int y, int z), bool>();
            foreach (var neighbor in possibleNeighbors)
            {
                cubes.TryGetValue(neighbor, out var state);
                neighbors[neighbor] = state;
            }
            return neighbors;
        }

        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day17_ConwayCubes\input.txt").ToList();
            var cubes = new Dictionary<(int x, int y, int z, int w), bool>();

            for (var y = 0; y < lines.Count; y++)
                for (var x = 0; x < lines[y].Length; x++)
                    cubes[(x, y, 0, 0)] = lines[y][x] == '#' ? true : false;

            var cycles = 0;

            while (cycles < 6)
            {
                var nextCubes = new Dictionary<(int x, int y, int z, int w), bool>();

                foreach (var cube in cubes)
                {
                    var neighbors = GetNeighbors(cube.Key, cubes);

                    foreach (var neighbor in neighbors)
                    {
                        if (cubes.ContainsKey(neighbor.Key))
                            continue;

                        var outerNeighbors = GetNeighbors(neighbor.Key, cubes)
                            .Where(x => cubes.ContainsKey(x.Key));

                        if (outerNeighbors.Count(x => x.Value) == 3)
                            nextCubes[neighbor.Key] = true;

                    }

                    if (cube.Value == true && (neighbors.Count(x => x.Value) == 2 || neighbors.Count(x => x.Value) == 3))
                        nextCubes[cube.Key] = cube.Value;
                    else if (cube.Value == false && neighbors.Count(x => x.Value) == 3)
                        nextCubes[cube.Key] = true;
                    else
                        nextCubes[cube.Key] = false;
                }

                cycles++;
                cubes = nextCubes.ToDictionary(x => x.Key, x => x.Value);
            }

            return cubes.Count(x => x.Value).ToString();
        }


        public static Dictionary<(int x, int y, int z, int w), bool> GetNeighbors((int x, int y, int z, int w) target, Dictionary<(int x, int y, int z, int w), bool> cubes)
        {
            var possibleNeighbors = new List<(int x, int y, int z, int w)>();

            for (int x = -1 + target.x; x <= 1 + target.x; x++)
                for (int y = -1 + target.y; y <= 1 + target.y; y++)
                    for (int z = -1 + target.z; z <= 1 + target.z; z++)
                        for (int w = -1 + target.w; w <= 1 + target.w; w++)
                            if (x == target.x && y == target.y && z == target.z && w == target.w)
                                continue;
                            else
                                possibleNeighbors.Add((x, y, z, w));


            var neighbors = new Dictionary<(int x, int y, int z, int w), bool>();
            foreach (var neighbor in possibleNeighbors)
            {
                cubes.TryGetValue(neighbor, out var state);
                neighbors[neighbor] = state;
            }
            return neighbors;
        }

    }
}

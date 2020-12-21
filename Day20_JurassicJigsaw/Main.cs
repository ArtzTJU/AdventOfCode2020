using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day20
{
    public class Main
    {
        public static string PartOne()
        {
            var input = File.ReadAllText(@"Day20_JurassicJigsaw\input.txt");

            var tiles = GetTilesGrid(input);
            var last = tiles.GetLength(0) - 1;

            var corners = new[] {
                tiles[0,0], tiles[last, 0], tiles[0, last], tiles[last, last]
            };

            return corners.Select(x => x.Id).Aggregate(1L, (a, b) => a * b).ToString();
        }

        public static string PartTwo()
        {
            var input = File.ReadAllText(@"Day20_JurassicJigsaw\input.txt");

            var grid = GetTilesGrid(input);
            var image = new List<string>();

            for (var x = 0; x < 12; x++)
                for (var i = 1; i < 9; i++)
                {
                    var st = "";
                    
                    for (var y = 0; y < 12; y++)
                        st += grid[x, y].Row(i).Substring(1, 8);

                    image.Add(st);
                }

            var tile = new Tile(-1, image.ToList());

            for (var i = 0; i < 9; i++)
            {
                var res = 0;

                for (var x = 0; x < tile.Length; x++)
                    for (var y = 0; y < tile.Length; y++)
                        if (IsMonster(x, y, tile))
                            res++;

                if (res > 0)
                {
                    var hashCount = 0;
                    for (var x = 0; x < tile.Length; x++)
                        for (var y = 0; y < tile.Length; y++)
                            if (tile.GetCharacter(x, y) == '#')
                                hashCount++;

                    return (hashCount - res * 15).ToString();
                }

                tile.Rotate();
            }

            return "-1";
        }

        private static bool IsMonster(int x, int y, Tile tile)
        {
            var monster = new List<string> {
                "                  # ",
                "#    ##    ##    ###",
                " #  #  #  #  #  #   "
            };

            var width = monster[0].Length;
            var height = monster.Count;

            if (y + width >= tile.Length)
                return false;

            if (x + height >= tile.Length)
                return false;

            for (var col = 0; col < width; col++)
                for (var row = 0; row < height; row++)
                    if (monster[row][col] == '#' && tile.GetCharacter(x + row, y + col) != '#')
                        return false;

            return true;
        }

        private static List<Tile> GetTiles(string input)
        {
            return input.Split("\r\n\r\n")
                .Select(line => new Tile(
                        int.Parse(line.Split("\r\n")[0].Split("Tile ")[1].TrimEnd(':')),
                        line.Split("\r\n").Skip(1).ToList()
                    ))
                .ToList();
        }

        private static Tile[,] GetTilesGrid(string input)
        {
            var tiles = GetTiles(input).ToList();
            var gridLength = (int)Math.Sqrt(tiles.Count);
            var grid = new Tile[gridLength, gridLength];

            for (var x = 0; x < gridLength; x++)
                for (var y = 0; y < gridLength; y++)
                {
                    var topPattern = x == 0 ? null : grid[x - 1, y].Bottom();
                    var leftPattern = y == 0 ? null : grid[x, y - 1].Right();

                    var tile = FindTile(topPattern, leftPattern, tiles);
                    grid[x, y] = tile;
                    tiles.Remove(tile);
                }

            return grid;
        }

        private static Tile FindTile(string top, string left, List<Tile> tiles)
        {
            foreach (var tile in tiles)
                for (var i = 0; i < 8; i++)
                {
                    var doesTopMatch = top != null 
                        ? tile.Top() == top 
                        : !tiles.Any(otherTile => 
                            otherTile.Id != tile.Id && 
                            otherTile.Edges.Contains(tile.Top()));

                    var doesLeftMatch = left != null 
                        ? tile.Left() == left 
                        : !tiles.Any(otherTile => 
                            otherTile.Id != tile.Id && 
                            otherTile.Edges.Contains(tile.Left()));

                    if (doesTopMatch && doesLeftMatch)
                        return tile;

                    tile.Rotate();
                }

            return null;
        }

        private class Tile
        {
            public int Id { get; set; }
            public List<string> Image { get; set; }
            public int Length { get; set; }
            public int Position { get; set; } = 0;
            public List<string> Edges { get; set; }
            public string Row(int irow) => GetEdge(irow, 0, 0, 1);
            public string Top() => GetEdge(0, 0, 0, 1);
            public string Bottom() => GetEdge(Length - 1, 0, 0, 1);
            public string Left() => GetEdge(0, 0, 1, 0);
            public string Right() => GetEdge(0, Length - 1, 1, 0);

            public Tile(int title, List<string> image)
            {
                Id = title;
                Image = image;
                Length = image.Count;

                Edges = new List<string> {
                    GetEdge(0, 0, 0, 1),
                    GetEdge(0, 0, 1, 0),
                    GetEdge(Length-1, 0, 0, 1),
                    GetEdge(Length-1, 0, -1, 0),
                    GetEdge(0, Length-1, 0, -1),
                    GetEdge(0, Length-1, 1, 0),
                    GetEdge(Length-1, Length-1, 0, -1),
                    GetEdge(Length-1, Length-1, -1, 0),
                };
            }

            public void Rotate()
            {
                Position++;
                Position %= 8;
            }

            public char GetCharacter(int x, int y)
            {
                for (var i = 0; i < Position % 4; i++)
                    (x, y) = (y, Length - 1 - x);

                if (Position % 8 >= 4)
                    y = Length - 1 - y;

                return Image[x][y];
            }


            public string GetEdge(int x, int y, int x2, int y2)
            {
                var st = "";
                for (var i = 0; i < Length; i++)
                {
                    st += GetCharacter(x, y);
                    x += x2;
                    y += y2;
                }
                return st;
            }
        }
    }
}

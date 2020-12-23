using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day22
{
    public class Main
    {
        public static string PartOne()
        {
            var text = File.ReadAllText(@"Day22_CrabCombat\input.txt");
            var split = text.Split("\r\n\r\n");

            var player1 = split[0]
                .Split("\r\n")
                .Skip(1)
                .Select(long.Parse)
                .ToList();

            var player2 = split[1]
                .Split("\r\n")
                .Skip(1)
                .Select(long.Parse)
                .ToList();

            while (player1.Any() && player2.Any())
            {
                var player1Card = player1.First();
                var player2Card = player2.First();

                if (player1Card > player2Card)
                {
                    player2.Remove(player2Card);
                    player1.Remove(player1Card);

                    player1.Add(player1Card);
                    player1.Add(player2Card);
                }
                else
                {
                    player2.Remove(player2Card);
                    player1.Remove(player1Card);

                    player2.Add(player2Card);
                    player2.Add(player1Card);
                }
            }

            var winner = player1.Any() ? player1 : player2;
            var i = winner.Count;

            return winner.Aggregate(0L, (a, b) => a + (b * i--)).ToString();
        }

        public static string PartTwo()
        {
            var text = File.ReadAllText(@"Day22_CrabCombat\input.txt");
            var split = text.Split("\r\n\r\n");

            var player1 = split[0]
                .Split("\r\n")
                .Skip(1)
                .Select(int.Parse)
                .ToList();

            var player2 = split[1]
                .Split("\r\n")
                .Skip(1)
                .Select(int.Parse)
                .ToList();

            Game(player1, player2);
            var winner = player1.Any() ? player1 : player2;
            var i = winner.Count;

            return winner.Aggregate(0L, (a, b) => a + (b * i--)).ToString();
        }

        private static int Game(List<int> player1, List<int> player2)
        {
            var p1History = new List<List<int>>();
            var p2History = new List<List<int>>();

            while (player1.Any() && player2.Any())
            {              
                if (CheckHistory(player1, p1History) && CheckHistory(player1, p1History))
                    return 1;
                    
                p1History.Add(player1.ToList());
                p2History.Add(player2.ToList());

                var player1Card = player1.First();
                var player2Card = player2.First();

                player2.Remove(player2Card);
                player1.Remove(player1Card);

                if (player1Card <= player1.Count && player2Card <= player2.Count)
                {
                    var winner = Game(player1.Take(player1Card).ToList(), player2.Take(player2Card).ToList());

                    if (winner == 1)
                    {
                        player1.Add(player1Card);
                        player1.Add(player2Card);

                    }
                    else
                    {
                        player2.Add(player2Card);
                        player2.Add(player1Card);
                    }
                }
                else if (player1Card > player2Card)
                {
                    player1.Add(player1Card);
                    player1.Add(player2Card);
                }
                else
                {
                    player2.Add(player2Card);
                    player2.Add(player1Card);
                }
            }

            return player1.Any() ? 1 : 2;
        }

        private static bool CheckHistory(List<int> player, List<List<int>> history)
        {
            foreach (var combination in history)
            {
                if (combination.SequenceEqual(player))
                    return true;
            }

            return false;
        }

    }
}

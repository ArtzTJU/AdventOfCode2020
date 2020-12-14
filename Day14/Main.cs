using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day14
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day14\input.txt").ToList();
            var mask = "";
            var memory = new Dictionary<int, string>();

            foreach (var line in lines)
            {
                var amount = line.Split(" = ")[1];

                if (line.Contains("mask"))
                {
                    mask = amount;
                    continue;
                }

                var index = int.Parse(Regex.Match(line, "\\[([0-9]*)\\]").Value.Replace("[", "").Replace("]", ""));
                var bitValue = Convert.ToString(int.Parse(amount), 2).PadLeft(36, '0').ToArray();

                for (int i = 0; i < mask.Length; i++)
                {
                    var character = mask[i];
                    if (character == 'X')
                        continue;
                    else if (character == '1')
                        bitValue[i] = '1';
                    else if (character == '0')
                        bitValue[i] = '0';
                }

                memory[index] = string.Join("", bitValue);
            }

            return memory.Sum(x => Convert.ToInt64(x.Value, 2)).ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day14\input.txt").ToList();
            var mask = "";
            var memory = new Dictionary<long, string>();

            foreach (var line in lines)
            {
                var amount = line.Split(" = ")[1];

                if (line.Contains("mask"))
                {
                    mask = amount;
                    continue;
                }

                var index = int.Parse(Regex.Match(line, "\\[([0-9]*)\\]").Value.Replace("[", "").Replace("]", ""));
                var bitValue = Convert.ToString(int.Parse(amount), 2).PadLeft(36, '0').ToArray();
                var indexBitValue = Convert.ToString(index, 2).PadLeft(36, '0').ToArray();

                for (int i = 0; i < mask.Length; i++)
                {
                    var character = mask[i];
                    if (character == '0')
                        continue;
                    else if (character == '1')
                        indexBitValue[i] = '1';
                    else if (character == 'X')
                        indexBitValue[i] = 'X';
                }


                var adresses = GenerateAdresses(string.Join("", indexBitValue));

                foreach (var adress in adresses)
                {
                    memory[Convert.ToInt64(adress, 2)] = string.Join("", bitValue);
                }
            }

            return memory.Sum(x => Convert.ToInt64(x.Value, 2)).ToString();
        }

        private static IEnumerable<string> GenerateAdresses(string adress)
        {
            if (!adress.Any(c => c.Equals('X')))
            {
                return new List<string> { adress };
            }
            else
            {
                var newWord1 = ReplaceFirstMatch(adress, "X", "0");
                var newWord2 = ReplaceFirstMatch(adress, "X", "1");
                return GenerateAdresses(newWord1).Concat(GenerateAdresses(newWord2));
            }
        }

        private static string ReplaceFirstMatch(string adress, string oldValue, string newValue)
        {
            int loc = adress.IndexOf(oldValue);
            if (loc < 0)
                return adress;
            return adress.Remove(loc, oldValue.Length).Insert(loc, newValue);
        }
    }
}

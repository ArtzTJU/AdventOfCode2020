using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day19
{
    public class Main
    {
        public static string PartOne()
        {
            var text = File.ReadAllText(@"Day19\input.txt");

            var rules = text.Split("\r\n\r\n")[0]
                .Split("\r\n")
                .ToDictionary(rule => int.Parse(rule.Split(": ")[0]), rule => rule.Split(": ")[1]);
            var messages = text.Split("\r\n\r\n")[1].Split("\r\n");
            var rule = ExpandRule(rules[0], rules);

            return messages.Count(message => Regex.IsMatch(message, $"^{rule}$")).ToString();
        }

        public static string ExpandRule(string rule, Dictionary<int, string> rules)
        {
            if (rule.StartsWith("\""))
                return rule.Substring(1, 1);

            return "(" +
                    rule
                        .Split(' ')
                        .Aggregate("",
                            (rule, part) => rule + (part == "|"
                                ? "|"
                                : ExpandRule(rules[int.Parse(part)], rules)))
                    + ")";
        }


        public static string PartTwo()
        {
            var text = File.ReadAllText(@"Day19\input.txt");

            var rules = text.Split("\r\n\r\n")[0]
                .Split("\r\n")
                .ToDictionary(rule => int.Parse(rule.Split(": ")[0]), rule => rule.Split(": ")[1]);
            var messages = text.Split("\r\n\r\n")[1].Split("\r\n");

            rules[8] = "42 | 42 8";
            rules[11] = "42 31 | 42 11 31";

            var rule = ExpandRule2(rules[0], rules, 0, 0, 0);

            return messages.Count(message => Regex.IsMatch(message, $"^{rule}$")).ToString();
        }

        public static string ExpandRule2(string rule, Dictionary<int, string> rules, int index, int line8HitCount, int line11HitCount)
        {
            if (index == 8 && line8HitCount == 4)
                return '(' + ExpandRule2(rules[42], rules, 42, line8HitCount, line11HitCount) + ')';
            if (index == 11 && line11HitCount == 3)
                return '(' + ExpandRule2(rules[42], rules, 42, line8HitCount, line11HitCount) + ExpandRule2(rules[31], rules, 31, line8HitCount, line11HitCount) + ')';
            if (index == 8)
                line8HitCount++;
            if (index == 11)
                line11HitCount++;

            if (rule.StartsWith("\""))
                return rule.Substring(1, 1);

            return "(" +
                    rule.Split(' ')
                        .Aggregate("",
                            (rule, part) => rule + (part == "|"
                                ? "|"
                                : ExpandRule2(rules[int.Parse(part)], rules, int.Parse(part), line8HitCount, line11HitCount)))
                    + ")";
        }
    }
}

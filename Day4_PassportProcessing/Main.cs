using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
    public static class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day4_PassportProcessing\input.txt").ToList();

            var required = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            var result = 0;
            var currentFields = new List<string>();

            foreach (var line in lines)
            {
                if (line == "")
                {
                    if (required.Except(currentFields).Count() == 0)
                        result++;

                    currentFields = new List<string>();
                    continue;
                }

                var fields = line.Split(" ")
                    .Select(f => f.Split(":")[0]);
                currentFields.AddRange(fields);
            }

            if (required.Except(currentFields).Count() == 0)
                result++;

            return result.ToString();
        }

        public static string PartTwo()

        {
            var lines = File.ReadLines(@"Day4_PassportProcessing\input.txt").ToList();

            var required = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            var result = 0;
            var currentFields = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                if (line == "")
                {
                    if (required.Except(currentFields.Keys).Count() == 0 && AreValidFields(currentFields))
                        result++;

                    currentFields = new Dictionary<string, string>();
                    continue;
                }

                var fields = line.Split(" ")
                    .ToDictionary(
                        key => key.Split(':')[0],
                        value => value.Split(':')[1]);

                foreach (var field in fields)
                    currentFields.Add(field.Key, field.Value);
            }

            if (AreValidFields(currentFields))
                result++;

            return result.ToString();
        }
        private static bool AreValidFields(Dictionary<string, string> fields)
        {
            var valid = true;

            foreach (var field in fields)
            {
                switch (field.Key)
                {
                    case "byr":
                        valid = int.TryParse(field.Value, out var byr)
                            && 1920 <= byr
                            && byr <= 2002;
                        break;
                    case "iyr":
                        valid = int.TryParse(field.Value, out var iyr)
                            && 2010 <= iyr
                            && iyr <= 2020;
                        break;
                    case "eyr":
                        valid = int.TryParse(field.Value, out var eyr)
                            && 2020 <= eyr
                            && eyr <= 2030;
                        break;
                    case "hgt":
                        if(field.Value.Contains("cm")){
                            valid = int.TryParse(field.Value.Split("cm")[0], out var hgt)
                                && 150 <= hgt
                                && hgt <= 193
                                && field.Value.Split("cm")[1] == "";
                        }
                        else if(field.Value.Contains("in")){
                            valid = int.TryParse(field.Value.Split("in")[0], out var hgt)
                                && 59 <= hgt
                                && hgt <= 76
                                && field.Value.Split("in")[1] == "";
                        }
                        else {
                            valid = false;
                        }
                        break;
                    case "hcl":
                        valid = Regex.IsMatch(field.Value, "#([0-9]|[a-f]){6}");
                        break;
                    case "ecl": 
                        valid = new []{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(field.Value);
                        break;
                    case "pid":
                        valid = Regex.IsMatch(field.Value, "^[0-9]{9}$");
                        break;
                }

                if (valid == false)
                    break;
            }

            return valid;
        }
    }
}
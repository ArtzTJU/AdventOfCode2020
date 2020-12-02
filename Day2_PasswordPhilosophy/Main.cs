using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day2
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day2_PasswordPhilosophy\partOne.txt");
            var result = 0;    
            
            foreach(var line in lines) {
                var lineSplit = line.Split(" ");
                var rangeSplit = lineSplit[0].Split("-");
                
                var min = int.Parse(rangeSplit[0]);
                var max = int.Parse(rangeSplit[1]);
                var character = lineSplit[1][0];

                var password = lineSplit[2];

                var characterCount = password.Count(x => x == character);
                if(min <= characterCount && characterCount <= max)
                    result++;
            }

            return result.ToString();
        }
        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day2_PasswordPhilosophy\partOne.txt");
            var result = 0;    
            
            foreach(var line in lines) {
                var lineSplit = line.Split(" ");
                var indexSplit = lineSplit[0].Split("-");
                
                var lowerIndex = int.Parse(indexSplit[0]) - 1;
                var upperIndex = int.Parse(indexSplit[1]) - 1;
                var character = lineSplit[1][0];

                var password = lineSplit[2];

                if(password[lowerIndex] == character ^ password[upperIndex] == character)
                    result++;;
            }

            return result.ToString();
        }
    }
}

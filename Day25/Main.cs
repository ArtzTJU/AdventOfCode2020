using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day25
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadAllLines(@"Day25\input.txt");

            var cardPublicKey = long.Parse(lines[0]);
            var doorPublicKey = long.Parse(lines[1]);

            var currentPublicKey = 1L;
            var doorLoopSize = 0;
            while (currentPublicKey != doorPublicKey)
            {
                doorLoopSize++;
                currentPublicKey = currentPublicKey * 7 % 20201227;
            }

            var currentEncryptionKey = 1L;
            for (int i = 0; i < doorLoopSize; i++)
            {
                currentEncryptionKey = currentEncryptionKey * cardPublicKey % 20201227;
            }

            return currentEncryptionKey.ToString();
        }

        public static string PartTwo()
        {
            return "No Part 2 for Day 25 ;)";
        }
    }
}

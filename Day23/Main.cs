using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day23
{
    public class Main
    {
        public static string PartOne()
        {
            var numbers = File.ReadAllText(@"Day23\input.txt")
                .Select(x => int.Parse(x.ToString()))
                .ToList();

            var index = 0;

            for (var i = 0; i < 100; i++)
            {
                var takeOut = new List<int>(3);
                var current = numbers[index];
                var destination = current;

                takeOut.Add(numbers[(index + 1) % numbers.Count]);
                takeOut.Add(numbers[(index + 2) % numbers.Count]);
                takeOut.Add(numbers[(index + 3) % numbers.Count]);

                for (var j = 0; j < 3; j++)
                    numbers.Remove(takeOut[j]);

                do
                {
                    destination--;
                    if (destination == 0)
                        destination = 9;
                }
                while (takeOut.Contains(destination));

                numbers.InsertRange(numbers.IndexOf(destination) + 1, takeOut);
                index = (numbers.IndexOf(current) + 1) % numbers.Count;
            }

            var split = string.Join("", numbers)
                .Split('1');

            return (split[1] + split[0]).ToString();
        }

        public static string PartTwo()
        {
            var numbers = File.ReadAllText(@"Day23\input.txt")
                .Select(x => int.Parse(x.ToString()))
                .ToList();

            var cups = new LinkedList<int>();
            var cupsDict = new Dictionary<int, LinkedListNode<int>>();  

            var prev = cups.AddFirst(numbers.First());
            cupsDict[prev.Value] = prev;

            foreach (var number in numbers.Skip(1)){
                prev = cups.AddAfter(prev, number);
                cupsDict[number] = prev;
            }

            for (var i = 10; i <= 1_000_000; i++)
            {
                prev = cups.AddAfter(prev, i);
                cupsDict[i] = prev;
            }

            var current = cups.First;

            for (var i = 0; i < 10_000_000; i++)
            {
                var destination = current.Value;

                var take1 = current.Next ?? cups.First;
                var take2 = take1.Next ?? cups.First;
                var take3 = take2.Next ?? cups.First;

                do
                {
                    destination--;
                    if (destination == 0)
                        destination = 1_000_000;
                }
                while (destination == take1.Value || destination == take2.Value || destination == take3.Value);

                cups.Remove(take1);
                cups.Remove(take2);
                cups.Remove(take3);

                var destinationNode = cupsDict[destination];

                cups.AddAfter(destinationNode, take1);
                cups.AddAfter(take1, take2);
                cups.AddAfter(take2, take3);

                current = current.Next ?? cups.First;
            }

            var nodeOne = cups.Find(1);

            return ((long)nodeOne.Next.Value * (long)nodeOne.Next.Next.Value).ToString();
        }

    }
}

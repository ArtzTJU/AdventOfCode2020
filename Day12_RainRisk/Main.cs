using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day12
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day12_RainRisk\input.txt");

            var x = 0;
            var y = 0;
            var direction = 90;

            foreach (var line in lines)
            {
                var instruction = line[0];
                var amount = int.Parse(line.Substring(1));

                switch (instruction)
                {
                    case 'F':
                        switch (direction)
                        {
                            case 0: y += amount; break;
                            case 90: x += amount; break;
                            case 180: y -= amount; break;
                            case 270: x -= amount; break;
                        }
                        break;
                    case 'N': y += amount; break;
                    case 'S': y -= amount; break;
                    case 'E': x += amount; break;
                    case 'W': x -= amount; break;
                    case 'L': direction = (direction - amount + 360) % 360; break;
                    case 'R': direction = (direction + amount) % 360; break;
                }
            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day12_RainRisk\input.txt");

            var x = 0;
            var y = 0;
            var xW = 10;
            var yW = 1;

            foreach (var line in lines)
            {
                var instruction = line[0];
                var amount = int.Parse(line.Substring(1));
                var tmp = 0;

                switch (instruction)
                {
                    case 'F':
                        x += amount * xW;
                        y += amount * yW;
                        break;
                    case 'N': yW += amount; break;
                    case 'S': yW -= amount; break;
                    case 'E': xW += amount; break;
                    case 'W': xW -= amount; break;
                    case 'L':
                        switch (amount)
                        {
                            case 90:
                                tmp = yW;
                                yW = xW;
                                xW = -tmp;
                                break;
                            case 180:
                                yW *= -1;
                                xW *= -1;
                                break;
                            case 270:
                                tmp = yW;
                                yW = -xW;
                                xW = tmp;
                                break;
                        }
                        break;
                    case 'R':
                        switch (amount)
                        {
                            case 90:
                                tmp = yW;
                                yW = -xW;
                                xW = tmp;
                                break;
                            case 180:
                                yW *= -1;
                                xW *= -1;
                                break;
                            case 270:
                                tmp = yW;
                                yW = xW;
                                xW = -tmp;
                                break;
                        }
                        break;
                }
            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }
    }
}

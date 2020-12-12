using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day12
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day12\input.txt");

            var x = 0;
            var y = 0;

            var direction = 90;

            foreach (var line in lines)
            {
                var instruction = line[0];
                var amount = int.Parse(line.Substring(1));

                if (instruction == 'F')
                {
                    if (direction == 90)
                    {
                        x += amount;
                    }
                    else if (direction == 0)
                    {
                        y += amount;
                    }
                    else if (direction == 270)
                    {
                        x -= amount;
                    }
                    else if (direction == 180)
                    {
                        y -= amount;
                    }
                }
                else if (instruction == 'N')
                {
                    y += amount;
                }
                else if (instruction == 'S')
                {
                    y -= amount;
                }
                else if (instruction == 'E')
                {
                    x += amount;
                }
                else if (instruction == 'W')
                {
                    x -= amount;
                }

                else if (instruction == 'L')
                {
                    direction = (direction - amount) % 360;
                    if (direction < 0)
                    {
                        direction = direction + 360;
                    }
                }
                else if (instruction == 'R')
                {
                    direction = Math.Abs((direction + amount) % 360);

                }
            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day12\input.txt");

            var x = 0;
            var y = 0;
            var xW = 10;
            var yW = 1;

            foreach (var line in lines)
            {
                var instruction = line[0];
                var amount = int.Parse(line.Substring(1));

                if (instruction == 'F')
                {
                    x += amount * xW;
                    y += amount * yW;
                }
                else if (instruction == 'N')
                {
                    yW += amount;
                }
                else if (instruction == 'S')
                {
                    yW -= amount;
                }
                else if (instruction == 'E')
                {
                    xW += amount;
                }
                else if (instruction == 'W')
                {
                    xW -= amount;
                }

                else if (instruction == 'L')
                {
                    if (amount == 90)
                    {
                        var tmp = yW;
                        yW = xW;
                        xW = -tmp;
                    }
                    else if (amount == 180)
                    {
                        yW *= -1;
                        xW *= -1;
                    }
                    else if (amount == 270)
                    {
                        var tmp = yW;
                        yW = -xW;
                        xW = tmp;
                    }
                }
                else if (instruction == 'R')
                {
                    if (amount == 90)
                    {
                        var tmp = yW;
                        yW = -xW;
                        xW = tmp;

                    }
                    else if (amount == 180)
                    {
                        yW *= -1;
                        xW *= -1;
                    }
                    else if (amount == 270)
                    {
                        var tmp = yW;
                        yW = xW;
                        xW = -tmp;
                    }
                }
            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }
    }
}

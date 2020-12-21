using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day18
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadLines(@"Day18_OperationOrder\input.txt").ToList();
            var result = 0L;

            foreach(var line in lines)
                result += Evaluate(line, out var _);

            return result.ToString();
        }

        public static long Evaluate(string input, out int jump)
        {
            jump = 0;
            var result = 0L;
            var lastOperator = '+';

            for(var i = 0; i < input.Length; i++)
            {
                jump++;
                var character = input[i];
                switch(character)
                {
                    case '(':
                        if(lastOperator == '+')
                        {
                            result += Evaluate(input.Substring(i + 1), out var increment);
                            i += increment;
                            jump += increment;
                        }
                        else
                        {
                            result *= Evaluate(input.Substring(i + 1), out var increment);
                            i += increment;
                            jump += increment;
                        }
                        break;
                    case ')':
                        return result;
                    case '+':
                        lastOperator = '+';
                        break;
                    case '*':
                        lastOperator = '*';
                        break;
                    case ' ':
                        continue; 
                    default:
                        if(lastOperator == '+')
                            result += long.Parse(character.ToString());
                        else 
                            result *= long.Parse(character.ToString());
                        break;
                }
            }

            return result;
        }


        public static string PartTwo()
        {
            var lines = File.ReadLines(@"Day18_OperationOrder\input.txt").ToList();
            var result = 0L;

            foreach(var line in lines)
                result += Evaluate2(line.Replace(" ", ""), 0, out var _);

            return result.ToString();
        }

        public static long Evaluate2(string input, int level, out int jump)
        {
            jump = 0;
            var result = 0L;
            var lastOperator = '+';

            for(var i = 0; i < input.Length; i++)
            {
                jump++;
                var character = input[i];
                switch(character)
                {
                    case '(':
                        if(lastOperator == '+')
                        {
                            result += Evaluate2(input.Substring(i + 1), level, out var increment);
                            i += increment;
                            jump += increment;
                            
                        }
                        else
                        {
                            result *= Evaluate2(input.Substring(i + 1), level, out var increment);
                            i += increment;
                            jump += increment;
                        }
                        break;
                    case ')':
                        return result;
                    case '+':
                        lastOperator = '+';
                        break;
                    case '*':
                        lastOperator = '*';
                        result *= Evaluate2(input.Substring(i + 1), level++, out var increment2);
                        i += increment2;
                        jump += increment2;

                        while(level != 0)
                            return result;
                        break;
                    case ' ':
                        continue; 
                    default:
                        if(lastOperator == '+')
                            result += long.Parse(character.ToString());
                        else 
                            result *= long.Parse(character.ToString());
                        break;
                }
            }

            return result;
        }
    }
}

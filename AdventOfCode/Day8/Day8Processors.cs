using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day8
{
    public class Day8Part1Processor : BaseProcessor<string[], int>
    {
        public Day8Part1Processor(IInputReader inputReader, string inputResourceName, Day8InputFormat day8InputFormat)
            : base(inputReader, inputResourceName, day8InputFormat)
        {
        }

        protected override int ComputeResultLogic(string[] input)
        {
            var accumulator = 0;
            Day8Helper.ExecuteInstructions(input, ref accumulator);

            return accumulator;
        }
    }

    public class Day8Part2Processor : BaseProcessor<string[], int>
    {
        public Day8Part2Processor(IInputReader inputReader, string inputResourceName, Day8InputFormat day8InputFormat)
            : base(inputReader, inputResourceName, day8InputFormat)
        {
        }

        protected override int ComputeResultLogic(string[] input)
        {
            var globalAcc = 0;

            for (var index = 0; index < input.Length; index++)
            {
                var previousValue = input[index];
                if (input[index].Contains("jmp"))
                    input[index] = input[index].Replace("jmp", "nop");
                else if (input[index].Contains("nop"))
                    input[index] = input[index].Replace("nop", "jmp");

                int accumulator = 0;
                var finished = Day8Helper.ExecuteInstructions(input, ref accumulator);

                if (finished)
                    globalAcc = accumulator;

                input[index] = previousValue;
            }

            return globalAcc;
        }
    }

    public static class Day8Helper
    {
        public static bool ExecuteInstructions(string[] lines, ref int accumulator)
        {
            var instructionRegex = new Regex(@"(nop|acc|jmp){1}\s((-|\+){1}\d+)");
            var instructionsVisisted = new Dictionary<int, bool>();
            int index = 0;

            while (true)
            {
                if (instructionsVisisted.ContainsKey(index))
                    return false;

                if (index >= lines.Length)
                    return true;

                var match = instructionRegex.Match(lines[index]);
                if (match.Groups.Count != 4)
                    throw new Exception();

                var instruction = match.Groups[1].Value;
                var instructionValue = int.Parse(match.Groups[2].Value);
                instructionsVisisted.Add(index, true);

                if (instruction == "acc")
                {
                    accumulator += instructionValue;
                    index++;
                }
                else if (instruction == "jmp")
                {
                    index += instructionValue;
                }
                else
                    index++;
            }
        }
    }
}

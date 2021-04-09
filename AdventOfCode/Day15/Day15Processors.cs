using AdventOfCode.Base;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day15
{
    public class Day15Part1Processor : BaseProcessor<int[], int>
    {
        public Day15Part1Processor(IInputReader inputReader, string inputResourceName, Day15InputTransform day15InputTransform)
            : base(inputReader, inputResourceName, day15InputTransform)
        {
        }

        protected override int ComputeResultLogic(int[] input)
        {
            return Day15Helper.MemoryGame(2020, input);
        }
    }

    public class Day15Part2Processor : BaseProcessor<int[], int>
    {
        public Day15Part2Processor(IInputReader inputReader, string inputResourceName, Day15InputTransform day15InputTransform)
            : base(inputReader, inputResourceName, day15InputTransform)
        {
        }

        protected override int ComputeResultLogic(int[] input)
        {
            return Day15Helper.MemoryGame(30000000, input);
        }
    }

    public static class Day15Helper
    {
        public static int MemoryGame(int count, params int[] inputNumbers)
        {
            int position = 0;
            int lastNumber = inputNumbers.Last();
            var positions = new Dictionary<int, int>();

            foreach (var number in inputNumbers)
                positions[number] = ++position;

            while (position < count)
            {
                int lastPosition = positions.ContainsKey(lastNumber) ? positions[lastNumber] : 0;
                int nextNumber = lastPosition != 0 ? position - lastPosition : 0;
                positions[lastNumber] = position++;
                lastNumber = nextNumber;
            }

            return lastNumber;
        }
    }
}

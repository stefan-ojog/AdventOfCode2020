using AdventOfCode.Base;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day5
{
    public class Day5Part1Processor : BaseProcessor<IEnumerable<Day5InputModel>, int>
    {
        public Day5Part1Processor(IInputReader inputReader, string inputResourceName, Day5InputTransform day5InputTransform)
            : base(inputReader, inputResourceName, day5InputTransform)
        {
        }

        protected override int ComputeResultLogic(IEnumerable<Day5InputModel> input)
        {
            var seatIds = Day5Helper.ComputeSeatIds(input);
            return seatIds.Max();
        }
    }

    public class Day5Part2Processor : BaseProcessor<IEnumerable<Day5InputModel>, int>
    {
        public Day5Part2Processor(IInputReader inputReader, string inputResourceName, Day5InputTransform day5InputTransform)
            : base(inputReader, inputResourceName, day5InputTransform)
        {
        }

        protected override int ComputeResultLogic(IEnumerable<Day5InputModel> input)
        {
            var seatIds = Day5Helper.ComputeSeatIds(input).ToList();
            seatIds.Sort();

            var specialSeatId = 0;

            for (var index = 1; index < seatIds.Count - 1; index += 2)
            {
                if (seatIds[index] != seatIds[index - 1] + 1 ||
                    seatIds[index] != seatIds[index + 1] - 1)
                    specialSeatId = seatIds[index];
            }

            return specialSeatId + 1;
        }
    }

    public static class Day5Helper
    {
        public static IEnumerable<int> ComputeSeatIds(IEnumerable<Day5InputModel> input)
        {
            var seatIds = new List<int>();

            foreach (var model in input)
            {
                var rowsResult = Traverse(model.RowDescription, 0, 0, 127, 'B', 'F');
                var columnResult = Traverse(model.ColumnDescription, 0, 0, 7, 'R', 'L');
                var seatId = rowsResult * 8 + columnResult;
                seatIds.Add(seatId);
            }

            return seatIds;
        }

        public static int Traverse(
                string description,
                int index,
                int min,
                int max,
                char upperHalfCharacter,
                char lowerHalfCharacter)
        {
            if (index >= description.Length)
                return min;

            var currentIdentifier = description[index];
            if (currentIdentifier == lowerHalfCharacter)
                return Traverse(description, index + 1, min, (max - min) / 2 + min, upperHalfCharacter, lowerHalfCharacter);
            else if (currentIdentifier == upperHalfCharacter)
                return Traverse(description, index + 1, (max - min) / 2 + min + 1, max, upperHalfCharacter, lowerHalfCharacter);

            return -1;
        }
    }
}

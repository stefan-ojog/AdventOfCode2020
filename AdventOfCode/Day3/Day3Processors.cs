using AdventOfCode.Base;

namespace AdventOfCode.Day3
{
    public class Day3Part1Processor : BaseProcessor<Day3InputModel, long>
    {
        public Day3Part1Processor(IInputReader inputReader, string inputResourceName, Day3InputTransform day3InputTransform)
            : base(inputReader, inputResourceName, day3InputTransform)
        {
        }

        protected override long ComputeResultLogic(Day3InputModel input)
        {
            return Day3Helper.GetNumberOfTreesBySlope(input, 1, 3);
        }
    }

    public class Day3Part2Processor : BaseProcessor<Day3InputModel, long>
    {
        public Day3Part2Processor(IInputReader inputReader, string inputResourceName, Day3InputTransform day3InputTransform)
            : base(inputReader, inputResourceName, day3InputTransform)
        {
        }

        protected override long ComputeResultLogic(Day3InputModel input)
        {
            var slope1 = Day3Helper.GetNumberOfTreesBySlope(input, 1, 1);
            var slope2 = Day3Helper.GetNumberOfTreesBySlope(input, 1, 3);
            var slope3 = Day3Helper.GetNumberOfTreesBySlope(input, 1, 5);
            var slope4 = Day3Helper.GetNumberOfTreesBySlope(input, 1, 7);
            var slope5 = Day3Helper.GetNumberOfTreesBySlope(input, 2, 1);

            long treesMultiplied = (long)slope1 * slope2 * slope3 * slope4 * slope5;
            return treesMultiplied;
        }
    }

    public static class Day3Helper
    {
        public static int GetNumberOfTreesBySlope(Day3InputModel inputModel, int lineIncrement, int columnIncrement)
        {
            var currentColumnPosition = 0;
            int treesCount = 0;

            for (var lineIndex = lineIncrement; lineIndex < inputModel.LinesCount; lineIndex += lineIncrement)
            {
                currentColumnPosition = GetColumnNextPosition(currentColumnPosition, columnIncrement, inputModel.ColumnsCount);

                if (inputModel.Map[lineIndex, currentColumnPosition] == '#')
                    treesCount++;
            }

            return treesCount;
        }

        private static int GetColumnNextPosition(int currentPosition, int columnIncrement, int columnsCount)
        {
            var nextPosition = currentPosition + columnIncrement;
            if (nextPosition >= columnsCount)
                nextPosition %= columnsCount;

            return nextPosition;
        }
    }
}

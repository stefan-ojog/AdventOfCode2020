using AdventOfCode.Base;
using System.Linq;

namespace AdventOfCode.Day11
{
    public class Day11InputTransform : IInputTransform<Day11Model>
    {
        public Day11Model Create(string[] inputLines)
        {
            var rows = inputLines.Length;
            var columns = inputLines.First().Length;
            var layout = new char[rows, columns];
            for (var index = 0; index < inputLines.Length; index++)
            {
                var lineChars = inputLines[index].ToCharArray();

                for (var charIndex = 0; charIndex < lineChars.Length; charIndex++)
                    layout[index, charIndex] = lineChars[charIndex];
            }

            return new Day11Model(layout, rows, columns);
        }
    }

    public class Day11Model
    {
        public char[,] SeatLaoyut { get; }

        public int Rows { get; }

        public int Columns { get; }

        public Day11Model(char[,] layout, int rows, int columns)
        {
            SeatLaoyut = layout;
            Rows = rows;
            Columns = columns;
        }
    }
}

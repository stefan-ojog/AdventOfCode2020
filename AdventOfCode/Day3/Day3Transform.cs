using AdventOfCode.Base;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day3
{
    public class Day3InputTransform : IInputTransform<Day3InputModel>
    {
        public Day3InputModel Create(string[] inputLines)
        {
            var result = new List<char[]>();
            result.AddRange(inputLines.Select(line => line.ToCharArray()));

            var linesCount = result.Count;
            var columnsCount = result.First().Length;
            var map = new char[linesCount, columnsCount];

            for (var lineIndex = 0; lineIndex < linesCount; lineIndex++)
                for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++)
                {
                    var line = result[lineIndex];
                    map[lineIndex, columnIndex] = line[columnIndex];
                }

            return new Day3InputModel(linesCount, columnsCount, map);
        }
    }
}

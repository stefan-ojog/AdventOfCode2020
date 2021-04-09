using AdventOfCode.Base;
using System;
using System.Linq;

namespace AdventOfCode.Day1
{
    public class Day1InputTransform : IInputTransform<long[]>
    {
        public long[] Create(string[] inputLines)
        {
            return inputLines.Select(i => Convert.ToInt64(i)).ToArray();
        }
    }
}

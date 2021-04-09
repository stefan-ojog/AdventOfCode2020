using AdventOfCode.Base;
using System.Linq;

namespace AdventOfCode.Day13
{
    public class Day13InputTransform : IInputTransform<Day13Model>
    {
        public Day13Model Create(string[] inputLines)
        {
            var earliestStartTime = int.Parse(inputLines[0]);
            var busesAsString = inputLines[1].Split(",");
            var busIds = busesAsString.Where(b => b != "x").Select(b => int.Parse(b)).ToArray();

            return new Day13Model(earliestStartTime, busIds);
        }
    }

    public class Day13Part2InputTransform : IInputTransform<Day13Part2Model>
    {
        public Day13Part2Model Create(string[] inputLines)
        {
            var busesAsString = inputLines[1].Split(",");
            var busIds = busesAsString.Select(b =>
            {
                if (b == "x")
                    return -1;

                return long.Parse(b);
            }).ToArray();

            return new Day13Part2Model(busIds);
        }
    }

    public class Day13Model
    {
        public int EarliestStartTime { get; }

        public int[] BusIds { get; }

        public Day13Model(int earliestStartTime, int[] busIds)
        {
            EarliestStartTime = earliestStartTime;
            BusIds = busIds;
        }
    }

    public class Day13Part2Model
    {
        public long[] BusIds { get; }

        public Day13Part2Model(long[] busIds)
        {
            BusIds = busIds;
        }
    }
}

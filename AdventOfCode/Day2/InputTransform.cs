using AdventOfCode.Base;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day2
{
    public class Day2Part1InputTransform : IInputTransform<IEnumerable<Day2InputModel>>
    {
        public IEnumerable<Day2InputModel> Create(string[] input)
        {
            return input.Select(inputLine =>
            {
                var dashIndex = inputLine.IndexOf("-");
                var startPasswordIndex = inputLine.IndexOf(":");

                int minNumber = int.Parse(inputLine.Substring(0, dashIndex));
                int maxNumber = int.Parse(inputLine.Substring(dashIndex + 1, startPasswordIndex - dashIndex - 2));
                var character = inputLine.Substring(startPasswordIndex - 1, 1).ToCharArray().Single();
                var password = inputLine.Substring(startPasswordIndex + 1);

                return new Day2InputModel(minNumber, maxNumber, character, password);
            });
        }
    }

    public class Day2Part2InputTransform : IInputTransform<IEnumerable<Day2Part2InputModel>>
    {
        public IEnumerable<Day2Part2InputModel> Create(string[] input)
        {
            return input.Select(inputLine =>
            {
                var dashIndex = inputLine.IndexOf("-");
                var startPasswordIndex = inputLine.IndexOf(":");

                int firstPosition = int.Parse(inputLine.Substring(0, dashIndex));
                int secondPosition = int.Parse(inputLine.Substring(dashIndex + 1, startPasswordIndex - dashIndex - 2));
                var character = inputLine.Substring(startPasswordIndex - 1, 1).ToCharArray().Single();
                var password = inputLine.Substring(startPasswordIndex + 1);

                return new Day2Part2InputModel(firstPosition, secondPosition, character, password);
            });
        }
    }
}

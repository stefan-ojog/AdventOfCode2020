using AdventOfCode.Base;
using System.Linq;

namespace AdventOfCode.Day9
{
	public class Day9InputFormat : IInputTransform<Day9InputModel>
	{
		public Day9InputModel Create(string[] inputLines)
		{
			var preamble = inputLines.Take(25).Select(l => long.Parse(l));
			var restOfNumbers = inputLines.TakeLast(inputLines.Length - 25).Select(l => long.Parse(l));

			return new Day9InputModel(preamble.ToArray(), restOfNumbers.ToArray(), 25);
		}
	}
}

using AdventOfCode.Base;
using System.Linq;

namespace AdventOfCode.Day15
{
	public class Day15InputTransform : IInputTransform<int[]>
	{
		public int[] Create(string[] inputLines)
		{
			var singleLine = inputLines[0];
			return singleLine.Split(",").Select(l => int.Parse(l)).ToArray();
		}
	}
}

using AdventOfCode.Base;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day2
{
	public class Day2Part1Processor : BaseProcessor<IEnumerable<Day2InputModel>, int>
	{
		public Day2Part1Processor(IInputReader inputReader, string inputResourceName, Day2Part1InputTransform inputTransform)
			: base(inputReader, inputResourceName, inputTransform)
		{
		}

		protected override int ComputeResultLogic(IEnumerable<Day2InputModel> input)
		{
			return input.Count(i => i.MinOccurencesNumber <= i.Password.Count(c => c == i.Character) &&
			 i.Password.Count(c => c == i.Character) <= i.MaxOccurencesNumber);
		}
	}

	public class Day2Part2Processor : BaseProcessor<IEnumerable<Day2Part2InputModel>, int>
	{
		public Day2Part2Processor(IInputReader inputReader, string inputResourceName, Day2Part2InputTransform inputTransform)
			: base(inputReader, inputResourceName, inputTransform)
		{
		}

		protected override int ComputeResultLogic(IEnumerable<Day2Part2InputModel> input)
		{
			return input.Count(i =>
			(i.Password[i.FirstPosition] == i.Character && i.Password[i.SecondPosition] != i.Character) ||
			(i.Password[i.FirstPosition] != i.Character && i.Password[i.SecondPosition] == i.Character));
		}
	}
}

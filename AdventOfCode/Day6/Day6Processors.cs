using AdventOfCode.Base;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day6
{
	public class Day6Part1Processor : BaseProcessor<IEnumerable<Day6InputModel>, int>
	{
		public Day6Part1Processor(IInputReader inputReader, string inputResourceName, Day6Part1InputTransform day6Part1InputTransform)
			: base(inputReader, inputResourceName, day6Part1InputTransform)
		{
		}

		protected override int ComputeResultLogic(IEnumerable<Day6InputModel> input)
		{
			var sumOfYesQuestionsPerGroup = input.Sum(m =>
						m.TotalYesQuestionsPerGroup.Count(kv => kv.Value));
			return sumOfYesQuestionsPerGroup;
		}
	}

	public class Day6Part2Processor : BaseProcessor<IEnumerable<Day6InputModel>, int>
	{
		public Day6Part2Processor(IInputReader inputReader, string inputResourceName, Day6Part2InputTransform day6Part1InputTransform)
		: base(inputReader, inputResourceName, day6Part1InputTransform)
		{
		}

		protected override int ComputeResultLogic(IEnumerable<Day6InputModel> input)
		{
			var sumOfYesQuestionsPerGroup = input.Sum(m =>
						m.TotalYesQuestionsPerGroup.Count(kv => kv.Value));
			return sumOfYesQuestionsPerGroup;
		}
	}
}

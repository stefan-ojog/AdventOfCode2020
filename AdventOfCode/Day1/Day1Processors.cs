using AdventOfCode.Base;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day1
{
	public class Day1Part1Processor : BaseProcessor<long[], long>
	{
		public Day1Part1Processor(IInputReader inputReader, string inputResourceName, Day1InputTransform inputTransform)
			: base(inputReader, inputResourceName, inputTransform)
		{
		}

		protected override long ComputeResultLogic(long[] input)
		{
			var result = new List<long>();

			for (var index = 0; index < input.Count(); index++)
				for (var index2 = index + 1; index2 < input.Count(); index2++)
					if (input[index] + input[index2] == 2020)
						result.Add(input[index] * input[index2]);

			return result.First();
		}
	}

	public class Day1Part2Processor : BaseProcessor<long[], long>
	{
		public Day1Part2Processor(IInputReader inputReader, string inputResourceName, Day1InputTransform inputTransform)
			: base(inputReader, inputResourceName, inputTransform)
		{
		}

		protected override long ComputeResultLogic(long[] input)
		{
			var result = new List<long>();

			for (var index = 0; index < input.Count(); index++)
				for (var index2 = index + 1; index2 < input.Count(); index2++)
					for (var index3 = index2 + 1; index3 < input.Count(); index3++)
						if (input[index] + input[index2] + input[index3] == 2020)
							result.Add(input[index] * input[index2] * input[index3]);

			return result.First();
		}
	}
}

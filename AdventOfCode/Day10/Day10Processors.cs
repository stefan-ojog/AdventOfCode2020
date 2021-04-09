using AdventOfCode.Base;
using AdventOfCode.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day10
{
	public class Day10Part1Processor : BaseProcessor<Graph<long>, long>
	{
		public Day10Part1Processor(IInputReader inputReader, string inputResourceName, Day10InputTransform day10InputTransform)
			: base(inputReader, inputResourceName, day10InputTransform)
		{
		}

		protected override long ComputeResultLogic(Graph<long> input)
		{
			var differencesResult = Day10Helper.ComputeDifferences(input);
			return differencesResult;
		}
	}
	public class Day10Part2Processor : BaseProcessor<Graph<long>, long>
	{
		public Day10Part2Processor(IInputReader inputReader, string inputResourceName, Day10InputTransform day10InputTransform)
			: base(inputReader, inputResourceName, day10InputTransform)
		{
		}

		protected override long ComputeResultLogic(Graph<long> input)
		{
			var maxValue = input.AllNodes.Values.Select(n => n.Value).Max();
			var lastNode = input.AllNodes.Single(n => n.Value.Value == maxValue);
			var totalPaths = new Dictionary<long, long>();
			var nodeValues = input.AllNodes.Select(n => n.Value.Value).ToList();
			nodeValues.Sort();
			nodeValues.Insert(0, 0);
			nodeValues.Add(nodeValues.Last() + 3);

			var countPaths = Day10Helper.ComputeTotalPaths(nodeValues.ToArray(),
				totalPaths);

			return countPaths;
		}
	}

	public static class Day10Helper
	{
		public static int ComputeDifferences(Graph<long> graph)
		{
			var allNodeValues = graph.AllNodes.Select(n => n.Value.Value).ToArray();
			Array.Sort(allNodeValues);
			var differences = new Dictionary<long, int>();
			differences.Add(allNodeValues[0], 1);
			differences.Add(3, 1);

			for (var index = 0; index < allNodeValues.Length - 1; index++)
			{
				var difference = allNodeValues[index + 1] - allNodeValues[index];
				if (differences.ContainsKey(difference))
					differences[difference]++;
				else
					differences.Add(difference, 1);
			}

			return differences[1] * differences[3];
		}

		public static long ComputeTotalPaths(long[] inputs, IDictionary<long, long> totalPaths)
		{
			if (totalPaths.ContainsKey(inputs.Length))
			{
				return totalPaths[inputs.Length];
			}

			if (inputs.Length == 1)
			{
				return 1;
			}

			long total = 0;
			for (int i = 1; i < inputs.Length; i++)
			{
				if (inputs[i] - inputs[0] <= 3)
				{
					total += ComputeTotalPaths(inputs[i..], totalPaths);
				}
				else
				{
					break;
				}
			}

			totalPaths.Add(inputs.Length, total);

			return total;
		}

	}
}

using AdventOfCode.Base;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day14
{
	public class Day14InputTransform : IInputTransform<Day14Model>
	{
		public Day14Model Create(string[] inputLines)
		{
			var maskRegex = new Regex(@"mask = ([01X]{36})");
			var memoryAllocationRegex = new Regex(@"mem\[(\d+)\] = (\d+)");
			var currentMask = string.Empty;
			var allInstructions = new List<InstructionWithBitmask>();

			foreach (var line in inputLines)
			{
				var maskMatch = maskRegex.Match(line);
				if (maskMatch.Success)
					currentMask = maskMatch.Groups[1].Value;
				else
				{
					var memoryAllocationMatch = memoryAllocationRegex.Match(line);
					var memIndex = int.Parse(memoryAllocationMatch.Groups[1].Value);
					var memValue = int.Parse(memoryAllocationMatch.Groups[2].Value);

					var instructionWithBitmask = new InstructionWithBitmask(currentMask, memIndex, memValue);
					allInstructions.Add(instructionWithBitmask);
				}
			}

			return new Day14Model(allInstructions.ToArray());
		}
	}

	public class Day14Model
	{
		public InstructionWithBitmask[] Instructions { get; }

		public Day14Model(InstructionWithBitmask[] instructions)
		{
			Instructions = instructions;
		}
	}

	public class InstructionWithBitmask
	{
		public string CurrentMask { get; }

		public long MemoryIndex { get; }

		public long MemoryValue { get; }

		public InstructionWithBitmask(string currentMask, long memoryIndex, long memoryValue)
		{
			CurrentMask = currentMask;
			MemoryIndex = memoryIndex;
			MemoryValue = memoryValue;
		}
	}
}

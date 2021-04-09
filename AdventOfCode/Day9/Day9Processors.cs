using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day9
{
	public class Day9Part1Processor : BaseProcessor<Day9InputModel, long>
	{
		public Day9Part1Processor(IInputReader inputReader, string inputResourceName, Day9InputFormat day9InputFormat)
			: base(inputReader, inputResourceName, day9InputFormat)
		{
		}

		protected override long ComputeResultLogic(Day9InputModel input)
		{
			var result = Day9Helper.GetFirstNonValidNumber(input, 25);
			return result;
		}
	}

	public class Day9Part2Processor : BaseProcessor<Day9InputModel, long>
	{
		public Day9Part2Processor(IInputReader inputReader, string inputResourceName, Day9InputFormat day9InputFormat)
			: base(inputReader, inputResourceName, day9InputFormat)
		{
		}

		protected override long ComputeResultLogic(Day9InputModel input)
		{
			var result = Day9Helper.GetFirstNonValidNumber(input, 25);
			var allNumbers = new List<long>();
			allNumbers.AddRange(input.Preamble);
			allNumbers.AddRange(input.RestOfInput);

			var sum = Day9Helper.GetSumOfMinAndMaxOfContigousSum(result, allNumbers.ToArray());

			return sum;
		}
	}

	public static class Day9Helper
	{
		public static long GetFirstNonValidNumber(Day9InputModel model, int preambleCount)
		{
			var currentPreamble = model.Preamble;

			foreach (var inputNumber in model.RestOfInput)
			{
				bool isSumOf = false;

				for (var index = 0; index < currentPreamble.Length; index++)
					for (var index2 = 0; index2 < currentPreamble.Length; index2++)
						if (index2 != index)
							isSumOf = isSumOf || IsNumberSumOf(inputNumber, currentPreamble[index], currentPreamble[index2]);

				if (!isSumOf)
					return inputNumber;

				var newPreamble = currentPreamble.TakeLast(preambleCount - 1).ToList();
				newPreamble.Add(inputNumber);
				currentPreamble = newPreamble.ToArray();
			}

			return -1;
		}

		public static long GetSumOfMinAndMaxOfContigousSum(long sumToVerify, long[] allNumbers)
		{
			for (var index = 0; index < allNumbers.Length - 1; index++)
				for (var index2 = index; index2 < allNumbers.Length; index2++)
				{
					var subSequence = allNumbers.SubArray(index, index2 - index + 1);
					var sumOfSequence = subSequence.Sum();
					if (sumOfSequence == sumToVerify)
						return subSequence.Min() + subSequence.Max();
				}

			return -1;
		}

		public static T[] SubArray<T>(this T[] array, int offset, int length)
		{
			T[] result = new T[length];
			Array.Copy(array, offset, result, 0, length);
			return result;
		}

		private static bool IsNumberSumOf(long numberToVerify, long first, long second)
		{
			return (first + second) == numberToVerify;
		}
	}
}

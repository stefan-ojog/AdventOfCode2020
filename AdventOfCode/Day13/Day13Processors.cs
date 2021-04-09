using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day13
{
	public class Day13Part1Processor : BaseProcessor<Day13Model, int>
	{
		public Day13Part1Processor(IInputReader inputReader, string inputResourceName, Day13InputTransform day13InputTransform)
			: base(inputReader, inputResourceName, day13InputTransform)
		{
		}

		protected override int ComputeResultLogic(Day13Model input)
		{
			var remainingMinutesForEachBus = new Dictionary<int, int>();

			foreach (var bus in input.BusIds)
			{
				remainingMinutesForEachBus.Add(bus, bus - input.EarliestStartTime % bus);
			}

			var earliest = remainingMinutesForEachBus.Values.Min();
			var busId = remainingMinutesForEachBus.First(kv => kv.Value == earliest).Key;
			var result = busId * earliest;

			return result;
		}
	}

	public class Day13Part2Processor : BaseProcessor<Day13Part2Model, long>
	{
		public Day13Part2Processor(IInputReader inputReader, string inputResourceName, Day13Part2InputTransform day13Part2InputTransform)
			: base(inputReader, inputResourceName, day13Part2InputTransform)
		{
		}

		protected override long ComputeResultLogic(Day13Part2Model input)
		{
			var part2Result = Day13Helper.ComputeResultPart2(input.BusIds);
			return part2Result;
		}
	}

	public static class Day13Helper
	{
		public static long ComputeResultPart2(long[] buses)
		{
			var increment = buses[0];
			var result = buses[0];
			var currentBusIndex = 1;
			var currentValue = buses[0];

			while (currentBusIndex < buses.Length)
			{
				if (buses[currentBusIndex] == -1)
				{
					currentBusIndex++;
				}
				else
				{
					if (VerifyNextBus(buses, currentValue, currentBusIndex))
					{
						result = currentValue;
						increment = LeastCommonMultiple(increment, buses[currentBusIndex]);
						currentBusIndex++;
					}

					currentValue += increment;
				}

				if (currentBusIndex == buses.Length)
					return result;
			}

			return -1;
		}

		private static bool VerifyNextBus(long[] buses, long currentValue, int currentIndex)
		{
			return (currentValue + currentIndex) % buses[currentIndex] == 0;
		}

		private static long GreatestCommonDivisor(long a, long b)
		{
			a = Math.Abs(a);
			b = Math.Abs(b);

			for (; ; )
			{
				long remainder = a % b;
				if (remainder == 0) return b;
				a = b;
				b = remainder;
			};
		}

		private static long LeastCommonMultiple(long a, long b)
		{
			return a * b / GreatestCommonDivisor(a, b);
		}
	}
}

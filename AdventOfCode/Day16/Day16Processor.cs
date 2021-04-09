using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day16
{
	public class Day16Part1Processor : BaseProcessor<Day16Model, int>
	{
		public Day16Part1Processor(IInputReader inputReader, string inputResourceName, Day16InputTransform day16InputTransform)
			: base(inputReader, inputResourceName, day16InputTransform)
		{
		}

		protected override int ComputeResultLogic(Day16Model input)
		{
			var invalidTicketsAndSumOfInvalidValues = Day16Helper.GetInvalidTicketsAndSumOfInvalidValues(input);
			return invalidTicketsAndSumOfInvalidValues.Item2;
		}
	}

	public class Day16Part2Processor : BaseProcessor<Day16Model, long>
	{
		public Day16Part2Processor(IInputReader inputReader, string inputResourceName, Day16InputTransform day16InputTransform)
			: base(inputReader, inputResourceName, day16InputTransform)
		{
		}

		protected override long ComputeResultLogic(Day16Model input)
		{
			var invalidTicketsAndSumOfInvalidValues = Day16Helper.GetInvalidTicketsAndSumOfInvalidValues(input);
			var validTickets = input.NeighborTickets.Where(t => !invalidTicketsAndSumOfInvalidValues.Item1.Contains(t));
			var fieldsPerIndex = new Dictionary<int, IEnumerable<string>>();
			var fieldsOrder = new string[input.FieldDefinitions.Count()];

			for (var index = 0; index < input.FieldDefinitions.Count(); index++)
			{
				var fieldValuesAtCurrentIndex = new List<int>();

				foreach (var ticket in validTickets)
				{
					fieldValuesAtCurrentIndex.Add(ticket.TicketValues.ElementAt(index));
				}

				var validFieldsForIndex = Day16Helper.GetValidFieldsByAllValidValues(fieldValuesAtCurrentIndex, input.FieldDefinitions);
				fieldsPerIndex.Add(index, validFieldsForIndex);
			}

			var currentIndex = 0;
			while (fieldsOrder.Contains(null))
			{
				if (fieldsPerIndex[currentIndex].Count() == 1)
				{
					fieldsOrder[currentIndex] = fieldsPerIndex[currentIndex].First();

					Day16Helper.RemoveDuplicates(fieldsOrder[currentIndex], currentIndex, fieldsOrder.Length, fieldsPerIndex);
				}

				if (currentIndex == fieldsOrder.Length - 1)
					currentIndex = 0;
				else
					currentIndex++;
			}

			long fieldValuesMultiplied = 1;
			var myTicketValues = input.MyTicket.TicketValues.ToArray();
			for (var fieldOrderIndex = 0; fieldOrderIndex < fieldsOrder.Length; fieldOrderIndex++)
			{
				if (fieldsOrder[fieldOrderIndex].StartsWith("departure"))
					fieldValuesMultiplied *= myTicketValues[fieldOrderIndex];
			}

			return fieldValuesMultiplied;
		}
	}

	public static class Day16Helper
	{
		public static void RemoveDuplicates(string field, int currentIndex, int length, IDictionary<int, IEnumerable<string>> fieldsPerIndex)
		{
			for (var index = 0; index < length; index++)
			{
				if (index == currentIndex)
					continue;

				if (fieldsPerIndex[index].Contains(field))
					fieldsPerIndex[index] = fieldsPerIndex[index].Where(f => f != field);
			}
		}

		public static IEnumerable<string> GetValidFieldsByAllValidValues(IEnumerable<int> fieldValues, IEnumerable<FieldDefinition> fieldDefinitions)
		{
			var validFields = new List<string>();

			foreach (var fieldDef in fieldDefinitions)
			{
				var allValuesMatch = fieldValues.All(v => (v >= fieldDef.Intervals.ElementAt(0).Item1 && v <= fieldDef.Intervals.ElementAt(0).Item2) ||
				   (v >= fieldDef.Intervals.ElementAt(1).Item1 && v <= fieldDef.Intervals.ElementAt(1).Item2));

				if (allValuesMatch)
					validFields.Add(fieldDef.Name);
			}

			return validFields;
		}

		public static Tuple<IEnumerable<Ticket>, int> GetInvalidTicketsAndSumOfInvalidValues(Day16Model model)
		{
			var sumOfInvalidTicketValues = 0;
			var invalidTickets = new List<Ticket>();

			foreach (var ticket in model.NeighborTickets)
			{
				foreach (var ticketValue in ticket.TicketValues)
				{
					bool isValid = false;

					foreach (var fieldDefinition in model.FieldDefinitions)
					{
						var firstInterval = fieldDefinition.Intervals.ElementAt(0);
						var secondInteval = fieldDefinition.Intervals.ElementAt(1);

						isValid = (ticketValue >= firstInterval.Item1 && ticketValue <= firstInterval.Item2) ||
							(ticketValue >= secondInteval.Item1 && ticketValue <= secondInteval.Item2);

						if (isValid)
							break;
					}

					if (!isValid)
					{
						if (!invalidTickets.Contains(ticket))
							invalidTickets.Add(ticket);

						sumOfInvalidTicketValues += ticketValue;
					}
				}
			}

			return new Tuple<IEnumerable<Ticket>, int>(invalidTickets, sumOfInvalidTicketValues);
		}
	}
}

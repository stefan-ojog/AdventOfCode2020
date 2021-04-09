using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day16
{
	public class Day16InputTransform : IInputTransform<Day16Model>
	{
		public Day16Model Create(string[] inputLines)
		{
			const string FieldNameRegex = @"(^[\w\s]+):{1}";
			const string FieldIntervalsRegex = @"(\d+)-(\d+) or (\d+)-(\d+)";
			int index = 0;
			var fieldDefinitions = new List<FieldDefinition>();

			while (!string.IsNullOrEmpty(inputLines[index]))
			{
				var fieldNameRegex = new Regex(FieldNameRegex);
				var fieldValuesRegex = new Regex(FieldIntervalsRegex);

				var fieldNameMatch = fieldNameRegex.Match(inputLines[index]);
				var fieldName = fieldNameMatch.Groups[1];

				var fieldValuesMatch = fieldValuesRegex.Match(inputLines[index]);
				var firstIntervalMinValue = int.Parse(fieldValuesMatch.Groups[1].Value);
				var firstIntervalMaxValue = int.Parse(fieldValuesMatch.Groups[2].Value);
				var secondIntervalMinValue = int.Parse(fieldValuesMatch.Groups[3].Value);
				var secondIntervalMaxValue = int.Parse(fieldValuesMatch.Groups[4].Value);

				var fieldDefinition = new FieldDefinition(fieldName.Value, new[] {
					new Tuple<int, int>(firstIntervalMinValue, firstIntervalMaxValue),
					new Tuple<int, int>(secondIntervalMinValue, secondIntervalMaxValue) });

				fieldDefinitions.Add(fieldDefinition);

				index++;
			}

			while (!inputLines[index].Contains("your ticket:"))
				index++;

			index++;

			var myTicket = CreateTicket(inputLines[index]);

			while (!inputLines[index].Contains("nearby tickets:"))
				index++;

			var nearbyTickets = new List<Ticket>();
			for (var neighborTicketIndex = index + 1; neighborTicketIndex < inputLines.Length; neighborTicketIndex++)
			{
				nearbyTickets.Add(CreateTicket(inputLines[neighborTicketIndex]));
			}

			return new Day16Model(fieldDefinitions, nearbyTickets, myTicket);
		}

		private Ticket CreateTicket(string line)
		{
			var ticketValuesAsString = line.Split(",");
			var ticketValues = ticketValuesAsString.Select(v => int.Parse(v));

			return new Ticket(ticketValues);
		}
	}

	public class Day16Model
	{
		public IEnumerable<FieldDefinition> FieldDefinitions { get; }

		public IEnumerable<Ticket> NeighborTickets { get; }

		public Ticket MyTicket { get; }

		public Day16Model(IEnumerable<FieldDefinition> fieldDefinitions, IEnumerable<Ticket> neighborTickets, Ticket ticket)
		{
			FieldDefinitions = fieldDefinitions;
			NeighborTickets = neighborTickets;
			MyTicket = ticket;
		}
	}

	public class FieldDefinition
	{
		public string Name { get; }

		public IEnumerable<Tuple<int, int>> Intervals { get; }

		public FieldDefinition(string name, IEnumerable<Tuple<int, int>> intervals)
		{
			Name = name;
			Intervals = intervals;
		}
	}

	public class Ticket
	{
		public IEnumerable<int> TicketValues { get; }

		public Ticket(IEnumerable<int> ticketValues)
		{
			TicketValues = ticketValues;
		}
	}
}

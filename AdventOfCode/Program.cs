using AdventOfCode.Base;
using Autofac;
using System;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
			var container = new Bootstrapper().Build();

			Console.WriteLine("Day:");
			var dayInput = Console.ReadLine();
			Console.WriteLine("Part:");
			var partInput = Console.ReadLine();

			if (!int.TryParse(dayInput, out int day))
				throw new ArgumentException("day");

			if (!int.TryParse(partInput, out int part) || part < 1 || part > 2)
				throw new ArgumentException("part");

			var scope = container.BeginLifetimeScope();
			var processorFactory = scope.Resolve<IProcessorFactory>();

			var processor = processorFactory.GetProcessor(day, part);
			if (processor == null)
				throw new ArgumentException("Processor not found");

			Console.WriteLine(processor.GetResultAsString());
		}
	}
}

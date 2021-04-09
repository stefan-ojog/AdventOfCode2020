using System.Reflection;
using System.IO;
using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Tests
{
	public static class TestsResourceHelper
	{
		public static string GetExpectedOutputByProcessor(Type type)
		{
			var assembly = Assembly.GetExecutingAssembly();

			var namePreffix = "AdventOfCode.Tests.Output";
			var dayAndPart = GetDayAndPartByProcessorType(type);

			var resourceName = $"{namePreffix}.{dayAndPart}.txt";

			using (var stream = assembly.GetManifestResourceStream(resourceName))
			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		public static string GetDayAndPartByProcessorType(Type type)
		{
			var regexString = @"Day\d+Part\d+";

			var regex = new Regex(regexString);
			var match = regex.Match(type.Name);

			return match.Value;
		}
	}
}
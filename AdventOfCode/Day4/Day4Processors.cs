using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
	public class Day4Part1Processor : BaseProcessor<IEnumerable<Day4InputModel>, int>
	{
		public Day4Part1Processor(IInputReader inputReader, string inputResourceName, Day4InputTransform day4InputTransform)
			: base(inputReader, inputResourceName, day4InputTransform)
		{
		}

		protected override int ComputeResultLogic(IEnumerable<Day4InputModel> input)
		{
			return input.Count(m => Day4Helper.Part1IsValid(m));
		}
	}

	public class Day4Part2Processor : BaseProcessor<IEnumerable<Day4InputModel>, int>
	{
		public Day4Part2Processor(IInputReader inputReader, string inputResourceName, Day4InputTransform day4InputTransform)
			: base(inputReader, inputResourceName, day4InputTransform)
		{
		}

		protected override int ComputeResultLogic(IEnumerable<Day4InputModel> input)
		{
			return input.Count(m => Day4Helper.Part2IsValid(m));
		}
	}

	public static class Day4Helper
	{
		public static bool Part1IsValid(Day4InputModel inputModel)
		{
			return !string.IsNullOrEmpty(inputModel.Byr) && !string.IsNullOrEmpty(inputModel.Ecl) && !string.IsNullOrEmpty(inputModel.Eyr) && !string.IsNullOrEmpty(inputModel.Hcl)
			 && !string.IsNullOrEmpty(inputModel.Hgt) && !string.IsNullOrEmpty(inputModel.Iyr) && !string.IsNullOrEmpty(inputModel.Pid);
		}

		public static bool Part2IsValid(Day4InputModel inputModel)
		{
			var byrValid = IsNumberAndInInterval(inputModel.Byr, 1920, 2002);
			var iyrValid = IsNumberAndInInterval(inputModel.Iyr, 2010, 2020);
			var eyrValid = IsNumberAndInInterval(inputModel.Eyr, 2020, 2030);
			var heightValid = IsHeightValidPart2(inputModel.Hgt);
			var hairColorValid = IsHairColorValidPart2(inputModel.Hcl);
			var eyeColorValid = IsEyeColorValidPart2(inputModel.Ecl);
			var pidValid = IsPidValidPart2(inputModel.Pid);

			return byrValid && iyrValid && eyrValid && heightValid && hairColorValid && eyeColorValid && pidValid;
		}

		private static bool IsHeightValidPart2(string height)
		{
			if (string.IsNullOrEmpty(height))
				return false;

			var heightRegex = new Regex(@"(\d+)([cm|in]+)");
			var heightMatch = heightRegex.Match(height);

			if (heightMatch.Groups.Count != 3)
				return false;

			var heightNumericValue = heightMatch.Groups[1];
			var heightMeasure = heightMatch.Groups[2];

			return heightMeasure.Value == "cm" ? IsNumberAndInInterval(heightNumericValue.Value, 150, 193) :
			   IsNumberAndInInterval(heightNumericValue.Value, 59, 76);
		}

		private static bool IsHairColorValidPart2(string hairColor)
		{
			if (string.IsNullOrEmpty(hairColor))
				return false;

			var hairColorRegex = new Regex(@"#[a-f0-9]{6}");
			return hairColorRegex.IsMatch(hairColor);
		}

		private static bool IsEyeColorValidPart2(string eyeColor)
		{
			var permittedColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
			return permittedColors.Contains(eyeColor);
		}

		private static bool IsPidValidPart2(string pid)
		{
			if (string.IsNullOrEmpty(pid))
				return false;

			var regex = new Regex(@"\b\d{9}\b");
			return regex.IsMatch(pid);
		}

		private static bool IsNumberAndInInterval(string value, int min, int max)
		{
			if (int.TryParse(value, out var intValue))
			{
				return intValue >= min && intValue <= max;
			}

			return false;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Day4
{
	public class Day4InputModel
	{
		public string Byr { get; set; }

		public string Iyr { get; set; }

		public string Eyr { get; set; }

		public string Hgt { get; set; }

		public string Hcl { get; set; }

		public string Ecl { get; set; }

		public string Pid { get; set; }

		public string Cid { get; set; }

		public Day4InputModel(string[] keyValues)
		{
			foreach (var keyValue in keyValues)
			{
				var separatorIndex = keyValue.IndexOf(":");
				var name = keyValue.Substring(0, separatorIndex);
				var value = keyValue.Substring(separatorIndex + 1);
				var normalizedNme = $"{Char.ToUpperInvariant(name.First())}{name.Substring(1)}";

				this.GetType().GetProperty(normalizedNme).SetValue(this, value);
			}
		}
	}
}

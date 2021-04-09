using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day5
{
	public class Day5InputModel
	{
		public string RowDescription { get; }

		public string ColumnDescription { get; }

		public Day5InputModel(string inputLine)
		{
			var rIndex = inputLine.IndexOf("R");
			var lIndex = inputLine.IndexOf("L");

			var colDescriptionIndex = rIndex != -1 && lIndex != -1 ? Math.Min(rIndex, lIndex) : (rIndex != -1 ? rIndex : lIndex);
			RowDescription = inputLine.Substring(0, colDescriptionIndex);
			ColumnDescription = inputLine.Substring(colDescriptionIndex, inputLine.Length - colDescriptionIndex);
		}
	}
}

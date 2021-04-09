using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day5
{
	public class Day5InputTransform : IInputTransform<IEnumerable<Day5InputModel>>
	{
		public IEnumerable<Day5InputModel> Create(string[] inputLines)
		{
			return inputLines.Select(l => new Day5InputModel(l));
		}
	}
}

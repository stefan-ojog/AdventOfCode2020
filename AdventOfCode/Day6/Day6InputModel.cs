using System.Collections.Generic;

namespace AdventOfCode.Day6
{
	public class Day6InputModel
	{
		public Dictionary<int, IDictionary<char, bool>> YesQuestionsPerPerson { get; }

		public IDictionary<char, bool> TotalYesQuestionsPerGroup { get; private set; }

		public Day6InputModel(Dictionary<int, IDictionary<char, bool>> yesQuestionsPerPerson, IDictionary<char, bool> totalYesQuestionsPerGroup)
		{
			YesQuestionsPerPerson = yesQuestionsPerPerson;
			TotalYesQuestionsPerGroup = totalYesQuestionsPerGroup;
		}
	}
}

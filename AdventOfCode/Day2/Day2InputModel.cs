namespace AdventOfCode.Day2
{
	public class Day2InputModel
	{
		public int MinOccurencesNumber { get; }

		public int MaxOccurencesNumber { get; }

		public char Character { get; }

		public string Password { get; }

		public Day2InputModel(int minOccurencesNumber, int maxOccurencesNumber, char character, string password)
		{
			MinOccurencesNumber = minOccurencesNumber;
			MaxOccurencesNumber = maxOccurencesNumber;
			Character = character;
			Password = password;
		}
	}
}

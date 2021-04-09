namespace AdventOfCode.Day2
{
    public class Day2Part2InputModel
    {
        public int FirstPosition { get; }

        public int SecondPosition { get; }

        public char Character { get; }

        public string Password { get; }

        public Day2Part2InputModel(int firstPosition, int secondPosition, char character, string password)
        {
            FirstPosition = firstPosition;
            SecondPosition = secondPosition;
            Character = character;
            Password = password;
        }
    }
}

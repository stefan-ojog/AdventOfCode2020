namespace AdventOfCode.Base
{
    public class InputReaderWrapper : IInputReader
    {
        public string[] GetInput(string resourceName)
        {
            return InputReader.GetInput(resourceName);
        }
    }
}

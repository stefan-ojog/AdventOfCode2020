namespace AdventOfCode.Day9
{
    public class Day9InputModel
    {
        public long[] Preamble { get; }

        public long[] RestOfInput { get; }

        public int PreambleLength { get; }

        public Day9InputModel(long[] preamble, long[] restOfInput, int preambleLength)
        {
            Preamble = preamble;
            RestOfInput = restOfInput;
            PreambleLength = preambleLength;
        }
    }
}

namespace AdventOfCode.Day3
{
	public class Day3InputModel
	{
		public int LinesCount { get; }

		public int ColumnsCount { get; }

		public char[,] Map { get; }

		public Day3InputModel(int linesCount, int columnsCount, char[,] map)
		{
			LinesCount = linesCount;
			ColumnsCount = columnsCount;
			Map = map;
		}
	}
}

using AdventOfCode.Base;
using System;

namespace AdventOfCode.Day11
{
	public class Day11Part1Processor : BaseProcessor<Day11Model, int>
	{
		public Day11Part1Processor(IInputReader inputReader, string inputResourceName, Day11InputTransform day11InputTransform)
			: base(inputReader, inputResourceName, day11InputTransform)
		{
		}

		protected override int ComputeResultLogic(Day11Model input)
		{
			var stateChanged = false;
			var currentModel = input;
			var numberOfRounds = 0;

			do
			{
				var roundResult = Day11Helper.Part1DoRoundAndGetChangedState(currentModel);
				stateChanged = roundResult.Item1;
				currentModel = roundResult.Item2;
				numberOfRounds++;
			} while (stateChanged);

			var numberOfOccupiedSeats = Day11Helper.GetTotalNumberOfOccupiedSeats(currentModel, 0,
			currentModel.Rows - 1, 0,
			currentModel.Columns - 1, null);

			return numberOfOccupiedSeats;
		}
	}

	public class Day11Part2Processor : BaseProcessor<Day11Model, int>
	{
		public Day11Part2Processor(IInputReader inputReader, string inputResourceName, Day11InputTransform day11InputTransform)
			: base(inputReader, inputResourceName, day11InputTransform)
		{
		}

		protected override int ComputeResultLogic(Day11Model input)
		{
			var stateChanged = false;
			var currentModel = input;
			var numberOfRounds = 0;

			do
			{
				var roundResult = Day11Helper.Part2DoRoundAndGetChangedState(currentModel);
				stateChanged = roundResult.Item1;
				currentModel = roundResult.Item2;
				numberOfRounds++;
			} while (stateChanged);

			var numberOfOccupiedSeats = Day11Helper.GetTotalNumberOfOccupiedSeats(currentModel, 0,
			currentModel.Rows - 1, 0,
			currentModel.Columns - 1, null);

			return numberOfOccupiedSeats;
		}
	}

	public static class Day11Helper
	{
		const char EmptySeat = 'L';
		const char FloorSeat = '.';
		const char OccupiedSeat = '#';

		public static void PrintLayout(Day11Model currentModel)
		{
			for (var rowIndex = 0; rowIndex < currentModel.Rows; rowIndex++)
			{
				for (var colIndex = 0; colIndex < currentModel.Columns; colIndex++)
				{
					Console.Write(currentModel.SeatLaoyut[rowIndex, colIndex]);
				}
				Console.WriteLine();
			}
		}

		public static Tuple<bool, Day11Model> Part2DoRoundAndGetChangedState(Day11Model layoutModel)
		{
			var stateChanged = false;
			var newLayout = new char[layoutModel.Rows, layoutModel.Columns];

			for (var rowIndex = 0; rowIndex < layoutModel.Rows; rowIndex++)
				for (var colIndex = 0; colIndex < layoutModel.Columns; colIndex++)
				{
					newLayout[rowIndex, colIndex] = layoutModel.SeatLaoyut[rowIndex, colIndex];

					if (layoutModel.SeatLaoyut[rowIndex, colIndex] == EmptySeat)
					{
						var numberOfOccupiedSeats = GetNumberOfOccuppiedSeatsInAllDirections(layoutModel, rowIndex, colIndex);
						if (numberOfOccupiedSeats == 0)
						{
							newLayout[rowIndex, colIndex] = OccupiedSeat;
							stateChanged = true;
						}
					}
					else if (layoutModel.SeatLaoyut[rowIndex, colIndex] == OccupiedSeat)
					{
						var numberOfOccupiedSeats = GetNumberOfOccuppiedSeatsInAllDirections(layoutModel, rowIndex, colIndex);
						if (numberOfOccupiedSeats >= 5)
						{
							newLayout[rowIndex, colIndex] = EmptySeat;
							stateChanged = true;
						}
					}
				}

			var newModel = new Day11Model(newLayout, layoutModel.Rows, layoutModel.Columns);
			return new Tuple<bool, Day11Model>(stateChanged, newModel);
		}

		public static Tuple<bool, Day11Model> Part1DoRoundAndGetChangedState(Day11Model layoutModel)
		{
			var stateChanged = false;
			var newLayout = new char[layoutModel.Rows, layoutModel.Columns];

			for (var rowIndex = 0; rowIndex < layoutModel.Rows; rowIndex++)
				for (var colIndex = 0; colIndex < layoutModel.Columns; colIndex++)
				{
					newLayout[rowIndex, colIndex] = layoutModel.SeatLaoyut[rowIndex, colIndex];

					if (layoutModel.SeatLaoyut[rowIndex, colIndex] == EmptySeat)
					{
						var numberOfOccupiedSeats = GetNumberOfNeighborOccupiedSeats(layoutModel, rowIndex, colIndex);
						if (numberOfOccupiedSeats == 0)
						{
							newLayout[rowIndex, colIndex] = OccupiedSeat;
							stateChanged = true;
						}
					}
					else if (layoutModel.SeatLaoyut[rowIndex, colIndex] == OccupiedSeat)
					{
						var numberOfOccupiedSeats = GetNumberOfNeighborOccupiedSeats(layoutModel, rowIndex, colIndex);
						if (numberOfOccupiedSeats >= 4)
						{
							newLayout[rowIndex, colIndex] = EmptySeat;
							stateChanged = true;
						}
					}
				}

			var newModel = new Day11Model(newLayout, layoutModel.Rows, layoutModel.Columns);
			return new Tuple<bool, Day11Model>(stateChanged, newModel);
		}

		private static int GetNumberOfNeighborOccupiedSeats(Day11Model layoutModel, int rowPosition, int colPosition)
		{
			return GetTotalNumberOfOccupiedSeats(layoutModel,
				rowPosition - 1,
				rowPosition + 1,
				colPosition - 1,
				colPosition + 1,
				new Tuple<int, int>(rowPosition, colPosition));
		}

		private static int GetNumberOfOccuppiedSeatsInAllDirections(Day11Model layoutModel, int rowPosition, int colPosition)
		{
			return GetNumberOfOccupiedSeatsInOneDirection(layoutModel, rowPosition, colPosition, -1, -1)
			+ GetNumberOfOccupiedSeatsInOneDirection(layoutModel, rowPosition, colPosition, -1, 0)
			+ GetNumberOfOccupiedSeatsInOneDirection(layoutModel, rowPosition, colPosition, -1, 1)
			+ GetNumberOfOccupiedSeatsInOneDirection(layoutModel, rowPosition, colPosition, 0, -1)
			+ GetNumberOfOccupiedSeatsInOneDirection(layoutModel, rowPosition, colPosition, 0, 1)
			+ GetNumberOfOccupiedSeatsInOneDirection(layoutModel, rowPosition, colPosition, 1, -1)
			+ GetNumberOfOccupiedSeatsInOneDirection(layoutModel, rowPosition, colPosition, 1, 0)
			+ GetNumberOfOccupiedSeatsInOneDirection(layoutModel, rowPosition, colPosition, 1, 1);
		}

		private static int GetNumberOfOccupiedSeatsInOneDirection(Day11Model layoutModel, int rowPosition, int columnPosition, int rowDirection, int columnDirection)
		{
			if (rowPosition + rowDirection < 0 || rowPosition + rowDirection >= layoutModel.Rows ||
				columnPosition + columnDirection < 0 || columnPosition + columnDirection >= layoutModel.Columns)
				return 0;

			if (layoutModel.SeatLaoyut[rowPosition + rowDirection, columnPosition + columnDirection] == OccupiedSeat)
				return 1;
			else if (layoutModel.SeatLaoyut[rowPosition + rowDirection, columnPosition + columnDirection] == EmptySeat)
				return 0;
			else
				return GetNumberOfOccupiedSeatsInOneDirection(layoutModel,
					rowPosition + rowDirection,
					columnPosition + columnDirection,
					rowDirection,
					columnDirection);
		}


		public static int GetTotalNumberOfOccupiedSeats(Day11Model layoutModel,
			int fromRowIndex,
			int toRowIndex,
			int fromColumnIndex,
			int toColumnIndex,
			Tuple<int, int> ignorePosition = null)
		{
			var numberOfOccupiedSeats = 0;
			for (var rowIndex = fromRowIndex; rowIndex <= toRowIndex; rowIndex++)
				for (var colIndex = fromColumnIndex; colIndex <= toColumnIndex; colIndex++)
				{
					var isValidIndex = rowIndex >= 0 && rowIndex < layoutModel.Rows &&
						colIndex >= 0 && colIndex < layoutModel.Columns;

					if (!isValidIndex || (ignorePosition != null
						&& ignorePosition.Item1 == rowIndex
						&& ignorePosition.Item2 == colIndex))
						continue;

					if (layoutModel.SeatLaoyut[rowIndex, colIndex] == OccupiedSeat)
						numberOfOccupiedSeats++;
				}

			return numberOfOccupiedSeats;
		}
	}
}

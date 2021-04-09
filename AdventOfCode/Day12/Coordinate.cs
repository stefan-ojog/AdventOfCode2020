namespace AdventOfCode.Day12
{
	public class Coordinate
	{
		public int VerticalCoordinate { get; set; }

		public int HorizontalCoordinate { get; set; }

		public override string ToString()
		{
			string verticalDirection;
			string horizontalDirection;

			if (VerticalCoordinate >= 0)
				verticalDirection = "N";
			else
				verticalDirection = "S";

			if (HorizontalCoordinate >= 0)
				horizontalDirection = "E";
			else
				horizontalDirection = "W";

			return $"{verticalDirection}{VerticalCoordinate},{horizontalDirection}{HorizontalCoordinate}";
		}
	}
}

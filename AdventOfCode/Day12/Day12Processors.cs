using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day12
{
    public class Day12Part1Processor : BaseProcessor<string[], int>
    {
        public Day12Part1Processor(IInputReader inputReader, string inputResourceName, Day12InputTransform day12InputTransform)
            : base(inputReader, inputResourceName, day12InputTransform)
        {
        }

        protected override int ComputeResultLogic(string[] input)
        {
            var directionsDictionary = new Dictionary<char, int>() {
                { 'N', 0 },
                { 'E',0 },
                { 'S', 0 },
                { 'W', 0 }
            };

            var coordinatesSum = Day12Helper.ComputePart1(input, directionsDictionary);

            return coordinatesSum;
        }
    }

    public class Day12Part2Processor : BaseProcessor<string[], int>
    {
        public Day12Part2Processor(IInputReader inputReader, string inputResourceName, Day12InputTransform day12InputTransform)
            : base(inputReader, inputResourceName, day12InputTransform)
        {
        }

        protected override int ComputeResultLogic(string[] input)
        {
            var coordinatesSum = Day12Helper.ComputePart2(input);

            return coordinatesSum;
        }
    }

    public static class Day12Helper
    {
        static IDictionary<char, char> TurnRightDictionary = new Dictionary<char, char>()
            {
                { 'N','E' },
                { 'E', 'S' },
                { 'S', 'W' },
                { 'W', 'N' }
            };

        static IDictionary<char, char> TurnLeftDictionary = new Dictionary<char, char>()
            {
                { 'N','W' },
                { 'E', 'N' },
                { 'S', 'E' },
                { 'W', 'S' }
            };

        public static int ComputePart1(string[] instructions, Dictionary<char, int> directionsDictionary)
        {
            var currentlyFacingDirection = 'E';

            foreach (var instruction in instructions)
            {
                var action = instruction.First();
                var value = int.Parse(instruction.Substring(1));

                if (action == 'L' || action == 'R')
                {
                    var factor = value / 90;
                    for (var index = 0; index < factor; index++)
                        currentlyFacingDirection = action == 'L' ?
                            TurnLeftDictionary[currentlyFacingDirection]
                            : TurnRightDictionary[currentlyFacingDirection];
                }
                else if (action == 'F')
                {
                    AddOrUpdateInDict(directionsDictionary, currentlyFacingDirection, value);
                }
                else
                {
                    AddOrUpdateInDict(directionsDictionary, action, value);
                }
            }

            var verticalIndex = Math.Abs(directionsDictionary['N'] - directionsDictionary['S']);
            var horizontalIndex = Math.Abs(directionsDictionary['E'] - directionsDictionary['W']);

            return verticalIndex + horizontalIndex;
        }

        public static int ComputePart2(string[] instructions)
        {
            Coordinate waypointCoordinate = new Coordinate()
            {
                VerticalCoordinate = 1,
                HorizontalCoordinate = 10
            };
            Coordinate shipCoordinate = new Coordinate()
            {
                VerticalCoordinate = 0,
                HorizontalCoordinate = 0
            };

            foreach (var instruction in instructions)
            {
                var action = instruction.First();
                var value = int.Parse(instruction.Substring(1));

                if (action == 'L' || action == 'R')
                {
                    var factor = value / 90;
                    waypointCoordinate = Rotate(waypointCoordinate, factor, action == 'L' ? -1 : 1);
                }
                else if (action == 'F')
                {
                    shipCoordinate = MoveToWaypoint(shipCoordinate, waypointCoordinate, value);
                }
                else
                {
                    waypointCoordinate = MoveWaypoint(waypointCoordinate, action, value);
                }
            }

            return Math.Abs(shipCoordinate.VerticalCoordinate) + Math.Abs(shipCoordinate.HorizontalCoordinate);
        }

        private static void AddOrUpdateInDict(IDictionary<char, int> dictionary, char key, int newValue)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] += newValue;
            else
                dictionary.Add(key, newValue);
        }

        private static Coordinate Rotate(Coordinate currentCoordinate, int numberOfRotations, int clockwiseSign)
        {
            var newCoordinate = new Coordinate();
            newCoordinate = currentCoordinate;

            for (var index = 0; index < numberOfRotations; index++)
            {
                var previousVertical = newCoordinate.VerticalCoordinate;
                var previousHorizontal = newCoordinate.HorizontalCoordinate;

                newCoordinate.HorizontalCoordinate = previousVertical * clockwiseSign;
                newCoordinate.VerticalCoordinate = previousHorizontal * (-1) * clockwiseSign;
            }

            return newCoordinate;
        }

        private static Coordinate MoveToWaypoint(Coordinate shipCoordinate, Coordinate waypointCoordinate, int value)
        {
            var verticalOffset = waypointCoordinate.VerticalCoordinate * value;
            var horizontalOffset = waypointCoordinate.HorizontalCoordinate * value;

            return new Coordinate
            {
                HorizontalCoordinate = shipCoordinate.HorizontalCoordinate + horizontalOffset,
                VerticalCoordinate = shipCoordinate.VerticalCoordinate + verticalOffset
            };
        }

        private static Coordinate MoveWaypoint(Coordinate waypointCoordinate, char direction, int value)
        {
            var coordinateSign = direction == 'N' || direction == 'E' ? 1 : -1;
            var isHorizontal = direction == 'E' || direction == 'W';

            if (isHorizontal)
                waypointCoordinate.HorizontalCoordinate = waypointCoordinate.HorizontalCoordinate + value * coordinateSign;
            else
                waypointCoordinate.VerticalCoordinate = waypointCoordinate.VerticalCoordinate + value * coordinateSign;

            return waypointCoordinate;
        }
    }
}

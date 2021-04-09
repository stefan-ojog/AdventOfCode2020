using AdventOfCode.Base;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Day14
{
    public class Day14Part1Processor : BaseProcessor<Day14Model, long>
    {
        public Day14Part1Processor(IInputReader inputReader, string inputResourceName, Day14InputTransform day14InputTransform)
            : base(inputReader, inputResourceName, day14InputTransform)
        {
        }

        protected override long ComputeResultLogic(Day14Model input)
        {
            var valuesDictionary = Day14Helper.ComputeResultPart1(input.Instructions);
            long sum = 0;
            foreach (var (key, value) in valuesDictionary)
            {
                sum += value;
            }

            return sum;
        }
    }

    public class Day14Part2Processor : BaseProcessor<Day14Model, long>
    {
        public Day14Part2Processor(IInputReader inputReader, string inputResourceName, Day14InputTransform day14InputTransform)
            : base(inputReader, inputResourceName, day14InputTransform)
        {
        }

        protected override long ComputeResultLogic(Day14Model input)
        {
            var part2Values = Day14Helper.ComputeResultPart2(input.Instructions);
            long sum = 0;
            foreach (var (key, value) in part2Values)
                sum += value;

            return sum;
        }
    }

    public static class Day14Helper
    {
        public static IDictionary<long, long> ComputeResultPart1(InstructionWithBitmask[] instructions)
        {
            var valuesDictionary = new Dictionary<long, long>();

            foreach (var instruction in instructions)
            {
                var base2Value = Convert.ToString(instruction.MemoryValue, 2);
                var normalizedBase2Value = base2Value.PadLeft(36, '0');
                var newBase2Value = new char[36];

                for (var maskIndex = 0; maskIndex < instruction.CurrentMask.Length; maskIndex++)
                {
                    newBase2Value[maskIndex] = normalizedBase2Value[maskIndex];

                    if (instruction.CurrentMask[maskIndex] == 'X')
                        continue;

                    newBase2Value[maskIndex] = instruction.CurrentMask[maskIndex];
                }

                var newValue = ConvertToLong(newBase2Value);

                if (valuesDictionary.ContainsKey(instruction.MemoryIndex))
                    valuesDictionary[instruction.MemoryIndex] = newValue;
                else
                    valuesDictionary.Add(instruction.MemoryIndex, newValue);
            }

            return valuesDictionary;
        }

        public static IDictionary<long, long> ComputeResultPart2(InstructionWithBitmask[] instructions)
        {
            var valuesDictionary = new Dictionary<long, long>();

            foreach (var instruction in instructions)
            {
                var base2Value = Convert.ToString(instruction.MemoryIndex, 2);
                var normalizedBase2Value = base2Value.PadLeft(36, '0');
                var newBase2Value = new char[36];

                for (var maskIndex = 0; maskIndex < instruction.CurrentMask.Length; maskIndex++)
                {
                    newBase2Value[maskIndex] = instruction.CurrentMask[maskIndex] == '0'
                        ? normalizedBase2Value[maskIndex]
                        : instruction.CurrentMask[maskIndex];
                }

                var currentAddresses = new List<char[]>();
                AddAllBitMasksFromWildcardBitmask(newBase2Value, 0, currentAddresses);

                foreach (var address in currentAddresses)
                {
                    var addressValue = ConvertToLong(address);

                    if (valuesDictionary.ContainsKey(addressValue))
                        valuesDictionary[addressValue] = instruction.MemoryValue;
                    else
                        valuesDictionary.Add(addressValue, instruction.MemoryValue);
                }
            }

            return valuesDictionary;
        }

        private static void AddAllBitMasksFromWildcardBitmask(char[] address, int index, List<char[]> allAddresses)
        {
            if (index == address.Length)
            {
                allAddresses.Add(address);
                return;
            }

            if (address[index] == 'X')
            {
                var addresses = GetCopiesOfBitMaskWithBitChanged(index, address);
                AddAllBitMasksFromWildcardBitmask(addresses.Item1, index + 1, allAddresses);
                AddAllBitMasksFromWildcardBitmask(addresses.Item2, index + 1, allAddresses);
            }
            else
                AddAllBitMasksFromWildcardBitmask(address, index + 1, allAddresses);
        }

        private static (char[], char[]) GetCopiesOfBitMaskWithBitChanged(int index, char[] address)
        {
            char[] zeroVersion = new char[address.Length];
            char[] oneVersion = new char[address.Length];

            address.CopyTo(zeroVersion, 0);
            address.CopyTo(oneVersion, 0);

            zeroVersion[index] = '0';
            oneVersion[index] = '1';

            return (zeroVersion, oneVersion);
        }

        private static long ConvertToLong(char[] base2Value)
        {
            var currentExponent = 0;
            long currentValue = 0;

            for (var index = base2Value.Length - 1; index >= 0; index--)
            {
                if (base2Value[index] == '1')
                {
                    currentValue += (long)Math.Pow(2, currentExponent);
                }

                currentExponent++;
            }

            return currentValue;
        }
    }
}

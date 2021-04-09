using AdventOfCode.Base;
using System.Collections.Generic;

namespace AdventOfCode.Day6
{
    public abstract class Day6BaseInputTransform : IInputTransform<IEnumerable<Day6InputModel>>
    {
        protected abstract Day6InputModel CreateModel(string[] lines);

        public IEnumerable<Day6InputModel> Create(string[] inputLines)
        {
            var inputModels = new List<Day6InputModel>();
            var currentGroupLines = new List<string>();

            for (var index = 0; index < inputLines.Length; index++)
            {
                if (inputLines[index].Length == 0)
                {
                    inputModels.Add(CreateModel(currentGroupLines.ToArray()));
                    currentGroupLines.Clear();
                }
                else
                    currentGroupLines.Add(inputLines[index]);
            }

            var lastItem = CreateModel(currentGroupLines.ToArray());
            inputModels.Add(lastItem);

            return inputModels;
        }
    }

    public class Day6Part1InputTransform : Day6BaseInputTransform, IInputTransform<IEnumerable<Day6InputModel>>
    {
        protected override Day6InputModel CreateModel(string[] lines)
        {
            var yesQuestionsPerPerson = new Dictionary<int, IDictionary<char, bool>>();
            var totalYesQuestionsPerGroup = new Dictionary<char, bool>();

            for (char character = 'a'; character <= 'z'; character++)
            {
                totalYesQuestionsPerGroup.Add(character, false);
            }

            for (var index = 0; index < lines.Length; index++)
            {
                var yesQuestionsPerCharacterDict = Day6Helper.GetYesQuestionsPerCharacter(lines[index]);
                yesQuestionsPerPerson.Add(index, yesQuestionsPerCharacterDict);

                foreach (var (key, value) in yesQuestionsPerCharacterDict)
                    if (totalYesQuestionsPerGroup.ContainsKey(key))
                        totalYesQuestionsPerGroup[key] = totalYesQuestionsPerGroup[key]
                             || yesQuestionsPerCharacterDict[key];
            }

            return new Day6InputModel(yesQuestionsPerPerson, totalYesQuestionsPerGroup);
        }
    }

    public class Day6Part2InputTransform : Day6BaseInputTransform, IInputTransform<IEnumerable<Day6InputModel>>
    {
        protected override Day6InputModel CreateModel(string[] inputLines)
        {
            var yesQuestionsPerPerson = new Dictionary<int, IDictionary<char, bool>>();
            var totalYesQuestionsPerGroup = new Dictionary<char, bool>();

            var firstPersonYesQuestions = Day6Helper.GetYesQuestionsPerCharacter(inputLines[0]);
            totalYesQuestionsPerGroup = firstPersonYesQuestions;
            yesQuestionsPerPerson.Add(0, firstPersonYesQuestions);

            for (var index = 1; index < inputLines.Length; index++)
            {
                var yesQuestionsPerCharacterDict = Day6Helper.GetYesQuestionsPerCharacter(inputLines[index]);
                yesQuestionsPerPerson.Add(index, yesQuestionsPerCharacterDict);

                foreach (var (key, value) in yesQuestionsPerCharacterDict)
                    totalYesQuestionsPerGroup[key] = totalYesQuestionsPerGroup[key]
                        && yesQuestionsPerCharacterDict[key];
            }

            return new Day6InputModel(yesQuestionsPerPerson, totalYesQuestionsPerGroup);
        }
    }

    public static class Day6Helper
    {
        public static Dictionary<char, bool> GetYesQuestionsPerCharacter(string line)
        {
            var dictionaryYesPerCharacter = new Dictionary<char, bool>();
            for (char character = 'a'; character <= 'z'; character++)
                dictionaryYesPerCharacter.Add(character, false);

            foreach (var character in line)
            {
                dictionaryYesPerCharacter[character] = true;
            }

            return dictionaryYesPerCharacter;
        }
    }
}

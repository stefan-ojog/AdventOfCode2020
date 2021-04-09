using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Base;

namespace AdventOfCode.Day4
{
    public class Day4InputTransform : IInputTransform<IEnumerable<Day4InputModel>>
    {
        public IEnumerable<Day4InputModel> Create(string[] inputLines)
        {
            var allKeyValuesForPassportBuilder = new List<string>();
            var inputModels = new List<Day4InputModel>();

            for (var index = 0; index < inputLines.Length; index++)
            {
                if (inputLines[index].Length == 0)
                {
                    inputModels.Add(new Day4InputModel(allKeyValuesForPassportBuilder.ToArray()));
                    allKeyValuesForPassportBuilder.Clear();
                }
                else
                {
                    var regex = new Regex(@"\w{3}:[\w|#]+\b");
                    var matches = regex.Matches(inputLines[index]);

                    allKeyValuesForPassportBuilder.AddRange(matches.Select(m => m.Value));
                }
            }

            var lastItem = new Day4InputModel(allKeyValuesForPassportBuilder.ToArray());
            inputModels.Add(lastItem);

            return inputModels;
        }
    }
}

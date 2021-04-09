using AdventOfCode.Base;
using AdventOfCode.CommonModels;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day7
{
	public class Day7InputFormat : IInputTransform<Graph<string>>
	{
		public Graph<string> Create(string[] inputLines)
		{
			var regexForParent = new Regex(@"(\w+\s\w+\s)bag[s]*");
			var childrenRegex = new Regex(@"(\d)\s(\w+\s\w+\s)bag[s]*");
			var graph = new Graph<string>();

			foreach (var line in inputLines)
			{
				var containKeywordIndex = line.IndexOf("contain");
				var parentSubstring = line.Substring(0, containKeywordIndex);
				var parentMatch = regexForParent.Match(parentSubstring);
				if (parentMatch.Success)
				{
					var attribute = parentMatch.Groups[1].Value.Trim();
					var parentNode = graph.GetOrCreateNode(attribute);

					var childrenSubstring = line.Substring(containKeywordIndex + 7);
					var childrenMatches = childrenRegex.Matches(childrenSubstring);

					for (var index = 0; index < childrenMatches.Count; index++)
					{
						var childMatch = childrenMatches[index];
						var value = int.Parse(childMatch.Groups[1].Value);
						var childAttribute = childMatch.Groups[2].Value.Trim();

						var childNode = graph.GetOrCreateNode(childAttribute);
						parentNode.ChildrenNodesLinks.Add(childNode, value);
						childNode.Parents.Add(parentNode);
					}
				}
			}

			return graph;
		}
	}
}

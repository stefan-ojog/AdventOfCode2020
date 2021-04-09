using AdventOfCode.Base;
using AdventOfCode.CommonModels;

namespace AdventOfCode.Day10
{
	public class Day10InputTransform : IInputTransform<Graph<long>>
	{
		public Graph<long> Create(string[] inputLines)
		{
			var graph = new Graph<long>();

			foreach (var line in inputLines)
			{
				var value = int.Parse(line);
				var currentNode = graph.GetOrCreateNode(value);

				for (var from = value - 3; from <= value + 3; from++)
				{
					if (from == value)
						continue;

					var existingNode = graph.TryGetNode(from);
					if (existingNode == null)
						continue;

					if (existingNode.Value < currentNode.Value)
					{
						existingNode.ChildrenNodesLinks.Add(currentNode, currentNode.Value - existingNode.Value);
						currentNode.Parents.Add(existingNode);
					}
					else
					{
						currentNode.ChildrenNodesLinks.Add(existingNode, existingNode.Value - currentNode.Value);
						existingNode.Parents.Add(currentNode);
					}
				}
			}

			return graph;
		}
	}
}

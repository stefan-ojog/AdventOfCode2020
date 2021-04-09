using AdventOfCode.Base;
using AdventOfCode.CommonModels;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day7
{
	public class Day7Part1Processor : BaseProcessor<Graph<string>, int>
	{
		const string ShinyGoldBagName = "shiny gold";

		public Day7Part1Processor(IInputReader inputReader, string inputResourceName, Day7InputFormat day7InputFormat)
			: base(inputReader, inputResourceName, day7InputFormat)
		{
		}

		protected override int ComputeResultLogic(Graph<string> input)
		{
			var permittedBags = new List<string>();
			var shinyGoldNode = input.AllNodes.SingleOrDefault(n => n.Key == ShinyGoldBagName);

			Day7Helper.FindAncestors(shinyGoldNode.Value, permittedBags);

			return permittedBags.Count;
		}
	}

	public class Day7Part2Processor : BaseProcessor<Graph<string>, long>
	{
		const string ShinyGoldBagName = "shiny gold";

		public Day7Part2Processor(IInputReader inputReader, string inputResourceName, Day7InputFormat day7InputFormat)
			: base(inputReader, inputResourceName, day7InputFormat)
		{
		}

		protected override long ComputeResultLogic(Graph<string> input)
		{
			var permittedBags = new List<string>();
			var shinyGoldNode = input.AllNodes.SingleOrDefault(n => n.Key == ShinyGoldBagName);

			Day7Helper.FindAncestors(shinyGoldNode.Value, permittedBags);
			var total = Day7Helper.ComputeTotalNumberOfChildrenBags(shinyGoldNode.Value, 1);

			return total;
		}
	}

	public static class Day7Helper
	{
		public static void FindAncestors(Node<string> node, IList<string> ancestors)
		{
			foreach (var parent in node.Parents)
			{
				if (!ancestors.Contains(parent.Value))
				{
					ancestors.Add(parent.Value);
					FindAncestors(parent, ancestors);
				}
			}
		}

		public static long ComputeTotalNumberOfChildrenBags(Node<string> node, long factor)
		{
			long sum = 0;
			foreach (var (childNode, count) in node.ChildrenNodesLinks)
			{
				var currentFactor = count;
				sum += count + ComputeTotalNumberOfChildrenBags(childNode, currentFactor);
			}

			return sum * factor;
		}
	}
}

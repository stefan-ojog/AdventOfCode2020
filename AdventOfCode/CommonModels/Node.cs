using System.Collections.Generic;

namespace AdventOfCode.CommonModels
{
	public class Node<T>
	{
		public T Value { get; }

		public IDictionary<Node<T>, long> ChildrenNodesLinks { get; } = new Dictionary<Node<T>, long>();

		public IList<Node<T>> Parents { get; } = new List<Node<T>>();

		public Node(T value)
		{
			Value = value;
		}
	}
}

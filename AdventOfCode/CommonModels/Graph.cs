using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.CommonModels
{
    public class Graph<T>
    {
        public IDictionary<T, Node<T>> AllNodes { get; }

        public Graph()
        {
            AllNodes = new Dictionary<T, Node<T>>();
        }

        public Node<T> GetOrCreateNode(T value)
        {
            if (AllNodes.TryGetValue(value, out var existingNode))
                return existingNode;

            var newNode = new Node<T>(value);
            AllNodes.Add(value, newNode);
            return newNode;
        }

        public Node<T> TryGetNode(T value)
        {
            if (AllNodes.TryGetValue(value, out var existingNode))
                return existingNode;

            return null;
        }
    }
}

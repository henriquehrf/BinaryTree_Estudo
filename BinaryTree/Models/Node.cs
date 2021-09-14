using System;

namespace BinaryTree.Models
{
	public class Node<T> : IComparable where T : IComparable
	{
		private readonly T _value;

		public Node<T> LeftNode { get; private set; }
		public Node<T> RightNode { get; private set; }
		public T Value
		{
			get
			{
				return _value;
			}
		}
		public Node(T value)
		{
			_value = value;
		}

		public void SetLeftNode(Node<T> node) => LeftNode = node;
		public void SetRIghtNode(Node<T> node) => RightNode = node;

		public int CompareTo(object obj)
		{
			var objeto = obj as Node<T>;

			return this.Value.CompareTo(objeto.Value);
		}

		//public static bool operator ==(Node<T> x, Node<T> y) => y != null && x.Value.Equals(y.Value);
		//public static bool operator !=(Node<T> x, Node<T> y) => y != null && !x.Value.Equals(y.Value);
		//public static bool operator >(Node<T> x, Node<T> y) => y != null &&  x.Value.CompareTo(y.Value) < 0;
		//public static bool operator <(Node<T> x, Node<T> y) => y != null &&  x.Value.CompareTo(y.Value) > 0;

	}
}

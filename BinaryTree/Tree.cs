using BinaryTree.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree
{
	public class Tree<T> : ICollection<Node<T>> where T : IComparable
	{
		private int _count;
		private Node<T> _rootNode;
		public int Count => _count;

		public bool IsReadOnly => false;

		public void Add(Node<T> item)
		{
			if (_count == 0)
				_rootNode = item;
			else if (_count == 1)
			{
				var result = _rootNode.CompareTo(item);
				SetPosition(_rootNode, item, result);
			}
			else
			{
				var node = MoveTree(_rootNode, item);

				var result = node.CompareTo(item);
				if (result < 0)
					node.SetRIghtNode(item);
				else
					node.SetLeftNode(item);
			}
			_count++;
		}

		private Node<T> MoveTree(Node<T> nodeFather, Node<T> nodeChild)
		{
			var result = nodeFather.CompareTo(nodeChild);

			if (result == 0)
				throw new Exception("Error!");

			if (result > 0 && nodeFather.LeftNode != null)
				return MoveTree(nodeFather.LeftNode, nodeChild);

			if (result < 1 && nodeFather.RightNode != null)
				return MoveTree(nodeFather.RightNode, nodeChild);

			return nodeFather;
		}

		private void SetPosition(Node<T> nodeFather, Node<T> nodeChild, int comparablePosition)
		{
			if (comparablePosition == 0)
				throw new Exception("Error!");

			if (comparablePosition > 0)
				nodeFather.SetLeftNode(nodeChild);

			if (comparablePosition < 1)
				nodeFather.SetRIghtNode(nodeChild);
		}

		public void Clear()
		{
			_count = 0;
			_rootNode = null;
		}

		public bool Contains(Node<T> item)
		{
			return Find(item) != null;
		}

		public void CopyTo(Node<T>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Node<T>> GetEnumerator()
		{
			return PrintTree(_rootNode).GetEnumerator();
		}

		public IEnumerable<Node<T>> PrintTree(Node<T> node)
		{
			if (node == null)
				yield break;

			if (node.LeftNode != null)
				foreach (var subNode in PrintTree(node.LeftNode))
					yield return subNode;

			yield return node;

			if (node.RightNode != null)
				foreach (var subNode in PrintTree(node.RightNode))
					yield return subNode;
		}


		public bool Remove(Node<T> item)
		{
			if (!Contains(item))
				return false;

			var nodeToRemove = Find(item);
			var nodeFather = FindNodeFather(nodeToRemove);

			if (nodeFather == _rootNode)
			{
				_rootNode = _rootNode.RightNode;
				Add(nodeToRemove.LeftNode);
				return true;
			}

			if (nodeToRemove.LeftNode == null && nodeToRemove.RightNode == null)
			{
				if (nodeToRemove.Value.CompareTo(nodeFather.Value) < 0)
					nodeFather.SetLeftNode(null);
				else
					nodeFather.SetRIghtNode(null);

				return true;
			}
			if (nodeToRemove.LeftNode != null && nodeToRemove.RightNode == null)
			{
				nodeFather.SetLeftNode(nodeToRemove.LeftNode);
				return true;
			}
			if (nodeToRemove.RightNode != null && nodeToRemove.LeftNode == null)
			{
				nodeFather.SetRIghtNode(nodeToRemove.RightNode);
				return true;
			}
			if (nodeToRemove.RightNode != null && nodeToRemove.LeftNode != null)
			{
				nodeFather.SetRIghtNode(nodeToRemove.RightNode);
				Add(nodeToRemove.LeftNode);
				return true;
			}

			return false;

		}

		private Node<T> FindNodeFather(Node<T> child)
		{
			var root = _rootNode;
			if (root == child)
				return root;

			while (root != null)
			{
				if (root.LeftNode == child || root.RightNode == child) return root;
				else if (child.Value.CompareTo(root.Value) > 0) root = root.RightNode;
				else
					root = root.LeftNode;
			}

			return null; ;
		}

		public Node<T> Find(Node<T> item)
		{
			var root = _rootNode;
			while (root != null)
			{
				if (root.Value.Equals(item.Value)) return root;
				else if (item.Value.CompareTo(root.Value) > 0) root = root.RightNode;
				else
					root = root.LeftNode;
			}

			return null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return PrintTree(_rootNode).GetEnumerator();
		}

		public int Height()
		{
			var lista = new List<Node<T>>();
			var height = 0;
			var enumerator = GetEnumerator();
			enumerator.MoveNext();
			do
			{
				var current = enumerator.Current;
				if (current.LeftNode == null && current.RightNode == null)
					lista.Add(current);

			} while (enumerator.MoveNext());

			foreach (var item in lista)
			{
				var h = HeightOfNode(item);

				if (h > height)
					height = h;
			}

			return height;

		}

		private int HeightOfNode(Node<T> node)
		{
			var root = _rootNode;
			int count = 0;
			while (root != null)
			{
				if (root.Value.Equals(node.Value)) return count;
				else if (node.Value.CompareTo(root.Value) > 0) root = root.RightNode;
				else
					root = root.LeftNode;
				count++;
			}

			return -1; ;
		}

		public void ReBalanceTree()
		{
			var itens = new List<Node<T>>();

			var enumerator = GetEnumerator();
			Clear();
			var index = 0;
			enumerator.MoveNext();
			do
			{
				itens.Add(enumerator.Current);

			} while (enumerator.MoveNext());

			itens.Sort();

			var middle = itens.Count / 2;

			//Add(itens[middle]);
			AddBalanced(itens.GetRange(0, middle));
			AddBalanced(itens.GetRange(middle, itens.Count - (middle )));

		}

		void AddBalanced(List<Node<T>> list)
		{
			if (list.Count == 0)
				return;
			var middle = Convert.ToInt32(Math.Ceiling((list.Count - 1) / 2.0));

			Add(list[middle]);

			if (middle == 0)
				return;

			AddBalanced(list.GetRange(0, middle));

			if (list.Count >= (middle + 1))
				AddBalanced(list.GetRange(middle + 1, list.Count - 1));
		}
	}
}

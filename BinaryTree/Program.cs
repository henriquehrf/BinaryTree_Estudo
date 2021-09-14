using BinaryTree.Models;
using System;
using System.Collections.Generic;

namespace BinaryTree
{
	class Program
	{
		static void Main(string[] args)
		{

			var tree = new Tree<int>();
			tree.Add(new Node<int>(5));
			tree.Add(new Node<int>(3));
			tree.Add(new Node<int>(2));
			tree.Add(new Node<int>(1));
			tree.Add(new Node<int>(7));
			tree.Add(new Node<int>(8));
			tree.Add(new Node<int>(6));

			//Imprimir(tree);

			//Console.WriteLine("---------");
			////Console.WriteLine(tree.Contains(new Node<int>(6)));
			//tree.Remove(new Node<int>(5));

			//Imprimir(tree);

			Console.WriteLine(tree.Height());

			tree.ReBalanceTree();

			Console.WriteLine(tree.Height());



		}

		static void Imprimir(Tree<int> tree)
		{
			var enumerator = tree.GetEnumerator();
			enumerator.MoveNext();
			do
			{
				Console.WriteLine(enumerator.Current.Value);

			} while (enumerator.MoveNext());
		}
	}
}

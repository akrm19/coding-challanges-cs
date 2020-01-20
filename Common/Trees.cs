using System;
using System.Collections.Generic;
using System.Linq;

namespace CrackingTheCodingInterviewProblems.Common
{
    #region Original Tree Code
    public class Trees
    {
        public Trees()
        {
        }
    }

    public class TreeNode
    {
        public TreeNode Left;
        public TreeNode Right;
        public TreeNode[] Adjacent;
        public string Name;
        public int Value;
        public bool Visited;

        public TreeNode(int val)
        {
            Value = val;
        }

        public static void InOrderTraversal(TreeNode node)
        {
            if(node != null)
            {
                InOrderTraversal(node.Left);
                Visit(node);
                InOrderTraversal(node.Right);
            }
        }

        public static void PreOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                Visit(node);
                InOrderTraversal(node.Left);
                InOrderTraversal(node.Right);
            }
        }

        public static void Visit(TreeNode node)
        {
            Console.WriteLine(node.Name);
        }

        public static void Search(TreeNode root)
        {
            if (root == null) return;
            Visit(root);
            root.Visited = true;
            foreach(var node in root.Adjacent)
            {
                if (!node.Visited)
                    Search(node);
            }
        }

        public static void BfsSearch(TreeNode root)
        {
            var queue = new Queue<TreeNode>();
            root.Visited = true;
            queue.Enqueue(root);

            while(queue.Count > 0)
            {
                var node = queue.Dequeue();
                Visit(node);
                foreach (var nexNode in root.Adjacent)
                {
                    if (!nexNode.Visited)
                    {
                        nexNode.Visited = true;
                        queue.Enqueue(nexNode);
                    }
                }
            }
        }

        public static bool IsThereRouteToNode_Bfs(TreeNode root, TreeNode nodeToFind)
        {
            var queue = new Queue<TreeNode>();
            root.Visited = true;
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                Visit(node);
                foreach (var nexNode in root.Adjacent)
                {
                    if (nexNode == nodeToFind)
                        return true;
                    if (!nexNode.Visited)
                    {
                        nexNode.Visited = true;
                        queue.Enqueue(nexNode);
                    }
                }
            }
            return false;
        }

        public static bool IsThereRouteToNode_Bfs_v2(TreeNode root, TreeNode nodeToFind)
        {
            var queue = new Queue<TreeNode>();
            root.Visited = true;
            queue.Enqueue(root);
            queue.Enqueue(nodeToFind);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                Visit(node);
                foreach (var nexNode in root.Adjacent)
                {
                    if (nexNode == nodeToFind)
                        return true;
                    if (!nexNode.Visited)
                    {
                        nexNode.Visited = true;
                        queue.Enqueue(nexNode);
                    }
                }
            }
            return false;
        }

        private bool IsThereRouteToNode(TreeNode root, TreeNode nodeToFind)
        {
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                currentNode.Visited = true;
                foreach (var nexNode in currentNode.Adjacent)
                {
                    if (nexNode == nodeToFind)
                        return true;
                    if (!nexNode.Visited)
                    {
                        nexNode.Visited = true;
                        queue.Enqueue(nexNode);
                    }
                }
            }
            return false;
        }

    }
    #endregion


    #region TempTree

    public class MyTreeNode<T>
    {
        public T Data;
        public MyTreeNode<T> LeftChild;
        public MyTreeNode<T> RightChild;

        public MyTreeNode(T value)
        {
            Data = value;
            LeftChild = null;
            RightChild = null; 
        }

        public int GetHeight()
        {
            return Math.Max(GetTreeHeight(LeftChild), GetTreeHeight(RightChild));
        }

        public static int GetTreeHeight(MyTreeNode<T> node)
        {
            if (node == null) return 0;

            return 1 + Math.Max(GetTreeHeight(node.LeftChild), GetTreeHeight(node.RightChild));
        }

        public void BfsPrint()
        {
            var queue = new Queue<MyTreeNode<T>>();
            Console.Write(Data);

            queue.Enqueue(this);
            while(queue.Count > 0)
            {
                var current = queue.Dequeue();
                Console.Write($" {current.Data} ");

                if (current.LeftChild != null)
                    queue.Enqueue(current.LeftChild);
                if (current.RightChild != null)
                    queue.Enqueue(current.RightChild);
            }
        }

        public void BfsTraversal()
        {
            BfdAction(i => Console.Write($" {i} "));
        }

        private void BfdAction(Action<T> action)
        {
            var queue = new Queue<MyTreeNode<T>>();
            queue.Enqueue(this);

            BfdAction(action, queue);
        }

        private void BfdAction(Action<T> action, Queue<MyTreeNode<T>> queue)
        {
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                action(current.Data);

                if(current.LeftChild != null)
                    queue.Enqueue(current.LeftChild);
                if(current.RightChild != null)
                    queue.Enqueue(current.RightChild);
            }
        }
    }

    public class MyTree<T>
    {
        public MyTreeNode<T> Root;

        public int GetHeight()
        {
            return Root.GetHeight();
        }

        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversalAction(Root, action);
        }

        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversalAction(Root, action);
        }

        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversalAction(Root, action);
        }

        public static void PreOrderTraversalAction(MyTreeNode<T> treeNode, Action<T> action)
        {
            if (treeNode == null) return;

            action(treeNode.Data);
            PreOrderTraversalAction(treeNode.LeftChild, action);
            PreOrderTraversalAction(treeNode.RightChild, action);
        }

        public static void InOrderTraversalAction(MyTreeNode<T> treeNode, Action<T> action)
        {
            if (treeNode == null) return;

            InOrderTraversalAction(treeNode.LeftChild, action);
            action(treeNode.Data);
            InOrderTraversalAction(treeNode.RightChild, action);
        }

        public static void PostOrderTraversalAction(MyTreeNode<T> treeNode, Action<T> action)
        {
            if (treeNode == null) return;

            PostOrderTraversalAction(treeNode.LeftChild, action);
            PostOrderTraversalAction(treeNode.RightChild, action);
            action(treeNode.Data);
        }

        /**************** Helper methods *************/

        public void PrintTree()
        {
            var padding = 0;
            var height = GetHeight() + 1; //add 1 for root
            var maxLength = (int)Math.Pow(2, height) + padding;
            var treeMatrix = new List<List<char>>(height * 2); //Double height to show edges
            for (int lvl = 0; lvl < treeMatrix.Capacity; lvl++)
            {
                treeMatrix.Insert(lvl, Enumerable.Repeat('*', maxLength).ToList());
            }

            int startingMidPosition = maxLength / 2;
            InsertNodeValInMatrix(treeMatrix, Root, 0, 0, 1);

            //print matrix
            Console.WriteLine("Printed Tree");
            foreach (var lvlList in treeMatrix)
            {
                lvlList.ForEach(Console.Write);
                Console.WriteLine();
            }
        }

        private void InsertNodeValInMatrix(List<List<char>> matrix, MyTreeNode<T> node, int matrixLvl, int treeLvl, int pos)
        {
            if (node == null) return;

            int maxPositionsForLvl = (int)Math.Pow(2, treeLvl);
            if (matrixLvl < (matrix.Count - 2))
                maxPositionsForLvl++; //need for spacing
            int spacingPerPosition = matrix[treeLvl].Capacity / maxPositionsForLvl;
            int placeIdx = (pos * spacingPerPosition) - 1;

            matrix[matrixLvl][placeIdx] = node.Data.ToString()[0];

            if (node.LeftChild != null)
            {
                matrix[matrixLvl + 1][placeIdx - 1] = '/';
                InsertNodeValInMatrix(matrix, node.LeftChild, matrixLvl + 2, treeLvl + 1, (pos * 2) - 1);
            }
            if (node.RightChild != null)
            {
                matrix[matrixLvl + 1][placeIdx + 1] = '\\';
                InsertNodeValInMatrix(matrix, node.RightChild, matrixLvl + 2, treeLvl + 1, pos * 2);
            }
            //if(pos > 8)
            //{
            //    Console.WriteLine($"Pos {pos} (treeLvl {treeLvl}) use spacing of {spacingPerPosition} per pos. Curr plxIdx is {placeIdx}. Total cap is {matrix[treeLvl].Capacity}");
            //    Console.WriteLine($"maxPositionsForLvl {maxPositionsForLvl}");
            //}
        }


        //FOR TESTING
        public static MyTree<int> CreateTestTree()
        {
            var tree = new MyTree<int>();
            tree.Root = new MyTreeNode<int>(4);
            tree.Root.LeftChild = new MyTreeNode<int>(2);
            tree.Root.LeftChild.LeftChild = new MyTreeNode<int>(1);
            tree.Root.LeftChild.RightChild = new MyTreeNode<int>(3);
            tree.Root.LeftChild.RightChild.LeftChild = new MyTreeNode<int>(4);
            tree.Root.RightChild = new MyTreeNode<int>(6);
            tree.Root.RightChild.LeftChild = new MyTreeNode<int>(5);
            tree.Root.RightChild.RightChild = new MyTreeNode<int>(7);
            tree.Root.RightChild.RightChild.RightChild = new MyTreeNode<int>(8);
            tree.Root.RightChild.RightChild.RightChild.RightChild = new MyTreeNode<int>(9);
            return tree;
        }

        public static void RunTest()
        {
            var testTree = MyTree<int>.CreateTestTree();
            testTree.PreOrderTraversal(Console.Write);
            Console.Write("\n\n");
            testTree.InOrderTraversal(Console.Write);
            Console.Write("\n\n");
            testTree.PostOrderTraversal(Console.Write);

            var height = testTree.GetHeight();
            Console.WriteLine($"\n\nTree Height: {height}");
            testTree.PrintTree();
            Console.WriteLine("\n\n");
            testTree.Root.BfsTraversal();
        }
    }

    public class MyBst : MyTree<int>
    {
        public MyBst(IList<int> unsortedArr)
        {
            Root = new MyTreeNode<int>(unsortedArr[0]);

            for (int i = 1; i < unsortedArr.Count(); i++)
                Insert(unsortedArr[i]);
        }

        public void Insert(int data)
        {
            Insert(data, Root);
        }

        private void Insert(int data, MyTreeNode<int> node)
        {
            if(data < node.Data)
            {
                if (node.LeftChild == null)
                    node.LeftChild = new MyTreeNode<int>(data);
                else
                    Insert(data, node.LeftChild);
            } else
            {
                if (node.RightChild == null)
                    node.RightChild = new MyTreeNode<int>(data);
                else
                    Insert(data, node.RightChild);
            }
        }

        public new static void RunTest()
        {
            Console.WriteLine("Input: \n");
            var input = new List<int> { 3, 5, 9, 6, 2, 7, 4, 1 };
            input.ForEach(i => Console.Write($" {i},"));

            Console.WriteLine("BST from input: \n:");
            var testTree = new MyBst(input);
            testTree.PrintTree();
            Console.WriteLine("\nIn order traversal: \n");
            testTree.InOrderTraversal(i => Console.Write($" {i}"));
        }
    }

    #endregion
}

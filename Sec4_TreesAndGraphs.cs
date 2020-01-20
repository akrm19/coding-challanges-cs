using System;
using System.Collections.Generic;
using CrackingTheCodingInterviewProblems.Common;

namespace CrackingTheCodingInterviewProblems
{
    public static class Sec4_TreesAndGraphs
    {
        #region Prob_4_1_Route_Between_Nodes

        /// <summary>
        /// Probs the 4 1 route between nodes test.
        /// Given a direct graph, design an algorithm to find out
        /// whether there is a route between two nodes
        /// </summary>
        public static void Prob_4_1_RouteBetweenNodes_Test()
        {

        }

        private static bool Prob_4_1_IsThereRouteToNode(TreeNode root, TreeNode nodeToFind)
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

        #endregion

        #region Prob_4_2_MinimalTree

        /// <summary>
        /// Probs the 4 2 minimal tree test.
        /// Given a sorted (increasing order) array with unique
        /// integer elements, write an algorithm to create
        /// a binary search tree w/minimal height.
        /// </summary>
        public static void Prob_4_2_MinimalTree_Test()
        {

        }

        public static TreeNode BinarySearchTreeFrom(int[] input)
        {
            if (input.Length == 0) return null;
            if (input.Length == 1) return new TreeNode(input[0]);


            var midPoint = input.Length / 2;
            var root = new TreeNode(input[midPoint]);

            var leftSubArrayLength = input.Length - midPoint;
            root.Left = BinarySearchTreeFrom(GetSubArray(input, 0, leftSubArrayLength));

            var rightSubArrayStartingIndex = midPoint + 1;
            var rightSubArrayLength = input.Length - rightSubArrayStartingIndex;
            if (rightSubArrayLength > 0)
                root.Right = BinarySearchTreeFrom(GetSubArray(input, rightSubArrayStartingIndex, rightSubArrayLength));

            return root;
        }

        private static int[] GetSubArray(int[] source, int startingIndex, int lenght)
        {
            var subArray = new int[lenght];
            Array.Copy(source, startingIndex, subArray, 0, lenght);
            return subArray;
        }

        public static TreeNode Prob_4_2_MinimalTree_BookSolution(int[] array)
        {
            return Prob_4_2_MinimalTree_BookSolution(array, 0, array.Length - 1);
        }

        private static TreeNode Prob_4_2_MinimalTree_BookSolution(int[] array, int start, int end)
        {
            if (end < start)
                return null;

            var midPoint = (start + end) / 2;
            TreeNode root = new TreeNode(array[midPoint]);
            root.Left = Prob_4_2_MinimalTree_BookSolution(array, start, midPoint - 1);
            root.Right = Prob_4_2_MinimalTree_BookSolution(array, midPoint + 1, end);

            return root;
        }
        #endregion

        #region Prob_4_3_ListOfDepths

        /// <summary>
        /// Probs the 4 3 list of depths test.
        /// Given a binary tree, design an algorithm which creates a linked
        /// list of all the nodes at each depth (e.g. if you have a tree
        /// w/depth D you'll have D linked Lists)
        /// </summary>
        public static void Prob_4_3_ListOfDepths_Test()
        {

        }

        public static List<LinkedNode<int>> Prob_4_3_ListOfDepths_v1(TreeNode root)
        {
            var listOfLinkedNodes = new List<LinkedNode<int>>();
            //var queue = new MyQueue<TreeNode>();
            //queue.Add(root);

            //while(!queue.IsEmpty())
            //{
            //var current = queue.Remove();
            //AddToLinkedList(current, listOfLinkedNodes, 0);
            //}

            AddToLinkedList(root, listOfLinkedNodes, 0);
            return listOfLinkedNodes;
        }

        private static List<LinkedNode<int>> AddToLinkedList(TreeNode root, List<LinkedNode<int>> list, int currentLevel = 0)
        {
            LinkedNode<int> currentList;
            if (list.Count == currentLevel)
            {
                currentList = new LinkedNode<int>();
                list.Add(currentList);
            }
            else
                currentList = list[currentLevel];

            AddToLinkedList(root, currentList);
            //root.Visited = true;

            if (root.Left != null)
                AddToLinkedList(root.Left, list, currentLevel + 1);
            if (root.Right != null)
                AddToLinkedList(root.Right, list, currentLevel + 1);

            return list;
        }

        private static LinkedNode<int> AddToLinkedList(TreeNode root, LinkedNode<int> list)
        {
            if (root == null)
                return null;

            if (list == null)
                return new LinkedNode<int>(root.Value);

            list.AppendToEnd(root.Value);
            return list;
        }


        #endregion

        #region Prob_4_4_CheckIsBalanced
        /// <summary>
        /// Probs the 4 4 check is balanced test.
        /// Implement a function to check if a binary tree is balanced.
        /// For the purposes of this question, a balanced tree is defined
        /// to be a tree such that the heights of the two subtrees of any
        /// node never differ by more than one
        /// </summary>
        public static void Prob_4_4_CheckIsBalanced_Test()
        {

        }

        public static bool Prob_4_4_CheckIsBalanced_V1(TreeNode root)
        {
            return GetHeightForNode(root) != int.MinValue;
        }

        private static int GetHeightForNode(TreeNode node)
        {
            if(node == null || (node.Left == null && node.Right == null))
                return 0;

            if (node.Left == null)
                return GetHeightForNode(node.Right);
            if (node.Right == null)
                return GetHeightForNode(node.Left);

            var leftNodeHeight = GetHeightForNode(node.Left);
            var rightNodeHeight = GetHeightForNode(node.Right);

            var diff = Math.Abs(leftNodeHeight - rightNodeHeight);
            if(diff > 1)
            {
                return int.MinValue;
            }

            return leftNodeHeight > rightNodeHeight
                ? leftNodeHeight
                : rightNodeHeight;
        }


        public static bool Prob_4_4_CheckIsBalanced_BookSolution(TreeNode root)
        {
            return Prob_4_4_CheckIsBalanced_BookSolution_GetHeightForNode(root) != int.MinValue;
        }

        private static int Prob_4_4_CheckIsBalanced_BookSolution_GetHeightForNode(TreeNode node)
        {
            if (node == null)
                return  -1;

            var leftNodeHeight = GetHeightForNode(node.Left);
            if (leftNodeHeight == int.MinValue)
                return int.MinValue;

            var rightNodeHeight = GetHeightForNode(node.Right);
            if (rightNodeHeight == int.MinValue)
                return int.MinValue;

            var diff = Math.Abs(leftNodeHeight - rightNodeHeight);
            if (diff > 1)
                return int.MinValue;
                
            return Math.Max(leftNodeHeight, rightNodeHeight);
        }
        #endregion


        #region Prob_4_5_Validate_BST

        public static void Prob_4_5_ValidateBst_Test(TreeNode root)
        {

        }

        public static bool Prob_4_5_IsBst(TreeNode node)
        {
            if (node == null) return true;
            if (node.Adjacent.Length > 2) return false;

            if(node.Left != null && node.Left.Value > node.Value)
                return false;

            if (node.Right != null && node.Right.Value < node.Value)
                return false;

            return Prob_4_5_IsBst(node.Right) && Prob_4_5_IsBst(node.Left);
        }

        public static bool Prob_4_5_ValidateBst_BookSolution(TreeNode  root)
        {
            return Prob_4_5_ValidateBst_BookSolution(root, int.MinValue, int.MinValue);
        }

        public static bool Prob_4_5_ValidateBst_BookSolution(TreeNode node, int min, int max)
        {
            if (node == null) return true;

            if ((min != int.MinValue && node.Value <= min) || (max != int.MinValue && node.Value > max))
                return false;

            if (!Prob_4_5_ValidateBst_BookSolution(node.Left, min, node.Value) 
                || !Prob_4_5_ValidateBst_BookSolution(node.Right, node.Value, max))
                return false;

            return true;
        }

        #endregion
    }

    public class Node
    {
        public string Name { get; set; }
        public Node[] Children { get; set; }
    }
}

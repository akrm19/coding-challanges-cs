using System;
using System.Collections.Generic;
using System.Linq;

namespace CrackingTheCodingInterviewProblems
{
    public class Sec4_v2
    {
        public Sec4_v2()
        {
            var test = new LinkedList<MyTreeNode<int>>();
            //test.AddFirst()
            
            //var testTreeNode = new MyTreeNode<int>(2);

        }
    }


    public class MyTree<T>
    {
        MyTreeNode<T> Root;

        public static MyTreeNode<int> S4_2_MinimalTree(int[] arr)
        {
            return S4_2_MinimalTree(arr, 0, arr.Length - 1);
        }

        public static MyTreeNode<int> S4_2_MinimalTree(int[] arr, int start, int end)
        {
            if (start > end) return null;
            if (start == end) return new MyTreeNode<int>(arr[start]);

            var midPoint = (start + end) / 2;
            var root = new MyTreeNode<int>(midPoint);
            root.LeftChild = S4_2_MinimalTree(arr, start, midPoint - 1);
            root.RightChild = S4_2_MinimalTree(arr, midPoint + 1, end);
            return root;
        }

        public static MyTreeNode<int> S4_2_MinimalTree2(int[] arr)
        {
            if (arr.Length == 0)
                return null;
            if (arr.Length == 1)
                return new MyTreeNode<int>(arr[0]);

            var mid = arr.Length / 2;
            var root = new MyTreeNode<int>(arr[mid]);

            root.LeftChild = S4_2_MinimalTree2(arr.Take(mid).ToArray());
            root.RightChild = S4_2_MinimalTree2(arr.Skip(mid).ToArray());

            return root;
        }

        public static List<LinkedList<TreeNode>> S4_3_ListOfDepths(TreeNode root)
        {
            //Create a List of LinkedList, a new entry for each level
            var list = new List<LinkedList<TreeNode>>();

            //Pass list & child nodes & current level to new method
            S4_3_ListOfDepths(root.LeftChild, list, 0);

            return list;
        }

        public static void S4_3_ListOfDepths(TreeNode root, List<LinkedList<TreeNode>> list, int lvl)
        {
            if (root == null) return;

            ///Check if list contains current level
            //Add new item if needed
            while (list.Count < (lvl + 1))
                list.Add(new LinkedList<TreeNode>());

            //Add root to current lvl
            list[lvl].AddFirst(root);

            //Recursively self on child elements (increasing lvl by one)
            S4_3_ListOfDepths(root.LeftChild, list, lvl + 1);
            S4_3_ListOfDepths(root.RightChild, list, lvl + 1);
        }

        public static bool S4_4_IsBalanced(TreeNode root)
        {
            //base case
            if (root == null) return true;

            //get height of left child
            var l = S4_4_GetHeight(root.LeftChild, 0);

            //get height of right child
            var r = S4_4_GetHeight(root.RightChild, 0);

            //return false if diff is > 1
            return Math.Abs(l - r) > 1
                ? false
                : S4_4_IsBalanced(root.LeftChild) && S4_4_IsBalanced(root.RightChild);
        }

        public static int S4_4_GetHeight(TreeNode node, int currH)
        {
            if (node == null) return currH;

            return Math.Max(S4_4_GetHeight(node.LeftChild, currH + 1), S4_4_GetHeight(node.RightChild, currH + 1));
        }

        /*
         Implement a function to check if a binary tree is a binary search tree.
        */
        public static bool S4_5_ValidateBst(TreeNode root)
        {
            //base
            if (root == null) return true;

            //Check if leftChild & rightChild are valid
            if ((root.LeftChild != null && root.LeftChild.Data > root.Data)
               || (root.RightChild != null && root.RightChild.Data < root.Data))
                return false;

            //If not return true
            return S4_5_ValidateBst(root.LeftChild) && S4_5_ValidateBst(root.RightChild);
        }

        public class TreeNode
        {
            public int Data;
            public TreeNode LeftChild;
            public TreeNode RightChild;
            public TreeNode(int val)
            {
                Data = val;
            }
        }
    }

    public class MyTreeNode<T>
    {
        public T Data;
        public MyTreeNode<T> LeftChild;
        public MyTreeNode<T> RightChild;

        public MyTreeNode(T val)
        {
            Data = val;
        }
    }
}

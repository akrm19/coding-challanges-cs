using System;
using System.Collections.Generic;

namespace CrackingTheCodingInterviewProblems.Common
{
    public class LinkedNode<T>
    {
        public LinkedNode<T> nextNode;
        public T data;

        public LinkedNode()
        {

        }

        public LinkedNode(T initData)
        {
            data = initData;
        }

        public void AppendToEnd(T valueToAppend)
        {
            AppendToEnd(new LinkedNode<T>(valueToAppend));
        }

        public void AppendToEnd(LinkedNode<T> nodeToApped)
        {
            LinkedNode<T> currentNode = this;

            while (currentNode.nextNode != null)
            {
                currentNode = nextNode;
            }

            currentNode.nextNode = nodeToApped;
        }

        public LinkedNode<T> GetFirstNodeWithValue(T value)
        {
            LinkedNode<T> currenNode = this;

            while (currenNode != null)
            {
                if (currenNode.data.Equals(value))
                    return currenNode;

                currenNode = currenNode.nextNode;
            }

            Console.WriteLine($"Did not find {value}. Returning.");
            return this;
        }

        public void PrintValues()
        {
            Console.WriteLine("Writing values in Linked Node");
            LinkedNode<T> currentNode = this;

            do
            {
                Console.WriteLine(currentNode.data);
                currentNode = currentNode.nextNode;
            }
            while (currentNode != null);

            Console.WriteLine("DONE writing values for Linked Node");
        }

        public bool IsLower(LinkedNode<T> nodeToCompare)
        {
            if (typeof(T) == typeof(int))
                return Convert.ToInt32(data) < Convert.ToInt32(nodeToCompare.data);

            return false;
        }

        public static LinkedNode<T> DeleteNodeWithValue(LinkedNode<T> head, T valueToDelete)
        {
            LinkedNode<T> currenNode = head;

            //move head node 
            if (currenNode.data.Equals(valueToDelete))
                return currenNode.nextNode;

            while(currenNode.nextNode != null)
            {
                if(currenNode.nextNode.data.Equals(valueToDelete))
                {
                    currenNode.nextNode = currenNode.nextNode.nextNode;
                    return head;
                }
                currenNode = currenNode.nextNode;
            }

            return head;
        }

        public static LinkedNode<T> GetFirstNodeWithValue(LinkedNode<T> head, T value)
        {
            LinkedNode<T> currenNode = head;

            while (currenNode != null)
            {
                if (currenNode.data.Equals(value))
                    return currenNode;

                currenNode = currenNode.nextNode;
            }

            Console.WriteLine($"Did not find {value}. Returning original node");
            return head;
        }

        public LinkedNode<T> GetNodeXPositionsFromCurrent(int positionsToAdvance)
        {
            var result = this;
            int currentPosition = 0;

            while (result.nextNode != null && currentPosition < positionsToAdvance)
            {
                result = result.nextNode;
                currentPosition++;
            }

            return result;
        }

        //public static LinkedNode<T> GetNodeXPositionsFromCurrent(LinkedNode<T> head, int positionsToAdvance)
        //{
        //    var result = head;
        //    int currentPosition = 0;

        //    while(result.nextNode != null && currentPosition < positionsToAdvance)
        //    {
        //        result = result.nextNode;
        //        currentPosition++;
        //    }

        //    return result;
        //}

        public static LinkedNode<char> CreateNodeFromString(string input)
        {
            Console.WriteLine($"Creating Linked node from {input}");
            var headNode = new LinkedNode<char>(input[0]);
            var currentNode = headNode;

            for(int i = 1; i < input.Length; i++)
            {
                var newNode = new LinkedNode<char>(input[i]);
                currentNode.nextNode = newNode;
                currentNode = newNode;
            }

            return headNode;
        }

        public static LinkedNode<T> CreateNodesFromList2(T[] input)
        {
            Console.WriteLine($"Creating Linked node from {input.ToString()}");
            LinkedNode<T> headNode = new LinkedNode<T>(input[0]);
            LinkedNode<T> currentNode = headNode;

            for (int i = 1; i < input.Length; i++)
            {
                var newNode = new LinkedNode<T>(input[i]);
                currentNode.nextNode = newNode;
                currentNode = currentNode.nextNode;
            }

            return headNode;
        }

        public static LinkedNode<T> CreateNodesFromList(List<T> input)
        {
            Console.WriteLine($"Creating Linked node from {input.ToString()}");
            LinkedNode<T> headNode = new LinkedNode<T>(input[0]);
            LinkedNode<T> currentNode = headNode;

            for (int i = 1; i < input.Count; i++)
            {
                var newNode = new LinkedNode<T>(input[i]);
                currentNode.nextNode = newNode;
                currentNode = currentNode.nextNode;
            }

            return headNode;
        }
    }
}

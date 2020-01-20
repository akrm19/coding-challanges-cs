using System;
using CrackingTheCodingInterviewProblems.Common;

namespace CrackingTheCodingInterviewProblems
{
    public static class Sec3_StackQueues
    {
        #region Prob_3_2_StackMin

        /// <summary>
        /// Probs the 3 2 stack minimum test.
        /// How would you design a stack which in addition
        /// to push and pop, has a function min, which 
        /// returns the minimum element? Push, pop, and min
        /// should all operate in O(1) time
        /// </summary>
        public static void Prob_3_2_StackMin_Test()
        {
            // Implemented in MyStackWithMin
        }

        #endregion

        #region Prob_3_3_StackOfPlates

        /// <summary>
        /// Imagine a (literal) stack of plates. If the stack gets too
        /// high, it might topple. Therefore, in real life, we woud likely
        /// start a new stack when the previous stack exceeds some threshold.
        /// Implement a data structure SetOfStacks that mimics this.
        /// SetOfStacks should be composed of several stacks and should create
        /// a new stack once the previous one exceeds capacity. 
        /// SetOfStacks.Push() and SetOfStacks.Pop() should behave identitcally
        /// to a single stack (that is, pop() should return the same values as
        /// it would if there were just a single stack).
        /// 
        /// Follow Up:
        /// Implement a function popAt(int index) which performs a pop operation
        /// on a specific sub-stack
        /// </summary>
        public static void Prob_3_3_StackOfPlates_Test()
        {
            // Implemented in MyStackWithMin
        }

        #endregion

        #region Prob_3_5_SortStack

        public static void Prob_3_5_SortStack_Test()
        {
            var unsortedNums = new int[] { 1, 20, 4, 2, 6, 9, 1, 3, 5, 6 };
            var unsortedStack = new MyStack<int>();

            Console.Write("Unsorted numbers: ");
            foreach (var num in unsortedNums)
            {
                unsortedStack.Push(num);
                Console.Write($" {num}");
            }

            Console.WriteLine($"{Environment.NewLine}Sorted Stack: ");
            //var sortedArray = unsortedStack.SortStack();
            var sortedArray = unsortedStack.SortStack_BookSolution();

            while (!sortedArray.IsEmpty())
                Console.Write($" {sortedArray.Pop()}");
        }

        /// <summary>
        /// Sorts the stack.
        /// Write a program to sort a stack such that the
        /// smallest items are on the top. You can use an 
        /// additional temporary stack, but you may not
        /// copy the elements into any other data structure
        /// (such an array). The stack suppors the following
        /// operations: push, pop, peek, and isEmpty
        /// </summary>
        /// <returns>The stack.</returns>
        /// <param name="queue">Queue.</param>
        public static MyStack<int> SortStack(this MyStack<int> queue)
        {
            var sortedStack = new MyStack<int>();

            while(!queue.IsEmpty())
            {
                var itemToPlace = queue.Pop();
                var sorted = false;

                while(!sorted)
                {
                    if(sortedStack.IsEmpty() || sortedStack.Peek() >= itemToPlace)
                    {
                        sortedStack.Push(itemToPlace);
                        sorted = true;
                        break;
                    }

                    var nextNodeInSortedStack = sortedStack.Pop();
                    queue.Push(nextNodeInSortedStack);
                }
            }

            return sortedStack;
        }

        public static MyStack<int> SortStack_BookSolution(this MyStack<int> queue)
        {
            var r = new MyStack<int>();

            while (!queue.IsEmpty())
            {
                var itemToPlace = queue.Pop();

                while (!r.IsEmpty() || r.Peek() >= itemToPlace)
                    queue.Push(r.Pop());

                r.Push(itemToPlace);
            }

            while (!r.IsEmpty())
                queue.Push(r.Pop());

            return r;
        }

        #endregion

        #region Prob_3_6_Animal_Shelter

        /// <summary>
        /// Probs the 3 6 animal shelter test.
        /// An animal shelter
        /// </summary>
        public static void Prob_3_6_Animal_Shelter_Test()
        {
            // implemented in MyQueue.AnimalShelter classs
        }

        #endregion
    }
}

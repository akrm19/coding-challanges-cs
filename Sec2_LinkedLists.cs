using System;
using System.Collections;
using System.Collections.Generic;
using CrackingTheCodingInterviewProblems.Common;

namespace CrackingTheCodingInterviewProblems
{
    public static class Sec2_LinkedLists
    {
        /// <summary>
        /// Probs the 2 1 remove dups test.
        /// Write code to remove duplicates from an unsorted list
        /// </summary>
        public static void Prob_2_1_RemoveDups_Test()
        {
            var testInput = LinkedNode<char>.CreateNodeFromString("FOLLOW UP");
            Console.WriteLine($"Input:");
            testInput.PrintValues();

            var expectedOutput = LinkedNode<char>.CreateNodeFromString("FOLW UP");
            Console.WriteLine($"Expected results:");
            expectedOutput.PrintValues();

            //var output = Pro2_1_RemoveDupsv1(testInput);
            var output = Pro_2_1_RemoveDupsv1(testInput);


            Console.WriteLine($"Actual results:");
            output.PrintValues();
        }

        /// <summary>
        /// Pro2s the 1 remove dupsv1.
        /// Write code to remove duplicates from an unsorted list
        /// Solution: More efficient, removed dups from current linked
        /// list w/out creating  a copy
        /// </summary>
        /// <returns>The 1 remove dupsv1.</returns>
        /// <param name="input">Input.</param>
        public static LinkedNode<char> Pro_2_1_RemoveDupsv1(LinkedNode<char> input)
        {
            var currentData = input.data;

            var occuranceTable = new Hashtable();
            occuranceTable.Add((int)currentData, true);

            //var headNode = input;
            var currentNode = input;

            while (currentNode != null && currentNode.nextNode != null)
            {
                var nextNodesData = currentNode.nextNode.data;

                if (occuranceTable.ContainsKey((int)nextNodesData))
                {
                    currentNode.nextNode = currentNode.nextNode.nextNode;
                    Console.WriteLine($"Skipping dup data of {nextNodesData}");
                }
                else
                {
                    occuranceTable.Add((int)nextNodesData, true);
                    currentNode = currentNode.nextNode;
                }
            }

            return input;
        }

        public static LinkedNode<char> Pro_2_1_RemoveDups_bookSolution(LinkedNode<char> input)
        {
            var occuranceTable = new Hashtable();
            LinkedNode<char> previousNode = null;

            while (input != null)
            {
                if (occuranceTable.ContainsKey(input.data))
                {
                    previousNode.nextNode = input.nextNode;
                    Console.WriteLine($"Skipping dup data of {input.data}");
                }
                else
                {
                    occuranceTable.Add(input.data, true);
                    previousNode = input;
                }
                input = input.nextNode;
            }

            return input;
        }



        /// <summary>
        /// Pro2s the 1 remove dupsv2.
        /// Write code to remove duplicates from an unsorted list
        /// Solution: Creates a new linekd list 
        /// </summary>
        /// <returns>The 1 remove dupsv1.</returns>
        /// <param name="input">Input.</param>
        public static LinkedNode<char> Pro_2_1_RemoveDupsv2(LinkedNode<char> input)
        {
            var currentData = input.data;

            var occuranceTable = new Hashtable();
            occuranceTable.Add((int)currentData, true);

            var headNode = new LinkedNode<char>(currentData);
            var currentNode = input.nextNode;
            var currentFilteredNode = headNode;

            while (currentNode != null)
            {
                currentData = currentNode.data;

                if (!occuranceTable.ContainsKey((int)currentData))
                {
                    occuranceTable.Add((int)currentData, true);

                    var newNode = new LinkedNode<char>(currentData);
                    currentFilteredNode.nextNode = newNode;
                    currentFilteredNode = currentFilteredNode.nextNode;
                }
                else
                    Console.Write($"Skipping dup data of {currentData}");

                currentNode = currentNode.nextNode;
            }

            return headNode;
        }


        #region Prob2_2_ReturnKthToTheLast

        /// <summary>
        /// Prob2s the 2 return kth to the last test.
        /// Implement an algorith to find the kth to the last element 
        /// og a silgly linked list
        /// </summary>
        public static void Prob_2_2_ReturnKthToTheLast_Test()
        {
            var inputString = "123456789";
            var input = LinkedNode<char>.CreateNodeFromString(inputString);
            var kthToLastElementNum = 7;
            ///var expectedOutput = '7';

            //var kthToLastNode = Prob2_2_ReturnKthToTheLast(input, kthToLastElementNum);
            var kthToLastNode = Prob_2_2_ReturnKthToTheLast_v2(input, kthToLastElementNum);


            Console.WriteLine("Input: ");
            input.PrintValues();

            Console.WriteLine($"{kthToLastElementNum} to last element is {inputString[(inputString.Length -1) - kthToLastElementNum]}");
            Console.WriteLine($"Actual output: {kthToLastNode.data}");
        }


        /// <summary>
        /// Prob2s the 2 return kth to the last.
        /// Implement an algorithm to find the kth to the last element 
        /// on a singly linked list
        /// </summary>
        /// <returns>The 2 return kth to the last.</returns>
        /// <param name="linkedNode">Linked node.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static LinkedNode<T> Prob_2_2_ReturnKthToTheLast<T>(LinkedNode<T> linkedNode, int kthToLastElement)
        {
            var slowPointer = linkedNode;
            var slowPointersPosition = 1;
            var fastPointer = linkedNode.nextNode;
            var fastPointersPosition = 2;

            while(fastPointer.nextNode != null)
            {
                slowPointer = slowPointer.nextNode;
                slowPointersPosition++;

                if(fastPointer.nextNode.nextNode != null)
                {
                    fastPointer = fastPointer.nextNode.nextNode;
                    fastPointersPosition += 2;
                } else
                {
                    fastPointer = fastPointer.nextNode;
                    fastPointersPosition++;
                }
            }

            var linkedListLenght = fastPointersPosition;
            var positionOfNodeToRemove = linkedListLenght - kthToLastElement;
            var slowPointerIsBeforeTheKthElement = slowPointersPosition < positionOfNodeToRemove;
            Console.WriteLine($"The lenght of the node is {fastPointersPosition}. " +
                $"The position of the node we want to retrieve is {positionOfNodeToRemove}. " +
                $"The slow pointer is at position {slowPointersPosition}. " +
            	$"Is the slowpointer before the node we want to retrieve: {slowPointerIsBeforeTheKthElement}");


            if (slowPointerIsBeforeTheKthElement)
            {
                while(slowPointersPosition != positionOfNodeToRemove)
                {
                    slowPointer = slowPointer.nextNode;
                    slowPointersPosition++;
                }

                Console.WriteLine($"Found the node at position {slowPointersPosition}. Value {slowPointer.data}");
                return slowPointer;
            }
            else
            {
                Console.WriteLine("Resetting slow pointer");
                slowPointer = linkedNode;
                slowPointersPosition = 1;

                while(slowPointersPosition != positionOfNodeToRemove)
                {
                    slowPointer = slowPointer.nextNode;
                    slowPointersPosition++;
                }

                Console.WriteLine($"Found the node at position {slowPointersPosition}. Value {slowPointer.data}");
                return slowPointer;
            }
        }

        /// <summary>
        /// Prob2s the 2 return kth to the last.
        /// Implement an algorithm to find the kth to the last element 
        /// on a singly linked list
        /// </summary>
        /// <returns>The 2 return kth to the last.</returns>
        /// <param name="linkedNode">Linked node.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static LinkedNode<T> Prob_2_2_ReturnKthToTheLast_v2<T>(LinkedNode<T> linkedNode, int kthToLastElement)
        {
            var currentNode = linkedNode;
            var linkedNodeLenght = 1;

            while(currentNode.nextNode != null)
            {
                currentNode = currentNode.nextNode;
                linkedNodeLenght++;
            }

            var positionOfKthElement = linkedNodeLenght - kthToLastElement;
            Console.WriteLine($"Linked node lenght: {linkedNode}. Position of {kthToLastElement} to last element: {positionOfKthElement}");

            var currentPosition = 1;
            currentNode = linkedNode;

            if (currentPosition == positionOfKthElement)
                return currentNode;

            while(currentNode.nextNode != null)
            {
                currentNode = currentNode.nextNode;
                currentPosition++;

                if(currentPosition == positionOfKthElement)
                {
                    Console.WriteLine($"Found {positionOfKthElement} node. Value {currentNode.data}");
                    return currentNode;
                }
            }

            Console.WriteLine("Did not find the node. Returning original node");
            return linkedNode;
        }

        public static LinkedNode<T> Prob_2_2_ReturnKthToTheLast_bookSolution<T>(LinkedNode<T> linkedNode, int kthToLastElement)
        {
            var currentNode = linkedNode;
            var scoutNode = linkedNode;

            //Move scount node kthToLastElement positions forward
            for (int i = 0; i < kthToLastElement; i++)
            {
                if (scoutNode == null) return null;
                scoutNode = scoutNode.nextNode;
            }

            while(scoutNode != null)
            {
                scoutNode = scoutNode.nextNode;
                currentNode = currentNode.nextNode;
            }

            return currentNode;
        }

        #endregion

        #region Prob2_2_DeleteMiddleNode

        /// <summary>
        /// Prob2s the 2 delete middle node test.
        /// Implement an algorithm to delete a node in the middle (i.e. any node
        /// but the first and last node, not necessarely the exact middle) of a 
        /// singly linked list WHEN GIVEN only access to THAT node. Example:
        /// 
        /// Input: The node C (from the linked list a->b->c->d->e->f
        /// Result: Nothing is returned but the new linked list looks like: a->b->d->e->f
        ///         
        /// </summary>
        public static void Prob_2_3_DeleteMiddleNode_test()
        {
            var input = LinkedNode<char>.CreateNodeFromString("abcdef");
            Console.WriteLine("Input before changes:");
            input.PrintValues();

            var nodeToRemove = input.GetFirstNodeWithValue('c');
            Console.WriteLine($"Vallue to remove {nodeToRemove.data}");
            Prob_2_3_DeleteMiddleNode_v1(nodeToRemove);

            Console.WriteLine("Input AFTER changes:");
            input.PrintValues();
        }

        public static void Prob_2_3_DeleteMiddleNode_v1<T>(LinkedNode<T> middleNode)
        {
            var nextNode = middleNode.nextNode;

            if(nextNode != null)
            {
                middleNode.data = nextNode.data;
                middleNode.nextNode = nextNode.nextNode;
            }
        }

        #endregion

        #region Prob_2_4_Partition

        /// <summary>
        /// Probs the 2 4 partition test.
        /// Write code to partition a linked list around a value X, such that all
        /// nodes less than X come before all nodes greater than or equal to X. 
        /// If X is contained within the list, the values of X only need to be
        /// after the elements than X . The partition element X can appear anywhere 
        /// in the "right partition"; it does not need to appear between the left 
        /// and right partitions. (so X can also be before values greater than X
        /// as long as those greater values come after the values less than X, see the
        /// example below, where 10 comes before 5, but after all values below 5).
        /// 
        /// Example: Partition is 5
        /// Input:  3->5->8->5->10->2->1  
        /// Output: 3->1->2->10->5->5->8
        /// </summary>
        public static void Prob_2_4_Partition_Test()
        {
            //var inputArray = new int[] { 3, 5, 8, 5, 10, 2, 1 };
            var inputArray = new int[] { 5, 10,3, 5, 8, 5, 10, 2, 1, 5 };

            var input = LinkedNode<int>.CreateNodesFromList2(inputArray);
            var partition = 5;

            Console.WriteLine($"Partition: {partition}. Input: ");
            input.PrintValues();

            var output = Prob_2_4_Partition_v1(input, partition);

            Console.WriteLine($"All numbers small than {partition} should be first.");
            Console.WriteLine($"Actual output:");
            output.PrintValues();
        }

        public static LinkedNode<int> Prob_2_4_Partition_v1(LinkedNode<int> head, int partitionNum)
        {
            LinkedNode<int> prePivot = null;
            var runner = head;

            //Needed if first node is equal or grater than partition.
            //We swipe the first node smaller to the parititon to the head.
            if(runner.data >= partitionNum)
            {
                Console.WriteLine($"First node {runner.data} is equal or greater than partition {partitionNum}");
                while(runner.nextNode != null && prePivot == null)
                {
                    Console.WriteLine($"Comparing next node {runner.nextNode.data} to {partitionNum}");
                    //If we find a smaller node, we set it as the prepivot
                    if (runner.nextNode.data < partitionNum)
                    {
                        var nodeToMoveBack = runner.nextNode;
                        Console.WriteLine($"Next node {nodeToMoveBack.data} is smaller than parition. Moving");
                        runner.nextNode = runner.nextNode.nextNode;

                        nodeToMoveBack.nextNode = head;
                        head = nodeToMoveBack;
                        prePivot = nodeToMoveBack;

                        Console.WriteLine($"New head {head.data}");
                    }
                    else
                        runner = runner.nextNode;
                }
            }

            //Continue with runner
            while(runner.nextNode != null)
            {
                if (prePivot == null && runner.nextNode.data >= partitionNum)
                    prePivot = runner;
                else if (prePivot != null && runner.nextNode.data < partitionNum)
                {
                    var nodeToMoveBack = runner.nextNode;
                    runner.nextNode = runner.nextNode.nextNode;

                    nodeToMoveBack.nextNode = prePivot.nextNode;
                    prePivot.nextNode = nodeToMoveBack;
                }
                else
                    runner = runner.nextNode;
            }

            return head;
        }

        public static LinkedNode<int> Prob_2_4_Partition_BookSolution(LinkedNode<int> node, int pivot)
        {
            LinkedNode<int> beforeStart = null;
            LinkedNode<int> afterStart = null;

            /* Partition list */
            while (node != null)
            {
                var next = node.nextNode;

                if (node.data < pivot)
                {
                    /* Insert node into start of before list */
                    node.nextNode = beforeStart;
                    beforeStart = node;
                }
                else
                {
                    /* Insert node into front of after list */
                    node.nextNode = afterStart;
                    afterStart = node;
                }
                node = next;
            }

            /* Merge before list and after list */
            if (beforeStart == null)
            {
                return afterStart;
            }

            var head = beforeStart;

            while (beforeStart.nextNode != null)
            {
                beforeStart = beforeStart.nextNode;
            }

            beforeStart.nextNode = afterStart;

            return head;
        }


        #endregion

        #region Prob_2_5_Sum_List


        /// <summary>
        /// Probs the 2 5 sum list test.
        /// You have two numbers represented by a linked list. Where each node
        /// contains a single digit. The digits are stored in REVERSE order, such 
        /// that 1's digit is at the head of the list. Write a function that adds
        /// the two numbers and returns the sum as a linked list.
        /// 
        /// Example: 1
        /// Input: (7->1->6) + (5->9->2)  Which is 617 + 295
        /// Output 2->1->9   that is 912
        /// </summary>
        public static void Prob_2_5_SumList_ReverOrder_Test()
        {
            var input1 = new int[] {7,1,6 };
            var input2 = new int[] { 5,9,2 };


            var number1 = LinkedNode<int>.CreateNodesFromList2(input1);
            var number2 = LinkedNode<int>.CreateNodesFromList2(input2);

            var expectedResult = new int[] { 2, 1, 9 };

            Console.WriteLine($"Input: 1");
            number1.PrintValues();
            Console.WriteLine($"Input: 2");
            number2.PrintValues();

            Console.WriteLine($"Expected output {expectedResult.ToString()}");
            var t =  LinkedNode<int>.CreateNodesFromList2(expectedResult);
            t.PrintValues();
            var output = AddTwoReservedNumsAsLists(number1, number2);

            Console.WriteLine($"Actual output:");
            output.PrintValues();
        }

        public static LinkedNode<int> AddTwoReservedNumsAsLists(LinkedNode<int> num1, LinkedNode<int> num2)
        {
            LinkedNode<int> resultHeadNode = null;
            LinkedNode<int> currentResult = null;

            var currentNum1 = num1;
            var currentNum2 = num2;
            var currentRemainder = 0;

            while(currentNum1 != null)
            {
                var sum = currentNum1.data + currentNum2.data + currentRemainder;
                Console.WriteLine($"Sum is {sum}:  Num1({currentNum1.data}) + Num2({currentNum2.data}) + Remainder({currentRemainder})");


                //currentRemainder = sum % 10;
                //int firstDigit = sum >= 10
                //? (sum / 10)
                //: sum;

                currentRemainder = (sum / 10);
                int firstDigit = sum >= 10
                ? sum % 10
                : sum;

                var newNode = new LinkedNode<int>(firstDigit);
                if (resultHeadNode == null)
                {
                    resultHeadNode = newNode;
                    currentResult = resultHeadNode;
                }
                else
                {
                    currentResult.nextNode = newNode;
                    currentResult = newNode;
                }

                //var newNode = new LinkedNode<int>(firstDigit);
                //if (resultHeadNode == null)
                //    resultHeadNode = newNode;
                //else
                //{
                //    newNode.nextNode = resultHeadNode;
                //    resultHeadNode = newNode;
                //}

                Console.WriteLine($"The first digit is {firstDigit} and the remainder is {currentRemainder}");
                currentNum1 = currentNum1.nextNode;
                currentNum2 = currentNum2.nextNode;
            }

            return resultHeadNode;
        }

        #endregion

        #region Prob_2_6_IsPalindrome

        /// <summary>
        /// Probs the 2 6 is palindrome.
        /// Implement a function to check if a linked list is a Palindrome
        /// </summary>
        public static void Prob_2_6_IsPalindrome_Test()
        {
            var inputText = "tacocat";
            //var inputText = "tacoocat";
            //var inputText = "bobo";
            Console.WriteLine($"Checking if {inputText} is a palindrome");
            var input = LinkedNode<char>.CreateNodeFromString(inputText);
            var output = Prob_2_6_IsPalindrome_v2(input);


            Console.WriteLine($"Expected result: " + true);
            Console.WriteLine($"Actual output: {output}");
        }

        public static bool Prob_2_6_IsPalindrome_v1(LinkedNode<char> head)
        {
            LinkedNode<char> runner = head;
            LinkedNode<char> currentNode = head;
            var instanceCounter = new List<char>();
            var lenght = 0;

            while (runner != null && runner.nextNode != null)
            {
                instanceCounter.Add(currentNode.data);
                currentNode = currentNode.nextNode;
                runner = runner.nextNode.nextNode;
                lenght += 2;
            }

            if (runner != null)
                lenght++;

            var isOdd = lenght % 2 != 0;
            Console.WriteLine($"Total lenght of list is {lenght}. IsOdd: {isOdd}. InstanceCounter has {instanceCounter.Count} vals");

            //Skip middle node if odd
            if (isOdd)
            {
                currentNode = currentNode.nextNode;
                Console.WriteLine($"Is odd. Moved current node one up. Node val: {currentNode.data}");
            }

            while (currentNode != null)
            {
                var currentVal = currentNode.data;
                var valToCompareTo = instanceCounter[instanceCounter.Count - 1];
                Console.WriteLine($"Comparing current node val {currentVal} to list val {valToCompareTo}");

                if (!currentVal.Equals(valToCompareTo))
                {
                    Console.WriteLine("Values are not equal. Returning false");
                    return false;
                }
                
                instanceCounter.RemoveAt(instanceCounter.Count - 1);
                currentNode = currentNode.nextNode;
            }

            return true;
        }

        public static bool Prob_2_6_IsPalindrome_v2(LinkedNode<char> head)
        {
            LinkedNode<char> runner = head;
            LinkedNode<char> currentNode = head;
            var charSequence = new Stack<char>();
            var lenght = 0;

            while (runner != null && runner.nextNode != null)
            {
                charSequence.Push(currentNode.data);
                currentNode = currentNode.nextNode;
                runner = runner.nextNode.nextNode;
                lenght += 2;
            }

            if (runner != null)
                lenght++;

            var isOdd = lenght % 2 != 0;
            Console.WriteLine($"Total lenght of list is {lenght}. IsOdd: {isOdd}. InstanceCounter has {charSequence.Count} vals");

            //Skip middle node if odd
            if (isOdd)
            {
                currentNode = currentNode.nextNode;
                Console.WriteLine($"Is odd. Moved current node one up. Node val: {currentNode.data}");
            }

            while (currentNode != null)
            {
                var currentVal = currentNode.data;
                var valToCompareTo = charSequence.Pop();
                Console.WriteLine($"Comparing current node val {currentVal} to list val {valToCompareTo}");

                if (!currentVal.Equals(valToCompareTo))
                {
                    Console.WriteLine("Values are not equal. Returning false");
                    return false;
                }

                currentNode = currentNode.nextNode;
            }

            return true;
        }

        public static bool Prob_2_6_IsPalindrome_BookSolution(LinkedNode<char> head)
        {
            LinkedNode<char> runner = head;
            LinkedNode<char> currentNode = head;
            var charSequence = new Stack<int>();

            while (runner != null && runner.nextNode != null)
            {
                charSequence.Push(currentNode.data);
                currentNode = currentNode.nextNode;
                runner = runner.nextNode.nextNode;
            }

            // Has odd number of elements, so skip the middle element
            if (runner != null)
                currentNode = currentNode.nextNode;

            while (currentNode != null)
            {
                var top = charSequence.Pop();

                if (top != currentNode.data)
                    return false;

                currentNode = currentNode.nextNode;
            }
            return true;
        }

        #endregion

        #region Prob_2_7_Intesection

        /// <summary>
        /// Probs the 2 7 integration test.
        /// Given two singly linked. Determine if the two 
        /// lists intersect. Return the intersection node.
        /// Note: The intersection is defined based on reference 
        /// , not value. That is, if the kth node of the first 
        /// linked list is the exact same node (by reference) as
        /// the jth node of the second list, then they are intersecting
        /// </summary>
        public static void Prob_2_7_Intesection_Test()
        {
            var firstList = LinkedNode<char>.CreateNodeFromString("hello world");
            var secondList = firstList.GetNodeXPositionsFromCurrent(3);
            var otherList = LinkedNode<char>.CreateNodeFromString("hello world");
            var result = Prob_2_7_DoListsIntersect_v1(firstList, otherList);

            Console.WriteLine("Expected Results: TRUE");
            Console.WriteLine($"Actual result: {result != null}");
            if (result != null)
                Console.WriteLine($"Intersecting node: {result.data}");
        }

        public static LinkedNode<T> Prob_2_7_DoListsIntersect_v1<T>(LinkedNode<T> firstList, LinkedNode<T> secondList)
        {
            var runner = firstList;
            var firsListLength = 1;
            while(runner.nextNode != null)
            {
                runner = runner.nextNode;
                firsListLength++;
            }

            var secondRunner = secondList;
            var secondListLength = 1;
            while (secondRunner.nextNode != null)
            {
                secondRunner = secondRunner.nextNode;
                secondListLength++;
            }

            //If nodes do not have the same last node
            // they must not intersect
            if (runner != secondRunner)
                return null;

            var offset = firsListLength - secondListLength;
            offset = offset < 0 ? (offset * -1) : offset;

            //reset runners
            runner = firstList;
            secondRunner = secondList;
            if(firsListLength > secondListLength)
            {
                runner = firstList.GetNodeXPositionsFromCurrent(offset);
                secondRunner = secondList;
            }
            else if(secondListLength > firsListLength)
            {
                runner = firstList;
                secondRunner = secondList.GetNodeXPositionsFromCurrent(offset);
            }

            while (runner.nextNode != null || secondRunner.nextNode != null)
            {
                if (runner == secondRunner)
                    return runner;

                runner = runner.nextNode;
                secondRunner = secondRunner.nextNode;
            }

            return null; 
        }

        #endregion

        #region Prob_2_8_Loop_Detection

        /// <summary>
        /// Probs the 2 8 loop detection test.
        /// Given a circular linked list, implement an algorithm 
        /// that returns the node at the beginning of the loop.
        /// 
        /// Defenition: 
        /// Circular linked list: A (corrupt) linked
        /// list in which a node's next pointer points to an earlier
        /// node, so as to make a loop in the linked list.
        /// 
        /// Examples:
        /// Input: a > b > c > d > e > c (that same c as earlier)
        /// Output: c
        /// </summary>
        public static void Prob_2_8_Loop_Detection_Test()
        {

        }

        public static LinkedNode<T> GetStartOfCircularLoop<T>(LinkedNode<T> circularNode)
        {
            return null;
        }

        #endregion
    }



    public class Node<T>
    {
        private T _data;
        public Node<T> Next = null;
        public Node<T> Prev = null;

        public Node(T data)
        {
            _data = data;
        }
    }

    public class SinglyLinkedList
    {
        protected Node<int> Head;

        public virtual void AddToHead(int data)
        {
            var newNode = new Node<int>(data);
            newNode.Next = Head;

            Head = newNode;
        }

        public void AddToTail(int data)
        {
            var newNode = new Node<int>(data);

            var lastNode = Head;
            while (lastNode.Next != null)
            {
                lastNode = lastNode.Next;
            }
            lastNode.Next = newNode;
        }
    }

    public class DoublyLinkedList: SinglyLinkedList
    {
        protected Node<int> Tail;

        public override void AddToHead(int data)
        {
            var newNode = new Node<int>(data);

            if(Head == null)
            {
                Head = newNode;
                Tail = newNode;
            } else
            {
                var oldHead = Head;
                newNode.Next = oldHead;
                Head = newNode;

                oldHead.Prev = Head;
            }
        }
    }
}

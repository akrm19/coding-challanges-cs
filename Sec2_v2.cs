using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodingInterviewProblems
{
    public static class Sec2_v2
    {
        public static void S2_1_RemoveDups(LinkedNode2 node)
        {
            var set = new HashSet<int>();
            LinkedNode2 prev = null;

            while (node != null)
            {
                if (set.Contains(node.Data))
                    prev.Next = node.Next;
                else
                {
                    set.Add(node.Data);
                    prev = node;
                }
                node = node.Next;
            }
        }

        public static void S2_2_RemoveKtoLast(LinkedNode2 node, int k)
        {
            var length = 0;
            var runner = node;

            while(runner != null)
            {
                length++;
                runner = runner.Next;
            }

            if (length < k) return;

            runner = node;
            for (int i = 1; i < length - k; i++)
                runner = runner.Next;

            runner.Next = runner.Next.Next;
        }

        public static int PrintKtoLastElement(LinkedNode2 node, int k)
        {
            if (node == null) return 0;

            var currXtoLastIdx = PrintKtoLastElement(node.Next, k) + 1;
            if (k == currXtoLastIdx)
            {
                //Print 
                Console.WriteLine(node.Data);
            }
            return currXtoLastIdx;
        }

        //Deletes middle node when given the Head as input
        public static void S2_3_DeleteMiddleNode(LinkedNode2 node)
        {
            var length = 0;
            LinkedNode2 runner = node;

            while (runner != null)
            {
                length++;
                runner = runner.Next;
            }

            if (length < 3) return;

            runner = node;
            var middle = length / 2;
            for (int i = 1; i < middle; i++)
                runner = runner.Next;

            runner.Next = runner.Next.Next;
        }

        //Deletes middle node when given that middle Node only
        public static void S2_3_DeleteNode(LinkedNode2 node)
        {
            if (node == null || node.Next != null) return;

            var next = node.Next;
            node.Data = next.Data;
            node.Next = next.Next;
        }

        public static LinkedNode2 S2_4_Partition3(LinkedNode2 node, int part)
        {
            LinkedNode2 head = node;
            LinkedNode2 tail = node;

            while (node != null)
            {
                var next = node.Next;
                //Move to tail if less than partition
                if (node.Data < part)
                {
                    node.Next = head;
                    head = node;
                }
                else
                {
                    tail.Next = node;
                    tail = node;
                }
                node = next;
            }
            //nt sure why this is needed:
            tail.Next = null;

            return head;
        }

        public static LinkedNode2 S2_4_Partition2(LinkedNode2 head, int partition)
        {
            LinkedNode2 prePart = null;
            LinkedNode2 postPart = null;
            var curr = head;

            while (curr != null)
            {
                if (curr.Data < partition)
                {
                    if (prePart == null)
                        prePart = curr;
                    else
                    {
                        prePart.Next = curr;
                        prePart = prePart.Next;
                    }
                }
                else
                {
                    if (postPart == null)
                        postPart = curr;
                    else
                    {
                        postPart.Next = curr;
                        postPart = postPart.Next;
                    }
                }
            }

            if (prePart != null)
                prePart.Next = postPart;

            return prePart == null
              ? postPart
              : prePart;
        }

        public static LinkedNode2 S2_4_Partition(LinkedNode2 head, int part)
        {
            LinkedNode2 prev = null;
            var curr = head;

            //iterate through Node
            while (curr != null)
            {
                //if node is >= to partition, move to next node
                if (curr.Data >= part)
                {
                    prev = curr;
                    curr = curr.Next;
                } //Else move current noe the head
                else
                {
                    //temp save curr
                    var oldCurr = curr;
                    //remove curr from current postion
                    curr = curr.Next;
                    prev.Next = curr;

                    //Make curr the new head
                    //Have ex-curr.Next point to head
                    var oldHead = head;
                    oldCurr.Next = oldHead;
                    //Set ex-curr as the new head
                    head = oldCurr;
                }
            }
            //return node
            return head;
        }



        public static LinkedNode2 S2_5_SumList(LinkedNode2 n1, LinkedNode2 n2)
        {
            LinkedNode2 result = null;
            LinkedNode2 resultHead = null;
            var carryover = 0;

            //iterate through lls
            while (n1 != null || n2 != null)
            {
                //add vals & carryover
                var sum = carryover + (n1 == null ? 0 : n1.Data) + (n2 == null ? 0 : n2.Data);
                //if > 9, set carryover to 1
                carryover = sum > 9 ? 1 : 0;
                if (carryover == 1)
                    sum = sum % 10;

                if (result == null)
                {
                    result = new LinkedNode2(sum);
                    resultHead = result;
                }
                else
                    result.Next = new LinkedNode2(sum);

                n1 = n1.Next == null ? null : n1.Next;
                n2 = n2.Next == null ? null : n2.Next;
            }
            //continue until all items are added

            //return new string
            return resultHead;
        }

        public static void S2_6_IsPalindrome_Test()
        {
            var input = new int[] { 1, 2, 3, 4, 5, };
            var node = LinkedNode2.CreateFromList(input);
            node.PrintNodes();

            var reversed = LinkedNode2.Reverse(node);
            reversed.PrintNodes();
        }

        public static bool S2_6_IsPalindrome(LinkedNode2 l)
        {
            var currR = LinkedNode2.Reverse(l);
            LinkedNode2 currL = l;

            while (currR != null && currL != null)
            {
                if (currR.Data != currL.Data)
                    return false;
                currR = currR.Next;
                currL = currL.Next;
            }
            return true;
        }

        public static bool S2_6_IsPalindrome3(LinkedNode2 l)
        {
            LinkedNode2 slow = l, fast = l;
            var stack = new Stack<int>();

            //iterate through node w/slow & fast runner until fast reaches end
            while (fast != null && fast.Next != null)
            {
                //add slow's data to Stack
                stack.Push(slow.Data);

                slow = slow.Next;
                fast = fast.Next.Next;
                //when fast reaches end, slow should be in middle
            }

            //Figure if we need to skip middle
            if (fast != null)
                slow = slow.Next;

            //Continue iterating using slow runner
            while (slow != null)
            {
                //compare slow node w/stack vals
                if (slow.Data != stack.Pop())
                    return false;
                //if diff found, return false

                slow = slow.Next;
            }
            return true;
        }

        public static bool S2_6_IsPalindrome4(LinkedList<int> l)
        {
            //find if middle needs to be skipped
            var skipMid = (l.Count % 2) != 0;
            var midPoint = l.Count / 2;
            var currPos = 0;
            LinkedListNode<int> curr = l.First;

            //iterate to middle of Node
            while (currPos < midPoint)
            {
                currPos++;

                curr = curr.Next;
            }

            //as we iterate, add node items to stack

            //when middle is reached, skip it if necessary


            //continue iterating through node, 
            //but now start comparing against popped node from stack

            //if diff found, return false


            //no diff found return true

            //temp
            return false;

        }
    }

    public class LinkedNode2 
    {
        public int Data;
        public LinkedNode2 Next;

        public LinkedNode2(int val)
        {
            Data = val;
        }

        public static void RemoveDups(LinkedNode2 node)
        {
            var set = new HashSet<int>();
            LinkedNode2 prev = null;

            while(node != null)
            {
                if (set.Contains(node.Data))
                    prev.Next = node.Next;
                else
                {
                    set.Add(node.Data);
                    prev = node;
                }
                node = node.Next;
            }
        }

        public static LinkedNode2 CreateFromList(IList<int> vals)
        {
            var head = new LinkedNode2(vals[0]);
            var curr = head;

            for (int i = 1; i < vals.Count; i++)
            {
                curr.Next = new LinkedNode2(vals[i]);
                curr = curr.Next;
            }

            return head;
        }
        public static LinkedNode2 Reverse(LinkedNode2 node)
        {
            var curr = node;
            LinkedNode2 newHead = null, next = null;

            while(curr.Next != null)
            {
                next = curr.Next;

                curr.Next = newHead;
                newHead = curr;

                curr = next;
            }
            curr.Next = newHead;

            return curr;
        }

        public void PrintNodes()
        {
            var curr = this;
            var result = new StringBuilder("Print: ");
            while(curr != null)
            {
                result.Append($" {curr.Data}"); 
                curr = curr.Next;
            }
            Console.WriteLine(result.ToString());
        }
    }


    public class MyLinkedList2<T>
    {
        public MyLinkedNode2<T> Head;

        public MyLinkedList2(T val)
        {
            Head = new MyLinkedNode2<T>(val);
        }
    }

    public class MyLinkedNode2<T>
    {
        public T Data;
        public MyLinkedNode2<T> Next;

        public MyLinkedNode2(T val)
        {
            Data = val;
        }
    }
}

using System;
using System.Collections.Generic;

namespace CrackingTheCodingInterviewProblems.Common
{
    public class MyLinkedNode<T>
    {
        public T Value;
        public MyLinkedNode<T> Next;

        public MyLinkedNode(T value)
        {
            Value = value;
            Next = null;
        }
    }

    public class MyLinkedList<T>
    {
        public MyLinkedNode<T> Head;
        public MyLinkedNode<T> Tail;

        public MyLinkedList()
        {
            Head = null;
            Tail = null;
        }

        public MyLinkedList(IList<T> list)
        {
            foreach(T i in list)
                InsertAtEnd(i);
        }

        public void InsertAsHead(T value)
        {
            if (Head == null)
                Head = new MyLinkedNode<T>(value);
            else
            {
                var newHead = new MyLinkedNode<T>(value);
                newHead.Next = Head;
                Head = newHead;
            }
        }

        public void InsertAtEnd(T value)
        {
            if (Head == null)
            {
                Head = new MyLinkedNode<T>(value);
                Tail = Head;
            }
            else
            {
                Tail.Next = new MyLinkedNode<T>(value);
                Tail = Tail.Next;
            }
        }

        public MyLinkedNode<T> PopHead()
        {
            var oldHead = Head;
            Head = Head.Next;

            return oldHead;
        }

        public MyLinkedNode<T> PopTail()
        {
            var oldTail = Tail;
            Tail = GetLastNodeManually();
            return oldTail;
        }

        public MyLinkedNode<T> GetLastNodeManually()
        {
            var last = Head;
            while(last.Next != null)
            {
                last = last.Next;
            }
            return last;
        }

        public MyLinkedList<T> Reverse()
        {
            ReverseList(this);
            return this;
        }

        public void PrintOut()
        {
            ApplyActionToEach(i => Console.Write($" {i},"));
        }

        private void ApplyActionToEach(Action<T> action)
        {
            var current = Head;
            while(current != null)
            {
                action(current.Value);
                current = current.Next;
            }
        }

        public static void ReverseList(MyLinkedList<T> list)
        {
            var current = list.Head;
            MyLinkedNode<T> prev = null, next = null;

            while(current.Next != null)
            {
                next = current.Next;

                current.Next = prev;
                prev = current;

                current = next;
            }

            current.Next = prev;
            list.Head = current;
        }
    }
}

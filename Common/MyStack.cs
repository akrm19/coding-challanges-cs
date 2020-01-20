using System;
using System.Collections.Generic;

namespace CrackingTheCodingInterviewProblems.Common
{
    public interface IMyStack<T>
    {
        void Push(T data);
        T Peek();
        T Pop();
        bool IsEmpty();
    }

    #region MyStack
    public class MyStack<T> : IMyStack<T>
    {
        LinkedNode<T> Top;

        public MyStack()
        {
        }

        public MyStack(T data)
        {
            Top = new LinkedNode<T>(data);
        }

        public void Push(T data)
        {
            var newNode = new LinkedNode<T>(data);
            newNode.nextNode = Top;
            Top = newNode;
        }

        public T Peek()
        {
            return Top.data;
        }

        public T Pop()
        {
            var data = Top.data;
            Top = Top.nextNode;

            return data;
        }

        public bool IsEmpty()
        {
            return Top == null;
        }
    }

    public class MyStackWithMin : MyStack<int>
    {
        MyStack<int> stackOfMins;

        public MyStackWithMin(int data) : base(data)
        {
            stackOfMins = new MyStack<int>(data);
        }

        public void Push(int data)
        {
            if (data < Min())
                stackOfMins.Push(data);

            base.Push(data);
        }

        public int Pop()
        {
            var poppedVal = base.Pop();

            if (poppedVal == Min())
                stackOfMins.Pop();

            return poppedVal;
        }

        public int Min()
        {
            return stackOfMins.Peek();
        }
    }

    public class SetOfStacks<T>
    {
        private int threshold = 10;
        private int counter = 0;
        private MyStack<MyStack<T>> stack;

        public SetOfStacks(T data)
        {
            stack.Push(new MyStack<T>(data));
            counter++;
        }

        public void Push(T data)
        {
            if(counter % threshold == 0)
                stack.Push(new MyStack<T>(data));
            else
                stack.Peek().Push(data);

            counter++;
        }

        public T Pop()
        {
            var currentStack = stack.Peek();
            var returnedValue = currentStack.Pop();
            if (currentStack.IsEmpty())
                stack.Pop();

            counter--;
            return returnedValue;
        }
    }

    #endregion


    #region MyOtherTempStackImplementation

    public class MyTempStack<T> : IMyStack<T>
    {
        private readonly MyLinkedList<T> _itemList;

        public MyTempStack()
        {
            _itemList = new MyLinkedList<T>();
        }

        public bool IsEmpty()
        {
            return _itemList.Head == null;
        }

        public T Peek()
        {
            return _itemList.Head.Value;
        }

        public T Pop()
        {
            return _itemList.PopHead().Value;
        }

        public void Push(T value)
        {
            _itemList.InsertAsHead(value);
        }
    }


    public class MyTempStackUsingList<T> : IMyStack<T>
    {
        private List<T> _items;

        public MyTempStackUsingList()
        {
            _items = new List<T>(); 
        }

        public bool IsEmpty()
        {
            return _items.Count == 0;
        }

        public T Peek()
        {
            return _items[0];
        }

        public T Pop()
        {
            var oldHead = _items[0];
            _items.RemoveAt(0);

            return oldHead;
        }

        public void Push(T data)
        {
            _items.Insert(0, data);
        }
    }

    public class MyTempStackUsingArray<T> : IMyStack<T>
    {
        private T[] _items;
        private int headIdx = -1;
        private int currentSize = 100;

        public MyTempStackUsingArray()
        {
            _items = new T[currentSize];
        }

        public bool IsEmpty()
        {
            return headIdx == -1;
        }

        public T Peek()
        {
            return _items[headIdx];
        }

        public T Pop()
        {
            var oldHead = _items[headIdx];
            headIdx--;
            return oldHead;
        }

        public void Push(T data)
        {
            headIdx++;
            if(headIdx > currentSize -1)
                GrowItemArray();

            _items[headIdx] = data;
        }

        private void GrowItemArray()
        {
            currentSize *= 2; //double array size
            var newArray = new T[currentSize];

            Array.Copy(_items, newArray, _items.Length);
            _items = newArray;
        }
    }
    #endregion
}

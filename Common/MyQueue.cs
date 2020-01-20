using System;
using System.Collections.Generic;

namespace CrackingTheCodingInterviewProblems.Common
{
    public interface IMyQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        T Peek();
        bool IsEmpty();
    }

    public class MyQueue<T>
    {
        LinkedNode<T> first;
        LinkedNode<T> last;

        private static bool NumberExists(TreeNode root, int n)
        {
            if (root == null)
                return false;

            if (root.Value == n)
                return true;

            if (root.Value > n)
                return NumberExists(root.Left, n);

            return NumberExists(root.Right, n);
        }

        public MyQueue()
        {
            var s1 = new Stack<int>();

        }

        private void SwapStack(Stack<T> sending, Stack<T> receiving)
        {
            while (sending.Count != 0)
            {
                T nodeToSwap = sending.Pop();
                receiving.Push(nodeToSwap);
            }
        }

        /// <summary>
        /// Add item to the end of the list (enqueue)
        /// </summary>
        /// <param name="data">Data.</param>
        public void Add(T data)
        {
            //var newNode = new LinkedNode<T>(data);
            //newNode.nextNode = last;
            //last = newNode;

            var newNode = new LinkedNode<T>(data);
            if (last != null)
                last.nextNode = newNode;
            last = newNode;

            if (first == null)
                first = last;
        }

        public T peek()
        {
            return first.data;
        }

        /// <summary>
        /// Remove the first item in the list
        /// </summary>
        /// <returns>The remove.</returns>
        public T Remove()
        {
            var data = first.data;
            first = first.nextNode;
            if (first == null)
                last = null;

            return data;
        }

        public bool IsEmpty()
        {
            return first == null;
        }
    }

    public class MyQueueWithStacks<T> : IMyQueue<T>
    {
        MyStack<T> newestFirst;
        MyStack<T> oldestFirst;
        Stack<int> n;

        public MyQueueWithStacks()
        {
            newestFirst = new MyStack<T>();
            oldestFirst = new MyStack<T>();
            n = new Stack<int>();
        }

        public T Dequeue()
        {
            //if (IsEmpty())
            //    return default;

            if (oldestFirst.IsEmpty())
                SwapStacks();

            return oldestFirst.Pop();
        }

        public T Peek()
        {
            //if (IsEmpty())
            //    return default;

            if (oldestFirst.IsEmpty())
                SwapStacks();

            return oldestFirst.Peek();
        }

        public void Enqueue(T item)
        {
            //n.Push
            newestFirst.Push(item);
        }

        public bool IsEmpty()
        {
            return newestFirst.IsEmpty() && oldestFirst.IsEmpty();
        }

        private void SwapStacks()
        {
            if (newestFirst.IsEmpty())
                SwapStacks(oldestFirst, newestFirst);
            else
                SwapStacks(newestFirst, oldestFirst);
        }

        private void SwapStacks(MyStack<T> sending, MyStack<T> receiving)
        {
            while(!sending.IsEmpty())
            {
                var nodeToSwap = sending.Pop();
                receiving.Push(nodeToSwap);
            }
        }
    }


    #region AnimalShelter

    public class AnimalShelter
    {
        LinkedList<Cat> Cats;
        LinkedList<Dog> Dogs;

        public AnimalShelter()
        {
            Cats = new LinkedList<Cat>();
            Dogs = new LinkedList<Dog>();
        }

        public void Enqueue(Animal animal)
        {
            if (animal is Dog)
                Dogs.AddLast(animal as Dog);
            else if (animal is Cat)
                Cats.AddLast(animal as Cat);
        }

        public Animal DequeueAny()
        {
            var nextCat = Cats.First.Value;
            var nextDog = Dogs.First.Value;

            if(nextCat.DateAdmitted.CompareTo(nextDog.DateAdmitted) > 0)
            {
                Cats.RemoveFirst();
                return nextCat;
            } else
            {
                Dogs.RemoveFirst();
                return nextDog;
            }
        }

        public Dog DequeueDog()
        {
            var nextDog = Dogs.First.Value;
            Dogs.RemoveFirst();

            return nextDog;
        }

        public Cat DequeueCat()
        {
            var nextCat = Cats.First.Value;
            Cats.RemoveFirst();

            return nextCat;
        }
    }

    public class Animal
    {
        public string Name;
        public DateTime DateAdmitted;

        public Animal()
        {
            DateAdmitted = DateTime.Now;
        }
    }

    public class Dog : Animal
    {
    }

    public class Cat : Animal
    {
    }
    #endregion

    #region MyTemQueues

    public class MyTempQueue<T> : IMyQueue<T>
    {
        private MyLinkedList<T> _items;


        public MyTempQueue()
        {
            _items = new MyLinkedList<T>();
        }

        public T Dequeue()
        {
            var oldHeadVal = _items.PopHead().Value;
            return oldHeadVal;
        }

        public void Enqueue(T item)
        {
            _items.InsertAtEnd(item);
        }

        public bool IsEmpty()
        {
            return _items.Head == null;
        }

        public T Peek()
        {
            return _items.Head.Value;
        }
    }


    public class MyTempQueueUsingLists<T> : IMyQueue<T>
    {
        private List<T> _items;

        public MyTempQueueUsingLists()
        {
            _items = new List<T>();
        }

        public T Dequeue()
        {
            var oldHead = _items[0];
            _items.RemoveAt(0);
            return oldHead;
        }

        public void Enqueue(T item)
        {
            _items.Add(item);
        }

        public bool IsEmpty()
        {
            return _items.Count == 0;
        }

        public T Peek()
        {
            return _items[0];
        }
    }

    public class MyTempQueueUsingTwoStacks<T> : IMyQueue<T>
    {
        private MyTempStack<T> _queueStack;
        private MyTempStack<T> _dequeueStack;

        public MyTempQueueUsingTwoStacks()
        {
            _queueStack = new MyTempStack<T>();
            _dequeueStack = new MyTempStack<T>();
        }

        public T Peek()
        {
            SwapIfNeeded();

            return _dequeueStack.Peek();
        }

        public T Dequeue()
        {
            SwapIfNeeded();

            return _dequeueStack.Pop();
        }

        public void Enqueue(T item)
        {
            _queueStack.Push(item);
        }

        public bool IsEmpty()
        {
            return _queueStack.IsEmpty() && _dequeueStack.IsEmpty();
        }

        private void SwapIfNeeded()
        {
            if (!_dequeueStack.IsEmpty()) return;

            while(!_queueStack.IsEmpty())
            {
                var item = _queueStack.Pop();
                _dequeueStack.Push(item);
            }
        }
    }

    /*
    public class MyTempQueueUsingArrays<T> : IMyQueue<T>
    {
        int arraySize = 100;
        T[] queueList;
        T[] dequeList;

        public MyTempQueueUsingArrays()
        {
            queueList = new T[arraySize];
            dequeList = new T[arraySize];
        }


    }
    */   

    #endregion
}

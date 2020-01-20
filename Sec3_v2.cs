using System;
using System.Collections.Generic;

namespace CrackingTheCodingInterviewProblems.Common
{
    public class Stack2<T>
    {
        protected List<T> vals;

        public Stack2()
        {
            vals = new List<T>();
        }

        public virtual void Push(T val)
        {
            vals.Add(val);
        }

        public virtual T Peek()
        {
            return vals.Count > 0
                ? vals[vals.Count - 1]
                : default;
        }

        public virtual T Pop()
        {
            if (vals.Count == 0) return default;

            var last = vals[vals.Count - 1];
            vals.RemoveAt(vals.Count - 1);
            return last;
        }
    }

    public class SetOfAStacks<T>
    {
        private List<Stack<T>> stacks;
        int count, maxSize;

        public SetOfAStacks(int maxStackSize)
        {
            stacks = new List<Stack<T>>();
            maxSize = maxStackSize;
        }

        public void Push(T item)
        {
            //Check if max stack size was reached
            if (count % maxSize == 0)
            {
                var newStack = new Stack<T>();
                newStack.Push(item);
                stacks.Add(newStack);
            }
        }

        public T Peek()
        {
            if (stacks.Count == 0) return default(T);

            return stacks[stacks.Count - 1].Peek();
        }

        public T Pop()
        {
            if (stacks.Count == 0) return default(T);

            var currStack = stacks[stacks.Count - 1];
            if (currStack.Count > 0) return currStack.Pop();

            stacks.RemoveAt(stacks.Count - 1);
            currStack = stacks[stacks.Count - 1];
            if (currStack.Count > 0)
                return currStack.Pop();
            
            return default(T);
        }
    }

    public class MyQueue2<T>
    {
        //Create 2 stacks. One inboud & other outbound
        private Stack<T> inboud;
        private Stack<T> outbound;

        public MyQueue2()
        {
            inboud = new Stack<T>();
            outbound = new Stack<T>();
        }

        public void Enqueue(T item)
        {
            inboud.Push(item);
        }

        public T Dequeue()
        {
            if (outbound.Count > 0)
                return outbound.Pop();

            if (inboud.Count == 0)
                return default(T);

            //swap items from in to out
            while (inboud.Count > 0)
                outbound.Push(inboud.Pop());

            return outbound.Pop();
        }
    }

    public class StackNum : Stack2<int>
    {
        private Stack<int> mins;

        public StackNum()
        {
            mins = new Stack<int>();
        }

        public override void Push(int val)
        {
            if (val <= mins.Peek())
                mins.Push(val);

            base.Push(val);
        }

        public int Min()
        {
            return mins.Count == 0
                ? int.MaxValue
                : mins.Peek();
        }

        public override int Pop()
        {
            var res = base.Pop();
            if (res == mins.Peek())
                mins.Pop();
            return res;
        }
    }

    public class Sec3_v2
    {
        public Sec3_v2()
        {
        }
    }


}

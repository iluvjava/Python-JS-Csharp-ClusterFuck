using MyDatastructure.PriorityQ;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Array;

namespace MyDatastructure.PriorityQueue
{

    /// <summary>
    /// This will be the simplest Binary Heap imaginable.
    /// 0 index will be a dummy 
    /// Null is not a accepted value. 
    /// </summary>
    class BinaryHeap<T> : IPriorityQ<T> where T : IComparable<T>
    {

        protected int ElementCount = 0;
        /// <summary>
        /// index 0 is dummy. 
        /// </summary>
        protected T[] HeapArray;

        public int Size
        {
            get
            {
                return ElementCount; 
            }
        }

        /// <summary>
        /// Get an instance of the Binary Heap. 
        /// </summary>
        public BinaryHeap()
        {
            HeapArray = new T[32];
        }

        /// <summary>
        /// Not supported for this class. 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public bool Contains(T arg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// add a new element into the queue. 
        /// </summary>
        /// <param name="arg"></param>
        public void Enqueue(T arg)
        {
            if (IsNull(arg))
                throw new InvalidArgumentException();
            AutomaticResize();
            HeapArray[ElementCount] = arg;
            PercolateUp(ElementCount);
            ElementCount++;
        }

        public T Peek()
        {
            return HeapArray[1]; // 0 element is the dummy node. 
        }

        /// <summary>
        /// Not supported. 
        /// </summary>
        /// <param name="arg"></param>
        public void Remove(T arg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the minimum element from the queue. 
        /// </summary>
        /// <returns></returns>
        public T RemoveMin()
        {
            T res = HeapArray[1];
            Swap(1, ElementCount);
            ElementCount--;
            PercolateDown(1);
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected int GetParentIndex(int arg)
        {
            if (arg <= 0)
            {
                throw new InvalidArgumentException();
            }
            return arg/2;
        }

        protected int GetFirstChildIndex(int arg)
        {
            if (arg < 0)
                throw new InvalidArgumentException();
            return 2 * arg;
        }

        protected int PercolateUp(int arg)
        {
            int Pidx = GetParentIndex(arg);
            T Parent = HeapArray[Pidx];
            T Child = HeapArray[arg];
            if (Child.CompareTo(Parent) < 0)
            {
                Swap(Pidx, arg);
                return PercolateUp(Pidx);
            }
            return arg;
        }

        protected int PercolateDown(int arg)
        {
            int LeftChildIdx = GetFirstChildIndex(arg);
            int RightChildIdx = LeftChildIdx + 1;
            T Parent = HeapArray[arg];
            T LChild = HeapArray[LeftChildIdx];
            T RChild = HeapArray[RightChildIdx];

            if (RightChildIdx <= ElementCount)
            {
                int TheSmallerChildIdx = LChild.CompareTo(RChild) < 0 ? LeftChildIdx: RightChildIdx;
                if (HeapArray[TheSmallerChildIdx].CompareTo(Parent) < 0)
                {
                    Swap(TheSmallerChildIdx, arg);
                    return PercolateDown(TheSmallerChildIdx);
                }
                return arg;
            }
            //Only one child
            if (LeftChildIdx <= ElementCount)
            {
                if (LChild.CompareTo(Parent) < 0)
                {
                    return PercolateDown(LeftChildIdx);
                }
                return arg;
            }
            //No child
            return arg;
        }

        public static bool IsNull(object o)
        {
            return object.Equals(o, null);
        }

        protected int Percolate(int arg)
        {
            return PercolateUp(PercolateDown(arg));
        }

        protected void Swap(int arg1, int arg2)
        {
            T a = HeapArray[arg1];
            T b = HeapArray[arg2];
            HeapArray[arg1] = a;
            HeapArray[arg2] = b;
        }

        protected void AutomaticResize()
        {
            if (ElementCount + 1 == HeapArray.Length)
            {
                Resize(ref HeapArray, HeapArray.Length * 2);
            }
        }





        

    }
}

using MyDatastructure.PriorityQ;
using System;
using static System.Array;

namespace MyDatastructure.PriorityQueue
{
    /// <summary>
    /// This will be the simplest Binary Heap imaginable.
    /// 0 index will be a dummy
    /// Null is not a accepted value.
    /// </summary>
    public class SimpleBinaryHeap<T> : IPriorityQ<T> where T : IComparable<T>
    {
        protected int ElementCount = 0;

        /// <summary>
        /// index 0 is dummy.
        /// </summary>
        protected T[] HeapArray;

        /// <summary>
        /// Get an instance of the Binary Heap.
        /// </summary>
        public SimpleBinaryHeap()
        {
            HeapArray = new T[32];
        }

        /// <summary>
        /// Give an array of element it will build the heap from it. 
        /// It will copy the array. 
        /// <remark>
        /// It uses floyd build heap, good for partial sort. 
        /// </remark>
        /// </summary>
        /// <param name="list">
        /// The list should not comtain null, it will blows up if it contains
        /// null.
        /// <param name="offset">
        /// Where you want to start add element from the array to the heap. 
        /// Invalid argument will be set to 0. 
        /// It's default to be 0. 
        /// </param>
        /// <param name="len">
        /// The length you want off from the offset you set.  
        /// </param>
        public SimpleBinaryHeap(T[] source, int offset = 0, int len = -1)
        {
            len = (len == -1 || len + offset >= source.Length) ? source.Length - offset: len;
            offset = (offset >= source.Length || offset < 0) ? 0 : offset;
            HeapArray = new T[len*2];
            Copy(source, offset, HeapArray, 1, len);
            ElementCount = len;
            for (int i = len / 2; i >= 1; i--)
            {
                PercolateDown(i);
            }
            
        }

        public int Size
        {
            get
            {
                return ElementCount;
            }
        }

        public static bool IsNull(object o)
        {
            return object.Equals(o, null);
        }

        /// <summary>
        /// Not supported for this class.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public bool Contains(T arg)
        {
            throw new NotSupportedException();
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
            HeapArray[ElementCount + 1] = arg;
            PercolateUp(ElementCount+ 1);
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
            throw new NotSupportedException();
        }

        /// <summary>
        /// Remove the minimum element from the queue.
        /// </summary>
        /// <throw>
        /// InvalidOperationException is the heap is empty when
        /// trying to remove elements. 
        /// </throw>
        /// <returns></returns>
        public T RemoveMin()
        {
            if (ElementCount == 0)
                throw new InvalidOperationException();
            T res = HeapArray[1];
            Swap(1, ElementCount);
            ElementCount--;
            PercolateDown(1);
            return res;
        }

        protected void AutomaticResize()
        {
            if (ElementCount + 1 == HeapArray.Length)
            {
                Resize(ref HeapArray, HeapArray.Length * 2);
            }
        }

        protected int GetFirstChildIndex(int arg)
        {
            if (arg < 1)
                throw new InvalidArgumentException();
            return 2 * arg;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected int GetParentIndex(int arg)
        {
            if (arg <= 1)
            {
                return 1;
            }
            return arg / 2;
        }

        protected int Percolate(int arg)
        {
            return PercolateUp(PercolateDown(arg));
        }

        protected int PercolateDown(int arg)
        {
            int LeftChildIdx = GetFirstChildIndex(arg);
            int RightChildIdx = LeftChildIdx + 1;
            T Parent = HeapArray[arg];

            if (RightChildIdx <= ElementCount)
            {
                T LChild = HeapArray[LeftChildIdx];
                T RChild = HeapArray[RightChildIdx];
                int TheSmallerChildIdx = LChild.CompareTo(RChild) < 0 ? LeftChildIdx : RightChildIdx;
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
                T LChild = HeapArray[LeftChildIdx];
                if (LChild.CompareTo(Parent) < 0)
                {
                    Swap(LeftChildIdx, arg);
                    return PercolateDown(LeftChildIdx);
                }
                return arg;
            }
            //No child
            return arg;
        }

        protected int PercolateUp(int arg)
        {
            if (arg == 1)
                return arg;
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

        protected void Swap(int arg1, int arg2)
        {
            if (arg1 == arg2)
                return;
            T a = HeapArray[arg1];
            T b = HeapArray[arg2];
            HeapArray[arg1] = b;
            HeapArray[arg2] = a;
        }
    }

    public class BinaryHeap<T> : SimpleBinaryHeap<T> where T : IComparable<T>
    {

    }
}
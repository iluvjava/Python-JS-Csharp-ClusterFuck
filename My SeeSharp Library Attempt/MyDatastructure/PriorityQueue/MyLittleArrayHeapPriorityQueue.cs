using MyDatastructure.PriorityQ;
using MyDatastructure.Maps;
using System;
using System.Collections.Generic;

using static System.Array;

namespace MyDatastructure
{
    
    
    /// <summary>
    /// This is a min heap using 4 children heap structure.
    /// <para>
    /// - Null elements are not allowed in this datastructure.
    /// </para>
    /// </summary>
    /// <typeparam name="T">
    /// T as a comparable Type for the class. 
    /// </typeparam>
    public class MyLittleArrayHeapPriorityQueue<T> : IPriorityQ<T> where T : IComparable<T>
    {
        protected T[] ArrayHeap;
        protected IComparer<T> CustomizedComparer;
        protected int HeapChildrenCount;
        protected IMap<T, int> Indices;
        protected IMap<T, int> Frequencies;
        protected int UniqueElementCount = 0; // Unique elements count
        protected int ElementCount = 0; // The number of element in queue includes repetitions.

        public int Size
        {
            get
            {
                return ElementCount;
            }
        }

        public MyLittleArrayHeapPriorityQueue
        (
            IMap<T, int> IndexMap,
            IMap<T, int> FreqMap,
            int heapchildrecount,
            int initialHeapSize,
            IComparer<T> comparer = null
        )
        {
            if (heapchildrecount <= 1 || initialHeapSize <= 1)
            {
                throw new InvalidArgumentException();
            }
            Indices = IndexMap;
            Frequencies = FreqMap;
            HeapChildrenCount = heapchildrecount;
            ArrayHeap = CreateGenericArray<T>(initialHeapSize);
            CustomizedComparer = comparer;
        }

        public MyLittleArrayHeapPriorityQueue(): 
            this(new SysDefaultMap<T, int>(), new SysDefaultMap<T, int>(), 4, 16)
        {

        }

        public MyLittleArrayHeapPriorityQueue(IComparer<T> arg):
            this(new SysDefaultMap<T, int>(),new SysDefaultMap<T, int>(), 4, 16, arg)
        {

        }

        /// <summary>
        /// it will use Floyd buildheap algorithm, it will not modify the inputs.
        /// </summary>
        /// <return>
        /// A new heap that made from the Floyd Buildheap Algorithm.
        /// </return>
        public static MyLittleArrayHeapPriorityQueue<R> BuildHeap<R>(R[] arg)
        where R : IComparable<R>
        {
            var res = new MyLittleArrayHeapPriorityQueue<R>();
            R[] newarr = new R[arg.Length];
            Copy(arg, 0, newarr, 0, arg.Length);
            res.ArrayHeap = newarr;
            res.UniqueElementCount = arg.Length;
            for (int i = 0; i < newarr.Length; i++)
            {
                res.Indices[newarr[i]] = i;
                res.Register(newarr[i]);
            }
            for (int i = res.Size - 1; i >= 0; i--)
            {
                res.PercolateDown(i);
            }
            
            return res;
        }

        public static T1[] CreateGenericArray<T1>(int len)
        {
            return new T1[len];
        }

        public bool Contains(T arg)
        {
            return Indices.ContainsKey(arg);
        }

        /// <summary>
        /// Enqueue an element in to the queue. Element cannot be repeated nor it can be
        /// null.
        /// </summary>
        /// <param name="arg"></param>
        public void Enqueue(T arg)
        {
            if (object.ReferenceEquals(null, arg))
            {
                throw new InvalidArgumentException();
            }
            AutomaticResize();
            if (!Register(arg))
            {
                ArrayHeap[UniqueElementCount++] = arg;
                Indices[arg] = UniqueElementCount - 1;
                Percolate(UniqueElementCount - 1);
            }
        }

        public T Peek()
        {
            return ArrayHeap[0];
        }

        /// <summary>
        /// Null is not welcome in this queue. 
        /// </summary>
        /// <param name="arg"></param>
        /// <Exception>
        /// Illegal argument exception if given argument with dafault value, or the element
        /// is not already presented in the queue.
        /// </Exception>
        public void Remove(T arg)
        {
            if (object.ReferenceEquals(arg, null) || !Indices.ContainsKey(arg))
            {
                throw new InvalidArgumentException();
            }
            if (Resign(arg))
            {
                int elementindex = Indices[arg];
                Swap(elementindex, UniqueElementCount - 1);
                Indices.Remove(ArrayHeap[UniqueElementCount - 1]);
                UniqueElementCount--;
                Percolate(elementindex);
            }
        }

        /// <summary>
        /// Remove the smallest element from the priority queue.
        /// </summary>
        /// <returns>
        /// Reference to the smaller element in the queue.
        /// </returns>
        public T RemoveMin()
        {
            T res = Peek();
            Remove(res);
            return res;
        }

        /// <summary>
        /// Check the last element in the array and satruation to see if there is the need
        /// for resizing.
        /// </summary>
        protected void AutomaticResize()
        {
            if (UniqueElementCount == ArrayHeap.Length - 1)
            {
                T[] newarr = CreateGenericArray<T>(ArrayHeap.Length * 2);
                Copy(ArrayHeap, 0, newarr, 0, ArrayHeap.Length);
                this.ArrayHeap = newarr;
            }
        }

        /// <summary>
        /// arg1.Compareto(arg2)
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        protected int CallCompare(T arg1, T arg2)
        {
            if (CustomizedComparer is null)
            {
                return arg1.CompareTo(arg2);
            }
            return CustomizedComparer.Compare(arg1, arg2);
        }

        protected int GetFirstChildIndex(int arg)
        {
            return arg * HeapChildrenCount + 1;
        }

        /// <summary>
        /// Return the index of the parent of the element at the current index in the array.
        /// </summary>
        /// <returns></returns>
        protected int GetParentIndex(int arg)
        {
            if (arg <= HeapChildrenCount)
            {
                return 0;
            }
            return (arg - 1) / HeapChildrenCount;
        }

        /// <summary>
        /// Percolate the element up, or down.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>
        /// The index which the element ended up with.
        /// </returns>
        protected int Percolate(int arg)
        {
            return PercolateUp(PercolateDown(arg));
        }

        /// <summary>
        /// Percolate the element down and return the index that it ended up to.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>
        /// The final place in the heap that the element ended up to.
        /// </returns>
        protected int PercolateDown(int arg)
        {
            int firstchildindex = GetFirstChildIndex(arg);
            // No children.
            if (firstchildindex >= UniqueElementCount)
            {
                return arg;
            }
            T parent = ArrayHeap[arg];
            // Null check.
            if (object.ReferenceEquals(parent, null))
            {
                throw new Exception("Internal Error.");
            }
            int indextoswap = -1;
            // Choose min child parent is larger than.
            for (
                    int i = firstchildindex;
                    i < UniqueElementCount && i < firstchildindex + HeapChildrenCount;
                    i++
                )
            {
                if (CallCompare(ArrayHeap[i], parent) < 0)
                {
                    if (indextoswap == -1)
                    {
                        indextoswap = i;
                    }
                    else if (CallCompare(ArrayHeap[i], ArrayHeap[indextoswap]) < 0)
                    {
                        indextoswap = i;
                    }
                }
            }

            if (indextoswap == -1)
            {
                return arg;
            }
            Swap(arg, indextoswap);
            return PercolateDown(indextoswap);
        }

        /// <summary>
        /// Returns the new index that the elements has been percolated up to.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>
        /// The index that the element got ended up with.
        /// </returns>
        protected int PercolateUp(int arg)
        {
            if (arg == 0)
            {
                return arg;
            }
            int parentindex = GetParentIndex(arg);
            T parent = ArrayHeap[parentindex];
            T child = ArrayHeap[arg];
            // null internal check
            if (object.ReferenceEquals(parent, null))
            {
                throw new Exception("Internal Error.");
            }
            if (CallCompare(child, parent) < 0)
            {
                Swap(parentindex, arg);
                return PercolateUp(parentindex);
            }
            return arg;
        }

        /// <summary>
        /// Swap 2 elements in the heap array. It will update all things.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name=""></param>
        protected void Swap(int arg1, int arg2)
        {
            if (arg1 == arg2)
                return;
            T firstthing = ArrayHeap[arg1];
            T secondthing = ArrayHeap[arg2];
            Indices[firstthing] = arg2;
            Indices[secondthing] = arg1;
            ArrayHeap[arg2] = firstthing;
            ArrayHeap[arg1] = secondthing;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>
        /// True if the element is already in the queue.
        /// Else false.
        /// </returns>
        protected bool Register(T arg)
        {
            ElementCount++;
            if (Frequencies.ContainsKey(arg))
            {
                Frequencies[arg]++;
                return true;
            }
            else
                Frequencies[arg] = 1;
            return false;
        }

       /// <summary>
       /// remove or decrement the element from the Frequency map.
       /// </summary>
       /// <param name="arg"></param>
       /// <returns>
       /// True if the element should be removed from the index map too. 
       /// Else false. 
       /// </returns>
        protected bool Resign(T arg)
        {
            if (!Frequencies.ContainsKey(arg))
            {
                throw new InvalidArgumentException();
            }
            Frequencies[arg]--;
            ElementCount--;
            if (Frequencies[arg] == 0)
            {
                Frequencies.Remove(arg);
                return true;
            }
            return false; 
        }
    }

   
}
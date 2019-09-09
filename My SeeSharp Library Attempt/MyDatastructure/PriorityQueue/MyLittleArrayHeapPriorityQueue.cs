using MyDatastructure.Maps;
using MyDatastructure.PriorityQ;
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
        /// <summary>
        /// The array that stores the heap structure inside. 
        /// </summary>
        protected T[] ArrayHeap;
        /// <summary>
        /// The comparer set by the client, null by default if client didn't set it. 
        /// </summary>
        protected IComparer<T> CustomizedComparer;
        /// <summary>
        /// The number of elements in the heap, including the repetition.
        /// </summary>
        protected int ElementCount = 0;
        /// <summary>
        /// A int[] where if the element in the array heap, then the same index in the Frequencies 
        /// array is the frequencies of that element. 
        /// </summary>
        protected int[] Frequencies;
        /// <summary>
        /// The number of children that each parent has in the heap. 
        /// </summary>
        protected int HeapChildrenCount;
        /// <summary>
        /// A map that stores all the index of the element in the heap array. 
        /// </summary>
        protected IMap<T, int> Indices;
        /// <summary>
        /// The number of unique elements in the array, it's for manaing the heap. 
        /// </summary>
        protected int UniqueElementCount = 0;

        public int Size
        {
            get
            {
                return ElementCount;
            }
        }
        /// <summary>
        /// Construct an istance of the The array Heap. 
        /// </summary>
        /// <param name="IndexMap">
        /// The type of Maps inplementations you want for storing the 
        /// index of element interally. This greatly affects speed. 
        /// </param>
        /// <param name="heapchildrecount">
        /// Change the number of children each parent has. larger than 1.
        /// </param>
        /// <param name="initialHeapSize">
        /// The initial size of the heap array. 
        /// </param>
        /// <param name="comparer">
        /// The comparer you want to use for the element.
        /// </param>
        public MyLittleArrayHeapPriorityQueue
        (
            IMap<T, int> IndexMap = null,
            int heapchildrecount = 4,
            int initialHeapSize = 1024,
            IComparer<T> comparer = null
        )
        {
            if (heapchildrecount <= 1 || initialHeapSize <= 1)
            {
                throw new InvalidArgumentException();
            }
            Indices = IndexMap?? new SysDefaultMap<T, int>();
            HeapChildrenCount = heapchildrecount;
            ArrayHeap = CreateGenericArray<T>(initialHeapSize);
            Frequencies = new int[initialHeapSize];
            CustomizedComparer = comparer;
        }


        /// <summary>
        /// Construct an instance of the MyLittleArrayHeapPriorityQueue.
        /// </summary>
        public MyLittleArrayHeapPriorityQueue() :
            this(new SysDefaultMap<T, int>(), 4, 16)
        {
        }

        /// <summary>
        /// Construct an instance of the MyLittleArrayHeapPriorityQueue.
        /// </summary>
        /// <param name="arg">
        /// The Comparer you want to use for ordering elements. 
        /// </param>
        public MyLittleArrayHeapPriorityQueue(IComparer<T> arg) :
            this(new SysDefaultMap<T, int>(), 4, 2048, arg)
        {
        }



        /// <summary>
        /// A helper methods that swaps any array.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="arr"></param>
        public static void ArrayElementSwapHelper<R>(int a, int b, R[] arr)
        {
            R A = arr[a], B = arr[b];
            arr[b] = A;
            arr[a] = B;
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
            IMap<R, int> freqmap = new SysDefaultMap<R, int>();
            for (
                int i = -1;
                ++i < arg.Length;
                freqmap[arg[i]] = freqmap.ContainsKey(arg[i]) ? freqmap[arg[i]] + 1 : 1
                ) ;

            // Split it into 2 array;
            int elementcount = 0;
            int[] freqtable = new int[freqmap.Size];
            R[] contentatble = new R[freqmap.Size];
            int uniquecount = 0;
            foreach (KVP<R, int> kvp in freqmap)
            {
                contentatble[uniquecount] = kvp.Key;
                freqtable[uniquecount] = kvp.Value;
                elementcount += kvp.Value;
                uniquecount++;
            }
            MyLittleArrayHeapPriorityQueue<R> q = new MyLittleArrayHeapPriorityQueue<R>();
            q.ArrayHeap = contentatble;
            q.Frequencies = freqtable;
            q.UniqueElementCount = uniquecount;
            q.ElementCount = elementcount;
            for (int i = (q.ArrayHeap.Length / 4) - 1; i >= 0; i--)
            {
                q.PercolateDown(i);
            }

            return q;


            /* MyLittleArrayHeapPriorityQueue<R> resultQ =
                 new MyLittleArrayHeapPriorityQueue<R>();
             resultQ.ElementCount = arg.Length;
             IMap<R, int> freqmap = new SysDefaultMap<R, int>();
             for (
                     int i = -1;
                     ++i < arg.Length;
                     freqmap[arg[i]] = freqmap.ContainsKey(arg[i]) ? freqmap[arg[i]] + 1 : 1
                 );
             resultQ.ArrayHeap = new R[freqmap.Size];
             resultQ.Frequencies = new int[freqmap.Size];
             int uniquecount = 0;
             foreach (KVP<R, int> kvp in freqmap)
             {
                 resultQ.ArrayHeap[uniquecount] = kvp.Key;
                 uniquecount++;
             }
             resultQ.UniqueElementCount = uniquecount;
             for (int i = uniquecount / 4; i >= 0; i--)
             {
                 resultQ.PercolateDown(i, true);
             }
             for (int i = 0; i < uniquecount; i++)
             {
                 resultQ.Frequencies[i] = freqmap[resultQ.ArrayHeap[i]];
                 resultQ.Indices[resultQ.ArrayHeap[i]] = i;
             }
             return resultQ; */
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
            if (Register(arg))
            {
                ArrayHeap[UniqueElementCount++] = arg;
                Indices[arg] = UniqueElementCount - 1;
                Percolate(UniqueElementCount - 1);
            }
        }

        /// <summary>
        /// Get a reference for the first element in the queue. 
        /// </summary>
        /// <returns>
        /// a reference for the first element in the queue. 
        /// </returns>
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
                {
                    T[] newarr = CreateGenericArray<T>(ArrayHeap.Length * 2);
                    Copy(ArrayHeap, 0, newarr, 0, ArrayHeap.Length);
                    this.ArrayHeap = newarr;
                }
                {
                    int[] newarr = new int[ArrayHeap.Length * 2];
                    Copy(Frequencies, 0, newarr, 0, Frequencies.Length);
                    this.Frequencies = newarr;
                }
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

        /// <summary>
        /// Get the first index of the heap. Internal use only. 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
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
        /// <param name="heapbuilding">
        /// True if the method is called under the context of floyd build heap. 
        /// </param>
        /// <returns>
        /// The final place in the heap that the element ended up to.
        /// </returns>
        protected int PercolateDown(int arg, bool heapbuilding = false)
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
            Swap(arg, indextoswap, heapbuilding);
            return PercolateDown(indextoswap, heapbuilding);
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
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>
        /// True if the element is NEW! (Yes, percolate, add it to the index map! )
        /// </returns>
        protected bool Register(T arg)
        {
            ElementCount++;
            if (Indices.ContainsKey(arg))
            {
                Frequencies[Indices[arg]]++;
                return false;
            }
            else
            {
                Frequencies[UniqueElementCount] = 1;
            }
            return true;
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
            if (!Indices.ContainsKey(arg))
            {
                throw new InvalidArgumentException();
            }
            int theindex = Indices[arg];
            Frequencies[theindex]--;
            ElementCount--;
            if (Frequencies[theindex] == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Swap 2 elements in the heap array. It will update all things.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="buildheapmode">True if it's called by buildheap method</param>
        /// <param name="arg2"></param>
        /// 
        ///
        protected void Swap(int arg1, int arg2, bool buildheapmode = false)
        {
            if (arg1 == arg2)
                return;
            T firstthing = ArrayHeap[arg1];
            T secondthing = ArrayHeap[arg2];
            if (!buildheapmode)
            {
                Indices[firstthing] = arg2;
                Indices[secondthing] = arg1;
                 ArrayElementSwapHelper(arg1, arg2, Frequencies);
            }
           ArrayElementSwapHelper(arg1, arg2, ArrayHeap);
        }
    }

}
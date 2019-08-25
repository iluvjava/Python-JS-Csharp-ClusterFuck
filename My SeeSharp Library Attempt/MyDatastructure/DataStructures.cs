using System;
using System.Runtime.Serialization;

using static System.Array;

namespace MyDatastructure
{
    /// <summary>
    /// this is an interface for priorityqueue.
    /// </summary>
    public interface IPriorityQ<T>
    {
        bool Contains(T arg);

        void Equeue(T arg);

        T Peek();

        void Remove(T arg);

        T RemoveMin();
    }

    /// <summary>
    /// This is a min heap using 4 children heap structure.
    /// <para>
    /// Whether repeating elements are allowed depends on specific implementations.
    /// </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyLittleArrayHeapPriorityQueue<T> : IPriorityQ<T> where T : IComparable<T>
    {
        protected T[] ArrayHeap;
        protected int HeapChildrenCount;
        protected IMap<T, int> Indices;
        protected int size = 0;

        public MyLittleArrayHeapPriorityQueue
        (
            IMap<T, int> IndexMap,
            int heapchildrecount,
            int initialHeapSize
        )
        {
            Indices = IndexMap;
            HeapChildrenCount = heapchildrecount;
            ArrayHeap = CreateGenericArray<T>(initialHeapSize);
        }

        public MyLittleArrayHeapPriorityQueue() : this(new SysDefaultMap<T, int>(), 4, 16)
        {
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
        public void Equeue(T arg)
        {
            if (object.ReferenceEquals(null, arg) || Indices.ContainsKey(arg))
            {
                throw new InvalidArgumentException();
            }
            AutomaticResize();
            ArrayHeap[size++] = arg;
            Indices[arg] = size - 1;
            Percolate(size - 1);
        }

        public T Peek()
        {
            return ArrayHeap[0];
        }

        /// <summary>
        ///
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
            int elementindex = Indices[arg];
            Swap(elementindex, size - 1);
            Indices.Remove(ArrayHeap[size - 1]);
            ArrayHeap[size--] = default;
            Percolate(elementindex);
        }

        /// <summary>
        /// Remove the smallest element from the priority queue.
        /// </summary>
        /// <returns>
        /// Reference to the smaller element in the queue.
        /// </returns>
        public T RemoveMin()
        {
            T res = ArrayHeap[0];
            Swap(size - 1, 0);
            Indices.Remove(ArrayHeap[size - 1]);
            size--;
            PercolateDown(0);
            return res;
        }

        /// <summary>
        /// Check the last element in the array and satruation to see if there is the need
        /// for resizing.
        /// </summary>
        protected void AutomaticResize()
        {
            if (size == ArrayHeap.Length - 1)
            {
                T[] newarr = CreateGenericArray<T>(ArrayHeap.Length * 2);
                Copy(ArrayHeap, 0, newarr, 0, ArrayHeap.Length);
                this.ArrayHeap = newarr;
            }
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
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <throw>
        /// Exception if there is an internal failure.
        /// </throw>
        protected void IsIndexValid(params int[] arg)
        {
            for (int i = -1; ++i != arg.Length;)
                if (arg[i] < 0 || arg[i] >= size)
                {
                    throw new Exception("Internal Error.");
                }
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
            if (firstchildindex >= size)
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
                    i < size && i < firstchildindex + HeapChildrenCount;
                    i++
                )
            {
                if (ArrayHeap[i].CompareTo(parent) < 0)
                {
                    if (indextoswap == -1)
                    {
                        indextoswap = i;
                    }
                    else if (ArrayHeap[i].CompareTo(ArrayHeap[indextoswap]) < 0)
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
            IsIndexValid(arg);
            if (arg == 0)
            {
                return arg;
            }
            int parentindex = GetParentIndex(arg);
            T parent = ArrayHeap[parentindex];
            T child = ArrayHeap[arg];
            //null internal check
            if (object.ReferenceEquals(parent, null))
            {
                throw new Exception("Internal Error.");
            }
            if (child.CompareTo(parent) < 0)
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
    }

    [Serializable]
    internal class InvalidArgumentException : Exception
    {
        public InvalidArgumentException()
        {
        }

        public InvalidArgumentException(string message) : base(message)
        {
        }

        public InvalidArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


    /// <summary>
    /// A simpler Binary Heap. It cannot remove element/check contain from the queue but support 
    /// multiple of the same element. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleBinaryHeapPriorityQeue<T> : IPriorityQ<T> where T : IComparable<T>
    {
        public bool Contains(T arg)
        {
            throw new NotImplementedException();
        }

        public void Equeue(T arg)
        {
            throw new NotImplementedException();
        }

        public T Peek()
        {
            throw new NotImplementedException();
        }

        public void Remove(T arg)
        {
            throw new NotImplementedException();
        }

        public T RemoveMin()
        {
            throw new NotImplementedException();
        }
    }
}

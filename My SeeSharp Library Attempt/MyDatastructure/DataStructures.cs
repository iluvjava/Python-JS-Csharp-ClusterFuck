using System;
using System.Collections;
using System.Collections.Generic;

namespace MyDatastructure
{

    /// <summary>
    /// this is an interface for priorityqueue.
    /// </summary>
    public interface IPriorityqueue<T>
    {
        void Equeue(T arg);

        void Peek();

        void RemoveMin();

        void Remove(T arg);

        void Contains(T arg);
    }

    public class MyLittleArrayHeapPriorityQueue<T> : IPriorityqueue<T>
    {
        private IMap<T, int> Indices;

        public void Contains(T arg)
        {
            throw new NotImplementedException();
        }

        public void Equeue(T arg)
        {
            throw new NotImplementedException();
        }

        public void Peek()
        {
            throw new NotImplementedException();
        }

        public void Remove(T arg)
        {
            throw new NotImplementedException();
        }

        public void RemoveMin()
        {
            throw new NotImplementedException();
        }
    }

    

    
}
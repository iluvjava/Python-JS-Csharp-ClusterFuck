using System;
using System.Collections.Generic;
using System.Text;

namespace MyDatastructure.PriorityQ
{   
    /// <summary>
    /// this is an interface for priorityqueue.
    /// </summary>
    public interface IPriorityQ<T>
    {
        int Size { get; }

        bool Contains(T arg);

        void Enqueue(T arg);

        T Peek();

        void Remove(T arg);

        T RemoveMin();
    }
}


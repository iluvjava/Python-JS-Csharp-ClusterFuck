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
        // Return the current size of the Queue. 
        int Size { get; }

        /// <summary>
        /// Return true or false if the current element is in the queue. 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        bool Contains(T arg);

        /// <summary>
        /// Equeue the element into the queue. 
        /// </summary>
        /// <param name="arg"></param>
        void Enqueue(T arg);

        /// <summary>
        /// Get an reference from the first element in the queue. 
        /// </summary>
        /// <returns></returns>
        T Peek();
        /// <summary>
        /// Remove the element from the queue. 
        /// </summary>
        /// <param name="arg"></param>

        void Remove(T arg);

        /// <summary>
        /// Remove and get a reference of the first memeber of the queue. 
        /// </summary>
        /// <returns>
        /// </returns>
        T RemoveMin();
    }
}


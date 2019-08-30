using System;
using System.Collections.Generic;
using System.Text;

namespace MyDatastructure.UnionFind
{   

    public interface IDisjointSet<T>
    {
        /// <summary>
        /// Register the element its own set. 
        /// If a already in it, then do nothing. 
        /// </summary>
        /// <param name="a"></param>
        void CreateSet(T a);

        /// <summary>
        /// Join 2 elements into the same set. 
        /// - if they are already in it, do nothing.
        /// - if any one of them are not in the set, throw exception.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        void Join(T a, T b);

        /// <summary>
        /// Return the integer representative of a set. 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        int FindSet(T a);

        /// <summary>
        /// True if they are in the samne set. 
        /// - All element is in the same set as itself. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>

        bool IsSameSet(T a, T b);


        /// <summary>
        /// Return the representative of the element. 
        /// - If the element is registered yet, throw exception.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        int GetRepresentative(T a);

    }
}

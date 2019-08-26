
using System.Collections.Generic;

namespace MyDatastructure.Maps
{
    /// <summary>
    /// An interface for my own set implementation.
    /// <para>
    /// Whether it handles null key depends on implementations. 
    /// </para>
    /// </summary>
    public interface IMap<K, V> : IEnumerable<KVP<K, V>>
    {
        int Size { get; }

        /// <summary>
        /// No such key excpetion!
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        V this[K key] { get; set; }

        /// <summary>
        /// Removes the key and return the value of the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>
        /// null is returned if the key is not in the map, null returned
        /// doesn't always mean the key is not in the map.
        /// </returns>
        V Remove(K key);

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns>
        ///
        /// </returns>
        bool ContainsKey(K key);
    }
}

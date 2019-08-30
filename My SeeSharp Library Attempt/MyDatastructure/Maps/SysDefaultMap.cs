using System;
using System.Collections;
using System.Collections.Generic;

namespace MyDatastructure.Maps
{
    public class KVP<K, V>
    {
        public KVP(K key, V val)
        {
            Key = key;
            Value = val;
        }

        public K Key { get; set; }
        public V Value { get; set; }
    }

    /// <summary>
    /// Uses the system default Idictionary for the implementation for IMap.
    ///
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class SysDefaultMap<K, V> : IMap<K, V>
    {
        protected IDictionary<K, V> TheThing;

        public int Size
        {
            get
            {
                return TheThing.Count;
            }
        }

        public SysDefaultMap()
        {
            TheThing = new Dictionary<K, V>();
        }

        public V this[K key]
        {
            get
            {
                return TheThing[key];
            }
            set
            {
                TheThing[key] = value;
            }
        }
        public bool ContainsKey(K key)
        {
            return TheThing.ContainsKey(key);
        }

        public IEnumerator<KVP<K, V>> GetEnumerator()
        {
            return new InternalIterator<KVP<K, V>>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new InternalIterator<KVP<K, V>>(this);
        }

        public V Remove(K key)
        {
            V res;
            if (!TheThing.ContainsKey(key))
                return default(V);
            res = TheThing[key];
            TheThing.Remove(key);
            return res;
        }
        private class InternalIterator<T> : IEnumerator<T> where T : KVP<K, V>
        {
            private IEnumerator<KeyValuePair<K, V>> TheDictIterator;

            public InternalIterator(SysDefaultMap<K, V> arg)
            {
                TheDictIterator = arg.TheThing.GetEnumerator();
            }

            public T Current
            {
                get
                {
                    var res =
                        new KVP<K, V>(TheDictIterator.Current.Key, TheDictIterator.Current.Value);
                    return (T)res;
                }
            }

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                return;
            }

            public bool MoveNext()
            {
                return TheDictIterator.MoveNext();
            }

            public void Reset()
            {
                TheDictIterator.Reset();
            }
        }
    }
}
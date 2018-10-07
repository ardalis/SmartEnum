// Guarantees that factory is called only once and avoids delegate allocation by using a value-type.
// Source: https://github.com/AutoMapper/AutoMapper/blob/5a4a48fcf14eb72c8d231cd86b54f3e8d9065a0d/src/AutoMapper/LockingConcurrentDictionary.cs
namespace AutoMapper
{
    using System;
    using System.Collections.Concurrent;

    internal readonly struct LockingConcurrentDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, Lazy<TValue>> _dictionary;
        private readonly Func<TKey, Lazy<TValue>> _valueFactory;

        public LockingConcurrentDictionary(Func<TKey, TValue> valueFactory)
        {
            _dictionary = new ConcurrentDictionary<TKey, Lazy<TValue>>();
            _valueFactory = key => new Lazy<TValue>(() => valueFactory(key));
        }

        public TValue GetOrAdd(TKey key)
        {
            return _dictionary.GetOrAdd(key, _valueFactory).Value;
        }
    }
}
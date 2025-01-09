
namespace System.Collections.Generic
{
    public static class DictionaryPool<TKey, TValue>
    {
        private static readonly Stack<Dictionary<TKey, TValue>> _pool = new Stack<Dictionary<TKey, TValue>>();
        private static readonly object _lock = new object();

        public static int MaxPoolSize { get; set; } = 255;

        public static Dictionary<TKey, TValue> Get()
        {
            lock (_lock)
            {
                if (_pool.Count == 0)
                {
                    return new Dictionary<TKey, TValue>();
                }
                return _pool.Pop();
            }
        }

        public static void Release(Dictionary<TKey, TValue> element)
        {
            element.Clear();

            lock (_lock)
            {
                if (_pool.Count > MaxPoolSize)
                {
                    return;
                }

                if (!_pool.Contains(element))
                {
                    _pool.Push(element);
                }
            }
        }

        public static void Clear()
        {
            lock (_lock)
            {
                _pool.Clear();
            }
        }
    }
}
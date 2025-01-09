
namespace System.Collections.Generic
{
    public static class HashSetPool<T>
    {
        private static readonly Stack<HashSet<T>> _pool = new Stack<HashSet<T>>();
        private static readonly object _lock = new object();

        public static int MaxPoolSize { get; set; } = 255;

        public static HashSet<T> Get()
        {
            lock (_lock)
            {
                if (_pool.Count == 0)
                {
                    return new HashSet<T>();
                }
                return _pool.Pop();
            }
        }

        public static void Release(HashSet<T> element)
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

namespace System.Collections.Generic
{
    public static class ListPool<T>
    {
        private static readonly Stack<List<T>> _pool = new Stack<List<T>>();
        private static readonly object _lock = new object();

        public static int MaxPoolSize { get; set; } = 255;

        public static List<T> Get()
        {
            lock (_lock)
            {
                if (_pool.Count == 0)
                {
                    return new List<T>();
                }
                return _pool.Pop();
            }
        }

        public static void Release(List<T> element)
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
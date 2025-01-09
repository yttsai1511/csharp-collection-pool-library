
namespace System.Collections.Generic
{
    public static class QueuePool<T>
    {
        private static readonly Stack<Queue<T>> _pool = new Stack<Queue<T>>();
        private static readonly object _lock = new object();

        public static int MaxPoolSize { get; set; } = 255;

        public static Queue<T> Get()
        {
            lock (_lock)
            {
                if (_pool.Count == 0)
                {
                    return new Queue<T>();
                }
                return _pool.Pop();
            }
        }

        public static void Release(Queue<T> element)
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
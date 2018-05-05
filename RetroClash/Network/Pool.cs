using System.Collections.Concurrent;

namespace RetroClash.Network
{
    public class Pool<T>
    {
        private readonly ConcurrentQueue<T> _stack;

        public Pool()
        {
            _stack = new ConcurrentQueue<T>();
        }

        public T Pop
        {
            get
            {
                var ret = default(T);

                if (_stack.Count > 0)
                    _stack.TryDequeue(out ret);

                return ret;
            }
        }

        public void Push(T item)
        {
            if (_stack.Count < Configuration.MaxClients)
                _stack.Enqueue(item);
        }
    }
}
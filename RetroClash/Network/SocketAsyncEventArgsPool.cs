using System.Collections.Generic;
using System.Net.Sockets;

namespace RetroClash.Network
{
    public class SocketAsyncEventArgsPool
    {
        private readonly object _gate = new object();
        private readonly Stack<SocketAsyncEventArgs> _pool;

        public SocketAsyncEventArgsPool()
        {
            _pool = new Stack<SocketAsyncEventArgs>();
        }

        public SocketAsyncEventArgs Dequeue()
        {
            lock (_gate)
            {
                return _pool.Count > 0 ? _pool.Pop() : null;
            }
        }

        public void Enqueue(SocketAsyncEventArgs args)
        {
            lock (_gate)
            {
                if (_pool.Count < Configuration.MaxClients)
                    _pool.Push(args);
            }
        }
    }
}
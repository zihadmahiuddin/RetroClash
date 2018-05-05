using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace RetroClash.Network
{
    public sealed class BufferManager
    {
        private readonly int _bufferSize;
        private readonly Stack<int> _freeIndexPool;
        private readonly int _numBytes;
        private byte[] _buffer;
        private int _currentIndex;

        public BufferManager(int totalBytes, int bufferSize)
        {
            _numBytes = totalBytes;
            _currentIndex = 0;
            _bufferSize = bufferSize;
            _freeIndexPool = new Stack<int>();
        }

        public void InitBuffer()
        {
            _buffer = new byte[_numBytes];
        }

        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            if (_freeIndexPool.Count > 0)
            {
                args.SetBuffer(_buffer, _freeIndexPool.Pop(), _bufferSize);
            }
            else
            {
                if (_numBytes - _bufferSize < _currentIndex)
                    return false;
                args.SetBuffer(_buffer, _currentIndex, _bufferSize);
                _currentIndex += _bufferSize;
            }
            return true;
        }

        public void FreeBuffer(UserToken token)
        {
            lock (token)
            {
                if (token.ReceiveArgs.Buffer == _buffer)
                    ReturnBuffer(token);
            }
        }

        public void ReturnBuffer(UserToken token)
        {
            _freeIndexPool.Push(token.ReceiveArgs.Offset);
        }

        public void ResetBuffer(UserToken token)
        {
            if (token.ReceiveArgs.Buffer != _buffer) return;
            Array.Clear(_buffer, token.ReceiveArgs.Offset, _bufferSize);
            token.ReceiveArgs.SetBuffer(token.ReceiveArgs.Offset, _bufferSize);
        }
    }
}
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using RetroClash.Logic;

namespace RetroClash.Network
{
    public class Token : IDisposable
    {
        private readonly SocketAsyncEventArgs _args;

        public Device Device;
        public MemoryStream Stream;

        public Token(SocketAsyncEventArgs args, Device device)
        {
            Device = device;
            Device.Token = this;

            _args = args;
            _args.UserToken = this;

            Stream = new MemoryStream();
        }

        public async Task SetData()
        {
            var data = new byte[_args.BytesTransferred];
            Array.Copy(_args.Buffer, 0, data, 0, _args.BytesTransferred);
            await Stream.WriteAsync(data, 0, data.Length);
        }

        public void Reset()
        {
            var buffer = Stream.GetBuffer();
            Array.Clear(buffer, 0, buffer.Length);
            Stream.Position = 0;
            Stream.SetLength(0);
        }

        public void Dispose()
        {
            Device.Dispose();
            Device = null;
            Stream = null;
        }
    }
}
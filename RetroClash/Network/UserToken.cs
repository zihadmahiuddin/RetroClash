using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using RetroClash.Logic;

namespace RetroClash.Network
{
    public class UserToken : IDisposable
    {
        public UserToken(SocketAsyncEventArgs args, Device device)
        {
            Device = device;
            Device.Token = this;

            ReceiveArgs = args;
            ReceiveArgs.UserToken = this;

            Stream = new MemoryStream();
        }

        public SocketAsyncEventArgs ReceiveArgs { get; set; }
        public Device Device { get; set; }
        public MemoryStream Stream { get; set; }

        public void Dispose()
        {
            Device.Dispose();
            Device = null;
            Stream = null;
        }

        public async Task SetData()
        {
            await Stream.WriteAsync(ReceiveArgs.Buffer, 0, ReceiveArgs.BytesTransferred);
        }

        public void Reset()
        {
            var buffer = Stream.GetBuffer();
            Array.Clear(buffer, 0, buffer.Length);
            Stream.Position = 0;
            Stream.SetLength(0);
        }
    }
}
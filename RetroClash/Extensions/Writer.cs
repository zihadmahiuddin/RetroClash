using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroClash.Extensions
{
    internal static class Writer
    {
        public static async Task WriteIntAsync(this Stream stream, int value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public static async Task WriteLongAsync(this Stream stream, long value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public static async Task WriteUShortAsync(this Stream stream, ushort value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public static async Task WriteBufferAsync(this Stream stream, byte[] buffer)
        {
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public static async Task WriteStringAsync(this Stream stream, string value)
        {
            if (value == null)
            {
                await stream.WriteIntAsync(-1);
            }
            else
            {
                var buffer = Encoding.UTF8.GetBytes(value);

                await stream.WriteIntAsync(buffer.Length);
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        public static async Task WriteHexAsync(this Stream stream, string value)
        {
            var tmp = value.Replace("-", string.Empty);
            await stream.WriteBufferAsync(Enumerable.Range(0, tmp.Length).Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(tmp.Substring(x, 2), 16)).ToArray());
        }
    }
}
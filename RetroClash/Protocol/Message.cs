using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol
{
    public class Message : IDisposable
    {
        public Message(Device device)
        {
            Device = device;
            Stream = new MemoryStream();
        }

        public Message(Device device, Reader reader)
        {
            Device = device;
            Reader = reader;
        }

        public MemoryStream Stream { get; set; }
        public Reader Reader { get; set; }
        public Device Device { get; set; }
        public ushort Id { get; set; }
        public ushort Length { get; set; }
        public ushort Version { get; set; }

        public virtual void Decrypt()
        {
            var buffer = Reader.ReadBytes(Length);

            Device.Rc4.Decrypt(ref buffer);

            Reader = new Reader(buffer);
            Length = (ushort) Reader.BaseStream.Length;
        }

        public virtual void Encrypt()
        {
            var buffer = Stream.ToArray();

            Device.Rc4.Encrypt(ref buffer);

            Stream = new MemoryStream(buffer);
            Length = (ushort) Stream.Length;
        }

        public virtual void Decode()
        {
        }

        public virtual async Task Encode()
        {
        }

        public virtual async Task Process()
        {
        }

        public async Task<byte[]> BuildPacket()
        {
            using (var stream = new MemoryStream())
            {
                Length = (ushort) Stream.Length;

                await stream.WriteUShortAsync(Id);

                stream.WriteByte(0);

                await stream.WriteUShortAsync(Length);
                await stream.WriteUShortAsync(Version);

                await stream.WriteBufferAsync(Stream.ToArray());

                return stream.ToArray();
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"PACKET ID: {Id}, ");
            builder.Append($"PACKET LENGTH: {Length}, ");
            builder.Append($"PACKET VERSION: {Version}, ");
            builder.Append($"IsServerToClientMessage: {IsServerToClientMessage()}, ");
            builder.Append($"IsClientToServerMessage: {IsClientToServerMessage()}");

            if (Stream != null)
                builder.AppendLine(
                    $", PACKET PAYLOAD: {BitConverter.ToString(Stream.ToArray()).Replace("-", string.Empty)}");

            return builder.ToString();
        }

        public bool IsServerToClientMessage()
        {
            return Id - 0x4E20 > 0x00;
        }

        public bool IsClientToServerMessage()
        {
            return Id - 0x2710 < 0x2710;
        }

        public void Dispose()
        {
            Stream = null;
            Reader = null;
            Device = null;          
        }
    }
}
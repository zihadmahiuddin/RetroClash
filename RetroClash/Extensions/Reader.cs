using System;
using System.IO;
using System.Text;

namespace RetroClash.Extensions
{
    public class Reader : BinaryReader
    {
        public Reader(byte[] buffer) : base(new MemoryStream(buffer))
        {
        }

        public byte[] ReadAllBytes => ReadBytes((int) BaseStream.Length - (int) BaseStream.Position);

        public override int Read(byte[] buffer, int offset, int count)
        {
            return BaseStream.Read(buffer, offset, count);
        }

        public override bool ReadBoolean()
        {
            var state = ReadByte();
            switch (state)
            {
                case 0:
                    return false;

                case 1:
                    return true;

                default:
                    throw new Exception("Error when reading a bool in packet.");
            }
        }

        public override byte ReadByte()
        {
            return (byte) BaseStream.ReadByte();
        }

        public byte[] ReadBytes()
        {
            var length = ReadInt32();
            return length == -1 ? null : ReadBytes(length);
        }

        public override short ReadInt16()
        {
            var buffer = ReadBytesWithEndian(2);
            return BitConverter.ToInt16(buffer, 0);
        }

        public int ReadInt24()
        {
            var temp = ReadBytesWithEndian(3, false);
            return (temp[0] << 16) | (temp[1] << 8) | temp[2];
        }

        public override int ReadInt32()
        {
            var buffer = ReadBytesWithEndian(4);
            return BitConverter.ToInt32(buffer, 0);
        }

        public override long ReadInt64()
        {
            var buffer = ReadBytesWithEndian(8);
            return BitConverter.ToInt64(buffer, 0);
        }

        public override string ReadString()
        {
            var length = ReadInt32();

            if (length <= -1 || length > BaseStream.Length - BaseStream.Position)
                return string.Empty;

            var buffer = ReadBytesWithEndian(length, false);
            return Encoding.UTF8.GetString(buffer);
        }

        public override ushort ReadUInt16()
        {
            var buffer = ReadBytesWithEndian(2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        public uint ReadUInt24()
        {
            return (uint) ReadInt24();
        }

        public override uint ReadUInt32()
        {
            var buffer = ReadBytesWithEndian(4);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public override ulong ReadUInt64()
        {
            var buffer = ReadBytesWithEndian(8);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        private byte[] ReadBytesWithEndian(int count, bool endian = true)
        {
            var buffer = new byte[count];
            BaseStream.Read(buffer, 0, count);

            if (BitConverter.IsLittleEndian && endian)
                Array.Reverse(buffer);

            return buffer;
        }
    }
}
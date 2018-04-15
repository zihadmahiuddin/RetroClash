using System;
using System.IO;
using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol
{
    public class Command : IDisposable
    {
        public Command(Device device)
        {
            Device = device;
            Stream = new MemoryStream();
        }

        public Command(Device device, Reader reader)
        {
            Device = device;
            Reader = reader;
        }

        public MemoryStream Stream { get; set; }
        public Device Device { get; set; }

        public int Id { get; set; }
        public Reader Reader { get; set; }

        public virtual void Decode()
        {
        }

        public virtual async Task Encode()
        {
        }

        public virtual async Task Process()
        {
        }

        public async Task<Command> Handle()
        {
            await Encode();
            return this;
        }

        public void Dispose()
        {
            Stream = null;
            Device = null;
            Reader = null;
        }
    }
}
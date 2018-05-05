using System;
using System.IO;
using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol
{
    public class DebugCommand : IDisposable
    {
        public DebugCommand(Device device)
        {
            Device = device;
            Stream = new MemoryStream();
        }

        public DebugCommand(Device device, Reader reader)
        {
            Device = device;
            Reader = reader;
        }

        public MemoryStream Stream { get; set; }
        public Device Device { get; set; }

        public int Id { get; set; }
        public Reader Reader { get; set; }

        public void Dispose()
        {
            Stream = null;
            Device = null;
            Reader = null;
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

        public async Task<DebugCommand> Handle()
        {
            await Encode();
            return this;
        }
    }
}
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Client
{
    public class KeepAlive : Message
    {
        public KeepAlive(Device device, Reader reader) : base(device, reader)
        {        
        }
    }
}

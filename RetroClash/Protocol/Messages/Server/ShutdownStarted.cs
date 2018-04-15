using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class ShutdownStarted : Message
    {
        public ShutdownStarted(Device device) : base(device)
        {
            Id = 20161;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0); // SecondsUntilShutdown
        }
    }
}

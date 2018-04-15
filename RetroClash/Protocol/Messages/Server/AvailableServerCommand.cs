using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AvailableServerCommand : Message
    {
        public AvailableServerCommand(Device device) : base(device)
        {
            Id = 24111;
        }

        public Command Command { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(Command.Id);
            await Stream.WriteBufferAsync(Command.Stream.ToArray());
        }
    }
}

using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class OutOfSync : Message
    {
        public OutOfSync(Device device) : base(device)
        {
            Id = 24104;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0);
            await Stream.WriteIntAsync(0);
            await Stream.WriteIntAsync(0);
        }
    }
}

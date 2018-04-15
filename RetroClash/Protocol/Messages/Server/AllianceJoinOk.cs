using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceJoinOk : Message
    {
        public AllianceJoinOk(Device device) : base(device)
        {
            Id = 24303;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(2); // Seconds?
        }
    }
}

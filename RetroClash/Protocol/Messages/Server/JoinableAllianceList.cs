using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class JoinableAllianceList : Message
    {
        public JoinableAllianceList(Device device) : base(device)
        {
            Id = 24304;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(1);

            await Stream.WriteLongAsync(1); // Id
            await Stream.WriteStringAsync("TEST RETRO"); // Name
            await Stream.WriteIntAsync(13000022); // Badge
            await Stream.WriteIntAsync(1); // Type
            await Stream.WriteIntAsync(1); // Member Count
            await Stream.WriteIntAsync(9999); // Score
            await Stream.WriteIntAsync(9999); // Required Score
        }
    }
}